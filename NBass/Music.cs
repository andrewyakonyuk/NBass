using System;
using System.Runtime.InteropServices;

namespace NBass
{
    /// <summary>
    /// Summary description for Music.
    /// </summary>
    public class Music : ChannelBase
    {
        private bool disposed = false;

        #region Construction / Desctruction
        public Music(IntPtr handle) : base(handle) { }

        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                try
                {
                    if (disposing)
                    {
                        // release any managed resources
                    }

                    // release any unmanaged resources
                    Free();

                    this.disposed = true;
                }
                finally
                {
                    base.Dispose(disposing);
                }
            }
        }
        #endregion

        //  Free a music//s resources. handle =  Music handle
        [DllImport("bass.dll", EntryPoint = "BASS_MusicFree")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool _Free(IntPtr handle); //OK point to HMUSIC 

        void Free()
        {
            _Free(base.Handle);
        }


    }
}
