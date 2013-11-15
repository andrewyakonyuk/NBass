using System;
using System.Runtime.InteropServices;
namespace NBass.Backstage
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct Data
    {
        public int a;
        public int b;
        public int c;
        public int d;
        public int e;
        public int f;
        public int g;
        public IntPtr h;
    }
}