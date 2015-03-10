using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyAnimationStuff.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAnimationStuff
{
    class PlayerCharTimeAnimation : DrawableGameComponent
    {

        protected Directions Direction = Directions.None;
        Texture2D PlayerChar;
        Vector2 Position;

        SpriteBatch spriteBatch;

        WalkingManAnimations PlayerAnim;

        float HorizVelocity, VertVelocity;
        float OldHorizVelocity, OldVertVelocity;
        string CurrentAnimationStateName;

        // Amount of pixels to move over deltaTime.
        int SpeedX, SpeedY;
        // factor the velocity is accelerated by
        float HorizAcceleration, VertAcceleration;

        public Vector2 CurrentPosition{get{return this.Position;}}

        public PlayerCharTimeAnimation(Game game, Vector2 StartPosition, int SpeedX, int SpeedY)
            : base(game)
        {
            this.Position = StartPosition;
            this.SpeedX = SpeedX;
            this.SpeedY = SpeedY;
            this.HorizAcceleration = 1f;
            this.VertAcceleration = 1f;
        }

        protected override void LoadContent()
        {
            this.PlayerChar = Game.Content.Load<Texture2D>("WalkingMan");
            this.PlayerAnim = new WalkingManAnimations(PlayerChar.Width, PlayerChar.Height, 144, 136);
            base.LoadContent();
        }

        public void SetSpriteBatch(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        public override void Update(GameTime gameTime)
        {
           
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            // Sets Character movement state
            CheckKeyboard();

            UpdatePosition(deltaTime);
            UpdateAnimation();

            this.Position.X += this.HorizVelocity;
            this.Position.Y += this.VertVelocity;

            this.PlayerAnim.Update(gameTime.ElapsedGameTime.Milliseconds);
            base.Update(gameTime);
        }

        protected void UpdatePosition(float deltaTime)
        {
            // Previous movements
            OldVertVelocity = VertVelocity;
            OldHorizVelocity = HorizVelocity;

            VertVelocity = 0f;
            HorizVelocity = 0f;
            // vertical
            if ((this.Direction & Directions.Up)!=0)
            {
                VertVelocity = (-SpeedY * deltaTime) * this.VertAcceleration;
            }
            else if ((this.Direction & Directions.Down) !=0)
            {
                VertVelocity = (+SpeedY * deltaTime) * this.VertAcceleration;
            }

            // horizontal
            if ((this.Direction & Directions.Left)!=0)
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
                    this.PlayerAnim.SetAnimationSet(this.CurrentAnimationStateName);
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
                    if (OldHorizVelocity  < 0)
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
                    if (OldHorizVelocity >0)
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

                this.PlayerAnim.SetAnimationSet(this.CurrentAnimationStateName);
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
            this.PlayerAnim.SetAnimationSet(this.CurrentAnimationStateName);


        }

        protected void CheckKeyboard(){
            var keyState = Keyboard.GetState();
            // Reset the Directions
            Direction = Directions.None;
            // vertical
            if (keyState.IsKeyDown(Keys.W))
            {
                Direction = Direction | Directions.Up;
            }
            else if (keyState.IsKeyDown(Keys.S))
            {
                Direction = Direction | Directions.Down;         
            }

            // horizontal
            if (keyState.IsKeyDown(Keys.A))
            {
                Direction = Direction | Directions.Left;
            }
            else if (keyState.IsKeyDown(Keys.D))
            {
                Direction = Direction | Directions.Right;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            var currentAnimFrame = this.PlayerAnim.CurrentFrameRectangle();
            spriteBatch.Draw(this.PlayerChar, this.Position, currentAnimFrame,Color.White, 0.0f, new Vector2(), 0.8f, SpriteEffects.None,1f);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
