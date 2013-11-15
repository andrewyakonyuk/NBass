//#define RECORD

using System;
using System.Runtime.InteropServices;
using NBass.Backstage;

namespace NBass
{
    /// <summary>
    /// Used  with setting /getting ChannelAttributes
    /// </summary>
    public class ChannelAttributes //really a struct
    {
        public int freq;
        public int volume;
        public int panning;

        public ChannelAttributes()
        {
            freq = volume = panning = -1;
        }

        public int Frequency { get { return freq; } set { freq = value; } }

        /// <summary>
        /// Used  with setting /getting ChannelAttributes
        /// </summary>
        /// <param name="freq">Playback rate (-1=leave current)</param>
        /// <param name="volume">Volume (-1=leave current, 0=silent, 100=max)</param>
        /// <param name="panning">pan position(-101 = current, -100 = Left, 0 = middle, 100 = Right)
        /// panning has no effect on 3D channels</param>
        public ChannelAttributes(int freq, int volume, int panning)
        {
            this.freq = freq;
            this.volume = volume;
            this.panning = panning;
        }

        public override string ToString()
        {
            return String.Format("Freq: {0} Vol: {1} Pan: {2}",
                freq, volume, panning);
        }
    }

    /// <summary>
    /// Used with setting and getting Channel3DAttributes
    /// </summary>
    public class Channel3DAttributes
    {
        public int mode;
        public float min;
        public float max;
        public int iangle;
        public int oangle;
        public int outvol;

        public Channel3DAttributes()
        {
            mode = iangle = oangle = -1;
            outvol = 100;
            min = max = 0F;
        }

        /// <summary>
        /// Used with setting and getting Channel3DAttributes
        /// </summary>
        /// <param name="mode">BASS_3DMODE_xxx mode (-1=leave current setting)</param>
        /// <param name="min">minimum distance, volume stops increasing within this distance (smaller than 0.0=leave current)</param>
        /// <param name="max">maximum distance, volume stops decreasing past this distance (smaller than 0.0=leave current)</param>
        /// <param name="iangle">angle of inside projection cone in degrees (360=omnidirectional, -1=leave current)</param>
        /// <param name="oangle">angle of outside projection cone in degrees (-1=leave current)
        /// NOTE: iangle & oangle must both be set in a single call</param>
        /// <param name="outvol">delta-volume outside the projection cone (0=silent, 100=same as inside)</param>
        /// <remarks>The iangle/oangle angles decide how wide the sound is projected around the
        /// orientation angle. Within the inside angle the volume level is the channel
        /// level as set with BASS_ChannelSetAttributes, from the inside to the outside
        /// angles the volume gradually changes by the "outvol" setting.
        /// </remarks>
        public Channel3DAttributes(int mode, float min, float max,
            int iangle, int oangle, int outvol)
        {
            this.mode = mode;
            this.min = min;
            this.max = max;
            this.iangle = iangle;
            this.oangle = oangle;
            this.outvol = outvol;
        }

        public override string ToString()
        {
            return String.Format("Mode: {0} Min: {1} Max: {2} IAngle: {3} OAngle: {4} OutVol: {5}",
                mode, min, max, iangle, oangle, outvol);
        }
    }

    /// <summary>
    /// Used with getting and setting Channel3DPosition
    /// </summary>
    public class Channel3DPosition
    {
        public Vector3D pos;
        public Vector3D orient;
        public Vector3D vel;

        /// <summary>
        /// Used with getting and setting Channel3DPosition
        /// </summary>
        /// <param name="pos">position of the sound </param>
        /// <param name="orient">orientation of the sound, this is irrelevant if it//s an
        /// omnidirectional sound source</param>
        /// <param name="vel">velocity of the sound</param>
        public Channel3DPosition(Vector3D pos, Vector3D orient, Vector3D vel)
        {
            this.pos = pos;
            this.orient = orient;
            this.vel = vel;
        }

        public override string ToString()
        {
            return String.Format("pos:{0}:orient:{1}:vel:{2}",
                pos,
                orient,
                vel);
        }
    }

    /// <summary>
    /// Use with setting and getting 3DPosition
    /// </summary>
    public class BASS3DPosition
    {
        public Vector3D pos;
        public Vector3D vel;
        public Vector3D top;
        public Vector3D front;

        /// <summary>
        /// Use with setting and getting 3DPosition
        /// </summary>
        /// <param name="pos">Position of the listener </param>
        /// <param name="vel">listener's velocity, used to calculate doppler effect</param>
        /// <param name="top">Direction that listener's front is pointing</param>
        /// <param name="front">Direction that listener's top is pointing </param>
        public BASS3DPosition(Vector3D pos, Vector3D vel, Vector3D top, Vector3D front)
        {
            this.pos = pos;
            this.vel = vel;
            this.top = top;
            this.front = front;
        }

        public override string ToString()
        {
            return String.Format("pos:{0}:vel:{1}:top:{2}:front:{3}",
                pos, vel, top, front);
        }
    }

    /// <summary>
    /// Creates a structure to be used with getting and setting 3DFactors
    /// </summary>
    public class BASS3DFactors
    {
        public float distf;
        public float rollf;
        public float doppf;

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
        public BASS3DFactors(float distf, float rollf, float doppf)
        {
            this.distf = distf;
            this.rollf = rollf;
            this.doppf = doppf;
        }

        public override string ToString()
        {
            return String.Format("Dist: {0} Roll: {1} Dopp: {2}",
                distf, rollf, doppf);
        }
    }

    /// <summary>
    /// Used with setting / getting EAXParameters
    /// </summary>
    public class BASSEAXParameters
    {
        public int env;
        public float vol;
        public float decay;
        public float damp;

        /// <summary>
        /// Used with setting / getting EAXParameters
        /// </summary>
        /// <param name="env">Reverb environment</param>
        /// <param name="vol">Reverb volume (0.0=off, 1.0=max, smaller than 0.0=leave current)</param>
        /// <param name="decay">Time in seconds it takes the reverb to diminish by 60dB (0.1-20.0, smaller than 0.0=leave current)</param>
        /// <param name="damp">The damping, high or low frequencies decay faster (0.0=high decays quickest,
        /// 1.0=low/high decay equally, 2.0=low decays quickest, smaller than 0.0=leave current)</param>
        public BASSEAXParameters(EAXEnvironment env, float vol, float decay, float damp)
        {
            this.env = (int)env;
            this.vol = vol;
            this.decay = decay;
            this.damp = damp;
        }

        public override string ToString()
        {
            return String.Format("Env: {0} Vol: {1} Decay: {2} Damp: {3}",
                ((EAXEnvironment)env), vol, decay, damp);
        }
    }

    #region API Structs

    [StructLayout(LayoutKind.Sequential)]
    public struct BASSInfo
    {
        private int size; //size of this struct (set this before calling the function)
        private int flags; //device capabilities (DSCAPS_xxx flags)
        private int hwsize; //size of total device hardware memory
        private int hwfree; //size of free device hardware memory
        private int freesam; //number of free sample slots in the hardware
        private int free3d; //number of free 3D sample slots in the hardware
        private int minrate; //min sample rate supported by the hardware
        private int maxrate; //max sample rate supported by the hardware
        private int eax; //device supports EAX? (always BASSFALSE if BASS_DEVICE_3D was not used) realy bool
        private int a3d; //unused
        private int dsver; //DirectSound version (use to check for DX5/7 functions)
        private int latency; //delay (in ms) before start of playback (requires BASS_DEVICE_LATENCY)

        //		public BASSInfo(bool dumb)
        //		{
        //			this.size = Marshal.SizeOf(typeof(BASSInfo));
        //		}

        public int MaxSecondarySampleRate
        {
            get { return this.maxrate; }
        }

        public int MinSecondarySampleRate
        {
            get { return this.minrate; }
        }

        public int TotalHardwareMemBytes
        {
            get { return this.hwsize; }
        }

        public int FreeHardware3DChannels
        {
            get { return this.free3d; }
        }

        public int FreeHardwareMixingAllChannels
        {
            get { return this.freesam; }
        }

        public int FreeHardwareMemBytes
        {
            get { return this.hwfree; }
        }

        public int Size { set { this.size = value; } }

        #region DeviceCaps

        public string DeviceCapabilities
        {
            get { return Helper.PrintFlags((DeviceCaps)this.flags); }
        }

        public bool Certified
        {
            get { return Helper.Int2Bool((this.flags >> 6 & 1)); }
        }

        public bool ContinuousRate
        {
            get { return Helper.Int2Bool((this.flags >> 4 & 1)); }
        }

        public bool EmulateDriver
        {
            get { return Helper.Int2Bool((this.flags >> 5 & 1)); }
        }

        public bool Primary16Bit
        {
            get { return Helper.Int2Bool((this.flags >> 3 & 1)); }
        }

        public bool Primary8Bit
        {
            get { return Helper.Int2Bool((this.flags >> 2 & 1)); }
        }

        public bool PrimaryMono
        {
            get { return Helper.Int2Bool((this.flags & 1)); }
        }

        public bool PrimaryStereo
        {
            get { return Helper.Int2Bool((this.flags >> 1 & 1)); }
        }

        public bool Secondary16Bit
        {
            get { return Helper.Int2Bool((this.flags >> 11 & 1)); }
        }

        public bool Secondary8Bit
        {
            get { return Helper.Int2Bool((this.flags >> 10 & 1)); }
        }

        public bool SecondaryMono
        {
            get { return Helper.Int2Bool((this.flags >> 8 & 1)); }
        }

        public bool SecondaryStereo
        {
            get { return Helper.Int2Bool((this.flags >> 9 & 1)); }
        }

        public bool EAXSupport
        {
            get { return Helper.Int2Bool(this.eax); }
        }

        public int DXVersion
        {
            get { return this.dsver; }
        }

        public int Latency
        {
            get { return this.latency; }
        }

        #endregion DeviceCaps
    }

    //*************************
    //* Sample info structure *
    //*************************
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

    //********************************************************
    //* 3D vector (for 3D positions/velocities/orientations) *
    //********************************************************
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3D
    {
        private float x; //+=right, -=left
        private float y; //+=up, -=down
        private float z; //+=front, -=behind

        public Vector3D(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        // Still trying to make this a class
        //		public Vector3D()
        //		{
        //			x = y = z = 0;
        //		}

        public override string ToString()
        {
            return String.Format("[{0}][{1}][{2}]", x, y, z);
        }

        public float X { get { return x; } set { x = value; } }

        public float Y { get { return y; } set { y = value; } }

        public float Z { get { return z; } set { z = value; } }
    }

    public struct RecordInfo
    {
        public int size; // size of this struct (set this before calling the function)
        public int flags; // device capabilities (DSCCAPS_xxx flags)
        public int formats; // supported standard formats (WAVE_FORMAT_xxx flags)
        public int inputs; // number of inputs
        public int singlein; // BASSTRUE = only 1 input can be set at a time
    }

    #endregion API Structs

    [Serializable]
    public sealed class ChannelInfo
    {
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
        public string Filename;

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

    [Flags]
    public enum ChannelFlags
    {
        Default = DeviceSetupFlags.Default,
        Sample8Bits = 1,
        SampleFloat = 256,
        SampleLoop = 4,
        Sample3D = 8,
        SampleSoftware = 16,
        SampleVam = 64,
        SampleMutemax = 32,
        SampleFX = 128,
        StreamRestrate = 524288,
        StreamBlock = 1048576,
        StreamAutofree = StreamFlags.AutoFree,
        StreamDecode = StreamFlags.DecodeOnly,
        MusicRAMP = MusicFlags.NormalRamping,
        MusicRAMPS = MusicFlags.SensitiveRamping,
        MusicSurround = MusicFlags.SurroundMode1,
        MusicSurround2 = MusicFlags.SurroundMode2,
        MusicMoninter = 65536,
        MusicFT2Mod = MusicFlags.FastTracker2Mode,
        MusicPT1Mod = MusicFlags.ProTracker1Mode,
        MusicPositionReset = MusicFlags.PositionReset,
        MUSIC_POSRESETEX = 4194304,
        MusicStopback = MusicFlags.StopOnBackJump,

        /// <summary>
        /// Front speakers (channel 1/2)
        /// </summary>
        SPEAKER_FRONT = 16777216,

        /// <summary>
        /// Rear/Side speakers (channel 3/4)
        /// </summary>
        SPEAKER_REAR = 33554432,

        /// <summary>
        /// Center &amp; LFE speakers (5.1, channel 5/6)
        /// </summary>
        SPEAKER_CENLFE = 50331648,

        /// <summary>
        /// Rear Center speakers (7.1, channel 7/8)
        /// </summary>
        SPEAKER_REAR2 = 67108864,

        /// <summary>
        /// Speaker Modifier: left channel only
        /// </summary>
        SPEAKER_LEFT = 268435456,

        /// <summary>
        /// Speaker Modifier: right channel only
        /// </summary>
        SPEAKER_RIGHT = 536870912,

        /// <summary>
        /// Front Left speaker only (channel 1)
        /// </summary>
        SPEAKER_FRONTLEFT = 285212672,

        /// <summary>
        /// Front Right speaker only (channel 2)
        /// </summary>
        SPEAKER_FRONTRIGHT = 553648128,

        /// <summary>
        /// Rear/Side Left speaker only (channel 3)
        /// </summary>
        SPEAKER_REARLEFT = 301989888,

        /// <summary>
        /// Rear/Side Right speaker only (channel 4)
        /// </summary>
        SPEAKER_REARRIGHT = 570425344,

        /// <summary>
        /// Center speaker only (5.1, channel 5)
        /// </summary>
        SPEAKER_CENTER = 318767104,

        /// <summary>
        /// LFE speaker only (5.1, channel 6)
        /// </summary>
        SPEAKER_LFE = 587202560,

        /// <summary>
        /// Rear Center Left speaker only (7.1, channel 7)
        /// </summary>
        SPEAKER_REAR2LEFT = 335544320,

        /// <summary>
        /// Rear Center Right speaker only (7.1, channel 8)
        /// </summary>
        SPEAKER_REAR2RIGHT = 603979776,

        /// <summary>
        /// speakers Pair 1
        /// </summary>
        SPEAKER_PAIR1 = 16777216,

        /// <summary>
        /// speakers Pair 2
        /// </summary>
        SPEAKER_PAIR2 = 33554432,

        /// <summary>
        /// speakers Pair 3
        /// </summary>
        SPEAKER_PAIR3 = 50331648,

        /// <summary>
        /// speakers Pair 4
        /// </summary>
        SPEAKER_PAIR4 = 67108864,

        /// <summary>
        /// speakers Pair 5
        /// </summary>
        SPEAKER_PAIR5 = 83886080,

        /// <summary>
        /// Speakers Pair 6
        /// </summary>
        SPEAKER_PAIR6 = 100663296,

        /// <summary>
        /// Speakers Pair 7
        /// </summary>
        SPEAKER_PAIR7 = 117440512,

        /// <summary>
        /// Speakers Pair 8
        /// </summary>
        SPEAKER_PAIR8 = 134217728,

        /// <summary>
        /// Speakers Pair 9
        /// </summary>
        SPEAKER_PAIR9 = 150994944,

        /// <summary>
        /// Speakers Pair 10
        /// </summary>
        SPEAKER_PAIR10 = 167772160,

        /// <summary>
        /// Speakers Pair 11
        /// </summary>
        SPEAKER_PAIR11 = 184549376,

        /// <summary>
        /// Speakers Pair 12
        /// </summary>
        SPEAKER_PAIR12 = 201326592,

        /// <summary>
        /// Speakers Pair 13
        /// </summary>
        SPEAKER_PAIR13 = 218103808,

        /// <summary>
        /// Speakers Pair 14
        /// </summary>
        SPEAKER_PAIR14 = 234881024,

        /// <summary>
        /// Speakers Pair 15
        /// </summary>
        SPEAKER_PAIR15 = 251658240,

        /// <summary>
        /// File is a Unicode (16-bit characters) filename
        /// </summary>
        Unicode = -2147483648
    }
}