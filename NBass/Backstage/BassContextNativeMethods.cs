using System;
using System.Runtime.InteropServices;

namespace NBass.Backstage
{
    public static class BassContextNativeMethods
    {
        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint = "BASS_Free")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Free();

        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint = "BASS_Init")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Init(IntPtr device, int frequency, int flags, IntPtr win, IntPtr clsid);

        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint = "BASS_StreamCreateFile")]
        public static extern int LoadFromFile([MarshalAs(UnmanagedType.Bool)] bool inMemory, [MarshalAs(UnmanagedType.LPWStr)] [In] string filePath, long position, long lenght, int flags);

        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint = "BASS_Start")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Start();

        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint = "BASS_Stop")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Stop();

        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint = "BASS_StreamCreateFile")]
        public static extern int LoadStream([MarshalAs(UnmanagedType.Bool)] bool inMemory, [MarshalAs(UnmanagedType.LPWStr)] [In] string file, long position, long length, int flags);

        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint = "BASS_StreamCreateURL")]
        public static extern int LoadStreamFromUrl([MarshalAs(UnmanagedType.LPWStr)] [In] string url, int offset, int flags, StreamCallback callback, IntPtr user);

        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint = "BASS_SetEAXParameters")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetEAXParameters(EAXEnvironment environment, float volume, float decay, float damp);

        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint = "BASS_GetEAXParameters")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetEAXParameters(ref EAXEnvironment environment, ref float volume, ref float decay, ref float damp);

        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint = "BASS_GetConfig")]
        public static extern int GetConfig(int option);

        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint="BASS_SetConfig")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetConfig(int option, int value);
    }
}