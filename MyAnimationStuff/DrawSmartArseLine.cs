using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MouseStuff.Extensions;
namespace MyAnimationStuff
{
    class DrawSmartArseLine : DrawableGameComponent
    {
        Texture2D EndMarker;

        Vector2 StartPoint, EndPoint;
        SpriteBatch sb;
        public DrawSmartArseLine(Game game)
            : base(game)
        {
            StartPoint = new Vector2(0,0);
            EndPoint = new Vector2(0, 0);
        }

        public void SetSpriteBatch(SpriteBatch sb)
        {
            this.sb = sb;
        }

        protected override void LoadContent()
        {
            EndMarker = this.Game.Content.Load<Texture2D>("markedPoint");
            base.LoadContent();
        }

        public void SetLine(Vector2 start, Vector2 end)
        {
            this.StartPoint = start;
            this.EndPoint = end;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            var comp = new Vector2(0,0);
            if (StartPoint !=  comp && EndPoint != comp)
            {
                sb.Begin();
                sb.DrawLine(StartPoint, EndPoint, 2, Color.White);
                sb.Draw(EndMarker, new Vector2(EndPoint.X - 8, EndPoint.Y - 8), Color.White);
                sb.End();
            }
            base.Draw(gameTime);
        }
    }
}
