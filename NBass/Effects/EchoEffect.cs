using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using NBass.Declarations;

namespace NBass.Effects
{
    public class EchoEffect : EffectBase
    {
        protected override void Initialize(IntPtr handle)
        {
            throw new NotImplementedException();
        }


    }

    [StructLayout(LayoutKind.Sequential)]
    public class FXECHO
    { // DSFXEcho
        public float fWetDryMix;
        public float fFeedback;
        public float fLeftDelay;
        public float fRightDelay;
        public int lPanDelay;
    }
}
