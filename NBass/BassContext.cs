using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using NBass.Backstage;
using NBass.Declaration;

namespace NBass
{
    //TODO make as singleton
    //TODO implement Plugin`s functionality
    //TODO Sample
    //TODO Record
    //TODO configuration
    //TODO special effects
    //TODO clone stream (using BASS_StreamCreatePush)

    public class BassContext : IDisposable, IBassContext
    {
        private bool _isDisposed = false;
        private ObservableCollection<Plugin> _plugins;

        public BassContext(IntPtr device, int frequence, DeviceSetupFlags flags, IntPtr win)
        {
            Init(device, frequence, flags, win, IntPtr.Zero);
        }

        ~BassContext()
        {
            Dispose(false);
        }

        public EAXEnvironment EAXEnvironment
        {
            get
            {
                EAXEnvironment environment = EAXEnvironment.Generic;
                var volume = 0f;
                var decay = 0f;
                var damp = 0f;
                BassException.Thrown(() => !_BassContext.GetEAXParameters(ref environment, ref volume, ref decay, ref damp));
                return environment;
            }
            set
            {
                bool result = false;
                switch (value)
                {
                    case EAXEnvironment.Generic:
                        result = _BassContext.SetEAXParameters(EAXEnvironment.Generic, 0.5f, 1.493f, 0.5f);
                        break;

                    case EAXEnvironment.PaddedCell:
                        result = _BassContext.SetEAXParameters(EAXEnvironment.PaddedCell, 0.25f, 0.1f, 0f);
                        break;

                    case EAXEnvironment.Room:
                        result = _BassContext.SetEAXParameters(EAXEnvironment.Room, 0.417f, 0.4f, 0.666f);
                        break;

                    case EAXEnvironment.Bathroom:
                        result = _BassContext.SetEAXParameters(EAXEnvironment.Bathroom, 0.653f, 1.499f, 0.166f);
                        break;

                    case EAXEnvironment.LivingRoom:
                        result = _BassContext.SetEAXParameters(EAXEnvironment.LivingRoom, 0.208f, 0.478f, 0f);
                        break;

                    case EAXEnvironment.StoneRoom:
                        result = _BassContext.SetEAXParameters(EAXEnvironment.StoneRoom, 0.5f, 2.309f, 0.888f);
                        break;

                    case EAXEnvironment.Auditorium:
                        result = _BassContext.SetEAXParameters(EAXEnvironment.Auditorium, 0.403f, 4.279f, 0.5f);
                        break;

                    case EAXEnvironment.ConcertHall:
                        result = _BassContext.SetEAXParameters(EAXEnvironment.ConcertHall, 0.5f, 3.961f, 0.5f);
                        break;

                    case EAXEnvironment.Cave:
                        result = _BassContext.SetEAXParameters(EAXEnvironment.Cave, 0.5f, 2.886f, 1.304f);
                        break;

                    case EAXEnvironment.Arena:
                        result = _BassContext.SetEAXParameters(EAXEnvironment.Arena, 0.361f, 7.284f, 0.332f);
                        break;

                    case EAXEnvironment.Hangar:
                        result = _BassContext.SetEAXParameters(EAXEnvironment.Hangar, 0.5f, 10f, 0.3f);
                        break;

                    case EAXEnvironment.CarpetedHallway:
                        result = _BassContext.SetEAXParameters(EAXEnvironment.CarpetedHallway, 0.153f, 0.259f, 2f);
                        break;

                    case EAXEnvironment.Hallway:
                        result = _BassContext.SetEAXParameters(EAXEnvironment.Hallway, 0.361f, 1.493f, 0f);
                        break;

                    case EAXEnvironment.StoneCorridor:
                        result = _BassContext.SetEAXParameters(EAXEnvironment.StoneCorridor, 0.444f, 2.697f, 0.638f);
                        break;

                    case EAXEnvironment.Alley:
                        result = _BassContext.SetEAXParameters(EAXEnvironment.Alley, 0.25f, 1.752f, 0.776f);
                        break;

                    case EAXEnvironment.Forest:
                        result = _BassContext.SetEAXParameters(EAXEnvironment.Forest, 0.111f, 3.145f, 0.472f);
                        break;

                    case EAXEnvironment.City:
                        result = _BassContext.SetEAXParameters(EAXEnvironment.City, 0.111f, 2.767f, 0.224f);
                        break;

                    case EAXEnvironment.Mountains:
                        result = _BassContext.SetEAXParameters(EAXEnvironment.Mountains, 0.194f, 7.841f, 0.472f);
                        break;

                    case EAXEnvironment.Quarry:
                        result = _BassContext.SetEAXParameters(EAXEnvironment.Quarry, 1f, 1.499f, 0.5f);
                        break;

                    case EAXEnvironment.Plain:
                        result = _BassContext.SetEAXParameters(EAXEnvironment.Plain, 0.097f, 2.767f, 0.224f);
                        break;

                    case EAXEnvironment.ParkingLot:
                        result = _BassContext.SetEAXParameters(EAXEnvironment.ParkingLot, 0.208f, 1.652f, 1.5f);
                        break;

                    case EAXEnvironment.SewerPipe:
                        result = _BassContext.SetEAXParameters(EAXEnvironment.SewerPipe, 0.652f, 2.886f, 0.25f);
                        break;

                    case EAXEnvironment.Underwater:
                        result = _BassContext.SetEAXParameters(EAXEnvironment.Underwater, 1f, 1.499f, 0f);
                        break;

                    case EAXEnvironment.Drugged:
                        result = _BassContext.SetEAXParameters(EAXEnvironment.Drugged, 0.875f, 8.392f, 1.388f);
                        break;

                    case EAXEnvironment.Dizzy:
                        result = _BassContext.SetEAXParameters(EAXEnvironment.Dizzy, 0.139f, 17.234f, 0.666f);
                        break;

                    case EAXEnvironment.Psychotic:
                        result = _BassContext.SetEAXParameters(EAXEnvironment.Psychotic, 0.486f, 7.563f, 0.806f);
                        break;
                }
                BassException.Thrown(() => result);
            }
        }

        public ICollection<Plugin> Plugins
        {
            get { return _plugins; }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IStream Load(string filePath, StreamFlags flags)
        {
            flags |= StreamFlags.Unicode;
            var handle = new IntPtr(_BassContext.LoadStream(false, filePath, 0, 0, (int)flags));
            BassException.Thrown(() => handle == IntPtr.Zero);
            return new Stream(handle)
            {
                Owner = this
            };
        }

        public IStream Load(string filePath, long position, long length, StreamFlags flags)
        {
            flags |= StreamFlags.Unicode;
            var handle = new IntPtr(_BassContext.LoadStream(false, filePath, position, length, (int)flags));
            BassException.Thrown(() => handle == IntPtr.Zero);
            return new Stream(handle)
            {
                Owner = this
            };
        }

        public IStream Load(Uri uri, StreamFlags flags)
        {
            flags |= StreamFlags.Unicode;
            var handle = new IntPtr(_BassContext.LoadStreamFromUrl(uri.AbsoluteUri, 0, (int)flags, null, IntPtr.Zero));
            BassException.Thrown(() => handle == IntPtr.Zero);
            return new Stream(handle)
            {
                Owner = this
            };
        }

        public IStream Load(Uri uri, StreamFlags flags, StreamCallback callback)
        {
            flags |= StreamFlags.Unicode;
            var handle = new IntPtr(_BassContext.LoadStreamFromUrl(uri.AbsoluteUri, 0, (int)flags, callback, IntPtr.Zero));
            BassException.Thrown(() => handle == IntPtr.Zero);
            return new Stream(handle)
            {
                Owner = this
            };
        }

        public void Start()
        {
            BassException.Thrown(() => !_BassContext.Start());
        }

        public void Stop()
        {
            BassException.Thrown(() => !_BassContext.Stop());
        }
        protected virtual void Dispose(bool disposing)
        {
            _isDisposed |= !_isDisposed;
            if (disposing)
            {
                //free manage resource
            }

            //free unmanage resource
            BassException.Thrown(() => !_BassContext.Free());
        }

        protected void Init(IntPtr device, int frequence, DeviceSetupFlags flags, IntPtr win, IntPtr clsid)
        {
            BassException.Thrown(() => !_BassContext.Init(device, frequence, (int)flags, win, clsid));

            _plugins = new ObservableCollection<Plugin>();
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
            get { return _GetConfig((int)Configuration.Buffer); }
            set { _SetConfig((int)Configuration.Buffer, value); }
        }

        /// <summary>
        /// Global music volume.
        /// <para>volume (int): MOD music global volume level... 0 (silent) - 10000 (full).</para>
        /// <para>This config option allows you to have control over the volume levels of all the MOD musics, which is useful for setup options (eg. separate music and fx volume controls).</para>
        /// <para>A channel's final volume = channel volume * global volume / max volume. So, for example, if a stream channel's volume is 0.5 and the global stream volume is 8000, then effectively the stream's volume level is 0.4 (0.5 * 8000 / 10000 = 0.4).</para>
        /// </summary>
        public int GlobalMusicVolume
        {
            get { return _GetConfig((int)Configuration.GlobalMusicVolume); }
            set { _SetConfig((int)Configuration.GlobalMusicVolume, value); }
        }

        /// <summary>
        /// Global sample volume.
        /// <para>volume (int): Sample global volume level... 0 (silent) - 10000 (full).</para>
        /// <para>This config option allows you to have control over the volume levels of all the samples, which is useful for setup options (eg. separate music and fx volume controls).</para>
        /// <para>A channel's final volume = channel volume * global volume / max volume. So, for example, if a stream channel's volume is 0.5 and the global stream volume is 8000, then effectively the stream's volume level is 0.4 (0.5 * 8000 / 10000 = 0.4).</para>
        /// </summary>
        public int GlobalSampleVolume
        {
            get { return _GetConfig((int)Configuration.GlobalSampleVolume); }
            set { _SetConfig((int)Configuration.GlobalSampleVolume, value); }
        }

        /// <summary>
        /// Global stream volume.
        /// <para>volume (int): Stream global volume level... 0 (silent) - 10000 (full).</para>
        /// <para>This config option allows you to have control over the volume levels of all streams, which is useful for setup options (eg. separate music and fx volume controls).</para>
        /// <para>A channel's final volume = channel volume * global volume / max volume. So, for example, if a stream channel's volume is 0.5 and the global stream volume is 8000, then effectively the stream's volume level is 0.4 (0.5 * 8000 / 10000 = 0.4).</para>
        /// </summary>
        public int GlobalStreamVolume
        {
            get { return _GetConfig((int)Configuration.GlobalStreamVolume); }
            set { _SetConfig((int)Configuration.GlobalStreamVolume, value); }
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
            get { return _GetConfig((int)Configuration.UpdatePeriod); }
            set { _SetConfig((int)Configuration.UpdatePeriod, value); }
        }

        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint = "BASS_GetConfig")]
        public static extern int _GetConfig(int option);

        [DllImport("bass.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool _SetConfig(int option, int value);

        #endregion Configuration
    }
}