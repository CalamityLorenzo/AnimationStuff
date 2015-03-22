using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyAnimationStuff;
using MyAnimationStuff.Extensions;
using MyAnimationStuff.Maps;

namespace MyAnimationStuff
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        DirectionFindingManComponent playerAnimaton;
        LevelMap lvlMap;

        MousePointer mp;
        DrawSmartArseLine sal;

        Vector2 lePos = new Vector2(500, 500);
        Texture2D centreBlock;
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
        
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            Content.RootDirectory = "Content";  
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            playerAnimaton = new DirectionFindingManComponent(this, new Vector2(100,500), 98,98);
            mp = new MousePointer(this);
           this.Components.Add(playerAnimaton);
            lvlMap = new LevelMap(this,this.GraphicsDevice.DisplayMode.Width, this.GraphicsDevice.DisplayMode.Height,  new DemoMapConfig(), new Vector2(0, 0));
            sal = new DrawSmartArseLine(this);

            this.Components.Add(sal);
            this.Components.Add(lvlMap);
            this.Components.Add(mp);
            SpriteBatchExtensions.SetDevice(this.GraphicsDevice);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.Services.AddService(typeof(SpriteBatch), spriteBatch);
            mp.SetSpriteBatch(spriteBatch);
            sal.SetSpriteBatch(spriteBatch);
            playerAnimaton.SetSpriteBatch(spriteBatch);
            // TODO: use this.Content to load your game content here

            centreBlock = new Texture2D(this.GraphicsDevice, 1, 1);
            centreBlock.SetData<Color>(new Color[]{Color.White});

         //playerAnimaton.SetDestination(new Vector2(500, 123));

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            DrawSmartArseLineTHingy();
            base.Update(gameTime);
        }

        private void DrawSmartArseLineTHingy()
        {
            var mState = Mouse.GetState();

            if (mState.LeftButton == ButtonState.Pressed && LineDraw == false)
            {
                LineDraw = true;
               lePos =  new Vector2(mState.X, mState.Y);
                //sal.SetLine(new Vector2(this.playerAnimaton.CurrentPosition.X + 72, this.playerAnimaton.CurrentPosition.Y + 68), new Vector2(mState.X, mState.Y));
               this.playerAnimaton.SetDestination(lePos);
            }   

            sal.SetLine(new Vector2(this.playerAnimaton.CurrentPosition().X + 72, this.playerAnimaton.CurrentPosition().Y + 68), lePos);

            if (mState.LeftButton == ButtonState.Released && LineDraw == true)
            {
                LineDraw = false;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.DrawRectangle(new Rectangle(100, 200, 100, 100),2, Color.Bisque);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public bool LineDraw { get; set; }
    }
}
