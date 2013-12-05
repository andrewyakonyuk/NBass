using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBass.Declarations
{
    public interface IEffect
    {
		IntPtr Hangle{ get; }
		int Type{ get; }
    }
}
