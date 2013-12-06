using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

    public class BassContext : IDisposable, IBassContext
    {
        private bool _isDisposed = false;
        private ObservableCollection<IPlugin> _plugins;
        Dictionary<EAXEnvironment, EnvironmentInfo> _environmentsPreset;

        public BassContext()
        {
            var defaultDevice = new IntPtr(-1);
            var rate = 44100;
            var win = IntPtr.Zero;
            Init(defaultDevice, rate, DeviceSetupFlags.Default, win, IntPtr.Zero);
            InitEnvironment();
        }

        private void InitEnvironment()
        {
            _environmentsPreset = new Dictionary<EAXEnvironment, EnvironmentInfo>
                {
                    {EAXEnvironment.Generic, new EnvironmentInfo{
                        Volume = 0.5f,
                        Decay = 1.493f,
                        Damp = 0.5f
                    }},
                    {EAXEnvironment.PaddedCell, new EnvironmentInfo{
                        Volume = 0.25f,
                        Decay = 0.1f,
                        Damp = 0f
                    }},
                    {EAXEnvironment.Room, new EnvironmentInfo{
                        Volume = 0.417f,
                        Decay = 0.4f,
                        Damp = 0.666f
                    }},
                    {EAXEnvironment.Bathroom, new EnvironmentInfo{
                        Volume = 0.653f,
                        Decay = 1.499f,
                        Damp = 0.166f
                    }},
                    {EAXEnvironment.LivingRoom, new EnvironmentInfo{
                        Volume = 0.208f,
                        Decay = 0.478f,
                        Damp = 0f
                    }},
                    {EAXEnvironment.StoneRoom, new EnvironmentInfo{
                        Volume = 0.5f,
                        Decay = 2.309f,
                        Damp = 0.888f
                    }},
                    {EAXEnvironment.Auditorium, new EnvironmentInfo{
                        Volume = 0.403f,
                        Decay = 4.279f,
                        Damp = 0.5f
                    }},
                    {EAXEnvironment.ConcertHall, new EnvironmentInfo{
                        Volume = 0.5f,
                        Decay = 3.961f,
                        Damp = 0.5f
                    }},
                    {EAXEnvironment.Cave, new EnvironmentInfo{
                        Volume = 0.5f,
                        Decay = 2.886f,
                        Damp = 1.304f
                    }},
                    {EAXEnvironment.Arena, new EnvironmentInfo{
                        Volume = 0.361f,
                        Decay = 7.284f,
                        Damp = 0.332f
                    }},
                    {EAXEnvironment.Hangar, new EnvironmentInfo{
                        Volume = 0.5f,
                        Decay = 10f,
                        Damp = 0.3f
                    }},
                    {EAXEnvironment.CarpetedHallway, new EnvironmentInfo{
                        Volume = 0.153f,
                        Decay = 0.259f,
                        Damp = 2f
                    }},
                    {EAXEnvironment.Hallway, new EnvironmentInfo{
                        Volume = 0.361f,
                        Decay = 1.493f,
                        Damp = 0f
                    }},
                    {EAXEnvironment.StoneCorridor, new EnvironmentInfo{
                        Volume = 0.444f,
                        Decay = 2.697f,
                        Damp = 0.638f
                    }},
                    {EAXEnvironment.Alley, new EnvironmentInfo{
                        Volume = 0.25f,
                        Decay = 1.752f,
                        Damp = 0.776f
                    }},
                    {EAXEnvironment.Forest, new EnvironmentInfo{
                        Volume = 0.111f,
                        Decay = 3.145f,
                        Damp = 0.472f
                    }},
                    {EAXEnvironment.City, new EnvironmentInfo{
                        Volume = 0.11f,
                        Decay = 2.767f,
                        Damp = 0.224f
                    }},
                    {EAXEnvironment.Mountains, new EnvironmentInfo{
                        Volume = 0.194f,
                        Decay = 7.841f,
                        Damp = 0.472f
                    }},
                    {EAXEnvironment.Quarry, new EnvironmentInfo{
                        Volume = 1f,
                        Decay = 1.499f,
                        Damp = 0.5f
                    }},
                    {EAXEnvironment.Plain, new EnvironmentInfo{
                        Volume = 0.097f,
                        Decay = 2.767f,
                        Damp = 0.224f
                    }},
                    {EAXEnvironment.ParkingLot, new EnvironmentInfo{
                        Volume = 0.208f,
                        Decay = 1.652f,
                        Damp = 1.5f
                    }},
                    {EAXEnvironment.SewerPipe, new EnvironmentInfo{
                        Volume = 0.652f,
                        Decay = 2.886f,
                        Damp = 0.25f
                    }},
                    {EAXEnvironment.Underwater, new EnvironmentInfo{
                        Volume = 1f,
                        Decay = 1.449f,
                        Damp = 0f
                    }},
                    {EAXEnvironment.Drugged, new EnvironmentInfo{
                        Volume = 0.875f,
                        Decay = 8.392f,
                        Damp = 1.388f
                    }},
                    {EAXEnvironment.Dizzy, new EnvironmentInfo{
                        Volume = 0.139f,
                        Decay = 17.234f,
                        Damp = 0.666f
                    }},
                    {EAXEnvironment.Psychotic, new EnvironmentInfo{
                        Volume = 0.486f,
                        Decay = 7.563f,
                        Damp = 0.806f
                    }}
                };
        }

        public BassContext(IntPtr device, int frequency, DeviceSetupFlags flags, IntPtr win)
        {
            Init(device, frequency, flags, win, IntPtr.Zero);
        }

        ~BassContext()
        {
            Dispose(false);
        }

        public bool IsDisposed { get { return _isDisposed; } }

        public EAXEnvironment EnvironmentPreset
        {
            get
            {
                return GetEnvironment().Type;
            }
            set
            {
                if (_environmentsPreset.ContainsKey(value))
                {
                    var preset = _environmentsPreset[value];
                    preset.Type = value;
                    SetEnvironment(preset);
                }
                else new ArgumentOutOfRangeException("value", value, "Non present environment pre-set");
            }
        }

        public void SetEnvironment(EnvironmentInfo item)
        {
            BassException.ThrowIfTrue(() => !BassContextNativeMethods.SetEAXParameters(item.Type, item.Volume, item.Decay, item.Damp));
        }

        public EnvironmentInfo GetEnvironment()
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

        public ICollection<IPlugin> Plugins
        {
            get {
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
            flags |= StreamFlags.Unicode;
            var handle = new IntPtr(BassContextNativeMethods.LoadStream(false, filePath, position, length, (int)flags));
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

        protected void Init(IntPtr device, int frequence, DeviceSetupFlags flags, IntPtr win, IntPtr clsid)
        {
            BassException.ThrowIfTrue(() => !BassContextNativeMethods.Init(device, frequence, (int)flags, win, clsid));

            _plugins = new ObservableCollection<IPlugin>();
            _plugins.CollectionChanged += _plugins_CollectionChanged;
        }

        protected void Init(IntPtr device, int frequence, DeviceSetupFlags flags)
        {
            Init(device, frequence, flags, IntPtr.Zero, IntPtr.Zero);
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
        /// <para>length (int): The buffer length in milliseconds. The minimum length is 1ms above the update period (see <see cref="F:Un4seen.Bass.BASSConfig.BASS_CONFIG_UPDATEPERIOD" />), the maximum is 5000 milliseconds. If the length specified is outside this range, it is automatically capped.</para>
        /// <para>The default buffer length is 500 milliseconds. Increasing the length, decreases the chance of the sound possibly breaking-up on slower computers, but also increases the latency for DSP/FX.</para>
        /// <para>Small buffer lengths are only required if the sound is going to be changing in real-time, for example, in a soft-synth. If you need to use a small buffer, then the minbuf member of BASS_INFO should be used to get the recommended minimum buffer length supported by the device and it's drivers. Even at this default length, it's still possible that the sound could break up on some systems, it's also possible that smaller buffers may be fine. So when using small buffers, you should have an option in your software for the user to finetune the length used, for optimal performance.</para>
        /// <para>Using this config option only affects the HMUSIC/HSTREAM channels that you create afterwards, not the ones that have already been created. So you can have channels with differing buffer lengths by using this config option each time before creating them.</para>
        /// <para>If automatic updating is disabled, make sure you call <see cref="M:Un4seen.Bass.Bass.BASS_Update(System.Int32)" /> frequently enough to keep the buffers updated.</para>
        /// </summary>
        public int Buffer
        {
            get { return BassContextNativeMethods.GetConfig((int)Configuration.Buffer); }
            set { BassContextNativeMethods.SetConfig((int)Configuration.Buffer, value); }
        }

        /// <summary>
        /// Global music volume.
        /// <para>volume (int): MOD music global volume level... 0 (silent) - 10000 (full).</para>
        /// <para>This config option allows you to have control over the volume levels of all the MOD musics, which is useful for setup options (eg. separate music and fx volume controls).</para>
        /// <para>A channel's final volume = channel volume * global volume / max volume. So, for example, if a stream channel's volume is 0.5 and the global stream volume is 8000, then effectively the stream's volume level is 0.4 (0.5 * 8000 / 10000 = 0.4).</para>
        /// </summary>
        public int GlobalMusicVolume
        {
            get { return BassContextNativeMethods.GetConfig((int)Configuration.GlobalMusicVolume); }
            set { BassContextNativeMethods.SetConfig((int)Configuration.GlobalMusicVolume, value); }
        }

        /// <summary>
        /// Global sample volume.
        /// <para>volume (int): Sample global volume level... 0 (silent) - 10000 (full).</para>
        /// <para>This config option allows you to have control over the volume levels of all the samples, which is useful for setup options (eg. separate music and fx volume controls).</para>
        /// <para>A channel's final volume = channel volume * global volume / max volume. So, for example, if a stream channel's volume is 0.5 and the global stream volume is 8000, then effectively the stream's volume level is 0.4 (0.5 * 8000 / 10000 = 0.4).</para>
        /// </summary>
        public int GlobalSampleVolume
        {
            get { return BassContextNativeMethods.GetConfig((int)Configuration.GlobalSampleVolume); }
            set { BassContextNativeMethods.SetConfig((int)Configuration.GlobalSampleVolume, value); }
        }

        /// <summary>
        /// Global stream volume.
        /// <para>volume (int): Stream global volume level... 0 (silent) - 10000 (full).</para>
        /// <para>This config option allows you to have control over the volume levels of all streams, which is useful for setup options (eg. separate music and fx volume controls).</para>
        /// <para>A channel's final volume = channel volume * global volume / max volume. So, for example, if a stream channel's volume is 0.5 and the global stream volume is 8000, then effectively the stream's volume level is 0.4 (0.5 * 8000 / 10000 = 0.4).</para>
        /// </summary>
        public int GlobalStreamVolume
        {
            get { return BassContextNativeMethods.GetConfig((int)Configuration.GlobalStreamVolume); }
            set { BassContextNativeMethods.SetConfig((int)Configuration.GlobalStreamVolume, value); }
        }

        /// <summary>
        /// The update period of HSTREAM and HMUSIC channel playback buffers.
        /// <para>period (int): The update period in milliseconds... 0 = disable automatic updating. The minimum period is 5ms, the maximum is 100ms. If the period specified is outside this range, it is automatically capped.</para>
        /// <para>The update period is the amount of time between updates of the playback buffers of HSTREAM/HMUSIC channels. Shorter update periods allow smaller buffers to be set with the <see cref="F:Un4seen.Bass.BASSConfig.BASS_CONFIG_BUFFER" /> option, but as the rate of updates increases, so the overhead of setting up the updates becomes a greater part of the CPU usage.
        /// The update period only affects HSTREAM and HMUSIC channels, it does not affect samples. Nor does it have any effect on decoding channels, as they are not played.</para>
        /// <para>BASS creates one or more threads (determined by <see cref="F:Un4seen.Bass.BASSConfig.BASS_CONFIG_UPDATETHREADS" />) specifically to perform the updating, except when automatic updating is disabled (period=0) - then you must regularly call <see cref="M:Un4seen.Bass.Bass.BASS_Update(System.Int32)" /> or <see cref="M:Un4seen.Bass.Bass.BASS_ChannelUpdate(System.Int32,System.Int32)" />instead. This allows you to synchronize BASS's CPU usage with your program's. For example, in a game loop you could call <see cref="M:Un4seen.Bass.Bass.BASS_Update(System.Int32)" /> once per frame, which keeps all the processing in sync so that the frame rate is as smooth as possible. BASS_Update should be called at least around 8 times per second, even more often if the <see cref="F:Un4seen.Bass.BASSConfig.BASS_CONFIG_BUFFER" /> option is used to set smaller buffers.</para>
        /// <para>The update period can be altered at any time, including during playback. The default period is 100ms.</para>
        /// </summary>
        public int UpdatePeriod
        {
            get { return BassContextNativeMethods.GetConfig((int)Configuration.UpdatePeriod); }
            set { BassContextNativeMethods.SetConfig((int)Configuration.UpdatePeriod, value); }
        }

        #endregion Configuration

        
    }
}