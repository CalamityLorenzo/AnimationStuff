using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAnimationStuff.Animation
{

    [Flags]
    enum Directions
    {
        None = 0,
        Up = 1,
        Down = 2,
        Left = 4,
        Right = 8
    }
}
