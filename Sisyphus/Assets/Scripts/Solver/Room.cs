using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sisyphus
{
    public class Room
    {
        bool[,,] roomBuffer;

        public Room(int width, int height, int depth)
        {
            roomBuffer = new bool[width, height, depth];
        }
    }
}
