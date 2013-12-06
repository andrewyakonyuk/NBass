using System;
using System.Runtime.InteropServices;

namespace NBass.Backstage
{
    internal static class StreamNativeMethods
    {
        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint = "BASS_StreamFree")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Free(IntPtr handle);
    }
}