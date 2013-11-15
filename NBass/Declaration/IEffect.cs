using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBass.Declaration
{
    public interface IEffect
    {
		IntPtr Hangle{ get; }
		int Type{ get; }
    }
}
