using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAnimationStuff.Animation
{
    class PlayerAnimation : AnimationBase
    {
        public PlayerAnimation(int FullWidth, int FullHeight, int CellWidth, int CellHeight)
            : base(FullWidth, FullHeight, CellHeight, CellWidth)
        {
            AssignAnimations();
        }

        private void AssignAnimations()
        {
            List<FrameData> Northfd = new List<FrameData>{
new FrameData{ FrameIndex= 8, MilliSecondInterval =125},
new FrameData{ FrameIndex= 32, MilliSecondInterval =125},
new FrameData{ FrameIndex= 16, MilliSecondInterval =125},
new FrameData{ FrameIndex= 24, MilliSecondInterval =125},
new FrameData{ FrameIndex= 16, MilliSecondInterval =125},
new FrameData{ FrameIndex= 32, MilliSecondInterval =125},

            };

            List<FrameData> SouthFd = new List<FrameData>{
new FrameData{ FrameIndex= 8+4, MilliSecondInterval =125},
new FrameData{ FrameIndex= 32+4, MilliSecondInterval =125},
new FrameData{ FrameIndex= 16+4, MilliSecondInterval =125},
new FrameData{ FrameIndex= 24+4, MilliSecondInterval =125},
new FrameData{ FrameIndex= 16+4, MilliSecondInterval =125},
new FrameData{ FrameIndex= 32+4, MilliSecondInterval =125},

            };


            List<FrameData> EastFd = new List<FrameData>{
new FrameData{ FrameIndex= 8+2, MilliSecondInterval =125},
new FrameData{ FrameIndex= 32+2, MilliSecondInterval =125},
new FrameData{ FrameIndex= 16+2, MilliSecondInterval =125},
new FrameData{ FrameIndex= 24+2, MilliSecondInterval =125},
new FrameData{ FrameIndex= 16+2, MilliSecondInterval =125},
new FrameData{ FrameIndex= 32+2, MilliSecondInterval =125},

            };

            List<FrameData> WestFd = new List<FrameData>{
new FrameData{ FrameIndex= 8+6, MilliSecondInterval =125},
new FrameData{ FrameIndex= 32+6, MilliSecondInterval =125},
new FrameData{ FrameIndex= 16+6, MilliSecondInterval =125},
new FrameData{ FrameIndex= 24+6, MilliSecondInterval =125},
new FrameData{ FrameIndex= 16+6, MilliSecondInterval =125},
new FrameData{ FrameIndex= 32+6, MilliSecondInterval =125},

            };




            AnimationSet Wlk_North = new AnimationSet("Wlk_North", true, Northfd.ToArray());// CreateFrameData(8, 4, 8, 250));
            AnimationSet Wlk_South = new AnimationSet("Wlk_South", true, SouthFd.ToArray());
            AnimationSet Wlk_East = new AnimationSet("Wlk_East", true, EastFd.ToArray()); //CreateFrameData(10, 4, 8, 250));
            AnimationSet Wlk_West = new AnimationSet("Wlk_West", true, WestFd.ToArray()); //CreateFrameData(14, 4, 8, 250));



            AnimationSet Wlk_NorthEast = new AnimationSet("Wlk_NorthEast", true, CreateFrameData(1, 5, 8, 250));
            AnimationSet Wlk_NorthWest = new AnimationSet("Wlk_NorthWest", true, CreateFrameData(7, 5, 8, 250));

            AnimationSet Wlk_SouthEast = new AnimationSet("Wlk_SouthEast", true, CreateFrameData(3, 5, 8, 250));
            AnimationSet Wlk_SouthWest = new AnimationSet("Wlk_SouthWest", true, CreateFrameData(5, 5, 8, 250));

            AnimationSet NormalPos = new AnimationSet("NormalPos", false, new FrameData[] { new FrameData { FrameIndex = 0, MilliSecondInterval = 0 } });

            this.Animations = new List<AnimationSet> { Wlk_North, Wlk_East, Wlk_South, Wlk_West, Wlk_NorthEast, Wlk_NorthWest, Wlk_SouthEast, Wlk_SouthWest, NormalPos };
            // set the default
            this.CurrentAnimationSet = NormalPos;

            this.CurrentFrameIndex = 0;
            this.CurrentFrame = this.CurrentAnimationSet.Frames[this.CurrentFrameIndex];
            this.CurrentFrameRect = this.FrameRects[this.CurrentFrame.FrameIndex];
            this.AggregateUpdateInterval = 0;

        }

        private FrameData[] CreateFrameData(int start, int count, int step, int frameInterval)
        {
            var frames = new FrameData[count];

            for (int x = 0; x < count; ++x)
            {
                frames[x] = new FrameData(start + x * step, frameInterval);
            }

            return frames;
        }
    }
}
