using System.Runtime.InteropServices;

namespace NBass.Backstage
{
    public class BassExceptionNativeMethods
    {
        // Get the BASS_ERROR_xxx error code. Use this function to get the reason for an error.
        [DllImport("bass.dll", EntryPoint = "BASS_ErrorGetCode")]
        public static extern int GetErrorCode();
    }
}