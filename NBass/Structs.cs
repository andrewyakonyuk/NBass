using System;
using System.Runtime.InteropServices;
using NBass.Backstage;

namespace NBass
{
    [Serializable]
    public sealed class ChannelInfo
    {
        private ChannelInfo() { }

        internal ChannelInfo(Data a)
        {
            NumberChannels = a.b;
            Type = (ChannelType)a.d;
            Flags = (ChannelFlags)a.c;
            Frequence = a.a;
            Resolution = a.e;
            Plugin = a.f;
            Sample = a.g;

            if ((Flags & ChannelFlags.Unicode) != ChannelFlags.Default)
            {
                Filename = Marshal.PtrToStringUni(a.h);
            }
            else
            {
                if (Environment.OSVersion.Platform > PlatformID.WinCE)
                {
                    Filename = Helper.IntPtrAsStringUtf8(a.h);
                }
                else
                {
                    Filename = Helper.IntPtrAsStringAnsi(a.h);
                }
            }
        }

        /// <summary>
        /// Default playback rate.
        /// </summary>
        public int Frequence { get; private set; }

        /// <summary>
        /// Number of channels... 1=mono, 2=stereo, etc.
        /// </summary>
        public int NumberChannels { get; private set; }

        public ChannelFlags Flags { get; private set; }

        /// <summary>
        /// The type of channel it is, which can be one of the following (see <see cref="T:Un4seen.Bass.BASSChannelType" />), or another value if it's an add-on format (see the add-on's API).
        /// <i>Other channel types may be supported by add-ons, see the documentation.</i>
        /// </summary>
        public ChannelType Type { get; private set; }

        /// <summary>
        /// The original resolution (bits per sample)... 0 = undefined.
        /// </summary>
        public int Resolution { get; private set; }

        /// <summary>
        /// The plugin that is handling the channel... 0 = not using a plugin.
        /// <para>Note this is only available with streams created using the plugin system via the standard BASS stream creation functions, not those created by add-on functions.
        /// Information on the plugin can be retrieved via <see cref="M:Un4seen.Bass.Bass.BASS_PluginGetInfo(System.Int32)" />.</para>
        /// </summary>
        public int Plugin { get; private set; }

        /// <summary>
        /// The sample that is playing on the channel. (HCHANNEL only)
        /// </summary>
        public int Sample { get; private set; }

        /// <summary>
        /// The filename associated with the channel. (HSTREAM only)
        /// </summary>
        public string Filename { get; private set; }

        /// <summary>
        /// Is the channel a decoding channel?
        /// </summary>
        /// <remarks>Checks, that the <see cref="F:Un4seen.Bass.BASS_CHANNELINFO.flags" /> property contains the BASS_STREAM_DECODE resp. BASS_MUSIC_DECODE flag.</remarks>
        public bool IsDecodingChannel
        {
            get
            {
                return (this.Flags & ChannelFlags.StreamDecode) != ChannelFlags.Default;
            }
        }

        /// <summary>
        /// Gets, if the <see cref="F:Un4seen.Bass.BASS_CHANNELINFO.flags" /> property contains the BASS_SAMPLE_FLOAT resp. BASS_MUSIC_FLOAT flag.
        /// </summary>
        /// <remarks><see langword="true" /> is returned for floating-point channels - else <see langword="false" />.</remarks>
        public bool Is32bit
        {
            get
            {
                return (this.Flags & ChannelFlags.SampleFloat) != ChannelFlags.Default;
            }
        }

        /// <summary>
        /// Gets, if the <see cref="F:Un4seen.Bass.BASS_CHANNELINFO.flags" /> property contains the BASS_SAMPLE_8BITS resp. BASS_MUSIC_8BITS flag.
        /// </summary>
        /// <remarks><see langword="true" /> is returned for 8-bit channels - else <see langword="false" />.</remarks>
        public bool Is8bit
        {
            get
            {
                return (this.Flags & ChannelFlags.Sample8Bits) != ChannelFlags.Default;
            }
        }
    }

    public sealed class EnvironmentInfo
    {
        public float Volume { get; set; }

        public float Damp { get; set; }

        public float Decay { get; set; }

        public EAXEnvironment Type { get; set; }
    }

    /// <summary>
    /// Used with setting and getting Channel3DAttributes
    /// </summary>
    public sealed class Channel3DAttributes
    {
        public Channel3DAttributes()
        {
            Mode = (ThreeDMode)(InsideAngle = OutsideAngle = -1);
            OutsideDeltaVolume = 100;
            MinimumDistance = MaximumDistance = 0f;
        }

        public ThreeDMode Mode { get; set; }

        public float MinimumDistance { get; set; }

        public float MaximumDistance { get; set; }

        public int InsideAngle { get; set; }

        public int OutsideAngle { get; set; }

        public int OutsideDeltaVolume { get; set; }
    }

    /// <summary>
    /// Used with getting and setting Channel3DPosition
    /// </summary>
    public sealed class Channel3DPosition
    {
        /// <summary>
        /// Used with getting and setting Channel3DPosition
        /// </summary>
        /// <param name="pos">position of the sound </param>
        /// <param name="orient">orientation of the sound, this is irrelevant if it//s an
        /// omnidirectional sound source</param>
        /// <param name="velocity">velocity of the sound</param>
        public Channel3DPosition(Vector3D pos, Vector3D orient, Vector3D velocity)
        {
            this.Position = pos;
            this.Orientation = orient;
            this.Velocity = velocity;
        }

        public Vector3D Position { get; set; }

        public Vector3D Orientation { get; set; }

        public Vector3D Velocity { get; set; }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3D
    {
        private float _x;
        private float _y;
        private float _z;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="x">+=right, -=left</param>
        /// <param name="y">+=up, -=down</param>
        /// <param name="z">+=front, -=behind</param>
        public Vector3D(float x, float y, float z)
        {
            this._x = x;
            this._y = y;
            this._z = z;
        }

        public override string ToString()
        {
            return String.Format("[{0}][{1}][{2}]", _x, _y, _z);
        }

        public float X { get { return _x; } set { _x = value; } }

        public float Y { get { return _y; } set { _y = value; } }

        public float Z { get { return _z; } set { _z = value; } }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SampleInfo
    {
        public int freq; //default playback rate
        public int volume; //default volume (0-100)
        public int pan; //default pan (-100=left, 0=middle, 100=right)
        public int flags; //BASS_SAMPLE_xxx flags
        public int Length; //length (in samples, not bytes)
        public int max; // maximum simultaneous playbacks

        // The following are the sample; //s default 3D attributes (if the sample
        // is 3D, BASS_SAMPLE_3D is in flags) see BASS_ChannelSet3DAttributes
        public int mode3d; //BASS_3DMODE_xxx mode

        public float mindist; //minimum distance
        public float MAXDIST; //maximum distance
        public int iangle; //angle of inside projection cone
        public int oangle; //angle of outside projection cone
        public int outvol; //delta-volume outside the projection cone

        // The following are the defaults used if the sample uses the DirectX 7
        // voice allocation/management features.
        public int vam; //voice allocation/management flags (BASS_VAM_xxx)

        public int priority; //priority (0=lowest, 0xffffffff=highest)

        public string Flags
        {
            get
            {
                return Helper.PrintFlags((DeviceSetupFlags)flags);
            }
        }
    }

    public struct RecordInfo
    {
        public int size; // size of this struct (set this before calling the function)
        public int flags; // device capabilities (DSCCAPS_xxx flags)
        public int formats; // supported standard formats (WAVE_FORMAT_xxx flags)
        public int inputs; // number of inputs
        public int singlein; // BASSTRUE = only 1 input can be set at a time
    }

    /// <summary>
    /// Creates a structure to be used with getting and setting 3DFactors
    /// </summary>
    public sealed class ThreeDFactors
    {
        public float Distance { get; set; }
        public float RollOff { get; set; }
        public float Doppler { get; set; }

        /// <summary>
        /// Creates a structure to be used with getting and setting 3DFactors
        /// </summary>
        /// <param name="distf">Distance factor (0.0-10.0, 1.0=use meters, 0.3=use feet, smaller than 0.0=leave current)
        /// By default BASS measures distances in meters, you can change this
        /// setting if you are using a different unit of measurement.</param>
        /// <param name="rollf">Rolloff factor, how fast the sound quietens with distance
        ///(0.0=no rolloff, 1.0=real world, 2.0=2x real... 10.0=max, smaller than 0.0=leave current)</param>
        /// <param name="doppf">Doppler factor (0.0=no doppler, 1.0=real world, 2.0=2x real... 10.0=max, smaller than 0.0=leave current)
        /// The doppler effect is the way a sound appears to change frequency when it is
        /// moving towards or away from you. The listener and sound velocity settings are
        /// used to calculate this effect, this "doppf" value can be used to lessen or
        /// exaggerate the effect.</param>
        public ThreeDFactors(float distf, float rollf, float doppf)
        {
            this.Distance = distf;
            this.RollOff = rollf;
            this.Doppler = doppf;
        }
    }
}