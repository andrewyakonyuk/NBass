using System;

namespace NBass.Backstage
{
    internal delegate void DSPCallBack(IntPtr handle, int channel, int buffer, int Length, int user);

    internal delegate void GetSyncCallBack(IntPtr handle, IntPtr channel, int data, int user);
}