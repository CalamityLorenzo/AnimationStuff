using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAnimationStuff.Animation
{
    public abstract class AnimationBase
    {
        public int FullHeight, FullWidth, CellWidth, CellHeight;

        // we have to calculate timings between Frames
        protected int AggregateUpdateInterval;

        public List<AnimationSet> Animations { get; protected set; }
        protected AnimationSet CurrentAnimationSet;

        protected int CurrentFrameIndex = 0;
        protected FrameData CurrentFrame;
        protected Rectangle CurrentFrameRect;
        // Each frame of the animation as a rectangle
        protected Rectangle[] FrameRects;

        public AnimationBase(int FullWidth, int FullHeight, int CellHeight, int CellWidth)
        {
            // The dimensions of the Texture
            this.FullHeight = FullHeight;
            this.FullWidth = FullWidth;
            // The cells we are creating
            this.CellHeight = CellHeight;
            this.CellWidth = CellWidth;
            CreateFrameRects();
        }

        private void CreateFrameRects()
        {
            var Rows = this.FullWidth / CellWidth;
            var Cols = this.FullHeight / CellHeight;

            this.FrameRects = new Rectangle[Rows * Cols];

            for (int y = 0; y < Cols; ++y)
            {
                for (int x = 0; x < Rows; ++x)
                {
                    FrameRects[x + y * Rows] = new Rectangle(x * this.CellWidth, y * CellHeight, CellWidth, CellHeight);
                }
            }
            // Set the default to the first 
            this.CurrentFrameRect = FrameRects[0];
        }

        public void Update(int  elapsedMilliseconds)
        {
            // We have made it to the next frame
            if (this.AggregateUpdateInterval > this.CurrentFrame.MilliSecondInterval)
            {

                if (this.CurrentFrame.MilliSecondInterval == 0 && this.CurrentAnimationSet.IsRepeating == false)
                {
                    return;
                }

                this.CurrentFrameIndex += 1;
                // reset the aggregate
                this.AggregateUpdateInterval = 0;

                if (this.CurrentFrameIndex >= this.CurrentAnimationSet.Frames.Length && this.CurrentAnimationSet.IsRepeating == true)
                {
                    this.CurrentFrameIndex = 0;
                    this.CurrentFrame = this.CurrentAnimationSet.Frames[this.CurrentFrameIndex];
                    this.CurrentFrameRect = FrameRects[this.CurrentFrame.FrameIndex];
                }
                else if (this.CurrentFrameIndex < this.CurrentAnimationSet.Frames.Length)
                {
                    this.CurrentFrame = this.CurrentAnimationSet.Frames[this.CurrentFrameIndex];
                    this.CurrentFrameRect = FrameRects[this.CurrentFrame.FrameIndex];
                }
            }
            else
            {
                // update the aggreate
                this.AggregateUpdateInterval += elapsedMilliseconds;
            }
        }

        public void SetAnimationSet(string Name)
        {
            if (Name == null)
            {
                Name = "NormalPos";
            }

            // if it's different to what's already chosen..
            if (CurrentAnimationSet == null || this.CurrentAnimationSet.Name !=Name)
            {
                this.CurrentFrameIndex = 0;
                this.CurrentAnimationSet = this.Animations.Where(an=>an.Name == Name).First();
                this.CurrentFrame = this.CurrentAnimationSet.Frames[this.CurrentFrameIndex];
                this.CurrentFrameRect = this.FrameRects[this.CurrentFrame.FrameIndex];
                this.AggregateUpdateInterval = 0;
            }
        }

        public Rectangle CurrentFrameRectangle()
        {
            return this.CurrentFrameRect;
        }
    }
}
