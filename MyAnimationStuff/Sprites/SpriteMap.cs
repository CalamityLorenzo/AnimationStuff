using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAnimationStuff.Sprites
{
    // All The rectangles on a regularaly shaped
    // spriteMap
    class SpriteMap
    {
        private Rectangle[] SpriteRects;

        private int FullHeight, FullWidth, CellHeight, CellWidth;

        // Total Columns and Rows in the Sheet
        int Cols, Rows;

        public SpriteMap(int FullWidth, int FullHeight, int CellWidth, int CellHeight)
        {
            this.FullHeight = FullHeight;
            this.FullWidth = FullWidth;
            this.CellHeight = CellHeight;
            this.CellWidth = CellWidth;

            // Generate Alllllllllll the rectangles for the map
            // We dont care about anything else other than the rects
            GenerateSpriteRectangles();
        }

        private void GenerateSpriteRectangles()
        {
            this.Rows = FullHeight / CellHeight;
            this.Cols = FullWidth / CellWidth;
            this.SpriteRects = new Rectangle[Rows * Cols];

            // Generate The Rectangles
            for (int y = 0; y < Cols; y++)
            {
                for (int x = 0; x < Rows; x++)
                {
                    SpriteRects[x + y * Rows] = new Rectangle(x * CellWidth, y * CellHeight, CellWidth, CellHeight);
                }
            }
        }

        public Rectangle this[int i]
        {
            get{
                return SpriteRects[i];
            }
        }
    }
}
