using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sisyphus
{
    public class Solver
    {
        public static void GenerateSolutionPath(Room room)
        {
            while (true)
            {
                try
                {
                    Walk(room);
                    return;
                }
                catch (InvalidSolveException)
                {
                }
            }
        }

        private static void Walk(Room room)
        {
            var entryPoint = room.entryPoint;
            Sides currentSide = Sides.Bottom;
            room.roomBuffer.Assign(x => Sides.None);
            room.roomBuffer[entryPoint.X, entryPoint.Y, entryPoint.Z] = currentSide;

            const double MinDimensionalTraversal = 0.5;
            var minPathLength =
                (int)
                    (room.Width*MinDimensionalTraversal*room.Height*MinDimensionalTraversal*room.Depth*
                     MinDimensionalTraversal);

            var currentLocation = entryPoint;
            var intendedLocation = entryPoint;

            var possibleSides = Enum.GetValues(typeof (Sides)).Cast<Sides>().ToArray();
            var possibleDirections = Enum.GetValues(typeof (Directions)).Cast<Directions>().Where(d => d != Directions.None && d != Directions.NUM_VALUES).ToArray();

            var solutionPath = new List<Int3> {currentLocation};
            room.roomBuffer[currentLocation.X, currentLocation.Y, currentLocation.Z] |= currentSide;

            for (var i = 0; i < minPathLength; i++)
            {
                currentSide = room.roomBuffer[currentLocation.X, currentLocation.Y, currentLocation.Z];

                if (UnityEngine.Random.Range(0, 2) == 0)
                {
                    var side = currentSide;
                    currentSide |= possibleSides.Where(s =>
                    {
                        return (s & side) == 0 &&
                               (s == Sides.Left && (side & Sides.Right) == 0
                                || s == Sides.Right && (side & Sides.Left) == 0
                                || s == Sides.Top && (side & Sides.Bottom) == 0
                                || s == Sides.Bottom && (side & Sides.Top) == 0
                                || s == Sides.Front && (side & Sides.Rear) == 0
                                || s == Sides.Rear && (side & Sides.Front) == 0);

                    }).SelectRandom();

                    room.roomBuffer[currentLocation.X, currentLocation.Y, currentLocation.Z] |= currentSide;
                }

                intendedLocation = DetermineNextLocation(room, currentLocation, possibleDirections);

                room.roomBuffer[intendedLocation.X, intendedLocation.Y, intendedLocation.Z] |= currentSide;
                currentLocation = intendedLocation;
                solutionPath.Add(currentLocation);

                if (i == minPathLength - 1)
                {
                    room.door = currentLocation;
                    room.solutionPath = solutionPath.ToList();
                }
            }
        }

        private static Int3 DetermineNextLocation(Room room, Int3 currentLocation, Directions[] possibleDirections)
        {
            Directions invalidDirections = Directions.None;
            var foundValidDirection = false;
            var intendedLocation = currentLocation;
            var currentSide = room.roomBuffer[currentLocation.X, currentLocation.Y, currentLocation.Z];

            if ((currentSide & Sides.Left) > 0)
            {
                invalidDirections |= Directions.Left;
                invalidDirections |= Directions.Right;
            }
            if ((currentSide & Sides.Right) > 0)
            {
                invalidDirections |= Directions.Left;
                invalidDirections |= Directions.Right;
            }
            if ((currentSide & Sides.Top) > 0)
            {
                invalidDirections |= Directions.Up;
                invalidDirections |= Directions.Down;
            }
            if ((currentSide & Sides.Bottom) > 0)
            {
                invalidDirections |= Directions.Up;
                invalidDirections |= Directions.Down;
            }
            if ((currentSide & Sides.Front) > 0)
            {
                invalidDirections |= Directions.Back;
                invalidDirections |= Directions.Forward;
            }
            if ((currentSide & Sides.Rear) > 0)
            {
                invalidDirections |= Directions.Back;
                invalidDirections |= Directions.Forward;
            }

            while (!foundValidDirection)
            {
                intendedLocation = currentLocation;

                if (invalidDirections >= Directions.NUM_VALUES - 1)
                {
                    throw new InvalidSolveException();
                }

                Directions direction;
                do
                {
                    direction = possibleDirections.SelectRandom();
                } while ((invalidDirections & direction) > 0);

                switch (direction)
                {
                    case Directions.Forward:
                        intendedLocation.Z += 1;
                        if (intendedLocation.Z == room.Depth)
                        {
                            invalidDirections |= direction;
                        }
                        else
                        {
                            if (room.roomBuffer[intendedLocation.X, intendedLocation.Y, intendedLocation.Z] !=
                                Sides.None)
                            {
                                invalidDirections |= direction;
                            }
                            else
                            {
                                foundValidDirection = true;
                            }
                        }
                        break;

                    case Directions.Back:
                        intendedLocation.Z -= 1;
                        if (intendedLocation.Z < 0)
                        {
                            invalidDirections |= direction;
                        }
                        else
                        {
                            if (room.roomBuffer[intendedLocation.X, intendedLocation.Y, intendedLocation.Z] !=
                                Sides.None)
                            {
                                invalidDirections |= direction;
                            }
                            else
                            {
                                foundValidDirection = true;
                            }
                        }
                        break;

                    case Directions.Up:
                        intendedLocation.Y += 1;
                        if (intendedLocation.Y == room.Height)
                        {
                            invalidDirections |= direction;
                        }
                        else
                        {
                            if (room.roomBuffer[intendedLocation.X, intendedLocation.Y, intendedLocation.Z] !=
                                Sides.None)
                            {
                                invalidDirections |= direction;
                            }
                            else
                            {
                                foundValidDirection = true;
                            }
                        }
                        break;

                    case Directions.Down:
                        intendedLocation.Y -= 1;
                        if (intendedLocation.Y < 0)
                        {
                            invalidDirections |= direction;
                        }
                        else
                        {
                            if (room.roomBuffer[intendedLocation.X, intendedLocation.Y, intendedLocation.Z] !=
                                Sides.None)
                            {
                                invalidDirections |= direction;
                            }
                            else
                            {
                                foundValidDirection = true;
                            }
                        }
                        break;

                    case Directions.Left:
                        intendedLocation.X -= 1;
                        if (intendedLocation.X < 0)
                        {
                            invalidDirections |= direction;
                        }
                        else
                        {
                            if (room.roomBuffer[intendedLocation.X, intendedLocation.Y, intendedLocation.Z] !=
                                Sides.None)
                            {
                                invalidDirections |= direction;
                            }
                            else
                            {
                                foundValidDirection = true;
                            }
                        }
                        break;

                    case Directions.Right:
                        intendedLocation.X += 1;
                        if (intendedLocation.X == room.Width)
                        {
                            invalidDirections |= direction;
                        }
                        else
                        {
                            if (room.roomBuffer[intendedLocation.X, intendedLocation.Y, intendedLocation.Z] !=
                                Sides.None)
                            {
                                invalidDirections |= direction;
                            }
                            else
                            {
                                foundValidDirection = true;
                            }
                        }
                        break;
                }
            }

            return intendedLocation;
        }

        public class InvalidSolveException : Exception
        {
            public InvalidSolveException() : base("This path is unsolvable")
            {
            }
        }
    }
}