using Microsoft.Xna.Framework;
using MyAnimationStuff.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAnimationStuff
{
    class WalkingManEntity
    {
        Directions Direction = Directions.None;
        Vector2 Position;
        AnimationBase WalkingAnimations;
        float HorizVelocity, VertVelocity;
        float OldHorizVelocity, OldVertVelocity;
        string CurrentAnimationStateName;

        // Amount of pixels to move over deltaTime.
        int SpeedX, SpeedY;
        // factor the velocity is accelerated by
        float HorizAcceleration, VertAcceleration;

        public readonly int CellWidth, CellHeight;

        public WalkingManEntity(Vector2 StartPosition, int SpeedX, int SpeedY, int TextureWidth, int TextureHeight, int CellWidth, int CellHeight)
        {
            this.Position = StartPosition;
            this.SpeedX = SpeedX;
            this.SpeedY = SpeedY;
            this.HorizAcceleration = 1f;
            this.VertAcceleration = 1f;

            this.CellWidth = CellWidth;
            this.CellHeight = CellHeight;

            WalkingAnimations = new WalkingManAnimations(TextureWidth, TextureHeight, CellWidth, CellHeight);

        }

        public Rectangle CurrentFrame()
        {
            return WalkingAnimations.CurrentFrameRectangle();
        }

        // This enum is additive
        public void SetDirection(Directions Directions)
        {
            //if (AddDirection == Directions.None)
            //{
            //    this.Direction = Directions.None;
            //}
            //else
            //{
            //    this.Direction = this.Direction | AddDirection;
            //}

            this.Direction = Directions;
        }

        public void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdatePosition(deltaTime);
            UpdateAnimation();

            this.Position.X += this.HorizVelocity;
            this.Position.Y += this.VertVelocity;

            this.WalkingAnimations.Update(gameTime.ElapsedGameTime.Milliseconds);
        }

        protected void UpdatePosition(float deltaTime)
        {
            // Previous movements
            OldVertVelocity = VertVelocity;
            OldHorizVelocity = HorizVelocity;

            VertVelocity = 0f;
            HorizVelocity = 0f;
            // vertical
            if ((this.Direction & Directions.Up) != 0)
            {
                VertVelocity = (-SpeedY * deltaTime) * this.VertAcceleration;
            }
            else if ((this.Direction & Directions.Down) != 0)
            {
                VertVelocity = (+SpeedY * deltaTime) * this.VertAcceleration;
            }

            // horizontal
            if ((this.Direction & Directions.Left) != 0)
            {
                HorizVelocity = (-SpeedX * deltaTime) * this.HorizAcceleration;
            }
            else if ((this.Direction & Directions.Right) != 0)
            {
                HorizVelocity = (+SpeedX * deltaTime) * this.HorizAcceleration;
            }
        }

        private void UpdateAnimation()
        {
            // Stopped
            if (HorizVelocity == 0 && VertVelocity == 0)
            {
                if (OldHorizVelocity == 0 && OldVertVelocity == 0)
                {
                    this.WalkingAnimations.SetAnimationSet(this.CurrentAnimationStateName);
                    return;
                }

                if (OldHorizVelocity == 0 && OldVertVelocity != 0)
                {
                    if (OldVertVelocity > 0)
                    {
                        this.CurrentAnimationStateName = "Std_South";
                    }
                    else
                    {
                        this.CurrentAnimationStateName = "Std_North";
                    }
                }

                if (OldHorizVelocity != 0 && OldVertVelocity == 0)
                {
                    if (OldHorizVelocity < 0)
                    {
                        this.CurrentAnimationStateName = "Std_West";
                    }
                    else if (OldHorizVelocity > 0)
                    {
                        this.CurrentAnimationStateName = "Std_East";
                    }
                }
                else if (OldHorizVelocity != 0 && OldVertVelocity != 0)
                {
                    if (OldHorizVelocity > 0)
                    {
                        if (OldVertVelocity > 0)
                        {
                            this.CurrentAnimationStateName = "Std_SouthEast";
                        }
                        else
                        {
                            this.CurrentAnimationStateName = "Std_NorthEast";
                        }
                    }
                    else
                    {
                        if (OldVertVelocity > 0)
                        {
                            this.CurrentAnimationStateName = "Std_SouthWest";
                        }
                        else
                        {
                            this.CurrentAnimationStateName = "Std_SouthWest";
                        }
                    }
                }

                this.WalkingAnimations.SetAnimationSet(this.CurrentAnimationStateName);
                return;
            }
            // Vertical
            if (HorizVelocity == 0 && VertVelocity != 0)
            {
                if (VertVelocity < 0)
                {
                    this.CurrentAnimationStateName = "Wlk_North";
                }
                else
                {
                    this.CurrentAnimationStateName = "Wlk_South";

                }
            }

            // HoRIzONTAL
            if (HorizVelocity != 0 && VertVelocity == 0)
            {
                if (HorizVelocity < 0)
                {
                    this.CurrentAnimationStateName = "Wlk_West";

                }
                else
                {
                    this.CurrentAnimationStateName = "Wlk_East";
                }
            }
            // DIags
            if (HorizVelocity != 0 && VertVelocity != 0)
            {
                if (HorizVelocity > 0)
                {
                    if (VertVelocity > 0)
                    {
                        this.CurrentAnimationStateName = "Wlk_SouthEast";
                    }
                    else
                    {
                        this.CurrentAnimationStateName = "Wlk_NorthEast";
                    }
                }

                if (HorizVelocity < 0)
                {
                    if (VertVelocity > 0)
                    {
                        this.CurrentAnimationStateName = "Wlk_SouthWest";
                    }
                    else
                    {
                        this.CurrentAnimationStateName = "Wlk_NorthWest";

                    }
                }

            }
            this.WalkingAnimations.SetAnimationSet(this.CurrentAnimationStateName);


        }

        internal Vector2 CurrentPosition()
        {
            return Position;
        }
    
    }
}
