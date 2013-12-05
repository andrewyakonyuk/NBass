using System;

namespace NBass
{
    public delegate int GetRecordCallBack(IntPtr pbuffer, int length, int user);

    /// <summary>
    /// Used for updating progress, just passes the channelbase derived object
    /// </summary>
    public delegate void ProgessHandler(ChannelBase channel);

    //public delegate bool RecordCallback(byte[] buffer, int length, int user);

    //public delegate bool RecordCallback(short[] buffer, int length, int user);

    public delegate void StreamCallback(IntPtr buffer, int length, IntPtr user);

}