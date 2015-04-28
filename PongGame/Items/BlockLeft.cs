using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace PongGame.Items
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class BlockLeft : DrawableGameComponent
    {
        #region Variables
        SpriteBatch _spriteBatch;
        // Distance of paddles from screen edge
        readonly float _margin;
        Texture2D _lPaddleTexture;
        Rectangle _lPaddleRectangle;
        const float LPaddleSpeed = 3;
        float _lPaddleY;
        private readonly Game _myGame;
        private TouchCollection touches;
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
        public Rectangle lPaddleRectangle { set { _lPaddleRectangle = value; } get { return _lPaddleRectangle; } }
        #endregion

        #region Constructor
        public BlockLeft(Game game, float margin)
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
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _lPaddleTexture = _myGame.Content.Load<Texture2D>("Images\\lpaddle");

            int x = Convert.ToInt16(_margin);
            int y = 0;
            int width = GraphicsDevice.Viewport.Width / 20;
            int height = GraphicsDevice.Viewport.Height / 5;

            _lPaddleRectangle = new Rectangle(x, y, width, height);
            _lPaddleY = _lPaddleRectangle.Y;

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
            touches = TouchPanel.GetState();

            //foreach (TouchLocation touch in touches)
            if (touches.Count > 0)
            {
                if (touches[0].Position.X < _screenHalfHorizontal)
                {
                    if (touches[0].Position.Y > _screenHalfVertical)
                    {
                        if (_lPaddleY + lPaddleRectangle.Height > _screenBottom)
                        {
                            //stop the paddle at the bottom
                            return;
                        }
                        else
                        {
                            TouchLocation x = touches[0];
                            _lPaddleY = _lPaddleY + LPaddleSpeed;
                        }
                    }

                    else
                    {
                        if (_lPaddleY < _screenTop)
                        {
                            //stop the paddle at the top
                            return;
                        }
                        else
                        {
                            _lPaddleY = _lPaddleY - LPaddleSpeed;
                        }
                    }
                }
            }
            //if (touches.Count > 0)
            //{
            //1st option to check for single touches
            //}

            _lPaddleRectangle.Y = (int)(_lPaddleY + 0.5f);

            #region BrenLogic
            //if (touches.Count > 0)
            //{
            //    //_paddleY=_lPaddleRectangle.Y;
            //    _paddleTop = _lPaddleRectangle.Y + LPaddleSpeed;
            //    _paddleBottom = _lPaddleRectangle.Y + LPaddleSpeed;

            //    _touchY = touches[0].Position.Y;
            //    //_touchTop = _touchY - _lPaddleRectangle.Height;
            //    //_touchBottom = _touchY + _lPaddleRectangle.Height;
            //    //TOP
            //    //If the touch is the top half we evaluate the top variables
            //    if ((_touchY > _screenTop))//(_touchY<_screenHalf) &&  IS TOP
            //    {
            //        _paddleY = _paddleY + LPaddleSpeed;
            //        //if the touch is inside the boundaries then keep on advancing to the TOP
            //        if ((Convert.ToInt32(_paddleY + 0.5f) > _screenTop))
            //        {
            //            avanza = Convert.ToInt32(_paddleY + 0.5f);
            //        }
            //        else
            //        {
            //            _paddleY = _paddleY - LPaddleSpeed;
            //            avanza = (int)_screenTop;
            //        }
            //    }
            //    //BOTTOM
            //    else// IF ((_touchY>=_screenHalf) && (_touchY < _screenBottom)) IS BOTTOM
            //    {
            //        _paddleY = _paddleY + LPaddleSpeed;
            //        if ((Convert.ToInt32(_paddleY + 0.5f) < _screenBottom))
            //        {
            //            avanza = Convert.ToInt32(_paddleY + 0.5f);
            //        }
            //        else
            //        {
            //            _paddleY = _paddleY - LPaddleSpeed;
            //            //_paddleY = (int)_screenBottom - _lPaddleRectangle.Height;
            //            avanza = (int)_screenBottom - _lPaddleRectangle.Height;
            //        }
            //    }
            //}

            //_lPaddleRectangle.Y = (int)avanza; 
            #endregion

            base.Update(gameTime);
        }

        /// <summary>
        /// Allows the game component to draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_lPaddleTexture, _lPaddleRectangle, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
        #endregion


}
