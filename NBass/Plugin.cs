using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NBass
{
    //todo implement plugin info

    public class Plugin : IDisposable
    {
        bool _isDisposed = false;

        internal IntPtr Handle { get; private set; }
        public string Name { get; private set; }
        public string Path { get; private set; }

        public Plugin(string path)
        {
            Path = path;
            Handle = IntPtr.Zero;
        }

        internal void Init()
        {
            int flags = 0;
            Handle = _Load(Path, flags);
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
            }

            if (Handle == IntPtr.Zero)
                throw new InvalidOperationException("Plugin don`t initialized");
            _Unload(Handle);
        }

        ~Plugin()
        {
            Dispose(false);
        }

        #endregion

        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint = "BASS_PluginLoad")]
        private static extern IntPtr _Load([MarshalAs(UnmanagedType.LPWStr)] [In] string path, int flags);

        [DllImport("bass.dll", CharSet = CharSet.Auto, EntryPoint = "BASS_PluginFree")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool _Unload(IntPtr handle);
    }
}
