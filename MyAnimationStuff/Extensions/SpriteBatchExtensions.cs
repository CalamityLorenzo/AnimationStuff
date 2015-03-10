using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyAnimationStuff.Extensions
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


        public static void DrawRectangle(this SpriteBatch sb, Rectangle Rectangle, int LineWidth, Color Colour)
        {
            // top
            sb.DrawLine(new Rectangle(Rectangle.X, Rectangle.Y, Rectangle.Width, LineWidth), Colour);
            //left
            sb.DrawLine(new Rectangle(Rectangle.X, Rectangle.Y, LineWidth, Rectangle.Height), Colour);
            //right
            sb.DrawLine(new Rectangle(Rectangle.X + Rectangle.Width-LineWidth, Rectangle.Y, LineWidth, Rectangle.Height), Colour);
            // bototm
            sb.DrawLine(new Rectangle(Rectangle.X, Rectangle.Y + Rectangle.Height - LineWidth, Rectangle.Width, LineWidth), Colour);


        }

        public static void DrawLine(this SpriteBatch sb, Vector2 startPos, Vector2 endPos, int Width, Color Colour)
        {
            if (startPos == endPos)
            {
                sb.DrawPoint(startPos, Colour);
                return;
            }

            //BasicBresenham(sb, startPos, endPos, Colour);

            // Get the enumerable points
            var LinePoints = Bresenham.GetLine(startPos, endPos);
            // slam 'e on the screen
            foreach (var point in LinePoints)
            {
                sb.DrawPoint(point, Colour);
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
