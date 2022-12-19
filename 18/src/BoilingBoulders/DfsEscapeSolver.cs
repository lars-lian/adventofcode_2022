using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace BoilingBoulders
{
    public static class DfsEscapeSolver
    {
        public static bool CanEscape(HashSet<Vector3> cubes, Vector3 start)
        {
            var boundsMax = new Vector3(cubes.Max(c => c.X), cubes.Max(c => c.Y), cubes.Max(c => c.Z));
            var boundsMin = new Vector3(cubes.Min(c => c.X), cubes.Min(c => c.Y), cubes.Min(c => c.Z));

            Stack<Vector3> stack = new();
            stack.Push(start);

            List<Vector3> visited = new() { start };

            while (stack.Count > 0)
            {
                Vector3 current = stack.Pop();

                if (IsOutOfBounds(current, boundsMax, boundsMin))
                    return true;

                // For each possible movement
                foreach (Vector3 movement in SurfaceAreaCalculator.AdjacentPositions)
                {
                    // Calculate the next position
                    Vector3 next = current + movement;

                    // Check if move is valid
                    if (cubes.Contains(next))
                        continue;

                    // If the next position is valid and has not been visited
                    if (!visited.Contains(next))
                    {
                        // Add the next position to the stack and visited set
                        stack.Push(next);
                        visited.Add(next);
                    }
                }
            }

            return false;
        }

        private static bool IsOutOfBounds(Vector3 cube, Vector3 max, Vector3 min)
        {
            return
               cube.X > max.X || cube.X < min.X ||
               cube.Y > max.Y || cube.Y < min.Y ||
               cube.Z > max.Z || cube.Z < min.Z;
        }
    }
}
