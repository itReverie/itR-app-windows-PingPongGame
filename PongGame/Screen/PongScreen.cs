using Microsoft.Xna.Framework;
using PongGame.Items;
using PongGame.Utilities;
using Microsoft.Xna.Framework.Audio;

namespace PongGame.Screen
{
    public class PongScreen
    {
         #region Variables

        //XNA Objects
        private readonly Game _game;
        float _margin;
        
        //Game Objects
        Ball _ball;
        readonly BlockLeft _blockLeft;
        readonly BlockRight _blockRight;

        //Game Values
        int _leftScore = 0;
        int _rightScore = 0;
        //string _Score = "0:0";        
        #endregion

        #region Constructor
        public PongScreen(Game game)
        {
            //XNA objects
            _game = game;
            //Pong objects
            _ball = new Ball(_game, _margin);
            _blockLeft = new BlockLeft(_game, _margin);
            _blockRight = new BlockRight(_game, _margin);
            
            //XNA-Pong objects integration in the game
            _game.Components.Add(_ball);
            _game.Components.Add(_blockLeft);
            _game.Components.Add(_blockRight);    
           
        }

        #endregion

        #region Properties
        //public string Score { set { _Score = value; } get { return _Score; } }
        public int RightScore { set { _rightScore = value; } get { return _rightScore; } }
        public int LeftScore { set { _leftScore = value; } get { return _leftScore; } }
        #endregion

        #region Methods
        /// <summary>
        /// Loads and/or initializes the necessary XNA objects to run the game.
        /// </summary>
        /// <remarks>In this game the objects inherit from DrawableGameComponent; therefore, the LoadComponent is not explicit.</remarks>
        public void LoadContent()
        {
            _margin = _game.GraphicsDevice.Viewport.Width / 20;
        }
        

        /// <summary>
        /// Update the objects participating in the game
        /// </summary>
        /// <param name="gameTime">Snapshot of the gameTiming.</param>
        public void Update(GameTime gameTime)
        {
            _ball.Update(gameTime);
            _blockLeft.Update(gameTime);
            _blockRight.Update(gameTime);

            if (Sound.BGMInstance.State == SoundState.Stopped)
            {
                Sound.BGMInstance.Play();
            }

            // Ball - Bat collisons
            if (_ball._ballRectangle.Intersects(_blockLeft.lPaddleRectangle))
            {
                _ball.ballXSpeed = -_ball.ballXSpeed;
                _ball.ballYSpeed = -_ball.ballYSpeed;
                Sound.PlayDing();
                _leftScore++;
               // _Score =_leftScore.ToString() + ":" + _rightScore.ToString();
            }
            if (_ball._ballRectangle.Intersects(_blockRight.rPaddleRectangle))
            {
                _ball.ballXSpeed = -_ball.ballXSpeed;
                _ball.ballYSpeed = -_ball.ballYSpeed;
                Sound.PlayDing();
                _rightScore++;
               // _Score = _leftScore.ToString() + ":" + _rightScore.ToString();
            }
        }

        public void Draw(GameTime gameTime)
        {
            _ball.Draw(gameTime);
            _blockLeft.Draw(gameTime);
            _blockRight.Draw(gameTime);
            string scoreText = _leftScore.ToString() + ":" + _rightScore.ToString();
            Vector2 scorePosition = new Vector2(0, _margin / 3);
            Text.Score(scoreText, scorePosition);
           
        }

        #endregion
    }
}
