using Microsoft.Xna.Framework;

namespace PongGame.Screen
{
    public class PauseScreen 
    {
        #region Variables
        private readonly Game _game;
        #endregion

        #region Properties
        public PongGameState ScreenPongGameState { get; set; }
        #endregion

        #region Constructor
        public PauseScreen(Game game)
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
        }
        /// <summary>
        /// Update the elements appearing in this screen
        /// </summary>
        /// <param name="gameTime">Snapshot of the gameTiming of the game</param>
        public void Update(GameTime gameTime)
        {
        }
        /// <summary>
        /// Draw the elements appearing in this screen 
        /// </summary>
        /// <param name="gameTime">Snapshot of the gameTiming of the game</param>
        public void Draw(GameTime gameTime)
        {
        }
        #endregion 
    }
}
