using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseStuff.Extensions
{
    class Bresenham
    {
        public static IEnumerable<Vector2> GetLine(Vector2 StartPosition, Vector2 EndPosition)
        {
          
            var StartPos = new Tuple<int, int>((int)StartPosition.X, (int)StartPosition.Y);
            var EndPos = new Tuple<int, int>((int)EndPosition.X, (int)EndPosition.Y);

            // Vertical Line
            if (StartPos.Item1 == EndPos.Item1 && StartPos.Item2 != EndPos.Item2)
            {
                var y0=0;
                var y1=0;

                if (EndPos.Item2 < StartPos.Item2)
                {
                    y0 = EndPos.Item2;
                    y1 = StartPos.Item2;
                }
                else
                {
                    y0 = StartPos.Item2;
                    y1 = EndPos.Item2;
                }

                for (int y = y0; y < y1; y ++)
                {
                    yield return new Vector2(StartPos.Item1, y);
                }
            }
            // Horizontal Line
            if (StartPos.Item2 == EndPos.Item2 && EndPos.Item1 != StartPos.Item2)
            {
                var x0 = 0;
                var x1 = 0;

                if (EndPos.Item1 < StartPos.Item1)
                {
                    x0 = EndPos.Item1;
                    x1 = StartPos.Item1;
                }
                else
                {
                    x0 = StartPos.Item1;
                    x1 = EndPos.Item1;
                }

                for(int x=x0;x<x1;++x){
                    yield return new Vector2(x, StartPos.Item2);
                }
            }
            
            // All the other lines

            //iS the total height, longer than than the total width
            bool steep = Math.Abs(EndPos.Item2 - StartPos.Item2) > Math.Abs(EndPos.Item1 - StartPos.Item1);

            // Exchange positions
            if (steep)
            {
                StartPos = new Tuple<int, int>(StartPos.Item2, StartPos.Item1);
                EndPos = new Tuple<int, int>(EndPos.Item2, EndPos.Item1);
            }
            // if the start horiz is in front of the end, flipem over.
            if (StartPos.Item1 > EndPos.Item1)
            {
                var Temp = new Tuple<int, int>(StartPos.Item1, StartPos.Item2); ;
                StartPos = new Tuple<int, int>(EndPos.Item1, EndPos.Item2);
                EndPos = new Tuple<int, int>(Temp.Item1, Temp.Item2);
            }

            // THe difference between the two Vector2s
            int diffX = EndPos.Item1 - StartPos.Item1;
            int diffY = Math.Abs(EndPos.Item2 - StartPos.Item2);
            var error = diffX / 2;

            // Are we going up or down?
            int yStep = (StartPos.Item2 < EndPos.Item2) ? 1 : -1;
            // Where Y is is starting
            var currentY = StartPos.Item2;


            for (int x = StartPos.Item1; x <= EndPos.Item1; ++x)
            {
                Vector2 DrawVector;
                if (steep)
                {
                    DrawVector = new Vector2(currentY, x);
                }
                else
                {
                    DrawVector = new Vector2(x, currentY);
                }

                yield return DrawVector;

                error = error - diffY;
                if (error < 0)
                {
                    currentY += yStep;
                    error += diffX;
                }
            }
        }
    }

}
