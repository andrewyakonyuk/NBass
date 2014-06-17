using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NBass.Backstage
{
    internal static class FXNativeMethods
    {
        /// <summary>
        /// Set the parameters of a DX8 effect.
        /// </summary>
        /// <param name="handle">FX handle></param>
        /// <param name="fxparam">Pointer to the parameter structure</param>
        /// <returns></returns>
        [DllImport("bass.dll", EntryPoint = "BASS_FXSetParameters", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetParameters(IntPtr handle, IntPtr fxparam);

        /// <summary>
        /// Retrieve the parameters of a DX8 effect.
        /// </summary>
        /// <param name="handle">FX handle</param>
        /// <param name="fxparam">Pointer to the parameter structure</param>
        /// <returns></returns>
        [DllImport("bass.dll", EntryPoint = "BASS_FXGetParameters", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetParameters(IntPtr handle, IntPtr fxparam);
    }
}
