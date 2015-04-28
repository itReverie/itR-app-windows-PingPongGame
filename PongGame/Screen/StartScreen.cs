using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using PongGame.Utilities;

namespace PongGame.Screen
{
    public class StartScreen
    {
        #region Variables
        private readonly Game _game;
        private Vector2 _playPosition;
        float _margin;
        #endregion

        #region Properties
        public PongGameState ScreenPongGameState { get; set; }
        #endregion

        #region Constructor
        public StartScreen(Game game)
        {
            _game = game;
        }
        #endregion 

        #region Methods
        /// <summary>
        /// Loads the elements (texture, sound, text) appearing in this screen 
        /// </summary>
        /// <remarks>In this game the objects inherit from DrawableGameComponent; therefore, the LoadComponent is not explicit.</remarks>
        public void LoadContent()
        {
            _margin = _game.GraphicsDevice.Viewport.Width / 20;
        }
        /// <summary>
        /// Update the elements appearing in this screen
        /// </summary>
        /// <param name="gameTime">Snapshot of the gameTiming of the game</param>
        public void Update(GameTime gameTime)
        {
             TouchCollection touches = TouchPanel.GetState();
             if (touches.Count > 0)
             {
                 if (touches[0].Position.Y > _playPosition.Y)
                 {
                     ScreenPongGameState= PongGameState.Play;
                 }
                 //else
                 //{
                 //    ScreenPongGameState = PongGameState.Start;
                 //}
             }
             //else
             //{
             //    ScreenPongGameState = PongGameState.Start;
             //}
        }
        /// <summary>
        /// Draw the elements appearing in this screen 
        /// </summary>
        /// <param name="gameTime">Snapshot of the gameTiming of the game</param>
        public void Draw(GameTime gameTime)
        {
            string playText = "Play";
            _playPosition = new Vector2(0, _margin / 3);
            Text.MenuOption(playText, _playPosition);

            string quitText= "Quit";
            Vector2 quitPosition = new Vector2(0, _margin / 3);
            Text.MenuOption(quitText, quitPosition);
       
        }
        #endregion 
    }
}
