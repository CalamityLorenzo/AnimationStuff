using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyAnimationStuff.Maps;
using MyAnimationStuff.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAnimationStuff.Maps
{
    // Both Display and Collision
    // The screen is the Camera, the map has to fit into that camera.

    class LevelMap : DrawableGameComponent
    {
        // SpriteMap for the background

        Texture2D backgroundTexture; 
        SpriteMap BackgroundSpriteMap;
        readonly int[] GraphicsArray;
        readonly int[] CollisionArray;

        LevelMapConfig MapConfig;

        // Where the top left of the map is in relation to the screen camera.
        Vector2 TopLeftPosition;

        SpriteBatch spriteBatch;

        // Helpers grab these every update.
        int ScreenWidth, ScreenHeight;

        public LevelMap(Game1 game1, int ScreenWidth, int ScreenHeight, LevelMapConfig LevelConfig, Vector2 StartPosition)
            : base(game1)
        {
            // TODO: Complete member initialization
            //this.backgroundTexture = backgroundTexture;
            this.MapConfig = LevelConfig;
            // create the map for the texture.
            this.GraphicsArray = LevelConfig.Map;
            this.CollisionArray = LevelConfig.Collision;
            this.TopLeftPosition = StartPosition;
            this.ScreenWidth = ScreenWidth;
            this.ScreenHeight = ScreenHeight;
        }

        protected override void LoadContent()
        {
            this.backgroundTexture = this.Game.Content.Load<Texture2D>("DummyBlocks");

            this.BackgroundSpriteMap = new SpriteMap(this.backgroundTexture.Width, this.backgroundTexture.Height, this.MapConfig.CellWidth, this.MapConfig.CellHeight);

            base.LoadContent();
        }

        public bool CollisionPoint(Vector2 screenPosition)
        {
            //ScreenPos is a vector of the screen.
            // first calculate the offset from the top left of the actual map (which can be off screen)
            var mapPosition = screenPosition + TopLeftPosition;
            // Convert the Vector into a map indicie
            var XRow = (int)mapPosition.X / this.MapConfig.CellWidth;
            var YCell = (int)mapPosition.Y / this.MapConfig.CellHeight;
            //var Cols = this.CollisionArray.Length
            return this.CollisionArray[XRow * YCell] != 0;

        }

        void CalculateDisplayArray()
        {
            // So displaying the MAP.
            // we need a position to work from. Where the hell do get that?
        }

        private int ConvertCoordsToIndex(Vector2 CoordPosition)
        {
            // we are at the top.
            if (this.TopLeftPosition == new Vector2(0, 0))
            {
                return (int)(CoordPosition.X * CoordPosition.Y + (this.MapConfig.CellHeight / this.MapConfig.CellHeight));
            }
            else
            {
                return 0;
                // do as above with the coord substitution.
            }
        }

        private int ConvertCoordsToIndex(int X, int Y)
        {
            return this.ConvertCoordsToIndex(X, Y);
        }


        public override void Update(GameTime gameTime)
        {
            if (spriteBatch == null)
            {
                this.spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            // here we identify what gets drawed.

            for (int y = 0; y < this.MapConfig.Cols; ++y)
            {
                for (int x = 0; x < this.MapConfig.Rows; ++x)
                {
                    var currentMapItem = this.MapConfig.Map[x + y * 32];
                    if (currentMapItem != 0)
                    {
                        var bkgroundSourceRect = this.BackgroundSpriteMap[currentMapItem - 1];
                        var PosVectore = new Vector2(x * this.MapConfig.CellWidth, y * this.MapConfig.CellHeight);
                        spriteBatch.Draw(this.backgroundTexture, PosVectore, bkgroundSourceRect, Color.White);
                    }
                }
            }


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
