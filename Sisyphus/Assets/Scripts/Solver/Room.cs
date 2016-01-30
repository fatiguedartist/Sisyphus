using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sisyphus
{
    public class Room
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Depth { get; private set; }
        public int NumKeyLockPairs { get; private set; }
        public bool[,,] roomBuffer;
        public Int3 entryPoint;
        public Int3[] keys;
        public Int3[] locks;
        public Int3 door;

        const int MinDistBetweenTargets = 2;

        private int iter = 0;

        public Room(int width, int height, int depth, int numKeyLockPairs)
        {
            Width = width;
            Height = height;
            Depth = depth;
            NumKeyLockPairs = numKeyLockPairs;

            roomBuffer = new bool[width, height, depth];
            keys = new Int3[numKeyLockPairs];
            locks = new Int3[numKeyLockPairs];

            DefineEntryPoint(width, height);
            DefineDoor(width, height, depth);
        }

        private void DefineEntryPoint(int width, int height)
        {
            var entryLateralPoint = Random.Range(0, width);
            var entryVerticalPoint = Random.Range(0, height);
            entryPoint = new Int3(entryLateralPoint, entryVerticalPoint, 0);
        }

        private void DefineDoor(int width, int height, int depth)
        {
            var lateralPoint = Random.Range(0, width);
            var verticalPoint = Random.Range(0, height);
            var depthPoint = Random.Range(MinDistBetweenTargets, depth);
            door = new Int3(lateralPoint, verticalPoint, depthPoint);
        }
    }
}
