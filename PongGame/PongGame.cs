using System;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PongGame.Utilities;

namespace PongGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class PongGame : Game
    {
        #region Variables
        readonly GraphicsDeviceManager _graphicsDeviceManager;
        readonly PongManager _pongGameManager;
        #endregion

        #region Constructor
        public PongGame()
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this);
            _graphicsDeviceManager.SupportedOrientations = DisplayOrientation.LandscapeLeft;
            _graphicsDeviceManager.IsFullScreen = true;

            Content.RootDirectory = "Content";
            TargetElapsedTime = TimeSpan.FromTicks(333333); // Frame rate is 30 fps by default for Windows Phone.

            _pongGameManager = new PongManager(this);

            
        }
        #endregion

        #region Methods
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Sound.Initialize(Content);
            Text.Initiallize(GraphicsDevice, Content);
            _pongGameManager.LoadContent();
            base.Initialize();
        }
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _pongGameManager.LoadContent();
            InitializaPhoneServices();
            base.LoadContent();
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            
        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                Exit();
            }
            _pongGameManager.Update(gameTime);
            base.Update(gameTime);
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
           _pongGameManager.Draw(gameTime);
            base.Draw(gameTime);
        }
        #endregion
        
        #region MobilePhoneStates
        /// <summary>
        /// Initializes the phone application services to TOMBSTONE the application
        /// </summary>
        private void InitializaPhoneServices()
        {
           
            PhoneApplicationService.Current.Activated += GameActivated;
            PhoneApplicationService.Current.Deactivated += GameDeactivated;
            PhoneApplicationService.Current.Closing += GameClosing;
            PhoneApplicationService.Current.Launching += GameLaunching;
        }

        #region EventHandlers
        /// <summary>
        /// This event is occurs when a new App instance is launched. A completely neww App so we have to save teh state of this game as the memory of the phone
        /// will be completely cleaned to load the new information of the new App.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void GameLaunching(object sender, LaunchingEventArgs eventArgs)
        {
            _pongGameManager.ReloadLastGameState(false);
        }

        /// <summary>
        /// This event occurs when the App is activated during a return from tomstoned state. This is if I am just pressing the back button and I have not even select any application 
        /// the state of my game is still saved on memory.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void GameActivated(object sender, ActivatedEventArgs eventArgs)
        {
            _pongGameManager.ReloadLastGameState(true);
        }
        /// <summary>
        /// This event occurs when the App exits during the exit cycle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void GameClosing(object sender, ClosingEventArgs eventArgs)
        {
            _pongGameManager.SaveActiveGameState(false);
        }
        /// <summary>
        /// This event occurs when the App is deactivated or tombstoned
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void GameDeactivated(object sender, DeactivatedEventArgs eventArgs)
        {
            _pongGameManager.SaveActiveGameState(true);
        }
        #endregion

       
 
        #endregion
    }
}
