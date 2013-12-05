using System;
using System.Runtime.InteropServices;

namespace NBass.Backstage
{
    public static class _Stream
    {
        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint="BASS_StreamFree")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Free(IntPtr handle);
    }
}
