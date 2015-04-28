using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace PongGame.Items
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Ball : DrawableGameComponent
    {
        #region Variables
        Texture2D ballTexture;
        public Rectangle _ballRectangle;
        readonly float _margin;
        float ballX;
        float ballY;
        float _ballXSpeed = 3;
        float _ballYSpeed = 6;
        public SpriteBatch _spriteBatch;
        private readonly Game _myGame;
        #endregion

        #region Properties
        public Rectangle ballRectangle { set { _ballRectangle = value; } get { return _ballRectangle; } }
        public float ballXSpeed { set { _ballXSpeed = value; } get { return _ballXSpeed; } }
        public float ballYSpeed { set { _ballYSpeed = value; } get { return _ballYSpeed; } }

        #endregion

        #region Constructor
        public Ball(Game game, float margin)
            : base(game)
        {
            _myGame = game;
            _margin = margin;
        }
        #endregion

        #region Methods
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ballTexture = _myGame.Content.Load<Texture2D>("Images\\ball");

            ballRectangle = new Rectangle(
        0, 0,
        GraphicsDevice.Viewport.Width / 20,
        GraphicsDevice.Viewport.Width / 20);

            ballX = GraphicsDevice.Viewport.Width / 2;
            ballY = GraphicsDevice.Viewport.Height / 2;

        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            ballX = ballX + _ballXSpeed;
            ballY = ballY + _ballYSpeed;

            if (ballX < 0 || ballX + ballRectangle.Width > GraphicsDevice.Viewport.Width)
            {
                _ballXSpeed = -_ballXSpeed;
            }

            if (ballY < 0 || ballY + ballRectangle.Height > GraphicsDevice.Viewport.Height)
            {
                _ballYSpeed = -_ballYSpeed;
            }

     

            _ballRectangle.X = (int)(ballX + 0.5f);
            _ballRectangle.Y = (int)(ballY + 0.5f);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(ballTexture, ballRectangle, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);


        }
        #endregion
    }
}