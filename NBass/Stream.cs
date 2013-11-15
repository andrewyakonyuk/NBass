using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBass.Backstage;
using NBass.Declaration;

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
            _Stream.Free(base.Handle);
            base.Dispose(disposing);
        }
    }
}
