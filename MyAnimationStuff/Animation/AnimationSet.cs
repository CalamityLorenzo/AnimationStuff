using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAnimationStuff.Animation
{
    /// <summary>
    /// A series of frames for a particular animation set
    /// A list of these makes up an entities list of animations
    /// </summary>
    public class AnimationSet
    {
        public string Name { get; private set; }
        public bool IsRepeating { get; private set; }
        public FrameData[] Frames { get; set; }

        public AnimationSet(string Name, bool IsRepeating, FrameData[] Frames)
        {
            this.Name = Name;
            this.IsRepeating = IsRepeating;
            this.Frames = Frames;
        }
    }
}
