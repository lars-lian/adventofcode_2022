using BoilingBoulders;

namespace BoilingBouldersTests;

public class ExampleMatchTests
{
    private static readonly string[] _input = { "2,2,2", "1,2,2", "3,2,2", "2,1,2", "2,3,2", "2,2,1", "2,2,3", "2,2,4", "2,2,6", "1,2,5", "3,2,5", "2,1,5", "2,3,5" };

    [Fact]
    public void StarOneExample_Matches()
    {
        var expected = 64;

        var cubes = SurfaceAreaCalculator.ParseInput(_input).ToHashSet();
        var actual = SurfaceAreaCalculator.CalculateSurfaceArea(cubes, false);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void StarTwoExample_Matches()
    {
        var expected = 58;

        var cubes = SurfaceAreaCalculator.ParseInput(_input).ToHashSet();
        var actual = SurfaceAreaCalculator.CalculateSurfaceArea(cubes, true);

        Assert.Equal(expected, actual);
    }
}