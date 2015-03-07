using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MouseStuff.Extensions
{
    public static class SpriteBatchExtensions
    {
        static GraphicsDevice Device;
        static Texture2D block;

        public static void SetDevice(GraphicsDevice device)
        {
            if (Device == null)
            {
                Device = device;
            }
        }

        static void CreateBlock()
        {
            block = new Texture2D(Device, 1, 1);
            block.SetData<Color>(new Color[] { Color.White });
        }



        public static void DrawLine(this SpriteBatch sb, Vector2 startPos, Vector2 endPos, int Width, Color Colour)
        {
            if (startPos == endPos)
            {
                sb.DrawPoint(startPos, Colour);
                return;
            }
            
            //BasicBresenham(sb, startPos, endPos, Colour);

            var LinePoints = Bresenham.GetLine(startPos, endPos);

            foreach (var point in LinePoints)
            {
                sb.DrawPoint(point, Colour);
            }



            // Bresenham Line Drawing

            //// base case :  x1, y1 < x2, y2
            //if (startPos.X < endPos.X && startPos.Y < endPos.Y)
            //{
            //    BasicBresenham(sb, startPos, endPos, Colour);
            //} // inverse
            //else if (endPos.X < startPos.X && endPos.Y < startPos.Y)
            //{
            //    // for the inverse we uhm...invert it
            //    BasicBresenham(sb, endPos, startPos, Colour);
            //}

            //if (startPos.X < endPos.X && startPos.Y > endPos.Y)
            //{
            //    NegativeYBresenham(sb, startPos, endPos, Colour);
            //}
            //else if (endPos.X < startPos.Y && endPos.Y > startPos.Y)
            //{
            //    NegativeYBresenham(sb, endPos, startPos, Colour);
            //}

        }

        static void NegativeYBresenham(SpriteBatch sb, Vector2 StartPos, Vector2 EndPos, Color Colour)
        {
            var StartPoint = new Tuple<int, int>((int)StartPos.X, (int)StartPos.Y);
            var EndPoint = new Tuple<int, int>((int)EndPos.X, (int)EndPos.Y);

            // THe difference between the two points
            int diffX = EndPoint.Item1 - StartPoint.Item1;
            int diffY = EndPoint.Item2 - StartPoint.Item2;
            // Where Y is is starting
            var currentY = StartPoint.Item2;
            var eps = 0;

            for (int x = StartPoint.Item1; x <= EndPoint.Item1; ++x)
            {
                sb.DrawPoint(new Vector2(x, currentY), Colour);
                eps += diffY;
                if (eps * 2 <= diffX)
                {
                    currentY--;
                    eps += diffX;
                }
            }
        }

 /// <summary>
        /// 
        /// </summary>
        /// <param name="sb">Sprite Batch</param>
        /// <param name="Dimensions">Width and Height (Direction, Width) in Horizontal, but (Width, Length) in Vertical</param>
        /// <param name="Colour"></param>
        public static void DrawLine(this SpriteBatch sb, Rectangle Dimensions, Color Colour)
        {
            if (block == null)
            {
                CreateBlock();
            }
            sb.Draw(block, Dimensions, Colour);
        }

        public static void DrawPoint(this SpriteBatch sb, Vector2 Point, Color Colour)
        {
            if (block == null)
            {
                CreateBlock();
            }

            sb.Draw(block, Point, Colour);
        }
    }
}
