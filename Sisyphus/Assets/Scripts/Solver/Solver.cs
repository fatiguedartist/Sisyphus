using System;
using UnityEngine;

namespace Sisyphus
{
    public class Solver
    {
        public static void GenerateSolutionPath(Room room)
        {
            room.roomBuffer.Assign(b => true);

            OpenEntryAndExit(room);
            Walk(room);
        }

        private static void OpenEntryAndExit(Room room)
        {
            var entryPoint = room.entryPoint;
            var door = room.door;

            room.roomBuffer[entryPoint.X, entryPoint.Y, entryPoint.Z] = false;
            room.roomBuffer[door.X, door.Y, door.Z] = false;
        }

        private static void Walk(Room room)
        {
            var entryPoint = room.entryPoint;
            var door = room.door;

            const double MinDimensionalTraversal = 0.5;
            var minPathLength = (int)(room.Width * MinDimensionalTraversal * room.Height * MinDimensionalTraversal * room.Depth * MinDimensionalTraversal);
        }
    }
}
