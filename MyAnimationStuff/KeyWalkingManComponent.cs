using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MyAnimationStuff.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAnimationStuff
{
    class KeyWalkingManComponent : WalkingManComponent
    {
        public KeyWalkingManComponent(Game1 game): base(game, new Vector2(100,100), 98,98){
        
        }
        KeyboardState oldKeyState;
        protected override void UpdatePosition()
        {

            var keyState = Keyboard.GetState();
            // Reset the Directions
            this.Directions = Directions.None;

            var keysPressed = keyState.GetPressedKeys();
            // vertical
            if (keyState.IsKeyDown(Keys.W))
            {
                this.Directions = Directions | Directions.Up;
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                this.Directions = Directions | Directions.Down;
            }

            // horizontal
            if (keyState.IsKeyDown(Keys.A))
            {
                this.Directions = Directions | Directions.Left;
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                this.Directions = Directions | Directions.Right;
            }

            oldKeyState = keyState;
        }
    }
}
