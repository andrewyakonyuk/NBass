using System;

namespace NBass.Backstage
{
    public delegate void DSPCallBack(IntPtr handle, int channel, int buffer, int Length, int user);

    public delegate void GetSyncCallBack(IntPtr handle, IntPtr channel, int data, int user);
}