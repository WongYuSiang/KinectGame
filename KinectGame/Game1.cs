using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Kinect;

namespace KinectGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {   enum gameScreen{Title,Test,GameStart,End}
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KinectSensor mykinect;
        SpriteFont wordFont;
        string textKinectStatus, KinectConnectStatus;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            KinectSensor.KinectSensors.StatusChanged += KinectSensorStatusChanged;
        }

        //KinectSensorConnect狀況
        private void KinectSensorStatusChanged(object sender, StatusChangedEventArgs e)
        {
            
            switch (e.Status)
            {
                case KinectStatus.Initializing:
                    textKinectStatus = "Initializing";
                    break;
                case KinectStatus.Connected:
                    textKinectStatus = "Connect";
                    this.mykinect = KinectSensor.KinectSensors[0];
                    break;
                case KinectStatus.Disconnected:
                    textKinectStatus = "Disconnected";
                    break;
                case KinectStatus.NotReady:
                    textKinectStatus = "NotReady";
                    break;
                default:
                    textKinectStatus = "No Sure";
                    break;
            }
        
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        /// 
        //初始化
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        //Loading
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            wordFont = Content.Load<SpriteFont>("SpriteFont");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            WindowLoading();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.DrawString(wordFont, KinectConnectStatus + "(" + textKinectStatus+")", new Vector2(10, graphics.PreferredBackBufferHeight - 50), Color.Black);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        private void WindowLoading()
        {
            if (KinectSensor.KinectSensors.Count == 0)
            {
                KinectConnectStatus = "Please Connect Kinect";
            }
            else if (KinectSensor.KinectSensors[0].Status == KinectStatus.Connected)
            {
                this.mykinect = KinectSensor.KinectSensors[0];
                KinectConnectStatus = "Kinect Connect Finished";
            }
        }
    }
}
