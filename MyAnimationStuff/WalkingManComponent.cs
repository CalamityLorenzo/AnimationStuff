using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyAnimationStuff.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAnimationStuff
{
    class WalkingManComponent : DrawableGameComponent
    {

        protected SpriteBatch sb;
        protected WalkingManEntity Man;
        protected Texture2D WalkingManTexture;
        protected Vector2 StartPosition;

        int speedX, speedY;

        protected Directions Directions = Directions.None;

        public WalkingManComponent(Game game, Vector2 StartPosition, int SpeedX, int SpeedY) : base(game) {

            speedX = SpeedX;
            speedY = SpeedY;
            this.StartPosition = StartPosition;
        }

        protected override void LoadContent()
        {
            this.WalkingManTexture = Game.Content.Load<Texture2D>("WalkingMan");
            this.Man = new WalkingManEntity(StartPosition, this.speedX, this.speedY, this.WalkingManTexture.Width, this.WalkingManTexture.Height, 144, 136);
            base.LoadContent();
        }

        public void SetSpriteBatch(SpriteBatch sb)
        {
            this.sb = sb;
        }

        // Method is subClassed is to be how you move the character in a subclass
        // by keys, or automagically.
        protected virtual void UpdatePosition(){
            // Default this to nothing.
            this.Directions = Directions.None;
        }

        public override void Update(GameTime gameTime)
        {
            // Where the interactive actions will happen (Keys pressed, next positio calculated etc)
            this.UpdatePosition();
            
            Man.SetDirection(this.Directions);
            Man.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(this.WalkingManTexture, this.Man.CurrentPosition(), this.Man.CurrentFrame(), Color.White, 0.0f, new Vector2(), 0.8f, SpriteEffects.None,1f);
            sb.End();
            base.Draw(gameTime);
        }

        public Vector2 CurrentPosition()
        {
            return this.Man.CurrentPosition();
        }

    }
}
