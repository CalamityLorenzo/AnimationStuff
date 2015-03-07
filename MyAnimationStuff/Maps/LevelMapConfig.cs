using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAnimationStuff.Maps
{
    class LevelMapConfig
    {
        public int MapWidth { get; set; }
        public int MapHeight { get; set;}
        public int CellWidth { get; set; }
        public int CellHeight { get; set; }
        // The sizes of these should mirror
        public int[] Map { get; set; }
        public int[] Collision { get; set; }
        public int Rows { get; set; }

        public int Cols { get; set; }

        public LevelMapConfig(int MapWidth, int MapHeight, int CellWidth, int CellHeight)
        {
            this.MapHeight = MapHeight;
            this.MapWidth = MapWidth;
            this.CellWidth = CellWidth;
            this.CellHeight = CellHeight;

            this.Rows = MapWidth / CellWidth;
            this.Cols = MapHeight / CellHeight;
            // initalise the arraysto the correct size.
            this.Map = new int [Rows * Cols];
            this.Collision = new int[Rows * Cols];
        }

    }
}
