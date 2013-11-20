using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NBass.Backstage
{
    public static class _Stream
    {
        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint="BASS_StreamFree")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Free(IntPtr handle);
    }
}
