using System;

namespace NBass.Declarations
{
    public interface IEffect
    {
		IntPtr Hangle{ get; }
		int Type{ get; }
    }
}
