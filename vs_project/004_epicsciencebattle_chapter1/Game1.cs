using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;
using Windows.UI.ViewManagement;
using System.Diagnostics;
using System.Collections.Generic;

namespace _004_epicsciencebattle_chapter1 {

    enum GameState { Menu, Fight, Pause };

    

    public class Game1 : Game {
 
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Texture2D bgMenu;
        public Texture2D bgScreen;
        BasicCharacter testCharacter;
        private Control movingControl;
        GameState currentState;
        Camera2D cam;
        public float timePause;

        public Game1 () {
            graphics = new GraphicsDeviceManager (this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize () {
            StatusBar.GetForCurrentView ().HideAsync ();
            timePause = 0f;
            cam = new Camera2D (new Vector2 (GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), GraphicsDevice);
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft;
            testCharacter = new BasicCharacter ();
            currentState = GameState.Menu;
            movingControl = new Control (new Vector2 (50f, 418f), 75f, 150f);
            base.Initialize ();
        }

        protected override void LoadContent () {
            spriteBatch = new SpriteBatch (GraphicsDevice);
            testCharacter.SpriteModel = Content.Load<Texture2D> ("spriteAction");
            movingControl.SpriteModel = Content.Load<Texture2D> ("movingControlSprite");
            bgScreen = Content.Load<Texture2D> ("bg_sprite");
            bgMenu = Content.Load<Texture2D> ("bgMenu");
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
            base.Update (gameTime);
        }

        protected override void Draw (GameTime gameTime) {
            GraphicsDevice.Clear (Color.Black);
            spriteBatch.Begin (SpriteSortMode.BackToFront, BlendState.AlphaBlend,
                               null, null, null, null, cam.GetTransformation ());
            switch (currentState) {
                case GameState.Menu:
                    spriteBatch.Draw (bgMenu, Vector2.Zero, Color.White);
                    break;
                case GameState.Fight:
                    spriteBatch.Draw (bgScreen, Vector2.Zero, Color.White);
                    testCharacter.Draw (spriteBatch);
                    spriteBatch.Draw (movingControl.SpriteModel, movingControl.PositionOnDisplay + cam.Offset, Color.White);
                    break;
            }
            spriteBatch.End ();
            base.Draw (gameTime);
        }
    }
}
