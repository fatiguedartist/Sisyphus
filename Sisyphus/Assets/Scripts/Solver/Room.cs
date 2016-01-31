using System;
using System.Collections.Generic;
using System.Linq;
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
        public Sides[,,] roomBuffer;
        public Int3 entryPoint;
        public Int3[] keys;
        public Int3[] locks;
        public List<Int3> solutionPath;
        public List<List<Int3>> divergentPaths;
        public Int3 door;

        private int iter = 0;

        public Room(int width, int height, int depth, int numKeyLockPairs)
        {
            Width = width;
            Height = height;
            Depth = depth;
            NumKeyLockPairs = numKeyLockPairs;

            roomBuffer = new Sides[width, height, depth];
            keys = new Int3[numKeyLockPairs];
            locks = new Int3[numKeyLockPairs];
            divergentPaths = new List<List<Int3>>();

            DefineEntryPoint(width, height);
        }

        private void DefineEntryPoint(int width, int height)
        {
            var entryLateralPoint = Random.Range(0, width);
            var entryVerticalPoint = Random.Range(0, height);
            entryPoint = new Int3(entryLateralPoint, entryVerticalPoint, 0);
        }

        public void CopyInto(Room room)
        {
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    for (var z = 0; z < Depth; z++)
                    {
                        room.roomBuffer[x, y, z] = roomBuffer[x, y, z];
                    }
                }
            }

            for (var i = 0; i < NumKeyLockPairs; i++)
            {
                room.keys[i] = keys[i];
                room.locks[i] = locks[i];
            }

            room.entryPoint = entryPoint;
            room.door = door;
            room.solutionPath = new List<Int3>(solutionPath);
            room.divergentPaths = new List<List<Int3>>(divergentPaths);
        }

        public Room Clone()
        {
            var clone = new Room(Width, Height, Depth, NumKeyLockPairs);
            CopyInto(clone);
            return clone;
        }
    }
}
