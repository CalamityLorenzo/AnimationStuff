using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAnimationStuff.Animation
{
    /// <summary>
    /// Represents one frame of action in an AnimationSet
    /// </summary>
    public class FrameData
    {
        public FrameData() { }

        public FrameData(int index, int MilliSecondInterval)
        {
            this.FrameIndex = index;
            this.MilliSecondInterval = MilliSecondInterval;
        }

        public int FrameIndex { get; set; }
        public int MilliSecondInterval { get; set; }

        public override string ToString()
        {
            return String.Format("Index : {0} MilliSeconds : {1}", this.FrameIndex, this.MilliSecondInterval);
        }
    }
}
