using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace PongGame.Utilities
{
    public static class Sound
    {
        #region Variables 
        private static SoundEffect _bgm;
        private static SoundEffect _ding;
        private static SoundEffect _colission;
        private static SoundEffectInstance _bgmInstance;
        #endregion

        #region Properties
        public static SoundEffectInstance BGMInstance { set { _bgmInstance = value;} get { return _bgmInstance;} }
        #endregion 

        #region Methods
        public static void Initialize(ContentManager content)
        {
            _bgm = content.Load<SoundEffect>(@"Sound\bgm");
            _ding = content.Load<SoundEffect>(@"Sound\ding");
            _colission = content.Load<SoundEffect>(@"Sound\explode");
            _bgmInstance = _bgm.CreateInstance();
        }


        public static void PlayBGM()
        {
            _bgm.Play();
        }

        public static void PlayDing()
        {
            _ding.Play();
        }
        public static void PlayCollision()
        {
            _colission.Play();
        }
        #endregion

    }
}
