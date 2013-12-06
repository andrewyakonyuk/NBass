using System;
using System.Runtime.InteropServices;

namespace NBass
{
    [Flags]
    internal enum ChannelDataFlags : long
    {
        //  BASS_ChannelGetData flags
        [MarshalAs(UnmanagedType.U4)]
        FFT512 = 0x80000000, //  512 sample FFT

        [MarshalAs(UnmanagedType.U4)]
        FFT1024 = 0x80000001, //  1024 FFT

        [MarshalAs(UnmanagedType.U4)]
        FFT2048 = 0x80000002, //  2048 FFT

        [MarshalAs(UnmanagedType.U4)]
        SFFT512 = 0x80000010, //  512 sample FFT

        [MarshalAs(UnmanagedType.U4)]
        SFFT1024 = 0x80000011, //  1024 stereo sample FFT

        [MarshalAs(UnmanagedType.U4)]
        SFFT2048 = 0x80000012, //  2048 stereo FFT
    }

    internal enum SyncType : long
    {
        // **********************************************************************
        // * Sync types (with BASS_ChannelSetSync() "param" and SYNCPROC "data" *
        // * definitions) & flags.                                              *
        // **********************************************************************
        //  Sync when a music or stream reaches a position.
        //  if HMUSIC...
        //  param: LOWORD=order (0=first, -1=all) HIWORD=row (0=first, -1=all)
        //  data : LOWORD=order HIWORD=row
        //  if HSTREAM...
        //  param: position in bytes
        //  data : not used
        [MarshalAs(UnmanagedType.U4)]
        POS = 0,

        [MarshalAs(UnmanagedType.U4)]
        MUSICPOS = 0,

        //  Sync when an instrument (sample for the non-instrument based formats)
        //  is played in a music (not including retrigs).
        //  param: LOWORD=instrument (1=first) HIWORD=note (0=c0...119=b9, -1=all)
        //  data : LOWORD=note HIWORD=volume (0-64)
        [MarshalAs(UnmanagedType.U4)]
        MUSICINST = 1,

        //  Sync when a music or file stream reaches the end.
        //  param: not used
        //  data : not used
        [MarshalAs(UnmanagedType.U4)]
        END = 2,

        //  Sync when the "sync" effect (XM/MTM/MOD: E8x/Wxx, IT/S3M: S2x) is used.
        //  param: 0:data=pos, 1:data="x" value
        //  data : param=0: LOWORD=order HIWORD=row, param=1: "x" value
        [MarshalAs(UnmanagedType.U4)]
        MUSICFX = 3,

        //  FLAG: post a Windows message (instead of callback)
        //  When using a window message "callback", the message to post is given in the "proc"
        //  parameter of BASS_ChannelSetSync, and is posted to the window specified in the BASS_Init
        //  call. The message parameters are: WPARAM = data, LPARAM = user.
        [MarshalAs(UnmanagedType.U4)]
        META = 4,

        //  Sync when metadata is received in a Shoutcast stream.
        //  param: not used
        //  data : pointer to the metadata
        [MarshalAs(UnmanagedType.U4)]
        MESSAGE = 0x20000000,

        // FLAG: sync at mixtime, else at playtime
        [MarshalAs(UnmanagedType.U4)]
        MIXTIME = 0x40000000,

        //  FLAG: sync only once, else continuously
        [MarshalAs(UnmanagedType.U4)]
        ONETIME = 0x80000000,
    }

    public enum ChannelFX
    {
        // DX8 effect types, use with BASS_ChannelSetFX
        Chorus = 0, // GUID_DSFX_STANDARD_CHORUS

        Compressor = 1, // GUID_DSFX_STANDARD_COMPRESSOR
        Distortion = 2, // GUID_DSFX_STANDARD_DISTORTION
        Echo = 3, // GUID_DSFX_STANDARD_ECHO
        Flanger = 4, // GUID_DSFX_STANDARD_FLANGER
        Gargle = 5, // GUID_DSFX_STANDARD_GARGLE
        I3DL2Reverb = 6, // GUID_DSFX_STANDARD_I3DL2REVERB
        ParametricEQ = 7, // GUID_DSFX_STANDARD_PARAMEQ
        Reverb = 8, // GUID_DSFX_WAVES_REVERB
    }

    //TODO remove?
    [Flags]
    public enum DeviceCaps
    {
        // ***********************************
        // * BASS_INFO flags (from DSOUND.H) *
        // ***********************************
        ContinuousRate = 0x010,

        //  supports all sample rates between min/maxrate
        EmulateDriver = 0x020,

        //  device does NOT have hardware DirectSound support
        Certified = 0x040,

        //  device driver has been certified by Microsoft
        //  The following flags tell what type of samples are supported by HARDWARE
        //  mixing, all these formats are supported by SOFTWARE mixing
        SecondaryMono = 0x100, //  mono

        SecondaryStereo = 0x200, //  stereo
        Secondary8Bit = 0x400, //  8 bit
        Secondary16Bit = 0x800, //  16 bit
    }

    /// <summary>
    /// Channel Position Mode flags to be used with e.g. <see cref="M:Un4seen.Bass.Bass.BASS_ChannelGetLength(System.Int32,Un4seen.Bass.BASSMode)" />, <see cref="M:Un4seen.Bass.Bass.BASS_ChannelGetPosition(System.Int32,Un4seen.Bass.BASSMode)" />, <see cref="M:Un4seen.Bass.Bass.BASS_ChannelSetPosition(System.Int32,System.Int64,Un4seen.Bass.BASSMode)" /> or <see cref="M:Un4seen.Bass.Bass.BASS_StreamGetFilePosition(System.Int32,Un4seen.Bass.BASSStreamFilePosition)" />.
    /// </summary>
    [Flags]
    internal enum BassMode
    {
        /// <summary>
        /// Byte position.
        /// </summary>
        POS_BYTES = 0,

        /// <summary>
        /// Order.Row position (HMUSIC only).
        /// </summary>
        POS_MusicORDERS = 1,

        /// <summary>
        /// Tick position (MIDI streams only).
        /// </summary>
        POS_MIDI_TICK = 2,

        /// <summary>
        /// OGG bitstream number.
        /// </summary>
        POS_OGG = 3,

        /// <summary>
        /// MOD Music Flag: Stop all notes when moving position.
        /// </summary>
        MusicPOSRESET = 32768,

        /// <summary>
        /// MOD Music Flag: Stop all notes and reset bmp/etc when moving position.
        /// </summary>
        MusicPOSRESETEX = 4194304,

        /// <summary>
        /// Mixer Flag: Don't ramp-in the start after seeking.
        /// </summary>
        MIXER_NORAMPIN = 8388608,

        /// <summary>
        /// Get the decoding (not playing) position.
        /// </summary>
        POS_DECODE = 268435456,

        /// <summary>
        /// Flag: decode to the position instead of seeking.
        /// </summary>
        POS_DECODETO = 536870912,

        /// <summary>
        /// Midi Add-On: Let the old sound decay naturally (including reverb) when changing the position, including looping and such can also be used in <see cref="M:Un4seen.Bass.Bass.BASS_ChannelSetPosition(System.Int32,System.Int64,Un4seen.Bass.BASSMode)" /> calls to have it apply to particular position changes.
        /// </summary>
        MIDI_DECAYSEEK = 16384
    }

    /// <summary>
    /// Channel attribute options used by <see cref="M:Un4seen.Bass.Bass.BASS_ChannelSetAttribute(System.Int32,Un4seen.Bass.BASSAttribute,System.Single)" /> and <see cref="M:Un4seen.Bass.Bass.BASS_ChannelGetAttribute(System.Int32,Un4seen.Bass.BASSAttribute,System.Single@)" />.
    /// </summary>
    internal enum Attribute
    {
        /// <summary>
        /// The sample rate of a channel.
        /// <para>freq: The sample rate... 100 (min) to 100000 (max), 0 = original rate (when the channel was created). The value will be rounded down to a whole number.</para>
        /// <para>This attribute applies to playback of the channel, and does not affect the channel's sample data, so has no real effect on decoding channels. It is still adjustable though, so that it can be used by the <see cref="N:Un4seen.Bass.AddOn.Mix">BassMix</see> add-on, and anything else that wants to use it.</para>
        /// <para>Although the standard valid sample rate range is 100 to 100000, some devices/drivers may have a different valid range. See the minrate and maxrate members of the <see cref="T:Un4seen.Bass.viewModelINFO" /> structure.</para>
        /// <para>It is not possible to change the sample rate of a channel if the "with FX flag" <a href="../Overview.html#DX8DMOEffects">DX8 effect implementation</a> enabled on it, unless DirectX 9 or above is installed.</para>
        /// <para>It requires an increased amount of CPU processing to play MOD musics and streams at increased sample rates. If you plan to play MOD musics or streams at greatly increased sample rates, then you should increase the buffer lengths (<see cref="F:Un4seen.Bass.BASSConfig.CONFIG_BUFFER" />) to avoid possible break-ups in the sound.</para>
        /// </summary>
        FREQ = 1,

        /// <summary>
        /// The volume level of a channel.
        /// <para>volume: The volume level... 0 (silent) to 1 (full). This can go above 1.0 on decoding channels.</para>
        /// <para>This attribute applies to playback of the channel, and does not affect the channel's sample data, so has no real effect on decoding channels. It is still adjustable though, so that it can be used by the <see cref="N:Un4seen.Bass.AddOn.Mix">BassMix</see> add-on, and anything else that wants to use it.</para>
        /// <para>When using <see cref="M:Un4seen.Bass.Bass.ChannelSlideAttribute(System.Int32,Un4seen.Bass.BASSAttribute,System.Single,System.Int32)" /> to slide this attribute, a negative volume value can be used to fade-out and then stop the channel.</para>
        /// </summary>
        VOL,

        /// <summary>
        /// The panning/balance position of a channel.
        /// <para>pan: The pan position... -1 (full left) to +1 (full right), 0 = centre.</para>
        /// <para>This attribute applies to playback of the channel, and does not affect the channel's sample data, so has no real effect on decoding channels. It is still adjustable though, so that it can be used by the <see cref="N:Un4seen.Bass.AddOn.Mix">BassMix</see> add-on, and anything else that wants to use it.</para>
        /// <para>It is not possible to set the pan position of a 3D channel. It is also not possible to set the pan position when using <a href="../Overview.html#SpeakerAssignement">speaker assignment</a>, but if needed, it can be done via a <see cref="T:Un4seen.Bass.DSPPROC">DSP function</see> instead (not on mono channels).</para>
        /// <para><b>Platform-specific</b></para>
        /// <para>On Windows, this attribute has no effect when <a href="../Overview.html#SpeakerAssignement">speaker assignment</a> is used, except on Windows Vista and newer with the <see cref="F:Un4seen.Bass.BASSConfig.CONFIG_VISTA_SPEAKERS" /> config option enabled. Balance control could be implemented via a <see cref="T:Un4seen.Bass.DSPPROC">DSP function</see> instead</para>
        /// </summary>
        PAN,

        /// <summary>
        /// The wet (reverb) / dry (no reverb) mix ratio on a sample, stream, or MOD music channel with 3D functionality.
        /// <para>mix: The wet / dry ratio... 0 (full dry) to 1 (full wet), -1 = automatically calculate the mix based on the distance (the default).</para>
        /// <para>Obviously, EAX functions have no effect if the output device does not support EAX. <see cref="M:Un4seen.Bass.Bass.GetInfo(Un4seen.Bass.INFO)" /> can be used to check that. EAX only affects 3D channels, but EAX functions do not require <see cref="M:Un4seen.Bass.Bass.Apply3D" /> to apply the changes.</para>
        /// <para>
        /// <list type="table">
        /// <listheader><term><see cref="T:Un4seen.Bass.BASSError">Additional ERROR CODE</see></term><description>Description</description></listheader>
        /// <item><term>ERROR_NOEAX</term><description>The channel does not have EAX support. EAX only applies to 3D channels that are mixed by the hardware/drivers. <see cref="M:Un4seen.Bass.Bass.ChannelGetInfo(System.Int32,Un4seen.Bass.CHANNELINFO)" /> can be used to check if a channel is being mixed by the hardware.</description></item>
        /// </list>
        /// </para>
        /// <para>EAX is only supported on Windows.</para>
        /// </summary>
        EAXMIX,

        /// <summary>
        /// Non-Windows: Disable playback buffering?
        /// <para>nobuffer: Disable playback buffering... 0 = no, else yes..</para>
        /// <para>A playing channel is normally asked to render data to its playback buffer in advance, via automatic buffer updates or the <see cref="M:Un4seen.Bass.Bass.Update(System.Int32)" /> and <see cref="M:Un4seen.Bass.Bass.ChannelUpdate(System.Int32,System.Int32)" /> functions, ready for mixing with other channels to produce the final mix that is given to the output device.
        /// When this attribute is switched on (the default is off), that buffering is skipped and the channel will only be asked to produce data as it is needed during the generation of the final mix. This allows the lowest latency to be achieved, but also imposes tighter timing requirements on the channel to produce its data and apply any DSP/FX (and run mixtime syncs) that are set on it; if too long is taken, there will be a break in the output, affecting all channels that are playing on the same device.</para>
        /// <para>The channel's data is still placed in its playback buffer when this attribute is on, which allows <see cref="M:Un4seen.Bass.Bass.ChannelGetData(System.Int32,System.IntPtr,System.Int32)" /> and <see cref="M:Un4seen.Bass.Bass.ChannelGetLevel(System.Int32)" /> to be used, although there is likely to be less data available to them due to the buffer being less full.</para>
        /// <para>This attribute can be changed mid-playback. If switched on, any already buffered data will still be played, so that there is no break in sound.</para>
        /// <para>This attribute is not available on Windows, as BASS does not generate the final mix.</para>
        /// </summary>
        NOBUFFER,

        /// <summary>
        /// The CPU usage of a channel.
        /// <para>cpu: The CPU usage (in percentage).</para>
        /// <para>This attribute gives the percentage of CPU that the channel is using, including the time taken by decoding and DSP processing, and any FX that are not using the "with FX flag" <a href="../Overview.html#DX8DMOEffects">DX8 effect implementation</a>. It does not include the time taken to add the channel's data to the final output mix during playback. The processing of some add-on stream formats may also not be entirely included, if they use additional decoding threads; see the add-on documentation for details.</para>
        /// <para>Like <see cref="M:Un4seen.Bass.Bass.GetCPU" />, this function does not strictly tell the CPU usage, but rather how timely the processing is. For example, if it takes 10ms to generate 100ms of data, that would be 10%. If the reported usage exceeds 100%, that means the channel's data is taking longer to generate than to play. The duration of the data is based on the channel's current sample rate (<see cref="F:Un4seen.Bass.BASSAttribute.FREQ" />).</para>
        /// <para>A channel's CPU usage is updated whenever it generates data. That could be during a playback buffer update cycle, or a <see cref="M:Un4seen.Bass.Bass.Update(System.Int32)" /> call, or a <see cref="M:Un4seen.Bass.Bass.ChannelUpdate(System.Int32,System.Int32)" /> call. For a decoding channel, it would be in a <see cref="M:Un4seen.Bass.Bass.ChannelGetData(System.Int32,System.IntPtr,System.Int32)" /> or <see cref="M:Un4seen.Bass.Bass.ChannelGetLevel(System.Int32)" /> call.</para>
        /// <para>This attribute is read-only, so cannot be modified via <see cref="M:Un4seen.Bass.Bass.ChannelSetAttribute(System.Int32,Un4seen.Bass.BASSAttribute,System.Single)" />.</para>
        /// </summary>
        CPU = 7,

        /// <summary>
        /// The sample rate conversion quality of a channel.
        /// <para>quality: The sample rate conversion quality... 0 = linear interpolation, 1 = 8 point sinc interpolation, 2 = 16 point sinc interpolation, 3 = 32 point sinc interpolation. Other values are also accepted but will be interpreted as 0 or 3, depending on whether they are lower or higher.</para>
        /// <para>When a channel has a different sample rate to what the output device is using, the channel's sample data will need to be converted to match the output device's rate during playback. This attribute determines how that is done. The linear interpolation option uses less CPU, but the sinc interpolation gives better sound quality (less aliasing), with the quality and CPU usage increasing with the number of points. A good compromise for lower spec systems could be to use sinc interpolation for music playback and linear interpolation for sound effects.</para>
        /// <para>Whenever possible, a channel's sample rate should match the output device's rate to avoid the need for any sample rate conversion. The device's sample rate could be used in <see cref="M:Un4seen.Bass.Bass.StreamCreate(System.Int32,System.Int32,Un4seen.Bass.BASSFlag,Un4seen.Bass.STREAMPROC,System.IntPtr)" /> or <see cref="M:Un4seen.Bass.Bass.MusicLoad(System.String,System.Int64,System.Int32,Un4seen.Bass.BASSFlag,System.Int32)" /> or <see cref="N:Un4seen.Bass.AddOn.Midi" /> stream creation calls, for example.</para>
        /// <para>The sample rate conversion occurs (when required) during playback, after the sample data has left the channel's playback buffer, so it does not affect the data delivered by <see cref="M:Un4seen.Bass.Bass.ChannelGetData(System.Int32,System.IntPtr,System.Int32)" />. Although this attribute has no direct effect on decoding channels, it is still available so that it can be used by the <see cref="N:Un4seen.Bass.AddOn.Mix" /> add-on and anything else that wants to use it.</para>
        /// <para>This attribute can be set at any time, and changes take immediate effect. A channel's initial setting is determined by the <see cref="F:Un4seen.Bass.BASSConfig.CONFIG_SRC" /> config option, or <see cref="F:Un4seen.Bass.BASSConfig.CONFIG_SRC_SAMPLE" /> in the case of a sample channel.</para>
        /// <para><b>Platform-specific</b></para>
        /// <para>On Windows, sample rate conversion is handled by Windows or the output device/driver rather than BASS, so this setting has no effect on playback there.</para>
        /// </summary>
        SRC,

        /// <summary>
        /// The amplification level of a MOD music.
        /// <para>amp: Amplification level... 0 (min) to 100 (max). This will be rounded down to a whole number.</para>
        /// <para>As the amplification level get's higher, the sample data's range increases, and therefore, the resolution increases. But if the level is set too high, then clipping can occur, which can result in distortion of the sound.</para>
        /// <para>You can check the current level of a MOD music at any time by using <see cref="M:Un4seen.Bass.Bass.ChannelGetLevel(System.Int32)" />. By doing so, you can decide if a MOD music's amplification level needs adjusting.</para>
        /// <para>The default amplification level is 50.</para>
        /// <para>During playback, the effect of changes to this attribute are not heard instantaneously, due to buffering. To reduce the delay, use the <see cref="F:Un4seen.Bass.BASSConfig.CONFIG_BUFFER" /> config option to reduce the buffer length.</para>
        /// </summary>
        MusicAMPLIFY = 256,

        /// <summary>
        /// The pan separation level of a MOD music.
        /// <para>pansep: Pan separation... 0 (min) to 100 (max), 50 = linear. This will be rounded down to a whole number.</para>
        /// <para>By default BASS uses a linear panning "curve". If you want to use the panning of FT2, use a pan separation setting of around 35. To use the Amiga panning (ie. full left and right) set it to 100.</para>
        /// </summary>
        MusicPANSEP,

        /// <summary>
        /// The position scaler of a MOD music.
        /// <para>scale: The scaler... 1 (min) to 256 (max). This will be rounded down to a whole number.</para>
        /// <para>When calling <see cref="M:Un4seen.Bass.Bass.ChannelGetPosition(System.Int32,Un4seen.Bass.BASSMode)" />, the row (HIWORD) will be scaled by this value. By using a higher scaler, you can get a more precise position indication.</para>
        /// <para>The default scaler is 1.</para>
        /// <para>
        /// <example>
        /// Get the position of a MOD music accurate to within a 10th of a row:
        /// <code>
        /// // set the scaler
        /// Bass.ChannelSetAttribute(music, BASSAttribute.MusicPSCALER, 10f);
        /// int pos = Bass.MusicGetOrderPosition(music);
        /// // the order
        /// int order = Utils.LowWord32(pos);
        /// // the row
        /// int row = HighWord32(pos) / 10;
        /// // the 10th of a row
        /// int row10th = HighWord32(pos) % 10;
        /// </code>
        /// <code lang="vbnet">
        /// ' set the scaler
        /// Bass.ChannelSetAttribute(music, BASSAttribute.MusicPSCALER, 10F)
        /// Dim pos As Integer = Bass.MusicGetOrderPosition(music)
        /// ' the order
        /// Dim order As Integer = Utils.LowWord32(pos)
        /// ' the row
        /// Dim row As Integer = HighWord32(pos) / 10
        /// ' the 10th of a row
        /// Dim row10th As Integer = HighWord32(pos) Mod 10
        /// </code>
        /// </example>
        /// </para>
        /// </summary>
        MusicPSCALER,

        /// <summary>
        /// The BPM of a MOD music.
        /// <para>bpm: The BPM... 1 (min) to 255 (max). This will be rounded down to a whole number.</para>
        /// <para>This attribute is a direct mapping of the MOD's BPM, so the value can be changed via effects in the MOD itself.</para>
        /// <para>Note that by changing this attribute, you are changing the playback length.</para>
        /// <para>During playback, the effect of changes to this attribute are not heard instantaneously, due to buffering. To reduce the delay, use the <see cref="F:Un4seen.Bass.BASSConfig.CONFIG_BUFFER" /> config option to reduce the buffer length.</para>
        /// </summary>
        MusicBPM,

        /// <summary>
        /// The speed of a MOD music.
        /// <para>speed: The speed... 0 (min) to 255 (max). This will be rounded down to a whole number.</para>
        /// <para>This attribute is a direct mapping of the MOD's speed, so the value can be changed via effects in the MOD itself.</para>
        /// <para>The "speed" is the number of ticks per row. Setting it to 0, stops and ends the music. Note that by changing this attribute, you are changing the playback length.</para>
        /// <para>During playback, the effect of changes to this attribute are not heard instantaneously, due to buffering. To reduce the delay, use the <see cref="F:Un4seen.Bass.BASSConfig.CONFIG_BUFFER" /> config option to reduce the buffer length.</para>
        /// </summary>
        MusicSPEED,

        /// <summary>
        /// The global volume level of a MOD music.
        /// <para>volume: The global volume level... 0 (min) to 64 (max, 128 for IT format). This will be rounded down to a whole number.</para>
        /// <para>This attribute is a direct mapping of the MOD's global volume, so the value can be changed via effects in the MOD itself.
        /// The "speed" is the number of ticks per row. Setting it to 0, stops and ends the music. Note that by changing this attribute, you are changing the playback length.</para>
        /// <para>During playback, the effect of changes to this attribute are not heard instantaneously, due to buffering. To reduce the delay, use the <see cref="F:Un4seen.Bass.BASSConfig.CONFIG_BUFFER" /> config option to reduce the buffer length.</para>
        /// </summary>
        MusicVOL_GLOBAL,

        /// <summary>
        /// The volume level of a channel in a MOD music + <i>channel#</i>.
        /// <para>channel: The channel to set the volume of... 0 = 1st channel.</para>
        /// <para>volume: The volume level... 0 (silent) to 1 (full).</para>
        /// <para>The volume curve used by this attribute is always linear, eg. 0.5 = 50%. The <see cref="F:Un4seen.Bass.BASSConfig.CONFIG_CURVE_VOL" /> config option setting has no effect on this. The volume level of all channels is initially 1 (full).</para>
        /// <para>This attribute can also be used to count the number of channels in a MOD Music.</para>
        /// <para>During playback, the effect of changes to this attribute are not heard instantaneously, due to buffering. To reduce the delay, use the <see cref="F:Un4seen.Bass.BASSConfig.CONFIG_BUFFER" /> config option to reduce the buffer length.</para>
        /// <para>
        /// <example>
        /// Count the number of channels in a MOD music:
        /// <code>
        /// int channels = 0;
        /// float dummy;
        /// while (Bass.ChannelGetAttribute(music, (BASSAttribute)((int)MusicVOL_CHAN + channels), ref dummy))
        /// {
        ///   channels++;
        /// }
        /// </code>
        /// <code lang="vbnet">
        /// Dim channels As Integer = 0
        /// Dim dummy As Single
        /// While Bass.ChannelGetAttribute(music, CType(CInt(MusicVOL_CHAN) + channels, BASSAttribute), dummy)
        ///   channels += 1
        /// End While
        /// </code>
        /// </example>
        /// </para>
        /// </summary>
        MusicVOL_CHAN = 512,

        /// <summary>
        /// The volume level of an instrument in a MOD music + <i>inst#</i>.
        /// <para>inst: The instrument to set the volume of... 0 = 1st instrument.</para>
        /// <para>volume: The volume level... 0 (silent) to 1 (full).</para>
        /// <para>The volume curve used by this attribute is always linear, eg. 0.5 = 50%. The <see cref="F:Un4seen.Bass.BASSConfig.CONFIG_CURVE_VOL" /> config option setting has no effect on this. The volume level of all instruments is initially 1 (full). For MOD formats that do not use instruments, read "sample" for "instrument".</para>
        /// <para>This attribute can also be used to count the number of instruments in a MOD music.</para>
        /// <para>During playback, the effect of changes to this attribute are not heard instantaneously, due to buffering. To reduce the delay, use the <see cref="F:Un4seen.Bass.BASSConfig.CONFIG_BUFFER" /> config option to reduce the buffer length.</para>
        /// <para>
        /// <example>
        /// Count the number of instruments in a MOD music:
        /// <code>
        /// int instruments = 0;
        /// float dummy;
        /// while (Bass.ChannelGetAttribute(music, (BASSAttribute)((int)BASSAttribute.MusicVOL_INST + instruments), ref dummy))
        /// {
        ///   instruments++;
        /// }
        /// </code>
        /// <code lang="vbnet">
        /// Dim instruments As Integer = 0
        /// Dim dummy As Single
        /// While Bass.ChannelGetAttribute(music, CType(CInt(BASSAttribute.MusicVOL_INST) + instruments, BASSAttribute), dummy)
        ///   instruments += 1
        /// End While
        /// </code>
        /// </example>
        /// </para>
        /// </summary>
        MusicVOL_INST = 768,

        /// <summary>
        /// FX Tempo: The Tempo in percents (-95%..0..+5000%).
        /// </summary>
        TEMPO = 65536,

        /// <summary>
        /// FX Tempo: The Pitch in semitones (-60..0..+60).
        /// </summary>
        TEMPO_PITCH,

        /// <summary>
        /// FX Tempo: The Samplerate in Hz, but calculates by the same % as TEMPO.
        /// </summary>
        TEMPO_FREQ,

        /// <summary>
        /// FX Tempo Option: Use FIR low-pass (anti-alias) filter (gain speed, lose quality)? <see langword="true" />=1 (default), <see langword="false" />=0.
        /// <para>See <see cref="M:Un4seen.Bass.AddOn.Fx.BassFx.FX_TempoCreate(System.Int32,Un4seen.Bass.BASSFlag)" /> for details.</para>
        /// <para>On iOS, Android, WinCE and Linux ARM platforms this is by default disabled for lower CPU usage.</para>
        /// </summary>
        TEMPO_OPTION_USE_AA_FILTER = 65552,

        /// <summary>
        /// FX Tempo Option: FIR low-pass (anti-alias) filter length in taps (8 .. 128 taps, default = 32, should be %4).
        /// <para>See <see cref="M:Un4seen.Bass.AddOn.Fx.BassFx.FX_TempoCreate(System.Int32,Un4seen.Bass.BASSFlag)" /> for details.</para>
        /// </summary>
        TEMPO_OPTION_AA_FILTER_LENGTH,

        /// <summary>
        /// FX Tempo Option: Use quicker tempo change algorithm (gain speed, lose quality)? <see langword="true" />=1, <see langword="false" />=0 (default).
        /// <para>See <see cref="M:Un4seen.Bass.AddOn.Fx.BassFx.FX_TempoCreate(System.Int32,Un4seen.Bass.BASSFlag)" /> for details.</para>
        /// </summary>
        TEMPO_OPTION_USE_QUICKALGO,

        /// <summary>
        /// FX Tempo Option: Tempo Sequence in milliseconds (default = 82).
        /// <para>See <see cref="M:Un4seen.Bass.AddOn.Fx.BassFx.FX_TempoCreate(System.Int32,Un4seen.Bass.BASSFlag)" /> for details.</para>
        /// </summary>
        TEMPO_OPTION_SEQUENCE_MS,

        /// <summary>
        /// FX Tempo Option: SeekWindow in milliseconds (default = 14).
        /// <para>See <see cref="M:Un4seen.Bass.AddOn.Fx.BassFx.FX_TempoCreate(System.Int32,Un4seen.Bass.BASSFlag)" /> for details.</para>
        /// </summary>
        TEMPO_OPTION_SEEKWINDOW_MS,

        /// <summary>
        /// FX Tempo Option: Tempo Overlap in milliseconds (default = 12).
        /// <para>See <see cref="M:Un4seen.Bass.AddOn.Fx.BassFx.FX_TempoCreate(System.Int32,Un4seen.Bass.BASSFlag)" /> for details.</para>
        /// </summary>
        TEMPO_OPTION_OVERLAP_MS,

        /// <summary>
        /// FX Tempo Option: Prevents clicks with tempo changes (default = FALSE).
        /// <para>See <see cref="M:Un4seen.Bass.AddOn.Fx.BassFx.FX_TempoCreate(System.Int32,Un4seen.Bass.BASSFlag)" /> for details.</para>
        /// </summary>
        TEMPO_OPTION_PREVENT_CLICK,

        /// <summary>
        /// FX Reverse: The Playback direction (-1=FX_RVS_REVERSE or 1=FX_RVS_FORWARD).
        /// </summary>
        REVERSE_DIR = 69632,

        /// <summary>
        /// BASSMIDI: Gets the Pulses Per Quarter Note (or ticks per beat) value of the MIDI file.
        /// <para>ppqn: The PPQN value.</para>
        /// <para>This attribute is the number of ticks per beat as defined by the MIDI file; it will be 0 for MIDI streams created via <see cref="M:Un4seen.Bass.AddOn.Midi.BassMidi.MIDI_StreamCreate(System.Int32,Un4seen.Bass.BASSFlag,System.Int32)" />,
        /// It is also read-only, so can't be modified via <see cref="M:Un4seen.Bass.Bass.ChannelSetAttribute(System.Int32,Un4seen.Bass.BASSAttribute,System.Single)" />.</para>
        /// <para>
        /// <example>
        /// Get the currnet position of a MIDI stream in beats:
        /// <code>
        /// float ppqn;
        /// Bass.ChannelGetAttribute(midi, BASSAttribute.MIDI_PPQN, ref ppqn);
        /// long tick = Bass.ChannelGetPosition(midi, BASSMode.POS_MIDI_TICK);
        /// float beat = tick / ppqn;
        /// </code>
        /// <code lang="vbnet">
        /// Dim ppqn As Single
        /// Bass.ChannelGetAttribute(midi, BASSAttribute.MIDI_PPQN, ppqn)
        /// Dim tick As Long = Bass.ChannelGetPosition(midi, BASSMode.POS_MIDI_TICK)
        /// Dim beat As Single = tick / ppqn
        /// </code>
        /// </example>
        /// </para>
        /// </summary>
        MIDI_PPQN = 73728,

        /// <summary>
        /// BASSMIDI: The maximum percentage of CPU time that a MIDI stream can use.
        /// <para>limit: The CPU usage limit... 0 to 100, 0 = unlimited.</para>
        /// <para>It is not strictly the CPU usage that is measured, but rather how timely the stream is able to render data. For example, a limit of 50% would mean that the rendering would need to be at least 2x real-time speed. When the limit is exceeded, BASSMIDI will begin killing voices, starting with the most quiet.</para>
        /// <para>When the CPU usage is limited, the stream's samples are loaded asynchronously so that any loading delays (eg. due to slow disk) do not hold up the stream for too long. If a sample cannot be loaded in time, then it will be silenced until it is available and the stream will continue playing other samples as normal in the meantime.
        /// This does not affect sample loading via <see cref="M:Un4seen.Bass.AddOn.Midi.BassMidi.MIDI_StreamLoadSamples(System.Int32)" />, which always operates synchronously.</para>
        /// <para>By default, a MIDI stream will have no CPU limit.</para>
        /// </summary>
        MIDI_CPU,

        /// <summary>
        /// BASSMIDI: The number of MIDI channels in a MIDI stream.
        /// <para>channels: The number of MIDI channels... 1 (min) - 128 (max). For a MIDI file stream, the minimum is 16.</para>
        /// <para>New channels are melodic by default. Any notes playing on a removed channel are immediately stopped.</para>
        /// </summary>
        MIDI_CHANS,

        /// <summary>
        /// BASSMIDI: The maximum number of samples to play at a time (polyphony) in a MIDI stream.
        /// <para>voices: The number of voices... 1 (min) - 500 (max).</para>
        /// <para>If there are currently more voices active than the new limit, then some voices will be killed to meet the limit. The number of voices currently active is available via the <see cref="F:Un4seen.Bass.BASSAttribute.MIDI_VOICES_ACTIVE" /> attribute.</para>
        /// <para>A MIDI stream will initially have a default number of voices as determined by the <see cref="F:Un4seen.Bass.BASSConfig.CONFIG_MIDI_VOICES" /> config option.</para>
        /// </summary>
        MIDI_VOICES,

        /// <summary>
        /// BASSMIDI: The number of samples currently playing in a MIDI stream.
        /// <para>voices: The number of voices.</para>
        /// <para>This attribute is read-only, so cannot be modified via <see cref="M:Un4seen.Bass.Bass.ChannelSetAttribute(System.Int32,Un4seen.Bass.BASSAttribute,System.Single)" />.</para>
        /// </summary>
        MIDI_VOICES_ACTIVE,

        /// <summary>
        /// BASSMIDI: The volume level of a track in a MIDI file stream + <i>track#</i>.
        /// <para>track#: The track to set the volume of... 0 = first track.</para>
        /// <para>volume: The volume level (0.0=silent, 1.0=normal/default).</para>
        /// <para>The volume curve used by this attribute is always linear, eg. 0.5 = 50%. The CONFIG_CURVE_VOL config option setting has no effect on this.</para>
        /// <para>During playback, the effect of changes to this attribute are not heard instantaneously, due to buffering. To reduce the delay, use the CONFIG_BUFFER config option to reduce the buffer length.</para>
        /// <para>This attribute can also be used to count the number of tracks in a MIDI file stream. MIDI streams created via <see cref="M:Un4seen.Bass.AddOn.Midi.BassMidi.MIDI_StreamCreate(System.Int32,Un4seen.Bass.BASSFlag,System.Int32)" /> do not have any tracks.</para>
        /// <para>
        /// <example>
        /// Count the number of tracks in a MIDI stream:
        /// <code>
        /// int tracks = 0;
        /// float dummy;
        /// while (Bass.ChannelGetAttribute(midi, (BASSAttribute)((int)BASSAttribute.MIDI_TRACK_VOL + tracks), ref dummy))
        /// {
        ///   tracks++;
        /// }
        /// </code>
        /// <code lang="vbnet">
        /// Dim tracks As Integer = 0
        /// Dim dummy As Single
        /// While Bass.ChannelGetAttribute(midi, CType(CInt(BASSAttribute.MIDI_TRACK_VOL) + tracks, BASSAttribute), dummy)
        ///   tracks += 1
        /// End While
        /// </code>
        /// </example>
        /// </para>
        /// </summary>
        MIDI_TRACK_VOL = 73984,

        /// <summary>
        /// BASSOPUS: The sample rate of an Opus stream's source material.
        /// <para>freq: The sample rate.</para>
        /// <para>Opus streams always have a sample rate of 48000 Hz, and an Opus encoder will resample the source material to that if necessary.
        /// This attribute presents the original sample rate, which may be stored in the Opus file header. This attribute is read-only, so cannot be modified via <see cref="M:Un4seen.Bass.Bass.ChannelSetAttribute(System.Int32,Un4seen.Bass.BASSAttribute,System.Single)" />.</para>
        /// </summary>
        OPUS_ORIGFREQ = 77824
    }

    internal enum Tag
    {
        ID3 = 0, // ID3v1 tags : 128 byte block
        ID3V2 = 1, // ID3v2 tags : variable length block
        OGG = 2, // OGG comments : array of null-terminated strings
        HTTP = 3, // HTTP headers : array of null-terminated strings
        ICY = 4, // ICY headers : array of null-terminated strings
        META = 5, // ICY metadata : null-terminated string
    }

    internal enum Configuration
    {
        Buffer,

        UpdatePeriod,

        GlobalSampleVolume = 4,

        GlobalStreamVolume,

        GlobalMusicVolume,

        /// <summary>
        /// Volume translation curve.
        /// <para>logvol (bool): Volume curve... <see langword="false" /> = linear, <see langword="true" /> = logarithmic.</para>
        /// <para>DirectSound uses logarithmic volume and panning curves, which can be awkward to work with.
        /// For example, with a logarithmic curve, the audible difference between 10000 and 9000, is not the same as between 9000 and 8000.
        /// With a linear "curve" the audible difference is spread equally across the whole range of values, so in the previous example the audible difference between 10000 and 9000, and between 9000 and 8000 would be identical.</para>
        /// <para>When using the linear curve, the volume range is from 0% (silent) to 100% (full).
        /// When using the logarithmic curve, the volume range is from -100 dB (effectively silent) to 0 dB (full). For example, a volume level of 0.5 is 50% linear or -50 dB logarithmic.</para>
        /// <para>The linear curve is used by default.</para>
        /// </summary>
        BASS_CONFIG_CURVE_VOL,

        /// <summary>
        /// Panning translation curve.
        /// <para>logpan (bool): Panning curve... <see langword="false" /> = linear, <see langword="true" /> = logarithmic.</para>
        /// <para>The panning curve affects panning in exactly the same way as the volume curve (BASS_CONFIG_CURVE_VOL) affects the volume.</para>
        /// <para>The linear curve is used by default.</para>
        /// </summary>
        BASS_CONFIG_CURVE_PAN,

        /// <summary>
        /// Pass 32-bit floating-point sample data to all DSP functions?
        /// <para>floatdsp (bool): If <see langword="true" />, 32-bit floating-point sample data is passed to all <see cref="T:Un4seen.Bass.DSPPROC" /> callback functions.</para>
        /// <para>Normally DSP functions receive sample data in whatever format the channel is using, ie. it can be 8, 16 or 32-bit. But using this config option, BASS will convert 8/16-bit sample data to 32-bit floating-point before passing it to DSP functions, and then convert it back after all the DSP functions are done. As well as simplifying the DSP code (no need for 8/16-bit processing), this also means that there is no degradation of quality as sample data passes through a chain of DSP.</para>
        /// <para>This config option also applies to effects set via <see cref="M:Un4seen.Bass.Bass.BASS_ChannelSetFX(System.Int32,Un4seen.Bass.BASSFXType,System.Int32)" />, except for DX8 effects when using the "With FX flag" <a href="../Overview.html#DX8DMO">DX8 effect implementation</a>.</para>
        /// <para>Changing the setting while there are DSP or FX set could cause problems, so should be avoided.</para>
        /// <para><b>Platform-specific:</b> On Android and Windows CE, 8.24 bit fixed-point is used instead of floating-point. Floating-point DX8 effect processing requires DirectX 9 (or above) on Windows.</para>
        /// </summary>
        BASS_CONFIG_FLOATDSP,

        /// <summary>
        /// The 3D algorithm for software mixed 3D channels.
        /// <para>algo (int): Use one of the <see cref="T:Un4seen.Bass.BASS3DAlgorithm" /> flags.</para>
        /// <para>These algorithms only affect 3D channels that are being mixed in software. <see cref="M:Un4seen.Bass.Bass.BASS_ChannelGetInfo(System.Int32,Un4seen.Bass.BASS_CHANNELINFO)" /> can be used to check whether a channel is being software mixed.</para>
        /// <para>Changing the algorithm only affects subsequently created or loaded samples, musics, or streams; it does not affect any that already exist.</para>
        /// <para>On Windows, DirectX 7 or above is required for this option to have effect. On other platforms, only the BASS_3DALG_DEFAULT and BASS_3DALG_OFF options are available.</para>
        /// </summary>
        BASS_CONFIG_3DALGORITHM,

        /// <summary>
        /// Time to wait for a server to respond to a connection request.
        /// <para>timeout (int): The time to wait, in milliseconds.</para>
        /// <para>The default timeout is 5 seconds (5000 milliseconds).</para>
        /// </summary>
        BASS_CONFIG_NET_TIMEOUT,

        /// <summary>
        /// Internet download buffer length.
        /// <para>length (int): The buffer length, in milliseconds.</para>
        /// <para>Increasing the buffer length decreases the chance of the stream stalling, but also increases the time taken by <see cref="M:Un4seen.Bass.Bass.BASS_StreamCreateURL(System.String,System.Int32,Un4seen.Bass.BASSFlag,Un4seen.Bass.DOWNLOADPROC,System.IntPtr)" /> to create the stream, as it has to pre-buffer more data (adjustable via the <see cref="F:Un4seen.Bass.BASSConfig.BASS_CONFIG_NET_PREBUF" /> option). Aside from the pre-buffering, this setting has no effect on streams without either the <see cref="F:Un4seen.Bass.BASSFlag.BASS_STREAM_BLOCK" /> or <see cref="F:Un4seen.Bass.BASSFlag.BASS_STREAM_RESTRATE" /> flags.</para>
        /// <para>When streaming in blocks, this option determines the download buffer length. The effective buffer length can actually be a bit more than that specified, including data that's been read from the buffer by the decoder but not been used yet.</para>
        /// <para>This config option also determines the buffering used by "buffered" user file streams created with <see cref="M:Un4seen.Bass.Bass.BASS_StreamCreateFileUser(Un4seen.Bass.BASSStreamSystem,Un4seen.Bass.BASSFlag,Un4seen.Bass.BASS_FILEPROCS,System.IntPtr)" />.</para>
        /// <para>The default buffer length is 5 seconds (5000 milliseconds). The net buffer length should be larger than the length of the playback buffer (<see cref="F:Un4seen.Bass.BASSConfig.BASS_CONFIG_BUFFER" />), otherwise the stream is likely to briefly stall soon after starting playback.</para>
        /// <para>Using this config option only affects streams created afterwards, not any that have already been created.</para>
        /// </summary>
        BASS_CONFIG_NET_BUFFER,

        /// <summary>
        /// Prevent channels being played when the output is paused?
        /// <para>noplay (bool): If <see langword="true" />, channels can't be played while the output is paused.</para>
        /// <para>When the output is paused using <see cref="M:Un4seen.Bass.Bass.BASS_Pause" />, and this config option is enabled, channels can't be played until the output is resumed using <see cref="M:Un4seen.Bass.Bass.BASS_Start" />. Attempts to play a channel will give a <see cref="F:Un4seen.Bass.BASSError.BASS_ERROR_START" /> error.</para>
        /// <para>By default, this config option is enabled.</para>
        /// </summary>
        BASS_CONFIG_PAUSE_NOPLAY,

        /// <summary>
        /// Amount to pre-buffer when opening internet streams.
        /// <para>prebuf (int): Amount (percentage) to pre-buffer.</para>
        /// <para>This setting determines what percentage of the buffer length (<see cref="F:Un4seen.Bass.BASSConfig.BASS_CONFIG_NET_BUFFER" />) should be filled by <see cref="M:Un4seen.Bass.Bass.BASS_StreamCreateURL(System.String,System.Int32,Un4seen.Bass.BASSFlag,Un4seen.Bass.DOWNLOADPROC,System.IntPtr)" />. The default is 75%. Setting this lower (eg. 0) is useful if you want to display a "buffering progress" (using <see cref="M:Un4seen.Bass.Bass.BASS_StreamGetFilePosition(System.Int32,Un4seen.Bass.BASSStreamFilePosition)" />) when opening internet streams, but note that this setting is just a minimum - BASS will always pre-download a certain amount to verify the stream.</para>
        /// <para>As well as internet streams, this config setting also applies to "buffered" user file streams created with <see cref="M:Un4seen.Bass.Bass.BASS_StreamCreateFileUser(Un4seen.Bass.BASSStreamSystem,Un4seen.Bass.BASSFlag,Un4seen.Bass.BASS_FILEPROCS,System.IntPtr)" />.</para>
        /// </summary>
        BASS_CONFIG_NET_PREBUF = 15,

        /// <summary>
        /// "User-Agent" header.
        /// <para>agent (string pointer): The "User-Agent" header.</para>
        /// <para>BASS does not make a copy of the config string, so it must reside in the heap (not the stack), eg. a global variable. This also means that the agent setting can subsequently be changed at that location without having to call this function again.</para>
        /// <para>Changes take effect from the next internet stream creation call.</para>
        /// </summary>
        BASS_CONFIG_NET_AGENT,

        /// <summary>
        /// Proxy server settings (in the form of "user:pass@server:port"... <see langword="null" /> = don't use a proxy).
        /// <para>proxy (string pointer): The proxy server settings, in the form of "user:pass@server:port"... <see langword="null" /> = don't use a proxy. "" (empty string) = use the OS's default proxy settings. If only the "user:pass@" part is specified, then those authorization credentials are used with the default proxy server. If only the "server:port" part is specified, then that proxy server is used without any authorization credentials.</para>
        /// <para>BASS does not make a copy of the config string, so it must reside in the heap (not the stack), eg. a global variable. This also means that the proxy settings can subsequently be changed at that location without having to call this function again.</para>
        /// <para>Changes take effect from the next internet stream creation call.</para>
        /// </summary>
        BASS_CONFIG_NET_PROXY,

        /// <summary>
        /// Use passive mode in FTP connections?
        /// <para>passive (bool): If <see langword="true" />, passive mode is used, otherwise normal/active mode is used.</para>
        /// <para>Changes take effect from the next internet stream creation call. By default, passive mode is enabled.</para>
        /// </summary>
        BASS_CONFIG_NET_PASSIVE,

        /// <summary>
        /// The buffer length for recording channels.
        /// <para>length (int): The buffer length in milliseconds... 1000 (min) - 5000 (max). If the length specified is outside this range, it is automatically capped.</para>
        /// <para>Unlike a playback buffer, where the aim is to keep the buffer full, a recording buffer is kept as empty as possible and so this setting has no effect on latency. The default recording buffer length is 2000 milliseconds. Unless processing of the recorded data could cause significant delays, or you want to use a large recording period with <see cref="M:Un4seen.Bass.Bass.BASS_RecordStart(System.Int32,System.Int32,Un4seen.Bass.BASSFlag,Un4seen.Bass.RECORDPROC,System.IntPtr)" />, there should be no need to increase this.</para>
        /// <para>Using this config option only affects the recording channels that are created afterwards, not any that have already been created. So you can have channels with differing buffer lengths by using this config option each time before creating them.</para>
        /// </summary>
        BASS_CONFIG_REC_BUFFER,

        /// <summary>
        /// Process URLs in PLS, M3U, WPL or ASX playlists?
        /// <para>netlists (int): When to process URLs in PLS, M3U, WPL or ASX playlists... 0 = never, 1 = in <see cref="M:Un4seen.Bass.Bass.BASS_StreamCreateURL(System.String,System.Int32,Un4seen.Bass.BASSFlag,Un4seen.Bass.DOWNLOADPROC,System.IntPtr)" /> only, 2 = in <see cref="M:Un4seen.Bass.Bass.BASS_StreamCreateFile(System.String,System.Int64,System.Int64,Un4seen.Bass.BASSFlag)" /> and <see cref="M:Un4seen.Bass.Bass.BASS_StreamCreateFileUser(Un4seen.Bass.BASSStreamSystem,Un4seen.Bass.BASSFlag,Un4seen.Bass.BASS_FILEPROCS,System.IntPtr)" /> too.</para>
        /// <para>When enabled, BASS will process PLS, M3U, WPL and ASX playlists, going through each entry until it finds a URL that it can play.
        /// By default, playlist procesing is disabled.</para>
        /// </summary>
        BASS_CONFIG_NET_PLAYLIST = 21,

        /// <summary>
        /// The maximum number of virtual music channels (1-512) to use.
        /// <para>number (int): The maximum number of virtual music channels (1-512), ), which should be set before loading the IT file (doesn't affect already loaded files).</para>
        /// <para>When there are no virtual channels free, the quietest one is killed to make way. That means any extra channels (due to raised limit) will be quieter ones.</para>
        /// <para>Note that the virtual channel count/limit is in addition to the normal channels.</para>
        /// </summary>
        BASS_CONFIG_MUSIC_VIRTUAL,

        /// <summary>
        /// The amount of data to check in order to verify/detect the file format.
        /// <para>length (int): The amount of data to check, in bytes... 1000 (min) to 100000 (max). If the value specified is outside this range, it is automatically capped.</para>
        /// <para>Of the file formats supported as standard, this setting only affects the detection of MP3/MP2/MP1 formats,
        /// but it may also be used by add-ons (see the documentation). For internet (and "buffered" user file) streams, a quarter of the length is used, up to a minimum of 1000 bytes.</para>
        /// <para>The verification length excludes any tags that may be at the start of the file. The default length is 16000 bytes.</para>
        /// </summary>
        BASS_CONFIG_VERIFY,

        /// <summary>
        /// The number of threads to use for updating playback buffers.
        /// <para>threads (int): The number of threads to use... 0 = disable automatic updating.</para>
        /// <para>The number of update threads determines how many HSTREAM/HMUSIC channel playback buffers can be updated in parallel;
        /// each thread can process one channel at a time. The default is to use a single thread, but additional threads can be used to take advantage of multiple CPU cores.
        /// There is generally nothing much to be gained by creating more threads than there are CPU cores, but one benefit of using multiple threads even with a single CPU core is that a slow updating channel need not delay the updating of other channels.</para>
        /// <para>When automatic updating is disabled (threads = 0), <see cref="M:Un4seen.Bass.Bass.BASS_Update(System.Int32)" /> or <see cref="M:Un4seen.Bass.Bass.BASS_ChannelUpdate(System.Int32,System.Int32)" /> should be used instead.</para>
        /// <para>The number of update threads can be changed at any time, including during playback.</para>
        /// <para><b>Platform-specific:</b> The number of update threads is limited to 1 on the Android and Windows CE platforms.</para>
        /// </summary>
        BASS_CONFIG_UPDATETHREADS,

        /// <summary>
        /// Linux, Android and CE only: The output device buffer length.
        /// <para>length (int): The buffer length in milliseconds.</para>
        /// <para>The device buffer is where the final mix of all playing channels is placed, ready for the device to play. Its length affects the latency of things like starting and stopping playback of a channel, so you will probably want to avoid setting it unnecessarily high, but setting it too short could result in breaks in the output.</para>
        /// <para>When using a large device buffer, the <see cref="F:Un4seen.Bass.BASSAttribute.BASS_ATTRIB_NOBUFFER" /> attribute could be used to skip the channel buffering stage, to avoid further increasing latency for real-time generated sound and/or DSP/FX changes.</para>
        /// <para>Changes to this config setting only affect subsequently initialized devices, not any that are already initialized.</para>
        /// <para>This config option is only available on Linux, Android and Windows CE. The device's buffer is determined automatically on other platforms.</para>
        /// <para><b>Platform-specific:</b> On Linux, the driver may choose to use a different buffer length if it decides that the specified length is too short or long. The buffer length actually being used can be obtained with <see cref="T:Un4seen.Bass.BASS_INFO" />, like this: latency + minbuf / 2.</para>
        /// </summary>
        BASS_CONFIG_DEV_BUFFER = 27,

        /// <summary>
        /// Use improved precision of position reporting on Vista/Win7 ?
        /// <para>precision (bool): Use the DSBCAPS_TRUEPLAYPOSITION option ? (default is <see langword="false" />).</para>
        /// <para>DirectSound offers a new DSBCAPS_TRUEPLAYPOSITION option under Vista/Win7 or above. This improves the precision of position reporting on Vista/Win7 (in a range within 10ms).
        /// But it also appears to increase latency (around 20ms on Vista but less on Win7). The question now is which is more important: more precise position reporting or lower latency?</para>
        /// <para>Set this option to <see langword="true" /> to have BASS use the DSBCAPS_TRUEPLAYPOSITION option, and <see langword="false" /> to not (default).
        /// It also applies to the latency measuring (BASS_DEVICE_LATENCY), so you can see its effect there too; it should be set before the <see cref="M:Un4seen.Bass.Bass.BASS_Init(System.Int32,System.Int32,Un4seen.Bass.BASSInit,System.IntPtr)" /> call in that case.</para>
        /// </summary>
        BASS_CONFIG_VISTA_TRUEPOS = 30,

        /// <summary>
        /// Supress silencing for corrupted MP3 frames.
        /// <para>errors (bool): Supress error correction silences? (default is <see langword="false" />).</para>
        /// <para>When BASS is detecting some corruption in an MP3 file's Huffman coding, it silences the frame to avoid any unpleasent noises that can result from corruption.
        /// Set this parameter to <see langword="true" /> in order to supress this behavior and </para>
        /// <para>This applies only to the regular BASS version and NOT the "mp3-free" version.</para>
        /// </summary>
        BASS_CONFIG_MP3_ERRORS = 35,

        /// <summary>
        /// Windows-only: Include a "Default" entry in the output device list?
        /// <para>default (bool): If <see langword="true" />, a 'Default' device will be included in the device list (default is <see langword="false" />).</para>
        /// <para>BASS does not usually include a "Default" entry in its device list, as that would ultimately map to one of the other devices and be a duplicate entry. When the default device is requested in a <see cref="M:Un4seen.Bass.Bass.BASS_Init(System.Int32,System.Int32,Un4seen.Bass.BASSInit,System.IntPtr)" /> call (with device = -1), BASS will check the default device at that time, and initialize it. But Windows 7 has the ability to automatically switch the default output to the new default device whenever it changes, and in order for that to happen, the default device (rather than a specific device) needs to be used. That is where this option comes in.</para>
        /// <para>When enabled, the "Default" device will also become the default device to <see cref="M:Un4seen.Bass.Bass.BASS_Init(System.Int32,System.Int32,Un4seen.Bass.BASSInit,System.IntPtr)" /> (with device = -1). When the "Default" device is used, the <see cref="M:Un4seen.Bass.Bass.BASS_SetVolume(System.Single)" /> and <see cref="M:Un4seen.Bass.Bass.BASS_GetVolume" /> functions work a bit differently to usual; they deal with the "session" volume, which only affects the current process's output on the device, rather than the device's volume.</para>
        /// <para>This option can only be set before <see cref="M:Un4seen.Bass.Bass.BASS_GetDeviceInfo(System.Int32,Un4seen.Bass.BASS_DEVICEINFO)" /> or <see cref="M:Un4seen.Bass.Bass.BASS_Init(System.Int32,System.Int32,Un4seen.Bass.BASSInit,System.IntPtr)" /> has been called.</para>
        /// <para><b>Platform-specific:</b> This config option is only available on Windows. It is available on all Windows versions (not including CE), but only Windows 7 has the default output switching feature.</para>
        /// </summary>
        BASS_CONFIG_DEV_DEFAULT,

        /// <summary>
        /// The time to wait for a server to deliver more data for an internet stream.
        /// <para>timeout (int): The time to wait in milliseconds (default=0, infinite).</para>
        /// <para>When the timeout is hit, the connection with the server will be closed. The default setting is 0, no timeout.</para>
        /// </summary>
        BASS_CONFIG_NET_READTIMEOUT,

        /// <summary>
        /// Enable speaker assignment with panning/balance control on Windows Vista and newer?
        /// <para>enable (bool): If <see langword="true" />, speaker assignment with panning/balance control is enabled on Windows Vista and newer.</para>
        /// <para>Panning/balance control via the <see cref="F:Un4seen.Bass.BASSAttribute.BASS_ATTRIB_PAN" /> attribute is not available when <a href="../Overview.html#SpeakerAssignement">speaker assignment</a> is used on Windows due to the way that the speaker assignment needs to be implemented there. The situation is improved with Windows Vista, and speaker assignment can generally be done in a way that does permit panning/balance control to be used at the same time, but there may still be some drivers that it does not work properly with, so it is disabled by default and can be enabled via this config option. Changes only affect channels that are created afterwards, not any that already exist.</para>
        /// <para><b>Platform-specific:</b> This config option is only available on Windows. It is available on all Windows versions (not including CE), but only has effect on Windows Vista and newer. Speaker assignment with panning/balance control is always possible on other platforms, where BASS generates the final mix.</para>
        /// </summary>
        BASS_CONFIG_VISTA_SPEAKERS,

        /// <summary>
        /// Gets the total number of HSTREAM/HSAMPLE/HMUSIC/HRECORD handles.
        /// <para>none: only used with <see cref="M:Un4seen.Bass.Bass.BASS_GetConfig(Un4seen.Bass.BASSConfig)" />.</para>
        /// <para>The handle count may not only include the app-created stuff but also internal stuff, eg. BASS_WASAPI_Init will create a stream when the BASS_WASAPI_BUFFER flag is used.</para>
        /// </summary>
        BASS_CONFIG_HANDLES = 41,

        /// <summary>
        /// Gets or Sets the Unicode character set in device information.
        /// <para>utf8 (bool): If <see langword="true" />, device information will be in UTF-8 form. Otherwise it will be ANSI.</para>
        /// <para>This config option determines what character set is used in the <see cref="T:Un4seen.Bass.BASS_DEVICEINFO" /> structure.
        /// The default setting is UNICODE for Bass.Net and should NOT be changed! It might only be changed before <see cref="M:Un4seen.Bass.Bass.BASS_GetDeviceInfo(System.Int32,Un4seen.Bass.BASS_DEVICEINFO)" /> or <see cref="M:Un4seen.Bass.Bass.BASS_Init(System.Int32,System.Int32,Un4seen.Bass.BASSInit,System.IntPtr)" /> or <see cref="M:Un4seen.Bass.Bass.BASS_RecordGetDeviceInfo(System.Int32,Un4seen.Bass.BASS_DEVICEINFO)" /> or <see cref="M:Un4seen.Bass.Bass.BASS_RecordInit(System.Int32)" /> has been called.</para>
        /// <para><b>Platform-specific:</b> This config option is only available on Windows.</para>
        /// </summary>
        BASS_CONFIG_UNICODE,

        /// <summary>
        /// Gets or Sets the default sample rate conversion quality.
        /// <para>quality (int): The sample rate conversion quality... 0 = linear interpolation, 1 = 8 point sinc interpolation, 2 = 16 point sinc interpolation, 3 = 32 point sinc interpolation. Other values are also accepted.</para>
        /// <para>This config option determines what sample rate conversion quality new channels will initially have, except for sample channels (HCHANNEL), which use the BASS_CONFIG_SRC_SAMPLE setting.
        /// A channel's sample rate conversion quality can subsequently be changed via the BASS_ATTRIB_SRC attribute (see <see cref="M:Un4seen.Bass.Bass.BASS_ChannelSetAttribute(System.Int32,Un4seen.Bass.BASSAttribute,System.Single)" />).</para>
        /// <para>The default setting is 1 (8 point sinc interpolation).</para>
        /// </summary>
        BASS_CONFIG_SRC,

        /// <summary>
        /// Gets or Sets the default sample rate conversion quality for samples.
        /// <para>quality (int): The sample rate conversion quality... 0 = linear interpolation, 1 = 8 point sinc interpolation, 2 = 16 point sinc interpolation, 3 = 32 point sinc interpolation. Other values are also accepted.</para>
        /// <para>This config option determines what sample rate conversion quality a new sample channel will initially have, following a <see cref="M:Un4seen.Bass.Bass.BASS_SampleGetChannel(System.Int32,System.Boolean)" /> call.
        /// The channel's sample rate conversion quality can subsequently be changed via the BASS_ATTRIB_SRC attribute (see <see cref="M:Un4seen.Bass.Bass.BASS_ChannelSetAttribute(System.Int32,Un4seen.Bass.BASSAttribute,System.Single)" />).</para>
        /// <para>The default setting is 0 (linear interpolation).</para>
        /// </summary>
        BASS_CONFIG_SRC_SAMPLE,

        /// <summary>
        /// Gets or Sets the buffer length for asynchronous file reading (default is 64KB).
        /// <para>length (int): The buffer length in bytes. This will be rounded up to the nearest 4096 byte (4KB) boundary.</para>
        /// <para>This determines the amount of file data that can be read ahead of time with asynchronous file reading. The default setting is 65536 bytes (64KB).</para>
        /// <para>Using this config option only affects channels that are created afterwards, not any that have already been created. So it is possible to have channels with differing buffer lengths by using this config option each time before creating them.</para>
        /// </summary>
        BASS_CONFIG_ASYNCFILE_BUFFER,

        /// <summary>
        /// Pre-scan chained OGG files?
        /// <para>prescan (bool): If <see langword="true" />, chained OGG files are pre-scanned.</para>
        /// <para>This option is enabled by default, and is equivalent to including the BASS_STREAM_PRESCAN flag in a <see cref="M:Un4seen.Bass.Bass.BASS_StreamCreateFile(System.String,System.Int64,System.Int64,Un4seen.Bass.BASSFlag)" /> call when opening an OGG file. It can be disabled if seeking and an accurate length reading are not required from chained OGG files, for faster stream creation.</para>
        /// </summary>
        BASS_CONFIG_OGG_PRESCAN = 47,

        /// <summary>
        /// Play audio from mp4 (video) files?
        /// <para>playmp4 (bool): If <see langword="true" /> (default) BASS will play the audio from mp4 video files (when using the Media Foundation). If <see langword="false" /> mp4 video files will not be played.</para>
        /// </summary>
        BASS_CONFIG_MF_VIDEO,

        /// <summary>
        /// BASS_AC3 add-on: dynamic range compression option
        /// <para>dynrng (bool): If <see langword="true" /> dynamic range compression is enbaled (default is <see langword="false" />).</para>
        /// </summary>
        BASS_CONFIG_AC3_DYNRNG = 65537,

        /// <summary>
        /// BASSWMA add-on: Prebuffer internet streams on creation, before returning from BASS_WMA_StreamCreateFile?
        /// <para>prebuf (bool): The Windows Media modules must prebuffer a stream before starting decoding/playback of it. This option determines when/where to wait for that to be completed.</para>
        /// <para>The Windows Media modules must prebuffer a stream before starting decoding/playback of it.
        /// This option determines whether the stream creation function (eg. <see cref="M:Un4seen.Bass.AddOn.Wma.BassWma.BASS_WMA_StreamCreateFile(System.String,System.Int64,System.Int64,Un4seen.Bass.BASSFlag)" />) will wait for the prebuffering to complete before returning.
        /// If playback of a stream is attempted before it has prebuffered, it will stall and then resume once it has finished prebuffering.
        /// The prebuffering progress can be monitored via <see cref="M:Un4seen.Bass.Bass.BASS_StreamGetFilePosition(System.Int32,Un4seen.Bass.BASSStreamFilePosition)" /> (BASS_FILEPOS_WMA_BUFFER).</para>
        /// <para>This option is enabled by default.</para>
        /// </summary>
        BASS_CONFIG_WMA_PREBUF = 65793,

        /// <summary>
        /// BASSWMA add-on: use BASS file handling.
        /// <para>bassfile (bool): Default is disabled (<see langword="false" />).</para>
        /// <para>When enabled (<see langword="true" />) BASSWMA uses BASS's file routines when playing local files. It uses the IStream interface to do that.
        /// This would also allow to support the "offset" parameter for WMA files with <see cref="M:Un4seen.Bass.Bass.BASS_StreamCreateFile(System.String,System.Int64,System.Int64,Un4seen.Bass.BASSFlag)" />.
        /// The downside of enabling this feature is, that it stops playback while encoding from working.</para>
        /// </summary>
        BASS_CONFIG_WMA_BASSFILE = 65795,

        /// <summary>
        /// BASSWMA add-on: enable network seeking?
        /// <para>seek (bool): If <see langword="true" /> seeking in network files/streams is enabled (default is <see langword="false" />).</para>
        /// <para>If <see langword="true" />, it allows seeking before the entire file has been downloaded/cached. Seeking is slow that way, so it's disabled by default.</para>
        /// </summary>
        BASS_CONFIG_WMA_NETSEEK,

        /// <summary>
        /// BASSWMA add-on: play audio from WMV (video) files?
        /// <para>playwmv (bool): If <see langword="true" /> (default) BASSWMA will play the audio from WMV video files. If <see langword="false" /> WMV files will not be played.</para>
        /// </summary>
        BASS_CONFIG_WMA_VIDEO,

        /// <summary>
        /// BASSWMA add-on: use a seperate thread to decode the data?
        /// <para>async (bool): If <see langword="true" /> BASSWMA will decode the data in a seperate thread. If <see langword="false" /> (default) the normal file system will be used.</para>
        /// <para>
        /// The WM decoder can by synchronous (decodes data on demand) or asynchronous (decodes in the background).
        /// With the background decoding, BASSWMA buffers the data that it receives from the decoder for the STREAMPROC to access.
        /// The start of playback/seeking may well be slightly delayed due to there being no data available immediately.
        /// Internet streams are only supported by the asynchronous system, but local files can use either, and BASSWMA uses the synchronous system by default.
        /// </para>
        /// </summary>
        BASS_CONFIG_WMA_ASYNC = 65807,

        /// <summary>
        /// BASSCD add-on: Automatically free an existing stream when creating a new one on the same drive?
        /// <para>freeold (bool): Only one stream can exist at a time per CD drive. So if a stream using the same drive already exists, stream creation function calls will fail, unless this config option is enabled to automatically free the existing stream. This is enabled by default.</para>
        /// </summary>
        BASS_CONFIG_CD_FREEOLD = 66048,

        /// <summary>
        /// BASSCD add-on: Number of times to retry after a read error.
        /// <para>retries (int): Number of times to retry reading...0 = don't retry. The default is 2 retries.</para>
        /// </summary>
        BASS_CONFIG_CD_RETRY,

        /// <summary>
        /// BASSCD add-on: Automatically reduce the read speed when a read error occurs?
        /// <para>autospd (bool): By default, this option is disabled.</para>
        /// <para>If <see langword="true" />, the read speed will be halved when a read error occurs, before retrying (if the BASS_CONFIG_CD_RETRY config setting allows).</para>
        /// </summary>
        BASS_CONFIG_CD_AUTOSPEED,

        /// <summary>
        /// BASSCD add-on: Skip past read errors?
        /// <para>skip (bool): If <see langword="true" />, reading will skip onto the next frame when a read error occurs, otherwise reading will stop.</para>
        /// <para>When skipping an error, it will be replaced with silence, so that the track length is unaffected. Before skipping past an error, BASSCD will first retry according to the <see cref="F:Un4seen.Bass.BASSConfig.BASS_CONFIG_CD_RETRY" /> setting.</para>
        /// </summary>
        BASS_CONFIG_CD_SKIPERROR,

        /// <summary>
        /// BASSCD add-on: The server to use in CDDB requests.
        /// <para>server (string): The CDDB server address, in the form of "user:pass@server:port/path". The "user:pass@", ":port" and "/path" parts are optional; only the "server" part is required. If not provided, the port and path default to 80 and "/~cddb/cddb.cgi", respectively.</para>
        /// <para>A copy is made of the provided server string, so it need not persist beyond the <see cref="M:Un4seen.Bass.Bass.BASS_SetConfigPtr(Un4seen.Bass.BASSConfig,System.IntPtr)" /> call. The default setting is "freedb.freedb.org". .</para>
        /// <para>The proxy server, as configured via the BASS_CONFIG_NET_PROXY option, is used when connecting to the CDDB server.</para>
        /// </summary>
        BASS_CONFIG_CD_CDDB_SERVER,

        /// <summary>
        /// BASSenc add-on: Encoder DSP priority (default -1000)
        /// <para>priority (int): The priorty determines where in the DSP chain the encoding is performed - all DSP with a higher priority will be present in the encoding. Changes only affect subsequent encodings, not those that have already been started. The default priority is -1000.</para>
        /// </summary>
        BASS_CONFIG_ENCODE_PRIORITY = 66304,

        /// <summary>
        /// BASSenc add-on: The maximum queue length (default 10000, 0=no limit)
        /// <para>limit (int): The async encoder queue size limit in milliseconds; 0=unlimited.</para>
        /// <para>When queued encoding is enabled, the queue's buffer will grow as needed to hold the queued data, up to a limit specified by this config option.
        /// The default limit is 10 seconds (10000 milliseconds). Changes only apply to new encoders, not any already existing encoders.</para>
        /// </summary>
        BASS_CONFIG_ENCODE_QUEUE,

        /// <summary>
        /// BASSenc add-on: ACM codec name to give priority for the formats it supports.
        /// <para>codec (string pointer): The ACM codec name to give priority (e.g. 'l3codecp.acm').</para>
        /// <para>BASSenc does make a copy of the config string, so it can be freed right after calling it.</para>
        /// </summary>
        BASS_CONFIG_ENCODE_ACM_LOAD,

        /// <summary>
        /// BASSenc add-on: The time to wait to send data to a cast server (default 5000ms)
        /// <para>timeout (int): The time to wait, in milliseconds.</para>
        /// <para>When an attempt to send data is timed-out, the data is discarded. <see cref="M:Un4seen.Bass.AddOn.Enc.BassEnc.BASS_Encode_SetNotify(System.Int32,Un4seen.Bass.AddOn.Enc.ENCODENOTIFYPROC,System.IntPtr)" /> can be used to receive a notification of when this happens.</para>
        /// <para>The default timeout is 5 seconds (5000 milliseconds). Changes take immediate effect.</para>
        /// </summary>
        BASS_CONFIG_ENCODE_CAST_TIMEOUT = 66320,

        /// <summary>
        /// BASSenc add-on: Proxy server settings when connecting to Icecast and Shoutcast (in the form of "[user:pass@]server:port"... <see langword="null" /> = don't use a proxy but a direct connection).
        /// <para>proxy (string pointer): The proxy server settings, in the form of "[user:pass@]server:port"... <see langword="null" /> = don't use a proxy but make a direct connection (default). If only the "server:port" part is specified, then that proxy server is used without any authorization credentials.</para>
        /// <para>BASSenc does not make a copy of the config string, so it must reside in the heap (not the stack), eg. a global variable. This also means that the proxy settings can subsequently be changed at that location without having to call this function again.</para>
        /// <para>Changes take effect from the next internet stream creation call. By default, BASSenc will not use any proxy settings when connecting to Icecast and Shoutcast.</para>
        /// </summary>
        BASS_CONFIG_ENCODE_CAST_PROXY,

        /// <summary>
        /// BASSMIDI add-on: Automatically compact all soundfonts following a configuration change?
        /// <para>compact (bool): If <see langword="true" />, all soundfonts are compacted following a MIDI stream being freed, or a <see cref="M:Un4seen.Bass.AddOn.Midi.BassMidi.BASS_MIDI_StreamSetFonts(System.Int32,Un4seen.Bass.AddOn.Midi.BASS_MIDI_FONT[],System.Int32)" /> call.</para>
        /// <para>The compacting isn't performed immediately upon a MIDI stream being freed or <see cref="M:Un4seen.Bass.AddOn.Midi.BassMidi.BASS_MIDI_StreamSetFonts(System.Int32,Un4seen.Bass.AddOn.Midi.BASS_MIDI_FONT[],System.Int32)" /> being called. It's actually done 2 seconds later (in another thread), so that if another MIDI stream starts using the soundfonts in the meantime, they aren't needlessly closed and reopened.</para>
        /// <para>Samples that have been preloaded by <see cref="M:Un4seen.Bass.AddOn.Midi.BassMidi.BASS_MIDI_FontLoad(System.Int32,System.Int32,System.Int32)" /> are not affected by automatic compacting. Other samples that have been preloaded by <see cref="M:Un4seen.Bass.AddOn.Midi.BassMidi.BASS_MIDI_StreamLoadSamples(System.Int32)" /> are affected though, so it is probably wise to disable this option when using that function.</para>
        /// <para>By default, this option is enabled.</para>
        /// </summary>
        BASS_CONFIG_MIDI_COMPACT = 66560,

        /// <summary>
        /// BASSMIDI add-on: The maximum number of samples to play at a time (polyphony).
        /// <para>voices (int): Maximum number of samples to play at a time... 1 (min) - 500 (max).</para>
        /// <para>This setting determines the maximum number of samples that can play together in a single MIDI stream. This isn't necessarily the same thing as the maximum number of notes, due to presets often layering multiple samples. When there are no voices available to play a new sample, the voice with the lowest volume will be killed to make way for it.</para>
        /// <para>The more voices that are used, the more CPU that is required. So this option can be used to restrict that, for example on a less powerful system. The CPU usage of a MIDI stream can also be restricted via the <see cref="F:Un4seen.Bass.BASSAttribute.BASS_ATTRIB_MIDI_CPU" /> attribute.</para>
        /// <para>Changing this setting only affects subsequently created MIDI streams, not any that have already been created. The default setting is 128 voices.</para>
        /// <para>Platform-specific</para>
        /// <para>The default setting is 100, except on iOS, where it is 40.</para>
        /// </summary>
        BASS_CONFIG_MIDI_VOICES,

        /// <summary>
        /// BASSMIDI add-on: Automatically load matching soundfonts?
        /// <para>autofont (bool): If <see langword="true" />, BASSMIDI will try to load a soundfont matching the MIDI file.</para>
        /// <para>This option only applies to local MIDI files, loaded using <see cref="M:Un4seen.Bass.AddOn.Midi.BassMidi.BASS_MIDI_StreamCreateFile(System.String,System.Int64,System.Int64,Un4seen.Bass.BASSFlag,System.Int32)" /> (or <see cref="M:Un4seen.Bass.Bass.BASS_StreamCreateFile(System.String,System.Int64,System.Int64,Un4seen.Bass.BASSFlag)" /> via the plugin system). BASSMIDI won't look for matching soundfonts for MIDI files loaded from the internet.</para>
        /// <para>By default, this option is enabled.</para>
        /// </summary>
        BASS_CONFIG_MIDI_AUTOFONT,

        /// <summary>
        /// BASSMIDI add-on: Default soundfont usage
        /// <para>filename (string): Filename of the default soundfont to use (<see langword="null" /> = no default soundfont).</para>
        /// <para>When setting the default soundfont, a copy is made of the filename, so it does not need to persist beyond the <see cref="M:Un4seen.Bass.Bass.BASS_SetConfigPtr(Un4seen.Bass.BASSConfig,System.IntPtr)" /> call. If the specified soundfont cannot be loaded, the default soundfont setting will remain as it is. <see cref="M:Un4seen.Bass.Bass.BASS_GetConfigPtr(Un4seen.Bass.BASSConfig)" /> can be used to confirm what that is.</para>
        /// <para>On Windows, the default is to use one of the Creative soundfonts (28MBGM.SF2 or CT8MGM.SF2 or CT4MGM.SF2 or CT2MGM.SF2), if present in the windows system directory.</para>
        /// </summary>
        BASS_CONFIG_MIDI_DEFFONT,

        /// <summary>
        /// BASSmix add-on: The order of filter used to reduce aliasing (only available/used pre BASSmix 2.4.7, where BASS_CONFIG_SRC is used).
        /// <para>order (int): The filter order... 2 (min) to 50 (max), and even. If the value specified is outside this range, it is automatically capped.</para>
        /// <para>The filter order determines how abruptly the level drops at the cutoff frequency, or the roll-off. The levels rolls off at 6 dB per octave for each order. For example, a 4th order filter will roll-off at 24 dB per octave. A low order filter may result in some aliasing persisting, and sounds close to the cutoff frequency being attenuated.
        /// Higher orders reduce those things, but require more processing.</para>
        /// <para>By default, a 4th order filter is used. Changes only affect channels that are subsequently plugged into a mixer, not those that are already plugged in.</para>
        /// </summary>
        BASS_CONFIG_MIXER_FILTER = 67072,

        /// <summary>
        /// BASSmix add-on: The source channel buffer size multiplier.
        /// <para>multiple (int): The buffer size multiplier... 1 (min) to 5 (max). If the value specified is outside this range, it is automatically capped.</para>
        /// <para>When a source channel has buffering enabled, the mixer will buffer the decoded data, so that it is available to the <see cref="M:Un4seen.Bass.AddOn.Mix.BassMix.BASS_Mixer_ChannelGetData(System.Int32,System.IntPtr,System.Int32)" /> and <see cref="M:Un4seen.Bass.AddOn.Mix.BassMix.BASS_Mixer_ChannelGetLevel(System.Int32)" /> functions.
        /// To reach the source channel's buffer size, the multiplier (multiple) is applied to the BASS_CONFIG_BUFFER setting at the time of the mixer's creation. If the source is played at it's default rate, then the buffer only need to be as big as the mixer's buffer.
        /// But if it's played at a faster rate, then the buffer needs to be bigger for it to contain the data that is currently being heard from the mixer. For example, playing a channel at 2x its normal speed would require the buffer to be 2x the normal size (multiple = 2).</para>
        /// <para>Larger buffers obviously require more memory, so the multiplier should not be set higher than necessary.</para>
        /// <para>The default multiplier is 2x. Changes only affect subsequently setup channel buffers. An existing channel can have its buffer reinitilized by disabling and then re-enabling the BASS_MIXER_BUFFER flag using <see cref="M:Un4seen.Bass.AddOn.Mix.BassMix.BASS_Mixer_ChannelFlags(System.Int32,Un4seen.Bass.BASSFlag,Un4seen.Bass.BASSFlag)" />.</para>
        /// </summary>
        BASS_CONFIG_MIXER_BUFFER,

        /// <summary>
        /// BASSmix add-on: How far back to keep record of source positions to make available for <see cref="M:Un4seen.Bass.AddOn.Mix.BassMix.BASS_Mixer_ChannelGetPositionEx(System.Int32,Un4seen.Bass.BASSMode,System.Int32)" />.
        /// <para>length (int): The length of time to back, in milliseconds.</para>
        /// <para>If a mixer is not a decoding channel (not using the <see cref="F:Un4seen.Bass.BASSFlag.BASS_STREAM_DECODE" /> flag), this config setting will just be a minimum and the mixer will always have a position record at least equal to its playback buffer length, as determined by the <see cref="F:Un4seen.Bass.BASSConfig.BASS_CONFIG_BUFFER" /> config option.</para>
        /// <para>The default setting is 2000ms. Changes only affect newly created mixers, not any that already exist.</para>
        /// </summary>
        BASS_CONFIG_MIXER_POSEX,

        /// <summary>
        /// BASSmix add-on: The splitter buffer length.
        /// <para>length (int): The buffer length in milliseconds... 100 (min) to 5000 (max). If the value specified is outside this range, it is automatically capped.</para>
        /// <para>When a source has its first splitter stream created, a buffer is allocated for its sample data, which all of its subsequently created splitter streams will share. This config option determines how big that buffer is. The default is 2000ms.</para>
        /// <para>The buffer will always be kept as empty as possible, so its size does not necessarily affect latency; it just determines how far splitter streams can drift apart before there are buffer overflow issues for those left behind.</para>
        /// <para>Changes do not affect buffers that have already been allocated; any sources that have already had splitter streams created will continue to use their existing buffers.</para>
        /// </summary>
        BASS_CONFIG_SPLIT_BUFFER = 67088,

        /// <summary>
        /// BASSaac add-on: play audio from mp4 (video) files?
        /// <para>playmp4 (bool): If <see langword="true" /> (default) BASSaac will play the audio from mp4 video files. If <see langword="false" /> mp4 video files will not be played.</para>
        /// </summary>
        BASS_CONFIG_MP4_VIDEO = 67328,

        /// <summary>
        /// BASSaac add-on: Support MP4 in BASS_AAC_StreamCreateXXX functions?
        /// <para>usemp4 (bool): If <see langword="true" /> BASSaac supports MP4 in the BASS_AAC_StreamCreateXXX functions. If <see langword="false" /> (default) only AAC is supported.</para>
        /// </summary>
        BASS_CONFIG_AAC_MP4,

        /// <summary>
        /// BASSWinamp add-on: Winamp input timeout.
        /// <para>timeout (int): The time (in milliseconds) to wait until timing out, because the plugin is not using the output system.</para>
        /// </summary>
        BASS_CONFIG_WINAMP_INPUT_TIMEOUT = 67584
    }
}