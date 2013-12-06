using System.Runtime.InteropServices;

namespace NBass.Backstage
{
    internal class BassExceptionNativeMethods
    {
        /// <summary>
        /// Get the BASS_ERROR_xxx error code. Use this function to get the reason for an error.
        /// </summary>
        /// <returns></returns>
        [DllImport("bass.dll", EntryPoint = "BASS_ErrorGetCode")]
        public static extern int GetErrorCode();
    }
}