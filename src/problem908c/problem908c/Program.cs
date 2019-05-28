using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace problem908c
{
    class Program
    {
        static void Main(string[] args)
        {
            //parsing and validating the first lne
            string[] line1 = Console.ReadLine()?.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if(line1==null || line1.Length <2 || !int.TryParse(line1[0],out var n) || !int.TryParse(line1[1], out var r))
                throw new ArgumentException("Expected n and r integers");
            //parsing anf validating the second line
            IEnumerable<int> line2 = Console.ReadLine()?.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.TryParse(x, out var num) ? num : throw new ArgumentException("Could not convert all input parameters into numbers"));
            //creating the machine instance
            var machine = new CurlingMachine(r, n);
            //executing
            if (line2 != null)
                foreach (var i in line2)
                {
                    Console.Write($"{machine.Slide(i).Y} ");
                }
        }
    }

    public struct LandingCoordinate
    {
        public LandingCoordinate(double x, double y)
        {
            X = x;
            Y = y;
        }
        public double X { get; }
        public double Y { get; }

    }

    public class CurlingMachine
    {
        private readonly int _radius;
        private readonly int _number;

        private LandingCoordinate[] _disks;
        private int _index;

        public CurlingMachine(int radius, int number)
        {
            _radius = radius;
            _number = number;
            Initialize();
        }

        private void Initialize()
        {
            _index = 0;
            _disks=new LandingCoordinate[_number];
        }

        public LandingCoordinate Slide(int x)
        {
            _disks[_index] = NextCoordinate(x);
            return _disks[_index++];
        }

        private LandingCoordinate NextCoordinate(int x)
        {

            for (int i = _index-1; i>=0; i--)
            {
                //could not possibly be hit because the x coordinate makes it impossible
                if(_disks[i].X+_radius<x-_radius || _disks[i].X-_radius>x+_radius)
                    continue;
                //will calculate base on pythagoras
                return new LandingCoordinate(x,PythagorasCoordinate(_disks[i],x));
            }
            return new LandingCoordinate(x,_radius);
        }

        private double PythagorasCoordinate(LandingCoordinate referenceCoordinate, int x)
        {
            double c = _radius * 2;
            double a = referenceCoordinate.X - x;
            double b = Sqrt2(Pow2(c) - Pow2(a));
            //we always use the upper value because we're stacking the disks - no need for extra computations
            return referenceCoordinate.Y+(Abs(b));
        }
        //wrapper for abs - works better than Math.Abs
        private static double Abs(double num)
        {
            if (num < 0)
                num *= -1;
            return num;
        }
        //Wrapper for pow
        private static double Pow2(double num)
        {
            return num * num;
        }
        //wrapper for sqrt
        private static double Sqrt2(double num)
        {
            return Math.Sqrt(num);
        }
    }
}
