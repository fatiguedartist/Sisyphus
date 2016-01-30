using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Sisyphus
{
    public struct Int3
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Int3(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3 ToV3()
        {
            return new Vector3(X, Y, Z);
        }
    }
}
