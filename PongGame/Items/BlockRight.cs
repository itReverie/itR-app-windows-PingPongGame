using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace PongGame.Items
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class BlockRight : DrawableGameComponent
    {
        #region Variables
        SpriteBatch _spriteBatch;
        readonly float _margin;
        Texture2D _rPaddleTexture;
        Rectangle _rPaddleRectangle;
        const float RPaddleSpeed = 6;
        float _rPaddleX;
        float _rPaddleY;
        private readonly Game _myGame;
        #endregion

        #region MovementVariables

        private float _screenTop;
        private float _screenBottom;
        private float _screenHalfVertical;
        private float _screenHalfHorizontal;

        //private float _paddleY;
        //private float _paddleTop;
        //private float _paddleBottom;

        //private float _touchY;
        //private float _touchTop;
        //private float _touchBottom;
        //private float avanza;
        #endregion

        #region Properties
        public Rectangle rPaddleRectangle { set { _rPaddleRectangle = value; } get { return _rPaddleRectangle; } }
        #endregion

        #region Constructor
        public BlockRight(Game game, float margin)
            : base(game)
        {
            _myGame = game;
            _margin = margin;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load the texture for this object
        /// </summary>
        ///<remarks>
        /// It is recommendable to create the instance of a SpriteBatch foreach component.Moreover, this instance should be instanciated in LoadContent as it is just done once.
        /// If we want to have a group of objects with the same SpriteBatch we can overload the constructor and send the SpriteBatch master as a parameter.
        /// </remarks> 
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _rPaddleTexture = _myGame.Content.Load<Texture2D>("Images\\rpaddle");

            int x = Convert.ToInt16(GraphicsDevice.Viewport.Width - (GraphicsDevice.Viewport.Width / 20) - _margin);
            int y = 0;
            int width = GraphicsDevice.Viewport.Width / 20;
            int height = GraphicsDevice.Viewport.Height / 5;

            _rPaddleRectangle = new Rectangle(x, y, width, height);
            _rPaddleX = _rPaddleRectangle.X;
            _rPaddleY = _rPaddleRectangle.Y;


            //Initialize boundaries of the game rectangle
            _screenTop = 0;
            _screenBottom = GraphicsDevice.Viewport.Height;
            _screenHalfVertical = (GraphicsDevice.Viewport.Height - _screenTop) / 2;
            _screenHalfHorizontal = GraphicsDevice.Viewport.Width / 2;
            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            //The block will be advancing litle by little through the window 

            TouchCollection touches = TouchPanel.GetState();
            if (touches.Count > 0)
            {
                if (touches[0].Position.X > _screenHalfHorizontal)
                {
                    if (touches[0].Position.Y > _screenHalfVertical)
                    {
                        if (_rPaddleY + _rPaddleRectangle.Height > _screenBottom)
                        {
                            //stop the paddle at the bottom
                            return;
                        }
                        else
                        {
                            TouchLocation x = touches[0];
                            _rPaddleY = _rPaddleY + RPaddleSpeed;
                        }

                    }
                    else
                    {
                        if (_rPaddleY < _screenTop)
                        {
                            //stop the paddle at the bottom
                            return;
                        }
                        else
                        {
                            _rPaddleY = _rPaddleY - RPaddleSpeed;
                        }
                    }
                }
                else
                {
                    // Do nothing as the touch is on the left side of the screen.
                    return;
                }
            }
            _rPaddleRectangle.Y = (int)(_rPaddleY + 0.5f);
            base.Update(gameTime);
        }

        /// <summary>
        /// Allows the game component to draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_rPaddleTexture, _rPaddleRectangle, Color.White);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
        #endregion
}
