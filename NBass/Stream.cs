using System;
using NBass.Backstage;
using NBass.Declarations;

namespace NBass
{
    public class Stream : ChannelBase, IStream
    {
        public Stream(IntPtr handle)
            : base(handle)
        {

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //free manage resource
            }
            StreamNativeMethods.Free(base.Handle);
            base.Dispose(disposing);
        }
    }
}
