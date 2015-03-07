using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAnimationStuff.Maps
{
    class DemoMapConfig : LevelMapConfig
    {
        public DemoMapConfig()
            : base(1024, 768, 32, 32)
        {

            PopulateMapData(this.Map);
            PopulateMapData(this.Collision);
        }

        private void PopulateMapData(int[] ArrayData)
        {
            for (int y = 0; y < 24; ++y)
                for (int x = 0; x < 32; ++x)
                {
                    if (y == 0)
                    {
                        this.Map[x + y * 32] = 1;
                    }

                    if (x == 0 || x == 32 - 1)
                    {
                        this.Map[x + y * 32] = 1;

                    }
                    if (y == 24 - 1)
                    {
                        this.Map[x + y * 32] = 1;
                    }
                }
        }
    }
}
