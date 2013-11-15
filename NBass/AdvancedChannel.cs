using System;
using System.Runtime.InteropServices;
using NBass.Backstage;

namespace NBass
{
    /// <summary>
    /// AdvancedChannel this class is not directly used
    /// </summary>
    [Obsolete]
    public abstract class AdvancedChannel : ChannelBase
    {
        private bool disposed = false;
        private IntPtr HSYNC;
        private GetSyncCallBack getSync;
        private EventHandler streamendstore;

        public AdvancedChannel(IntPtr handle)
            : base(handle)
        {
        }

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

                    this.disposed = true;
                }
                finally
                {
                    base.Dispose(disposing);
                }
            }
        }

        #region Other Common

        /// <summary>
        /// Retrieves upto "length" bytes of the channel//s current sample data. This is
        /// useful if you wish to "visualize" the sound.
        /// </summary>
        /// <param name="buffer">A buffer to place retrieved data</param>
        /// <param name="flags">ChannelDataFlags</param>
        public int GetData(float[] buffer, ChannelDataFlags flags)
        {
            if (this.disposed)
                throw new ObjectDisposedException(this.ToString());

            int output = _Channel.GetData(base.Handle, buffer, (int)flags);
            if (output < 0) throw new BassException();
            return output;
        }

        private static int GetDataLength(ChannelDataFlags flags)
        {
            switch (flags)
            {
                case ChannelDataFlags.FFT512:
                    return 256;

                case ChannelDataFlags.FFT1024:
                    return 512;

                case ChannelDataFlags.FFT2048:
                    return 1024;

                case ChannelDataFlags.SFFT512:
                    return 512;

                case ChannelDataFlags.SFFT1024:
                    return 1024;

                case ChannelDataFlags.SFFT2048:
                    return 2048;

                default:
                    return 1024;
            }
        }

        /// <summary>
        /// Retrieves upto "length" bytes of the channel//s current sample data. 16-bit data
        /// </summary>
        /// <param name="buffer">A buffer to place retrieved data</param>
        /// <param name="length">length in bytes</param>
        public int GetData(short[] buffer, int length)
        {
            if (this.disposed)
                throw new ObjectDisposedException(this.ToString());

            int output = _Channel.GetData(base.Handle, buffer, length);
            if (output < 0) throw new BassException();
            return output;
        }

        /// <summary>
        /// Setup a DX8 effect on a channel. Can only be used when the channel
        /// is not playing. Use FX.Parameters to set the effect parameters.
        /// Obviously requires DX8.
        /// </summary>
        /// <param name="chanfx">ChannelFX</param>
        /// <returns>An FX object</returns>
        public FX SetFX(ChannelFX chanfx)
        {
            if (this.disposed)
                throw new ObjectDisposedException(this.ToString());

            IntPtr fx = _Channel.SetFX(base.Handle, (int)chanfx, 1);
            if (fx == IntPtr.Zero) throw new BassException();
            return new FX(fx, chanfx);
        }

        /// <summary>
        /// Remove a DX8 effect from a channel. Can only be used when the
        /// channel is not playing.
        /// </summary>
        /// <param name="fx">The FX object to remove</param>
        public void RemoveFX(FX fx)
        {
            if (this.disposed)
                throw new ObjectDisposedException(this.ToString());

            if (_Channel.RemoveFX(base.Handle, fx.Handle) == 0)
                throw new BassException();
        }

        /// <summary>
        /// Retrieves upto "length" bytes of the channel//s current sample data. 8-bit data
        /// </summary>
        /// <param name="buffer">A buffer to place retrieved data</param>
        /// <param name="length">length in bytes</param>
        public int GetData(byte[] buffer, int length)
        {
            if (this.disposed)
                throw new ObjectDisposedException(this.ToString());

            int output = _Channel.GetData(base.Handle, buffer, length);
            if (output < 0) throw new BassException();
            return output;
        }

        #endregion Other Common
    }
}