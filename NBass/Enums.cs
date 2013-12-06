using System;
namespace NBass
{
    public enum ChannelState
    {
        Stopped = 0,
        Playing = 1,
        Stalled = 2,
        Paused = 3,
    }

    //TODO rewrite ChannelFlags
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

    public enum ThreeDMode
    {
        LeaveCurrent = -1,
        /// <summary>
        /// Normal 3D processing
        /// </summary>
        Normal,
        /// <summary>
        /// The channel's 3D position (position/velocity/orientation) are relative to the listener. 
        /// </summary>
        Relative,
        /// <summary>
        /// Turn off 3D processing on the channel, the sound will be played in the center.
        /// </summary>
        Off
    }

    [Flags]
    public enum DeviceSetupFlags
    {
        Default = 0x0000,
        EightBits = 0x0001, // use 8 bit resolution, else 16 bit
        Mono = 0x0002, // use mono, else stereo
        ThreeDee = 0x0004, // enable 3D functionality
        //  If the BASS_DEVICE_3D flag is not specified when initilizing BASS,
        //  then the 3D flags (BASS_SAMPLE_3D and BASS_Music3D) are ignored when
        //  loading/creating a sample/stream/music.
        Latency = 0x100,  // calculate device latency (BASS_INFO struct)
        CPSpeakers = 0x400,  // detect speakers via Windows control panel
        Speakers = 0x800,  // force enabling of speaker assignment
        NoSpeaker = 0x1000, // ignore speaker arrangement
        Dmix = 0x2000, // use ALSA "dmix" plugin
        Freq = 0x4000  // set device sample rate
    }

    [Flags]
    public enum StreamFlags
    {
        Default = DeviceSetupFlags.Default,
        EightBits = DeviceSetupFlags.EightBits, // use 8 bit resolution, else 16 bit
        Mono = DeviceSetupFlags.Mono, // use mono, else stereo
        ThreeDee = DeviceSetupFlags.ThreeDee, // enable 3D functionality
        FX = SampleInfoFlags.FX,
        HalfRate = 65536, //  reduced quality MP3/MP2/MP1 (half sample rate)
        SetPosition = 131072, //  enable pin-point seeking on the MP3/MP2/MP1/OGG
        AutoFree = 262144, //  automatically free the stream when it stop/ends
        DecodeOnly = 0x200000, //  don't play the stream, only decode (BASS_ChannelGetData)
        Unicode = -2147483648
    }

    [Flags]
    public enum ChannelType
    {
        /// <summary>
        /// Unknown channel format.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Sample channel. (HCHANNEL)
        /// </summary>
        Sample = 1,
        /// <summary>
        /// Recording channel. (HRECORD)
        /// </summary>
        Record = 2,
        /// <summary>
        /// MO3 format music.
        /// </summary>
        MusicMO3 = 256,
        /// <summary>
        /// User sample stream. This can also be used as a flag to test if the channel is any kind of HSTREAM.
        /// </summary>
        Stream = 65536,
        /// <summary>
        /// OGG format stream.
        /// </summary>
        StreamOGG = 65538,
        /// <summary>
        /// MP1 format stream.
        /// </summary>
        StreamMP1 = 65539,
        /// <summary>
        /// MP2 format stream.
        /// </summary>
        StreamMP2 = 65540,
        /// <summary>
        /// MP2 format stream.
        /// </summary>
        StreamMP3 = 65541,
        /// <summary>
        /// WAV format stream.
        /// </summary>
        StreamAIFF = 65542,
        /// <summary>
        /// CoreAudio codec stream. Additional information is avaliable via the <see cref="T:Un4seen.Bass.BASS_TAG_CACODEC" /> tag.
        /// </summary>
        StreamCA = 65543,
        /// <summary>
        /// Media Foundation codec stream. Additional information is avaliable via the <see cref="F:Un4seen.Bass.BASSTag.BASS_TAG_MF" /> tag.
        /// </summary>
        StreamMF = 65544,
        /// <summary>
        /// BASSmix mixer stream.
        /// </summary>
        StreamMIXER = 67584,
        /// <summary>
        /// BASSmix splitter stream.
        /// </summary>
        StreamSPLIT = 67585,
        /// <summary>
        /// WAV format stream, LOWORD=codec.
        /// </summary>
        StreamWAV = 262144,
        /// <summary>
        /// WAV format stream, PCM 16-bit.
        /// </summary>
        StreamWAV_PCM = 327681,
        /// <summary>
        /// WAV format stream, FLOAT 32-bit.
        /// </summary>
        StreamWAV_FLOAT = 327683,
        /// <summary>
        /// MOD format music. This can also be used as a flag to test if the channel is any kind of HMUSIC.
        /// </summary>
        MusicMOD = 131072,
        /// <summary>
        /// MTM format music.
        /// </summary>
        MusicMTM = 131073,
        /// <summary>
        /// S3M format music.
        /// </summary>
        MusicS3M = 131074,
        /// <summary>
        /// XM format music.
        /// </summary>
        MusicXM = 131075,
        /// <summary>
        /// IT format music.
        /// </summary>
        MusicIT = 131076,
        /// <summary>
        /// WavPack Lossless format stream.
        /// </summary>
        StreamWV = 66816,
        /// <summary>
        ///  WavPack Hybrid Lossless format stream.
        /// </summary>
        StreamWV_H = 66817,
        /// <summary>
        /// WavPack Lossy format stream.
        /// </summary>
        StreamWV_L = 66818,
        /// <summary>
        ///  WavPack Hybrid Lossy format stream.
        /// </summary>
        StreamWV_LH = 66819,
        /// <summary>
        /// Audio-CD, CDA
        /// </summary>
        StreamCD = 66048,
        /// <summary>
        /// WMA format stream.
        /// </summary>
        StreamWMA = 66304,
        /// <summary>
        /// MP3 over WMA format stream.
        /// </summary>
        StreamWMA_MP3 = 66305,
        /// <summary>
        /// FLAC format stream.
        /// </summary>
        StreamFLAC = 67840,
        /// <summary>
        /// FLAC OGG format stream.
        /// </summary>
        StreamFLAC_OGG = 67841,
        /// <summary>
        /// Optimfrog format stream.
        /// </summary>
        StreamOFR = 67072,
        /// <summary>
        /// APE format stream.
        /// </summary>
        StreamAPE = 67328,
        /// <summary>
        /// MPC format stream.
        /// </summary>
        StreamMPC = 68096,
        /// <summary>
        /// AAC format stream.
        /// </summary>
        StreamAAC = 68352,
        /// <summary>
        /// MP4 format stream.
        /// </summary>
        StreamMP4 = 68353,
        /// <summary>
        /// Speex format stream.
        /// </summary>
        StreamSPX = 68608,
        /// <summary>
        /// Apple Lossless (ALAC) format stream.
        /// </summary>
        StreamALAC = 69120,
        /// <summary>
        /// TTA format stream.
        /// </summary>
        StreamTTA = 69376,
        /// <summary>
        /// AC3 format stream.
        /// </summary>
        StreamAC3 = 69632,
        /// <summary>
        /// Opus format stream.
        /// </summary>
        StreamOPUS = 70144,
        /// <summary>
        /// Winamp input format stream.
        /// </summary>
        StreamWINAMP = 66560,
        /// <summary>
        /// MIDI sound format stream.
        /// </summary>
        StreamMIDI = 68864,
        /// <summary>
        /// ADX format stream.
        /// <para>ADX is a lossy proprietary audio storage and compression format developed by CRI Middleware specifically for use in video games, it is derived from ADPCM.</para>
        /// </summary>
        StreamADX = 126976,
        /// <summary>
        /// AIX format stream.
        /// <para>Only for ADX of all versions (with AIXP support).</para>
        /// </summary>
        StreamAIX = 126977,
        /// <summary>
        /// Video format stream.
        /// </summary>
        StreamVideo = 69888
    }

    public enum EAXEnvironment
    {
        Generic = 0,
        PaddedCell,
        Room,
        Bathroom,
        LivingRoom,
        StoneRoom,
        Auditorium,
        ConcertHall,
        Cave,
        Arena,
        Hangar,
        CarpetedHallway,
        Hallway,
        StoneCorridor,
        Alley,
        Forest,
        City,
        Mountains,
        Quarry,
        Plain,
        ParkingLot,
        SewerPipe,
        Underwater,
        Drugged,
        Dizzy,
        Psychotic,
        //  total number of environments
        //COUNT = 26,
    }

    [Flags]
    public enum MusicFlags
    {
        NormalRamping = 0x0001, //  normal ramping
        SensitiveRamping = 0x0002, //  sensitive ramping
        //  Ramping doesn// t take a lot of extra processing and improve
        //  the sound quality by removing "clicks". Sensitive ramping will
        //  leave sharp attacked samples, unlike normal ramping.
        Loop = 0x0004, //  loop music
        FastTracker2Mode = 0x0010, //  play .MOD as FastTracker 2 does
        ProTracker1Mode = 0x0020, //  play .MOD as ProTracker 1 does
        Mono = 0x0040, //  force mono mixing (less CPU usage)
        ThreeDee = 0x0080, //  enable 3D functionality
        PositionReset = 0x0100, //  stop all notes when moving position
        SurroundMode1 = 0x0200, // surround sound
        SurroundMode2 = 0x0400, // surround sound (mode 2)
        StopOnBackJump = 0x0800, // stop the music on a backwards jump effect
        FX = 0x1000, // enable DX8 effects
        CalculateLength = 0x2000, // calculate playback length
        DecodeOnly = 0x200000, // don// t play the music, only decode (BASS_ChannelGetData)
        NoLoadSample = 0x400000, //  don// t load the samples
    }

    [Flags]
    public enum SampleInfoFlags
    {
        Default = 0x0000,
        EightBits = 0x0001, //  8 bit, else 16 bit
        Mono = 0x0002, //  mono, else stereo
        Loop = 0x0004, //  looped
        ThreeDee = 0x0008, //  3D functionality enabled
        Software = 0x0010, //  it// s NOT using hardware mixing
        MuteMax = 0x0020, //  muted at max distance (3D only)
        VAM = 0x0040, //  uses the DX7 voice allocation & management
        FX = 0x0080, //  the DX8 effects are enabled
        OverrideVolume = 0x00010000, //  override lowest volume
        OverridePosition = 0x00020000, //  override longest playing
        OverrideDistance = 0x00030000 //  override furthest from listener (3D only)
    }

    [Flags]
    public enum RecordFlags
    {
        /// <summary>
        /// Use 8-bit resolution. If neither this or the SampleFloat flags are specified, then the recorded data is 16-bit.
        /// </summary>
        SampleEightBit = 1,
        /// <summary>
        /// Use 32-bit floating-point sample data. WDM drivers are required to use this flag in Windows.
        /// </summary>
        SampleFloat = 256,
        /// <summary>
        /// Start the recording paused. 
        /// </summary>
        Pause = 32768
    }
}