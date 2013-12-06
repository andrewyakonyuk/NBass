using System;
using System.Runtime.InteropServices;

namespace NBass.Backstage
{
    public static class ChannelNativeMethods
    {
        [DllImport("bass.dll", EntryPoint = "BASS_ChannelBytes2Seconds", CharSet = CharSet.Auto)]
        public static extern double BytesToSeconds(IntPtr handle, long pos);

        [DllImport("bass.dll", EntryPoint = "BASS_ChannelGet3DAttributes")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Get3DAttributes(IntPtr handle, ref int mode, ref float min, ref float max, ref int iangle, ref int oangle, ref int outvol);

        [DllImport("bass.dll", EntryPoint = "BASS_ChannelGet3DPosition", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Get3DPosition(IntPtr handle,
            out Vector3D pos,
            out Vector3D orient,
            out Vector3D vel);

        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint = "BASS_ChannelGetAttribute")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetAttribute(IntPtr handle, int attrib, ref float value);

        [DllImport("bass.dll", EntryPoint = "BASS_ChannelGetData", CharSet = CharSet.Auto)]
        public static extern int GetData(IntPtr handle, [Out] byte[] buffer, int Length);

        // Retrieves upto "length" bytes of the channel//s current sample data. This is
        // useful if you wish to "visualize" the sound.
        // handle:  Channel handle(HMUSIC / HSTREAM, or RECORDCHAN)
        // buffer : Location to write the data
        // length : Number of bytes wanted, or a BASS_DATA_xxx flag
        // RETURN : Number of bytes actually written to the buffer (-1=error)
        [DllImport("bass.dll", EntryPoint = "BASS_ChannelGetData", CharSet = CharSet.Auto)]
        public static extern int GetData(IntPtr handle, float[] buffer, int Length);

        [DllImport("bass.dll", EntryPoint = "BASS_ChannelGetData", CharSet = CharSet.Auto)]
        public static extern int GetData(IntPtr handle, [Out] short[] buffer, int Length);

        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint = "BASS_ChannelGetDevice")]
        public static extern int GetDevice(IntPtr handle);

        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint = "BASS_ChannelGetLength")]
        public static extern long GetLength(IntPtr handle, int mode);

        //
        // handle : channel handle(HMUSIC / HSTREAM, or RECORDCHAN)
        // RETURN : LOWORD=left level (0-128) HIWORD=right level (0-128) (-1=error)
        //          Use LoWord and HiWord functions on return function.
        [DllImport("bass.dll", EntryPoint = "BASS_ChannelGetLevel", CharSet = CharSet.Auto)]
        public static extern int GetLevel(IntPtr handle);

        // Get the current playback position of a channel.
        // handle : Channel handle (HCHANNEL/HMUSIC/HSTREAM, or CDCHANNEL)
        // RETURN : the position (-1=error)
        //          if HCHANNEL: position in bytes
        //          if HMUSIC: LOWORD=order HIWORD=row (use GetLoWord(position), GetHiWord(Position))
        //          if HSTREAM: total bytes played since the stream was last flushed
        //          if CDCHANNEL: position in milliseconds from start of track
        /// <summary>
        /// Use to override in derived classes
        /// </summary>
        [DllImport("bass.dll", EntryPoint = "BASS_ChannelGetPosition")]
        public static extern long GetPosition(IntPtr handle, int bassMode);

        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint = "BASS_ChannelGetTags")]
        public static extern IntPtr GetTags(IntPtr handle, int tags);

        //OK
        [DllImport("bass.dll", EntryPoint = "BASS_ChannelIsActive")]
        public static extern int IsActive(IntPtr handle);

        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint = "BASS_ChannelLock")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Lock(IntPtr handle, [MarshalAs(UnmanagedType.Bool)] bool state);

        [DllImport("bass.dll", EntryPoint = "BASS_ChannelPause")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Pause(IntPtr handle);

        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint = "BASS_ChannelPlay")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Play(IntPtr handle, [MarshalAs(UnmanagedType.Bool)] bool restart);

        // Remove a DSP function from a channel
        // handle : channel handle(HMUSIC / HSTREAM)
        // dsp    : Handle of DSP to remove */
        // RETURN : BASSTRUE / BASSFALSE
        [DllImport("bass.dll", EntryPoint = "BASS_ChannelRemoveDSP", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RemoveDSP(IntPtr handle, IntPtr dsp);

        [DllImport("bass.dll", EntryPoint = "BASS_ChannelRemoveFX", CharSet = CharSet.Auto)]
        public static extern int RemoveFX(IntPtr handle, IntPtr fx);

        [DllImport("bass.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RemoveLink(IntPtr handle, IntPtr chan);

        // Remove a sync from a channel
        // handle : channel handle(HMUSIC/HSTREAM)
        // sync   : Handle of sync to remove
        [DllImport("bass.dll", EntryPoint = "BASS_ChannelRemoveSync", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RemoveSync(IntPtr handle, IntPtr sync);

        [DllImport("bass.dll", EntryPoint = "BASS_ChannelSeconds2Bytes")]
        public static extern long SecondsToBytes(IntPtr handle, double pos);

        [DllImport("bass.dll", EntryPoint = "BASS_ChannelSet3DAttributes")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Set3DAttributes(IntPtr handle, int mode, float min, float max, int iangle, int oangle, int outvol);

        [DllImport("bass.dll", EntryPoint = "BASS_ChannelSet3DPosition", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Set3DPosition(IntPtr handle,
            ref Vector3D pos,
            ref Vector3D orient,
            ref Vector3D vel);

        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint = "BASS_ChannelSetAttribute")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetAttribute(IntPtr handle, int attrib, float value);

        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint = "BASS_ChannelSetDevice")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetDevice(IntPtr handle, int device);

        // Setup a user DSP function on a channel. When multiple DSP functions
        // are used on a channel, they are called in the order that they were added.
        // handle:  channel handle(HMUSIC / HSTREAM)
        // proc   : User defined callback function
        // user   : The //user// value passed to the callback function
        //priority : The priority of the new DSP, which determines it's position in the DSP chain - DSPs with higher priority are called before those with lower.
        // RETURN : DSP handle (NULL=error)
        [DllImport("bass.dll", EntryPoint = "BASS_ChannelSetDSP", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetDSP(IntPtr handle, DSPCallBack proc, IntPtr user, int priority);

        [DllImport("bass.dll", EntryPoint = "BASS_ChannelSetFX", CharSet = CharSet.Auto)]
        public static extern IntPtr SetFX(IntPtr handle, int type, int priority);

        [DllImport("bass.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetLink(IntPtr handle, IntPtr chan);

        // handle : Channel handle (HCHANNEL/HMUSIC/HSTREAM, or CDCHANNEL)
        // pos    : the position
        //          if HCHANNEL: position in bytes
        //          if HMUSIC: LOWORD=order HIWORD=row ... use MAKELONG(order,row)
        //          if HSTREAM: position in bytes, file streams only
        //          if CDCHANNEL: position in milliseconds from start of track
        /// <summary>
        /// Used to override in derived classes
        /// </summary>
        [DllImport("bass.dll", EntryPoint = "BASS_ChannelSetPosition")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetPosition(IntPtr handle, long pos, int bassMode);

        //OK return bool

        // Setup a sync on a channel. Multiple syncs may be used per channel.
        // handle : Channel handle (currently there are only HMUSIC/HSTREAM syncs)
        // atype  : Sync type (BASS_SYNC_xxx type & flags)
        // param  : Sync parameters (see the BASS_SYNC_xxx type description)
        // proc   : User defined callback function (use AddressOf SYNCPROC)
        // user   : The //user// value passed to the callback function
        // Return : Sync handle(Null = Error)
        [DllImport("bass.dll", EntryPoint = "BASS_ChannelSetSync", CharSet = CharSet.Auto)]
        public static extern IntPtr SetSync(IntPtr handle,
            int atype, long param, GetSyncCallBack proc, IntPtr user);

        [DllImport("bass.dll", EntryPoint = "BASS_ChannelStop")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Stop(IntPtr handle);

        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint = "BASS_ChannelGetInfo")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetInfo(IntPtr handle, [In] [Out] ref Data data);
    }
}