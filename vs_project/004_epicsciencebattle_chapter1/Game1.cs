using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;
using Windows.UI.ViewManagement;
using System.Diagnostics;
using System.Collections.Generic;

namespace _004_epicsciencebattle_chapter1 {

    enum MainState { InitGame, PromoGame, Menu, Break };
    
    enum GameState { Menu, Fight, Pause };
   
    public class Game1 : Game {
        private SpriteFont font;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        
        
        BasicCharacter testCharacter;
        public Control movingControl;
        GameState currentState;
      public Camera2D camera;
        public float timePause;
        MenuGame menu;
        MainState currentMainState;

        public Game1 () {
            graphics = new GraphicsDeviceManager (this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize () {
            StatusBar.GetForCurrentView ().HideAsync ();
            timePause = 0f;
            Tools.camera = new Camera2D (new Vector2 (GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), GraphicsDevice);
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft;
            testCharacter = new BasicCharacter ();
            currentState = GameState.Menu;
            movingControl = new Control (new Vector2 (50f, 418f), 75f, 150f);
            currentMainState = MainState.InitGame;
            base.Initialize ();
        }

        protected override void LoadContent () {
            spriteBatch = new SpriteBatch (GraphicsDevice);
            Tools.bgCharacter = testCharacter.SpriteModel = Content.Load<Texture2D> ("spriteAction");
            Tools.bgControl =  movingControl.SpriteModel = Content.Load<Texture2D> ("movingControlSprite");
            Tools.bgScreen = Content.Load<Texture2D> ("bg_sprite");
            Tools.bgMenu = Content.Load<Texture2D> ("bgMenu");
            font = Content.Load<SpriteFont>("MyFont");
        }

        public void displayMenu () {
            TouchCollection currentTouch = TouchPanel.GetState ();
            foreach (TouchLocation element in currentTouch) {
                if (element.State == TouchLocationState.Pressed || element.State == TouchLocationState.Moved) {
                    if (element.Position.X >= 192 && element.Position.X <= 640 && element.Position.Y >= 384 && element.Position.Y <= 573) {
                        currentState = GameState.Fight;
                        break;
                    }
                }
            }
        }

        public bool getPressPause () {
            TouchCollection currentTouch = TouchPanel.GetState();
            foreach (TouchLocation element in currentTouch)
            {
                if (element.State == TouchLocationState.Pressed || element.State == TouchLocationState.Moved)
                {
                    if (element.Position.X >= 0 && element.Position.X <= 100 && element.Position.Y >= 0 && element.Position.Y <= 100)
                    {
                        return true;
                    }
                }
            }
           
            return false;
        }

        protected override void Update (GameTime gameTime) {

      
             
            
            switch (currentMainState) {
                case MainState.InitGame:
                    currentMainState = MainState.PromoGame;
                    break;
                case MainState.PromoGame:
                    menu = new MenuGame(Tools.bgMenu);
                    currentMainState = MainState.Menu;
                    break;
                case MainState.Menu:
                  //  System.Diagnostics.Debug.WriteLine(2);
                    if (!menu.Update(gameTime)) {

                        currentMainState = MainState.Break;
                    }
                    break;
                case MainState.Break:
                    break;
            }
            /*
            switch (currentState) {
                case GameState.Menu:
                    displayMenu ();
                    break;
                case GameState.Fight:
                    testCharacter.update (gameTime, cam, bgScreen, movingControl);
                    if (getPressPause ())
                    {
                        currentState = GameState.Pause;
                    }
                    break;
                case GameState.Pause:
                    timePause += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (timePause >= 0.5f)
                    {
                        if (getPressPause())
                        {
                            timePause = 0f;
                            currentState = GameState.Fight;
                        }
                    }
                    break;
            }
            */
            base.Update (gameTime);
        }

        protected override void Draw (GameTime gameTime) {
            GraphicsDevice.Clear (Color.Black);
            spriteBatch.Begin (SpriteSortMode.BackToFront, BlendState.AlphaBlend,
                               null, null, null, null, Tools.camera.GetTransformation ());

            
            switch (currentMainState) {
                case MainState.InitGame:
                    
                    break;
                case MainState.PromoGame:
                    
                    break;
                case MainState.Menu:
                    //spriteBatch.Draw(bgMenu, Vector2.Zero, Color.White);
                    menu.Draw(spriteBatch);
                    break;
                case MainState.Break:
                    break;
            }
           // spriteBatch.DrawString(font, "Score", new Vector2(100, 100), Color.Red);
            /*
            switch (currentState) {
                case GameState.Menu:
                  //  spriteBatch.DrawString(font, "Score", new Vector2(100, 100), Color.Black);
                    spriteBatch.Draw (bgMenu, Vector2.Zero, Color.White);
                    break;
                case GameState.Fight:
                    spriteBatch.Draw (bgScreen, Vector2.Zero, Color.White);
                    testCharacter.Draw (spriteBatch);
                    spriteBatch.Draw (movingControl.SpriteModel, movingControl.PositionOnDisplay + cam.Offset, Color.White);
                    break;
            }
            */
            spriteBatch.End ();
            base.Draw (gameTime);
        }
    }
}
