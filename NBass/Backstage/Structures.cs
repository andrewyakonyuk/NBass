using System;
using System.Runtime.InteropServices;

namespace NBass.Backstage
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct Data
    {
        public int a;
        public int b;
        public int c;
        public int d;
        public int e;
        public int f;
        public int g;
        public IntPtr h;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct BASSInfo
    {
        private int size; //size of this struct (set this before calling the function)
        private int flags; //device capabilities (DSCAPS_xxx flags)
        private int hwsize; //size of total device hardware memory
        private int hwfree; //size of free device hardware memory
        private int freesam; //number of free sample slots in the hardware
        private int free3d; //number of free 3D sample slots in the hardware
        private int minrate; //min sample rate supported by the hardware
        private int maxrate; //max sample rate supported by the hardware
        private int eax; //device supports EAX? (always BASSFALSE if BASS_DEVICE_3D was not used) realy bool
        private int a3d; //unused
        private int dsver; //DirectSound version (use to check for DX5/7 functions)
        private int latency; //delay (in ms) before start of playback (requires BASS_DEVICE_LATENCY)

        //		public BASSInfo(bool dumb)
        //		{
        //			this.size = Marshal.SizeOf(typeof(BASSInfo));
        //		}

        public int MaxSecondarySampleRate
        {
            get { return this.maxrate; }
        }

        public int MinSecondarySampleRate
        {
            get { return this.minrate; }
        }

        public int TotalHardwareMemBytes
        {
            get { return this.hwsize; }
        }

        public int FreeHardware3DChannels
        {
            get { return this.free3d; }
        }

        public int FreeHardwareMixingAllChannels
        {
            get { return this.freesam; }
        }

        public int FreeHardwareMemBytes
        {
            get { return this.hwfree; }
        }

        public int Size { set { this.size = value; } }

        #region DeviceCaps

        public string DeviceCapabilities
        {
            get { return Helper.PrintFlags((DeviceCaps)this.flags); }
        }

        public bool Certified
        {
            get { return Helper.Int2Bool((this.flags >> 6 & 1)); }
        }

        public bool ContinuousRate
        {
            get { return Helper.Int2Bool((this.flags >> 4 & 1)); }
        }

        public bool EmulateDriver
        {
            get { return Helper.Int2Bool((this.flags >> 5 & 1)); }
        }

        public bool Primary16Bit
        {
            get { return Helper.Int2Bool((this.flags >> 3 & 1)); }
        }

        public bool Primary8Bit
        {
            get { return Helper.Int2Bool((this.flags >> 2 & 1)); }
        }

        public bool PrimaryMono
        {
            get { return Helper.Int2Bool((this.flags & 1)); }
        }

        public bool PrimaryStereo
        {
            get { return Helper.Int2Bool((this.flags >> 1 & 1)); }
        }

        public bool Secondary16Bit
        {
            get { return Helper.Int2Bool((this.flags >> 11 & 1)); }
        }

        public bool Secondary8Bit
        {
            get { return Helper.Int2Bool((this.flags >> 10 & 1)); }
        }

        public bool SecondaryMono
        {
            get { return Helper.Int2Bool((this.flags >> 8 & 1)); }
        }

        public bool SecondaryStereo
        {
            get { return Helper.Int2Bool((this.flags >> 9 & 1)); }
        }

        public bool EAXSupport
        {
            get { return Helper.Int2Bool(this.eax); }
        }

        public int DXVersion
        {
            get { return this.dsver; }
        }

        public int Latency
        {
            get { return this.latency; }
        }

        #endregion DeviceCaps
    }
}