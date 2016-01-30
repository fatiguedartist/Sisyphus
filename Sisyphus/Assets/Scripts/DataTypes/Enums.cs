using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sisyphus
{
    [Flags]
    public enum Sides
    {
        None = 0,
        Top = 1,
        Bottom = 2,
        Left = 4,
        Right = 8,
        Front = 16,
        Rear = 32
    }

    [Flags]
    public enum Directions
    {
        None = 0,
        Left = 1,
        Right = 2,
        Forward = 4,
        Back = 8,
        Up = 16,
        Down = 32,
        NUM_VALUES = 64
    }

    public enum Axis
    {
        X,
        Y,
        Z
    }
}
