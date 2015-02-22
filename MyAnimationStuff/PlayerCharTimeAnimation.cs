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
        Texture2D PlayerChar;
        Vector2 Position;

        SpriteBatch spriteBatch;

        PlayerAnimation PlayerAnim;

        public PlayerCharTimeAnimation(Game game, Vector2 StartPosition)
            : base(game)
        {
            this.Position = StartPosition;
            
        }

        protected override void LoadContent()
        {
            this.PlayerChar = Game.Content.Load<Texture2D>("WalkingMan");
            this.PlayerAnim = new PlayerAnimation(PlayerChar.Width, PlayerChar.Height, 144, 136);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (spriteBatch == null)
            {
                this.spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            }

            UpdatePosition();

            this.PlayerAnim.Update(gameTime);

            base.Update(gameTime);
        }

        private void UpdatePosition()
        {
            var keyState = Keyboard.GetState();
            var moving = false;
            // vertical
            if (keyState.IsKeyDown(Keys.W))
            {
                moving = true;
                this.PlayerAnim.SetAnimationSet("Wlk_North");
            }
            else if (keyState.IsKeyDown(Keys.S))
            {
                this.PlayerAnim.SetAnimationSet("Wlk_South");
                moving = true;
            }

            // horizontal
            if (keyState.IsKeyDown(Keys.A))
            {
                this.PlayerAnim.SetAnimationSet("Wlk_West");
                moving =true;
            }
            else if (keyState.IsKeyDown(Keys.D))
            {
                moving = true;
                this.PlayerAnim.SetAnimationSet("Wlk_East");
            }

            if (moving == false)
            {
                this.PlayerAnim.SetAnimationSet("NormalPos");
            }

        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            var currentAnimFrame = this.PlayerAnim.CurrentFrameRectangle();
            spriteBatch.Draw(this.PlayerChar, this.Position, currentAnimFrame, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
