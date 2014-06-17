using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using NBass.Backstage;
using NBass.Declarations;

namespace NBass
{
    //TODO make as singleton
    //TODO implement Plugin`s functionality
    //TODO Sample
    //TODO Record
    //TODO configuration
    //TODO special effects
    //TODO clone stream (using BASS_StreamCreatePush)
    //TODO BASS_GET3DFactors and BASS_SET3DFactors
    //TODO add comments for native methods
    //TODO add comments for high-level member
    //TODO add music native methods
    //TODO add music info property
    //TODO add sample native methods
    //TODO update interfaces
    public class BassContext : IDisposable, IBassContext
    {
        private bool _isDisposed = false;
        private ObservableCollection<IPlugin> _plugins;

        public BassContext()
            : this(new IntPtr(-1), 44100, DeviceSetupFlags.Default, IntPtr.Zero)
        {
        }

        public BassContext(IntPtr device, int frequency, DeviceSetupFlags flags, IntPtr win)
        {
            if (frequency < 0)
                throw new ArgumentException("Frequency can't be less than zero", "frequency");

            InitDevice(device, frequency, flags, win, IntPtr.Zero);
            _plugins = new ObservableCollection<IPlugin>();
            _plugins.CollectionChanged += _plugins_CollectionChanged;
        }

        ~BassContext()
        {
            Dispose(false);
        }

        public EnvironmentInfo Environment
        {
            get
            {
                EAXEnvironment environment = EAXEnvironment.Generic;
                var volume = 0f;
                var decay = 0f;
                var damp = 0f;
                BassException.ThrowIfTrue(() => !BassContextNativeMethods.GetEAXParameters(ref environment, ref volume, ref decay, ref damp));
                return new EnvironmentInfo
                {
                    Type = environment,
                    Volume = volume,
                    Decay = decay,
                    Damp = damp
                };
            }
            set
            {
                BassException.ThrowIfTrue(() => !BassContextNativeMethods.SetEAXParameters(value.Type, value.Volume, value.Decay, value.Damp));
            }
        }

        public ICollection<IPlugin> Plugins
        {
            get
            {
                throw new NotImplementedException();
                return _plugins;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IStream Load(string filePath)
        {
            return Load(filePath, StreamFlags.Default);
        }

        public IStream Load(string filePath, StreamFlags flags)
        {
            return Load(filePath, 0, 0, flags);
        }

        public IStream Load(string filePath, long position, long length, StreamFlags flags)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException("filePath");

            if (!IsValidFilePath(filePath))
                throw new ArgumentException("Filepath is not valid", "filePath");

            if (position < 0)
                throw new ArgumentException("Position can't be less than zero", "position");

            if (length < 0)
                throw new ArgumentException("Length can't be less than zero", "length");

            flags |= StreamFlags.Unicode;
            var handle = new IntPtr(BassContextNativeMethods.LoadStream(false, filePath, (long)position, (long)length, (int)flags));
            BassException.ThrowIfTrue(() => handle == IntPtr.Zero);
            return new Stream(handle)
            {
                Owner = this
            };
        }

        public IStream Load(Uri uri)
        {
            return Load(uri, StreamFlags.Default, null);
        }

        public IStream Load(Uri uri, StreamFlags flags)
        {
            return Load(uri, flags, null);
        }

        public IStream Load(Uri uri, StreamFlags flags, StreamCallback callback)
        {
            flags |= StreamFlags.Unicode;
            var handle = new IntPtr(BassContextNativeMethods.LoadStreamFromUrl(uri.AbsoluteUri, 0, (int)flags, callback, IntPtr.Zero));
            BassException.ThrowIfTrue(() => handle == IntPtr.Zero);
            return new Stream(handle)
            {
                Owner = this
            };
        }

        public void Start()
        {
            BassException.ThrowIfTrue(() => !BassContextNativeMethods.Start());
        }

        public void Stop()
        {
            BassException.ThrowIfTrue(() => !BassContextNativeMethods.Stop());
        }

        protected virtual void Dispose(bool disposing)
        {
            _isDisposed |= !_isDisposed;
            if (disposing)
            {
                //free manage resource
            }

            //free unmanaged resource
            BassException.ThrowIfTrue(() => !BassContextNativeMethods.Free());
        }

        protected virtual void InitDevice(IntPtr device, int frequence, DeviceSetupFlags flags, IntPtr win, IntPtr clsid)
        {
            BassException.ThrowIfTrue(() => !BassContextNativeMethods.Init(device, frequence, (int)flags, win, clsid));
        }

        protected void InitDevice(IntPtr device, int frequence, DeviceSetupFlags flags)
        {
            InitDevice(device, frequence, flags, IntPtr.Zero, IntPtr.Zero);
        }

        private void _plugins_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems.OfType<Plugin>().Where(t => t != null))
                {
                    item.Init();
                }
            }
            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems.OfType<Plugin>().Where(t => t != null))
                {
                    item.Dispose();
                }
            }
        }

        #region Configuration

        /// <summary>
        /// Playback buffer length.
        /// </summary>
        public int Buffer
        {
            get { return BassContextNativeMethods.GetConfig((int)Configuration.Buffer); }
            set { BassContextNativeMethods.SetConfig((int)Configuration.Buffer, value); }
        }

        /// <summary>
        /// Global music volume.
        /// </summary>
        public int GlobalMusicVolume
        {
            get { return BassContextNativeMethods.GetConfig((int)Configuration.GlobalMusicVolume); }
            set { BassContextNativeMethods.SetConfig((int)Configuration.GlobalMusicVolume, value); }
        }

        /// <summary>
        /// Global sample volume.
        /// </summary>
        public int GlobalSampleVolume
        {
            get { return BassContextNativeMethods.GetConfig((int)Configuration.GlobalSampleVolume); }
            set { BassContextNativeMethods.SetConfig((int)Configuration.GlobalSampleVolume, value); }
        }

        /// <summary>
        /// Global stream volume.
        /// </summary>
        public int GlobalStreamVolume
        {
            get { return BassContextNativeMethods.GetConfig((int)Configuration.GlobalStreamVolume); }
            set { BassContextNativeMethods.SetConfig((int)Configuration.GlobalStreamVolume, value); }
        }

        /// <summary>
        /// The update period of HSTREAM and HMUSIC channel playback buffers.
        /// </summary>
        public int UpdatePeriod
        {
            get { return BassContextNativeMethods.GetConfig((int)Configuration.UpdatePeriod); }
            set { BassContextNativeMethods.SetConfig((int)Configuration.UpdatePeriod, value); }
        }

        #endregion Configuration

        bool IsValidFilePath(string filePath)
        {
            return !string.IsNullOrEmpty(filePath) && File.Exists(filePath);
        }
    }
}