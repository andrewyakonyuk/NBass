using System;
using System.Runtime.InteropServices;

namespace NBass
{
    public delegate int GetRecordCallBack(IntPtr pbuffer, int length, int user);

    /// <summary>
    /// Used for updating progress, just passes the channelbase derived object
    /// </summary>
    public delegate void ProgessHandler(ChannelBase channel);

    /// <summary>
    /// User defined callback function to process recorded sample data.
    /// </summary>
    /// <param name="handle">The recording handle that the data is from.</param>
    /// <param name="buffer">The pointer to the buffer containing the recorded sample data. The sample data is in standard Windows PCM format, that is 8-bit samples are unsigned, 16-bit samples are signed, 32-bit floating-point samples range from -1 to +1.</param>
    /// <param name="length">The number of bytes in the buffer.</param>
    /// <param name="user">The user instance data</param>
    /// <returns>Return <see langword="true" /> to stop recording, and anything else to continue recording.</returns>
    [return: MarshalAs(UnmanagedType.Bool)]
    public delegate bool RecordCallback(int handle, IntPtr buffer, int length, IntPtr user);

    public delegate void StreamCallback(IntPtr buffer, int length, IntPtr user);

}