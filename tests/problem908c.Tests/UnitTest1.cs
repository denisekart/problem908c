using System;
using System.Linq;
using Xunit;

namespace problem908c.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var machine = new CurlingMachine(2, 6);
            var coords = new int[] {5, 5, 6, 8, 3, 12};
            var expected = new double[] {2, 6.0, 9.87298334621, 13.3370849613, 12.5187346573, 13.3370849613};
            var actual = coords.Select(x => machine.Slide(x).Y).ToArray();
            Assert.True(Enumerable.Range(0,6).All(x=>ConformsToErrorMargin(expected[x],actual[x])));
        }

        public static bool ConformsToErrorMargin(double expected, double actual)
        {
            return (actual - expected) / Math.Max(expected, 1) < Math.Pow(10, -6);
        }
    }
}
