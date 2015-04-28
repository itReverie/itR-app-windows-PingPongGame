using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PongGame.Utilities
{
    /// <summary>
    /// Writes any sort of text into the game
    /// </summary>
    public static class Text
    {
        #region Variables
        private static GraphicsDevice _graphicsDevice;
        private static ContentManager _contentManager;
        private static SpriteBatch _spriteBatch;
        private static SpriteFont _spriteFont;
        private static Vector2 _position;
        private static Color _fontColor;
        private static float _messageWidth ;
        private static float _margin;
        #endregion

        #region Methods

        /// <summary>
        /// Initialize the SpriteFont necessary to write all the texts in the game
        /// </summary>
        /// <param name="graphicsDevice">Performs the basics rendering operations used when we want to draw the text in teh screen. (The SpriteBatch need it to write the SpriteFont)</param>
        /// <param name="contentManager">We used this object to load the resources needed in the game.</param>
        public static void Initiallize(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            _graphicsDevice = graphicsDevice;
            _contentManager = contentManager;
            _margin = _graphicsDevice.Viewport.Width / 20;
            _spriteFont = _contentManager.Load<SpriteFont>("Font\\ScoreFont");
        }

        /// <summary>
        /// Writes the score of the game at the top of teh screen
        /// </summary>
        /// <param name="text">Score of the game.</param><example>5:6</example>
        /// <param name="position">Position to draw the text</param>
        public static void Score(string text, Vector2 position)
        {
            _fontColor = new Color(255, 192, 0);
            _messageWidth = _spriteFont.MeasureString(text).X;
            _position = new Vector2((_graphicsDevice.Viewport.Width-_messageWidth)/2, position.Y);

            _spriteBatch = new SpriteBatch(_graphicsDevice);
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_spriteFont, text, _position, _fontColor);
            _spriteBatch.End();
        }

       /// <summary>
       /// Writes the score of the game at the top of teh screen
       /// </summary>
       /// <param name="text">Score of the game.</param><example>5:6</example>
       /// <param name="position">Position to draw the text</param>
       public static void MenuOption(string text, Vector2 position)
       {
           _fontColor = new Color(255, 192, 0);
           _messageWidth = _spriteFont.MeasureString(text).X;
           _position = new Vector2((_graphicsDevice.Viewport.Width - _messageWidth) / 2, position.Y);

           _spriteBatch = new SpriteBatch(_graphicsDevice);
           _spriteBatch.Begin();
           _spriteBatch.DrawString(_spriteFont, text, position, _fontColor);
           _spriteBatch.End();
       }
        #endregion
    }
}
