using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mustashe_ic
{
    class RandomDist
    {
        public static double mean { set; get; }
        public static double SD { set; get; }
        Random rand;

        static double u1, u2;

        static double randStdNormal;

        public RandomDist()
        {
            mean = 0; //tileClass.imageCatagorys[tileClass.imageCatagorys.Count - 1].Item2 - tileClass.imageCatagorys[0].Item1;
            SD = 1.0;
            rand = new Random();
            u1 = rand.NextDouble();
            u2 = rand.NextDouble();
        }

        public RandomDist(int me, int sd)
        {
            mean = me;
            SD = sd;
            rand = new Random();
            u1 = rand.NextDouble();
            u2 = rand.NextDouble();

        }

        public double getRandomNormal()
        {
            randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            return (mean + SD * randStdNormal);
        }

        public int equalRandom(int x)
        {
            return rand.Next(x);
        }

        public int equalRandom(int x, int y)
        {
            return rand.Next(x, y);
        }

    }
}
