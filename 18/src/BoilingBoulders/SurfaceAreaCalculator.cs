using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace BoilingBoulders
{
    public static class SurfaceAreaCalculator
    {
        // Define relative positions to check for adjacent cubes
        public static readonly HashSet<Vector3> AdjacentPositions = new()
        {
            Vector3.UnitX,
            -Vector3.UnitX,
            Vector3.UnitY,
            -Vector3.UnitY,
            Vector3.UnitZ,
            -Vector3.UnitZ,
        };

        public static IEnumerable<Vector3> ParseInput(string[] input)
        {
            foreach (var line in input)
            {
                var parts = line.Split(',');
                var x = int.Parse(parts[0]);
                var y = int.Parse(parts[1]);
                var z = int.Parse(parts[2]);
                yield return new Vector3(x, y, z);
            }
        }

        public static int CalculateSurfaceArea(HashSet<Vector3> cubes, bool part2)
        {
            var surfaceAreaSum = 0;

            var airPockets = part2
                ? FindTrappedAirPockets(cubes)
                : new();

            foreach (var cube in cubes)
            {
                // Initialize the cube's total surface area
                var cubeSurfaceArea = 6;

                foreach (var adjacentPosition in AdjacentPositions)
                {
                    var adjacent = cube + adjacentPosition;

                    // If the cube is touching an adjecent cube, or if the adjecent volume is not exposed
                    if (cubes.Contains(adjacent) || airPockets.Contains(adjacent))
                        cubeSurfaceArea -= 1;
                }

                // Add the surface area of the current cube to the total surface area
                surfaceAreaSum += cubeSurfaceArea;
            }

            return surfaceAreaSum;
        }

        private static HashSet<Vector3> FindTrappedAirPockets(HashSet<Vector3> cubes)
        {
            // Part 2: Find "cubes" which are unable to escape to the outside of the existing cubes.

            HashSet<Vector3> airPockets = new();

            for (var x = cubes.Min(c => c.X); x < cubes.Max(c => c.X); x++)
            {
                for (var y = cubes.Min(c => c.Y); y < cubes.Max(c => c.Y); y++)
                {
                    for (var z = cubes.Min(c => c.Z); z < cubes.Max(c => c.Z); z++)
                    {
                        var point = new Vector3(x, y, z);
                        if (cubes.Contains(point))
                            continue;

                        if (!DfsEscapeSolver.CanEscape(cubes, point))
                            airPockets.Add(point);
                    }
                }
            }

            return airPockets;
        }
    }
}
