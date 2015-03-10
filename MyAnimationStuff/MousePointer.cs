using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAnimationStuff.Extensions;

namespace MyAnimationStuff
{
    class MousePointer :DrawableGameComponent
    {
        Vector2 currentPosition;

        Rectangle Horiz, Vert;
        SpriteBatch spriteBatch;

        public MousePointer(Game game) : base(game) { }

        public void SetSpriteBatch(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        public override void Update(GameTime gameTime)
        {
            SetPosition();
            base.Update(gameTime);
        }

        private void SetPosition()
        {
            var mState = Mouse.GetState();
            this.currentPosition = new Vector2(mState.X, mState.Y);

            Horiz = new Rectangle((int)this.currentPosition.X - 14, (int)this.currentPosition.Y, 30, 1);
            Vert = new Rectangle((int)this.currentPosition.X, (int)this.currentPosition.Y - 14, 1, 30);
        }

        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();

            spriteBatch.DrawLine(Horiz, Color.White);
            spriteBatch.DrawLine(Vert, Color.White);

            // Draw the actual point    
            spriteBatch.DrawLine(new Rectangle((int)currentPosition.X, (int)currentPosition.Y, 1, 1), Color.Black);


            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
