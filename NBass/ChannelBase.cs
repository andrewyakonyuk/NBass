using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Timers;
using NBass.Backstage;
using NBass.Declaration;

namespace NBass
{
    //TODO implement channel info

    /// <summary>
    /// ChannelBase. The class is not used directly.
    /// </summary>
    public abstract class ChannelBase : IChannel, IDisposable, ICloneable
    {
        #region Field

        private readonly ObservableCollection<IEffect> _effects = new ObservableCollection<IEffect>();
        private readonly IntPtr _handle;
        private readonly Timer _progresstimer;
        private GetSyncCallBack _getSync;
        private bool _isDisposed;
        private bool _isLocked;
        private BassContext _owner;
        private EventHandler _streamendstore;
        private IntPtr HSYNC;

        #endregion Field

        #region Ctor

        protected ChannelBase(IntPtr handle)
        {
            _handle = handle;
            _progresstimer = new Timer(20);
            _progresstimer.Elapsed += ProgressTimerElapsed;
            _effects.CollectionChanged += _effects_CollectionChanged;
        }

        #endregion Ctor

        #region Events

        public event ProgessHandler Progress;

        protected virtual void OnProgress()
        {
            if (Progress != null) Progress(this);
        }

        #endregion Events

        ~ChannelBase()
        {
            Dispose(false);
        }

        public ChannelInfo Info
        {
            get
            {
                var data = new Data();
                BassException.ThrowIfTrue(() => !_Channel.GetInfo(Handle, ref data));
                return new ChannelInfo(data);
            }
        }

        public virtual IID3Tag TagID3
        {
            get
            {
                CheckDisposed();

                var bytetag = new byte[128];
                IntPtr ptr = _Channel.GetTags(Handle, (int)Tag.ID3);
                if (ptr != IntPtr.Zero)
                    Marshal.Copy(ptr, bytetag, 0, bytetag.Length);
                return new ID3v1Tag(bytetag);
            }
        }

        /// <summary>
        /// Translate a byte position into time (seconds)
        /// </summary>
        /// <param name="pos">The position to translate</param>
        /// <returns>The millisecond position</returns>
        public double BytesToSeconds(long pos)
        {
            CheckDisposed();

            var result = _Channel.BytesToSeconds(_handle, pos);
            if (result < 0) throw new BassException();
            return result;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public string[] GetTag(Tag tag)
        {
            return GetTagGen(tag);
        }

        public void LinkWith(IChannel channel)
        {
            BassException.ThrowIfTrue(() => !_Channel.SetLink(this.Handle, channel.Handle));
        }

        /// <summary>
        /// Pause a channel.
        /// </summary>
        public virtual void Pause()
        {
            CheckDisposed();

            if (!_Channel.Pause(_handle)) throw new BassException();
        }

        public virtual void Play()
        {
            CheckDisposed();

            if (!_Channel.Play(_handle, false)) throw new BassException();
            StartTimer();
        }

        /// <summary>
        /// Translate a time (seconds) position into bytes
        /// </summary>
        /// <param name="pos">The position to translate</param>
        /// <returns>The byte position</returns>
        public long SecondsToBytes(float pos)
        {
            CheckDisposed();

            long result = _Channel.SecondsToBytes(_handle, pos);
            if (result < 0) throw new BassException();
            return result;
        }

        /// <summary>
        /// Stop a channel.
        /// </summary>
        public virtual void Stop()
        {
            CheckDisposed();

            _progresstimer.Stop();
            if (!_Channel.Stop(Handle)) throw new BassException();
        }

        public void UnlinkFrom(IChannel channel)
        {
            BassException.ThrowIfTrue(() => !_Channel.RemoveLink(this.Handle, channel.Handle));
        }

        protected virtual void CheckDisposed()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(BassResource.ChannelDisposedMessage);
        }

        protected virtual void Dispose(bool disposing)
        {
            _isDisposed |= !_isDisposed;
        }

        #region Properties

        /// <summary>
        /// Gets the objects current State
        /// </summary>
        public virtual ChannelState ActivityState
        {
            get
            {
                CheckDisposed();

                return (ChannelState)_Channel.IsActive(_handle);
            }
        }

        /// <summary>
        /// Gets/Sets a channel's 3D attributes.
        /// </summary>
        public virtual Channel3DAttributes Attributes3D
        {
            get
            {
                CheckDisposed();

                int mode = 0;
                float min = 0;
                float max = 0;
                int iangle = 0;
                int oangle = 0;
                int outvol = 0;

                if (!_Channel.Get3DAttributes(Handle, ref mode, ref min, ref max,
                    ref iangle, ref oangle, ref outvol))
                    throw new BassException();
                return new Channel3DAttributes(mode, min, max, iangle, oangle, outvol);
            }
            set
            {
                CheckDisposed();

                if (!_Channel.Set3DAttributes(Handle, value.mode, value.min, value.max,
                    value.iangle, value.oangle, value.outvol))
                    throw new BassException();
            }
        }

        public virtual int Device
        {
            get
            {
                CheckDisposed();

                return _Channel.GetDevice(Handle);
            }
            set
            {
                CheckDisposed();

                if (!_Channel.SetDevice(Handle, value))
                    throw new BassException();
            }
        }

        public IntPtr Handle
        {
            get
            {
                CheckDisposed();
                return _handle;
            }
        }

        public virtual long Length
        {
            get
            {
                CheckDisposed();

                return _Channel.GetLength(Handle, (int)BassMode.POS_BYTES);
            }
        }

        public virtual bool Lock
        {
            get
            {
                CheckDisposed();

                return _isLocked;
            }
            set
            {
                CheckDisposed();

                if (!_Channel.Lock(Handle, value))
                    throw new BassException();
                _isLocked = value;
            }
        }

        public BassContext Owner
        {
            get
            {
                CheckDisposed();

                return _owner;
            }
            internal set
            {
                CheckDisposed();

                _owner = value;
            }
        }

        /// <summary>
        /// Gets/Sets the current playback position of a channel.
        /// </summary>
        public virtual long Position
        {
            get
            {
                CheckDisposed();

                return _Channel.GetPosition(Handle, (int)BassMode.POS_BYTES);
            }
            set
            {
                CheckDisposed();

                if (!_Channel.SetPosition(Handle, value, (int)BassMode.POS_BYTES))
                    throw new BassException();
            }
        }

        /// <summary>
        /// Gets/Sets a channel's 3D Position
        /// </summary>
        public virtual Channel3DPosition Position3D
        {
            get
            {
                CheckDisposed();

                Vector3D pos, orient, vel;
                if (!_Channel.Get3DPosition(Handle, out pos, out orient, out vel))
                    throw new BassException();
                return new Channel3DPosition(pos, orient, vel);
            }
            set
            {
                CheckDisposed();

                Vector3D pos = value.pos;
                Vector3D orient = value.orient;
                Vector3D vel = value.vel;
                if (!_Channel.Set3DPosition(Handle, ref pos, ref orient, ref vel))
                    throw new BassException();
            }
        }

        public virtual double ProgressInterval
        {
            get
            {
                CheckDisposed();

                return _progresstimer.Interval;
            }
            set
            {
                CheckDisposed();

                _progresstimer.Interval = value;
            }
        }

        public virtual float Volume
        {
            get
            {
                CheckDisposed();

                float value = 0;
                if (!_Channel.GetAttribute(Handle, (int)Attribute.VOL, ref value))
                {
                    throw new BassException();
                }
                return value;
            }
            set
            {
                CheckDisposed();

                if (!_Channel.SetAttribute(Handle, (int)Attribute.VOL, value))
                {
                    throw new BassException();
                }
            }
        }

        #endregion Properties

        protected string[] GetTagGen(Tag tag)
        {
            var tags = new ArrayList();
            IntPtr ptr = _Channel.GetTags(Handle, (int)tag);
            if (ptr == IntPtr.Zero)
                throw new BassException();
            do
            {
                string tagName = Marshal.PtrToStringAnsi(ptr);
                if (tagName == "")
                    break;
                tags.Add(tagName);
                ptr = new IntPtr(ptr.ToInt32() + (tagName.Length + 1));
            }
            while (true);

            var output = new string[tags.Count];
            for (int i = 0; i < output.Length; i++)
                output[i] = (string)tags[i];
            return output;
        }

        protected virtual void StartTimer()
        {
            CheckDisposed();

            _progresstimer.Start();
        }

        private void _effects_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //if (e.NewItems != null)
            //{
            //    foreach (var item in e.NewItems.OfType<IEffect>().Where(t => t != null))
            //    {
            //        if (_isDisposed)
            //            throw new ObjectDisposedException(ToString());

            //        IntPtr fx = _Channel.SetFX(Handle, item.Type, 1);
            //        if (fx == IntPtr.Zero) throw new BassException();
            //        return new FX(fx, chanfx);
            //    }
            //}
            //if (e.OldItems != null)
            //{
            //    foreach (var item in e.OldItems.OfType<IEffect>().Where(t => t != null))
            //    {
            //        item.Dispose();
            //    }
            //}
        }

        private void ProgressTimerElapsed(object sender, ElapsedEventArgs e)
        {
            OnProgress();
        }

        #region IChannel Members

        public virtual event EventHandler End
        {
            add
            {
                CheckDisposed();

                _streamendstore += value;
                _getSync += new GetSyncCallBack(OnGetSyncCallBack);
                HSYNC = _Channel.SetSync(Handle, 2, 0, _getSync, IntPtr.Zero);
            }
            remove
            {
                CheckDisposed();

                _streamendstore -= value;
                _getSync -= new GetSyncCallBack(OnGetSyncCallBack);
                _Channel.RemoveSync(Handle, HSYNC);
            }
        }

        public ICollection<IEffect> Effects
        {
            get
            {
                throw new NotImplementedException();
                return _effects;
            }
        }

        public bool IsDisposed
        {
            get { return _isDisposed; }
        }

        /// <summary>
        /// Calculate a channel's current left output level.
        /// </summary>
        public int LeftLevel
        {
            get
            {
                CheckDisposed();

                int result = _Channel.GetLevel(Handle);
                return result < 0 ? 0 : Helper.LoWord(result);/*throw new BASSException();*/
            }
        }

        /// <summary>
        /// Calculate a channel's current right output level.
        /// </summary>
        public int RightLevel
        {
            get
            {
                CheckDisposed();

                int result = _Channel.GetLevel(Handle);
                return result < 0 ? 0 : Helper.HiWord(result);/*throw new BASSException();*/
            }
        }

        public float Balance
        {
            get
            {
                CheckDisposed();
                var speed = 0f;
                BassException.ThrowIfTrue(() => !_Channel.GetAttribute(Handle, (int)Attribute.PAN, ref speed));
                return speed;
            }
            set
            {
                CheckDisposed();
                BassException.ThrowIfTrue(() => !_Channel.SetAttribute(Handle, (int)Attribute.PAN, value));
            }
        }

        protected virtual void OnEnd()
        {
            if (_streamendstore != null) _streamendstore(this, null);
        }

        private void OnGetSyncCallBack(IntPtr handle, IntPtr channel, int data, int user)
        {
            OnEnd();
        }

        #endregion IChannel Members

        #region ICloneable Members

        public object Clone()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}