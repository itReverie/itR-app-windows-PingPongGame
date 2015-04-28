using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PongGame.Screen;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;


namespace PongGame
{
    //TODO: Later on we can impleement a list of screens and we can actually save different information in each screen
    // for the purpose of thsi assignment we will just save the information of the PongScreen
    /// <summary>
    /// Manages the possible states and screens on the game
    /// </summary>
    public class PongManager
    {
        #region Variables
        private const string FileName="PongGame.dat";
        readonly PongScreen _pongScreen;
        #endregion
        
        #region Constructor
        public PongManager(Game game)
        {
            _pongScreen = new PongScreen(game);
        }
        #endregion
        
        #region GameMethods
        /// <summary>
        /// Loads and/or initializes the necessary XNA objects to run the game.
        /// </summary>
        public void LoadContent()
        {
            if (PhoneApplicationService.Current.State.ContainsKey("IsReload"))
            {
                try
                {
                    _pongScreen.RightScore =Convert.ToInt32(PhoneApplicationService.Current.State["RightScore"]);
                    _pongScreen.LeftScore = Convert.ToInt32(PhoneApplicationService.Current.State["LeftScore"]);
                }
                catch (Exception exception)
                {
                    
                    throw exception;
                }
               
            }
            else
            {
                ReloadLastGameState(false);
            }

            _pongScreen.LoadContent();

        }
        /// <summary>
        /// Update the elements appearing in this screen
        /// </summary>
        /// <param name="gameTime">Snapshot of the gameTiming of the game</param>
        public void Update(GameTime gameTime)
        {
           _pongScreen.Update(gameTime);         
        }
        /// <summary>
        /// Draw the elements appearing in this screen 
        /// </summary>
        /// <param name="gameTime">Snapshot of the gameTiming of the game</param>
        public void Draw(GameTime gameTime)
        {
            _pongScreen.Draw(gameTime);
        }

        #endregion

        #region MobilePhoneStates
        ///The  Boolean argument  indicates what capability to use: if the argument is true, we use Tombstoning, else, we use Isolation Storage.
        /// <summary>
        /// Reload last active game state from Isolated Storage or State object
        /// </summary>
        /// <param name="isTombstoning"></param>
        public void ReloadLastGameState(bool isTombstoning)
        {
            bool isHumanTurn = false;
            bool isLoaded;

            if (isTombstoning)
            {
                isLoaded = LoadFromStateObject(out isHumanTurn);
            }
            else
            {
                isLoaded = LoadFromIsolatedStorage();

                if (isLoaded)
                {
                    if (System.Windows.MessageBox.Show("Old game available.\nDo you want to continue last game?", "Load Game", System.Windows.MessageBoxButton.OKCancel) == System.Windows.MessageBoxResult.OK)
                        isLoaded |= true;//|
                    else
                        isLoaded = false;
                }
            }

            if (isLoaded)
            {
                PhoneApplicationService.Current.State.Add("IsReload", true);
                PhoneApplicationService.Current.State.Add("RightScore", _pongScreen.RightScore);
                PhoneApplicationService.Current.State.Add("LeftScore", _pongScreen.LeftScore);
                PhoneApplicationService.Current.State.Add("isHumanTurn", isHumanTurn);
            }
        }

        /// <summary>
        /// Initializes the information necessary to start the game from the IsolatedStorage.
        /// </summary>
        /// <remarks>When we use the isolated storage is becasue the application was completely closed
        /// and we need to retrieve the state or the information from the .DAT file. It could have been closed by the user or by the phone. </remarks>
        /// <returns>Boolean indicating if the information was loaded.</returns>
        private bool LoadFromIsolatedStorage()
        {
            IsolatedStorageSettings isolatedStore = IsolatedStorageSettings.ApplicationSettings;
            try
            {
                _pongScreen.LeftScore = (int)isolatedStore["LeftScore"];
                _pongScreen.RightScore = (int)isolatedStore["RightScore"];
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Initializes the information necessary to start the game from the Phone State.
        /// </summary>
        /// <param name="isHumanTurn">Boolean indicating  wether the appliction was closed by the human or by the phone.</param>
        /// <returns>Boolean indicating if the information was loaded.</returns>
        private bool LoadFromStateObject(out bool isHumanTurn)
        {
            isHumanTurn = false;

            IDictionary<string, object> stateStore = PhoneApplicationService.Current.State;
            
            if (!stateStore.ContainsKey(FileName)) 
                return false;

            _pongScreen.RightScore = (int)stateStore["RightScore"];
            _pongScreen.LeftScore = (int)stateStore["LeftScore"];

            return true;
        }       
        /// <summary>
        /// Saves the current game state (if game is running) to Isolated Storage or State object
        /// </summary>
        /// <param name="isTombstoning">Indicates wheter the app is tombstoned</param>
        public void SaveActiveGameState(bool isTombstoning)
        {
            if (isTombstoning)
            {
                SaveToStateObject();
            }
            else
            {
                SaveToIsolatedStorageFile();
            }
        }
        /// <summary>
        /// Saves the gameplay screen data to Isolated storage file
        /// </summary>
        private void SaveToIsolatedStorageFile()
        {
            IsolatedStorageSettings isolatedStore = IsolatedStorageSettings.ApplicationSettings;
            isolatedStore.Remove("RightScore");
            isolatedStore.Remove("LeftScore");
            isolatedStore.Add("RightScore", _pongScreen.RightScore);
            isolatedStore.Add("LeftScore", _pongScreen.LeftScore);
            isolatedStore.Save();
        }
        /// <summary>
        /// Saves the gamepleay screen data to State Object
        /// </summary>
        private void SaveToStateObject()
        {
            IDictionary<string, object> stateStore = PhoneApplicationService.Current.State;
            stateStore.Remove("RightScore");
            stateStore.Remove("LeftScore");
            stateStore.Add("RightScore", _pongScreen.RightScore);
            stateStore.Add("LeftScore", _pongScreen.LeftScore);
        }

        #endregion
    }
}
