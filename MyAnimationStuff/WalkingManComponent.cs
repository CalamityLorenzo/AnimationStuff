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

        SpriteBatch sb;
        WalkingManEntity Man;
        Texture2D WalkingManTexture;
        Vector2 StartPosition;

        int speedX, speedY;

        Directions Directions = Directions.None;

        public WalkingManComponent(Game game, Vector2 StartPosition, int SpeedX, int SpeedY) : base(game) {

            speedX = SpeedX;
            speedY = SpeedY;
            this.StartPosition = StartPosition;
        }

        protected override void LoadContent()
        {
            this.WalkingManTexture = Game.Content.Load<Texture2D>("WalkingMan");
            this.Man = new WalkingManEntity(StartPosition, this.speedX, this.speedY, this.WalkingManTexture.Width, this.WalkingManTexture.Height);
            base.LoadContent();
        }

        public void SetSpritebatch(SpriteBatch sb)
        {
            this.sb = sb;
        }

        // Method is subClass is to be how you move the character in a subclass
        // by keys, or automagically.
        protected void UpdatePosition(){
            // Default this to nothing.
            this.Directions = Directions.None;
        }

        public override void Update(GameTime gameTime)
        {
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
    }
}
