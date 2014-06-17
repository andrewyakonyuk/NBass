using System;
using System.Runtime.InteropServices;

namespace NBass.Backstage
{
    internal static class RecordNativeMethods
    {
        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint = "BASS_RecordInit")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Init(int device);

        /// <summary>
        /// Start recording
        /// </summary>
        /// <param name="frequency">The sample rate to record at.</param>
        /// <param name="channels">The number of channels... 1 = mono, 2 = stereo, etc.</param>
        /// <param name="flags">Any combination of these flags (see <see cref="T:NBass.RecordFlags"/></param>
        /// <param name="callback">The user defined function to receive the recorded sample data... can be <see langword="null" /> if you do not wish to use a callback.</param>
        /// <param name="user">User instance data to pass to the callback function.</param>
        /// <returns>If successful, the new recording's handle is returned, else <see langword="false" /> is returned.</returns>
        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint="BASS_RecordStart")]
        public static extern int Start(int frequency, int channels, RecordFlags flags, RecordCallback callback, IntPtr user);

        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint = "BASS_RecordGetInfo")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetInfo([In] [Out] RecordInfo info);

        /// <summary>
        /// Frees all resources used by the recording device.
        /// </summary>
        /// <returns>If successful, then <see langword="true" /> is returned, else <see langword="false" /> is returned. </returns>
        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint = "BASS_RecordFree")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Free();
    }
}
