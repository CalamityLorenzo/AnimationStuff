using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAnimationStuff.Extensions;
using MyAnimationStuff.Animation;

namespace MyAnimationStuff
{
    class DirectionFindingManComponent : WalkingManComponent
    {
        Vector2 Destination;
        Rectangle DestinationRect;
        Rectangle CurrentPathPosRect;
        public DirectionFindingManComponent(Game game, Vector2 StartPosition, int SpeedX, int SpeedY) : base(game, StartPosition, SpeedX, SpeedY) { }


        public void SetDestination(Vector2 destination)
        {
            this.Destination = destination;

            // Ratther than fighting with points, lets make a small rectangle that we have to -fit- into
            this.DestinationRect = new Rectangle((int)this.Destination.X , (int)this.Destination.Y , 12, 12);
        }

        protected override void UpdatePosition()
        {
            // check our position relative to the destionation.
            Directions = Directions.None;
                // Current Position Reectn agle. basically a half sized box in the middle of the sprte
            //CurrentPathPosRect = new Rectangle((int)this.CurrentPosition().X + this.Man.CellWidth/2, (int)this.CurrentPosition().Y , this.Man.CellWidth / 2, this.Man.CellHeight / 2);
            CurrentPathPosRect = new Rectangle((int)this.CurrentPosition().X+ this.Man.CellWidth/4, (int)this.CurrentPosition().Y + this.Man.CellHeight/4, this.Man.CellWidth/2, this.Man.CellHeight/2);

            if (CurrentPathPosRect.Intersects(DestinationRect))
            {
                return;
            }
            else
            {
                // calcuate the difference in the points

                var diff =  new Vector2(CurrentPathPosRect.X, CurrentPathPosRect.Y) - Destination;
                // DEpending on the pluses or da minuses we have to move 
                if (diff.X > 0)
                {
                    this.Directions = this.Directions | Directions.Left;
                }
                else if (diff.X<0){
                    this.Directions = this.Directions | Directions.Right;
                }

                if (diff.Y < 0)
                {
                    this.Directions = this.Directions | Directions.Down;
                }
                else if (diff.Y > 0)
                {
                    this.Directions = this.Directions | Directions.Up;

                }
            }

         

        }

        public override void Draw(GameTime gameTime)
        {
            this.sb.Begin();
            this.sb.DrawPoint(Destination, Color.Black);
            this.sb.DrawRectangle(this.DestinationRect, 2, Color.Red);
            this.sb.DrawRectangle(this.CurrentPathPosRect, 2, Color.Red);
            this.sb.DrawRectangle(new Rectangle((int)this.CurrentPosition().X, (int)this.CurrentPosition().Y, this.Man.CellWidth, this.Man.CellHeight), 2, Color.Black);
            this.sb.DrawPoint(this.CurrentPosition(), Color.Black);
            this.sb.End();
            base.Draw(gameTime);
        }
    }
}
