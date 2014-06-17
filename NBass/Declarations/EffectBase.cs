using System;
using System.Runtime.InteropServices;
using NBass.Backstage;

namespace NBass.Declarations
{
    public abstract class EffectBase
    {
        public IntPtr Handle { get; protected set; }
        //public int Type { get; protected set; }
        public void Initialize(IChannel channel)
        {

        }
        protected abstract void Initialize(IntPtr handle);

        protected void EnsureGetEffect()
        {
            IntPtr alloc = Marshal.AllocHGlobal(Marshal.SizeOf(this));

            Marshal.StructureToPtr(this, alloc, true);

            if (!FXNativeMethods.GetParameters(this.Handle, alloc))
                throw new BassException();

            Marshal.PtrToStructure(alloc, this); // ????
            Marshal.FreeHGlobal(alloc);
        }

        protected void EnsureSetEffect()
        {
            IntPtr paramptr = Marshal.AllocHGlobal(Marshal.SizeOf(this));
            Marshal.StructureToPtr(this, paramptr, true);
            if (!FXNativeMethods.SetParameters(this.Handle, paramptr))
            {
                Marshal.FreeHGlobal(paramptr);
                throw new BassException();
            }
            else Marshal.FreeHGlobal(paramptr);
        }

        public object Parameters
        {
            get
            {
                object param = null;
                //switch (this.FXType)
                //{
                //    case ChannelFX.Chorus:
                //        param = new FXCHORUS();
                //        break;
                //    case ChannelFX.Compressor:
                //        param = new FXCOMPRESSOR();
                //        break;
                //    case ChannelFX.Distortion:
                //        param = new FXDISTORTION();
                //        break;
                //    case ChannelFX.Echo:
                //        param = new FXECHO();
                //        break;
                //    case ChannelFX.Flanger:
                //        param = new FXFLANGER();
                //        break;
                //    case ChannelFX.Gargle:
                //        param = new FXGARGLE();
                //        break;
                //    case ChannelFX.I3DL2Reverb:
                //        param = new FXI3DL2REVERB();
                //        break;
                //    case ChannelFX.ParametricEQ:
                //        param = new FXPARAMEQ();
                //        break;
                //    case ChannelFX.Reverb:
                //        param = new FXREVERB();
                //        break;
                //}
                
                return param;
            }
            set
            {
                
            }
        }
    }
}
