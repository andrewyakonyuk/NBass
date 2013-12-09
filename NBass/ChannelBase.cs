using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Timers;
using NBass.Backstage;
using NBass.Declarations;

namespace NBass
{
    /// <summary>
    /// ChannelBase. The class is not used directly.
    /// </summary>
    public abstract class ChannelBase : IChannel, IDisposable
    {
        #region Field

        private readonly ObservableCollection<IEffect> _effects = new ObservableCollection<IEffect>();
        private readonly IntPtr _handle;
        private readonly Timer _progresstimer;
        private EventHandler _channelEnd;
        private GetSyncCallBack _getSync;
        private bool _isDisposed;
        private bool _isLocked;
        private BassContext _owner;
        private Channel3DPosition _position3d;
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
                BassException.ThrowIfTrue(() => !ChannelNativeMethods.GetInfo(Handle, ref data));
                return new ChannelInfo(data);
            }
        }

        public bool CanPlay { get; private set; }

        public virtual IID3Tag TagID3
        {
            get
            {
                CheckDisposed();

                var bytetag = new byte[128];
                IntPtr ptr = ChannelNativeMethods.GetTags(Handle, (int)Tag.ID3);
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
        protected double BytesToSeconds(long pos)
        {
            CheckDisposed();

            var result = ChannelNativeMethods.BytesToSeconds(_handle, pos);
            if (result < 0) throw new BassException();
            return result;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Retrieves upto "length" bytes of the channel//s current sample data. 16-bit data
        /// </summary>
        /// <param name="buffer">A buffer to place retrieved data</param>
        /// <param name="length">length in bytes</param>
        public int GetData(short[] buffer, int length)
        {
            CheckDisposed();
            int output = ChannelNativeMethods.GetData(Handle, buffer, length);
            if (output < 0) throw new BassException();
            return output;
        }

        /// <summary>
        /// Retrieves upto "length" bytes of the channel//s current sample data. 8-bit data
        /// </summary>
        /// <param name="buffer">A buffer to place retrieved data</param>
        /// <param name="length">length in bytes</param>
        public int GetData(byte[] buffer, int length)
        {
            CheckDisposed();
            int output = ChannelNativeMethods.GetData(Handle, buffer, length);
            if (output < 0) throw new BassException();
            return output;
        }

        /// <summary>
        /// Retrieves upto "length" bytes of the channel//s current sample data. This is
        /// useful if you wish to "visualize" the sound.
        /// </summary>
        /// <param name="buffer">A buffer to place retrieved data</param>
        /// <param name="flags">ChannelDataFlags</param>
        public int GetData(float[] buffer, int length)
        {
            CheckDisposed();
            int output = ChannelNativeMethods.GetData(Handle, buffer, length);
            if (output < 0) throw new BassException();
            return output;
        }

        public void LinkTo(IChannel channel)
        {
            CheckDisposed();
            BassException.ThrowIfTrue(() => !ChannelNativeMethods.SetLink(this.Handle, channel.Handle));
        }

        /// <summary>
        /// Pause a channel.
        /// </summary>
        public virtual void Pause()
        {
            CheckDisposed();
            if (!ChannelNativeMethods.Pause(_handle)) throw new BassException();
        }

        public virtual void Play()
        {
            CheckDisposed();
            if (!ChannelNativeMethods.Play(_handle, false)) throw new BassException();
            _progresstimer.Start();
        }

        /// <summary>
        /// Translate a time (seconds) position into bytes
        /// </summary>
        /// <param name="pos">The position to translate</param>
        /// <returns>The byte position</returns>
        protected long SecondsToBytes(double pos)
        {
            CheckDisposed();
            long result = ChannelNativeMethods.SecondsToBytes(_handle, pos);
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
            if (!ChannelNativeMethods.Stop(Handle)) throw new BassException();
        }

        public void UnlinkFrom(IChannel channel)
        {
            CheckDisposed();
            BassException.ThrowIfTrue(() => !ChannelNativeMethods.RemoveLink(this.Handle, channel.Handle));
        }

        protected virtual void CheckDisposed()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(BassResource.ChannelDisposedMessage);
        }

        protected virtual void Dispose(bool disposing)
        {
            _isDisposed |= !_isDisposed;
            _progresstimer.Dispose();
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

                return (ChannelState)ChannelNativeMethods.IsActive(_handle);
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

                if (!ChannelNativeMethods.Get3DAttributes(Handle, ref mode, ref min, ref max,
                    ref iangle, ref oangle, ref outvol))
                    throw new BassException();
                return new Channel3DAttributes
                {
                    Mode = (ThreeDMode)mode,
                    MinimumDistance = min,
                    MaximumDistance = max,
                    InsideAngle = iangle,
                    OutsideAngle = oangle,
                    OutsideDeltaVolume = outvol
                };
            }
            set
            {
                CheckDisposed();

                if (!ChannelNativeMethods.Set3DAttributes(Handle,
                    (int)value.Mode,
                    value.MinimumDistance,
                    value.MaximumDistance,
                    value.InsideAngle,
                    value.OutsideAngle,
                    value.OutsideDeltaVolume))
                    throw new BassException();
            }
        }

        public virtual int Device
        {
            get
            {
                CheckDisposed();

                return ChannelNativeMethods.GetDevice(Handle);
            }
            set
            {
                CheckDisposed();

                if (!ChannelNativeMethods.SetDevice(Handle, value))
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

        public virtual TimeSpan Length
        {
            get
            {
                CheckDisposed();
                long lenght = ChannelNativeMethods.GetLength(Handle, (int)BassMode.POS_BYTES);
                double seconds = BytesToSeconds(lenght);
                return TimeSpan.FromSeconds(seconds);
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

                if (!ChannelNativeMethods.Lock(Handle, value))
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
        public virtual TimeSpan Position
        {
            get
            {
                CheckDisposed();

                var position = ChannelNativeMethods.GetPosition(Handle, (int)BassMode.POS_BYTES);
                double seconds = BytesToSeconds(position);
                return TimeSpan.FromSeconds(seconds);
            }
            set
            {
                CheckDisposed();

                if (!ChannelNativeMethods.SetPosition(Handle, SecondsToBytes(value.TotalSeconds), (int)BassMode.POS_BYTES))
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

                Vector3D position, orientation, velocity;
                if (!ChannelNativeMethods.Get3DPosition(Handle, out position, out orientation, out velocity))
                    throw new BassException();
                if (_position3d == null)
                {
                    _position3d = new Channel3DPosition(position, orientation, velocity);
                }
                else
                {
                    _position3d.Position = position;
                    _position3d.Orientation = orientation;
                    _position3d.Velocity = velocity;
                }
                return _position3d;
            }
            set
            {
                CheckDisposed();

                Vector3D position = value.Position;
                Vector3D orientation = value.Orientation;
                Vector3D velocity = value.Velocity;
                if (!ChannelNativeMethods.Set3DPosition(Handle, ref position, ref orientation, ref velocity))
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
                if (!ChannelNativeMethods.GetAttribute(Handle, (int)Attribute.VOL, ref value))
                {
                    throw new BassException();
                }
                return value;
            }
            set
            {
                CheckDisposed();

                if (!ChannelNativeMethods.SetAttribute(Handle, (int)Attribute.VOL, value))
                {
                    throw new BassException();
                }
            }
        }

        #endregion Properties

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

                _channelEnd += value;
                _getSync += new GetSyncCallBack(OnGetSyncCallBack);
                HSYNC = ChannelNativeMethods.SetSync(Handle, 2, 0, _getSync, IntPtr.Zero);
            }
            remove
            {
                CheckDisposed();

                _channelEnd -= value;
                _getSync -= new GetSyncCallBack(OnGetSyncCallBack);
                ChannelNativeMethods.RemoveSync(Handle, HSYNC);
            }
        }

        public float Balance
        {
            get
            {
                CheckDisposed();
                var speed = 0f;
                BassException.ThrowIfTrue(() => !ChannelNativeMethods.GetAttribute(Handle, (int)Attribute.PAN, ref speed));
                return speed;
            }
            set
            {
                CheckDisposed();
                BassException.ThrowIfTrue(() => !ChannelNativeMethods.SetAttribute(Handle, (int)Attribute.PAN, value));
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

                int result = ChannelNativeMethods.GetLevel(Handle);
                return result < 0 ? 0 : Helper.LoWord(result);
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

                int result = ChannelNativeMethods.GetLevel(Handle);
                return result < 0 ? 0 : Helper.HiWord(result);
            }
        }

        protected virtual void OnEnd()
        {
            if (_channelEnd != null) _channelEnd(this, null);
        }

        void OnGetSyncCallBack(IntPtr handle, IntPtr channel, int data, int user)
        {
            OnEnd();
        }

        #endregion IChannel Members
    }
}