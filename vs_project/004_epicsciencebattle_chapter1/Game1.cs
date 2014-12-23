using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;
using Windows.UI.ViewManagement;
using System.Diagnostics;
using System.Collections.Generic;

namespace _004_epicsciencebattle_chapter1 {

    enum GameState { Menu, Fight };
    public enum CharacterActions { Nothing, Up, Right, Down, Left };
    public enum Momentum { Negative, Neutral, Positive };

    public class Game1 : Game {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D bgAdapter;
        Texture2D bgMenu;
        public Texture2D bgScreen;
        BasicCharacter testCharacter;
        Vector2[] adapterSectionLeft;
        Vector2[] adapterSectionRight;
        
        SpriteFont font;

        private Control movingControl;
        Texture2D point;
        GameState currentState;
        Camera2D cam;
        public Game1 () {
            graphics = new GraphicsDeviceManager (this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize () {
            StatusBar.GetForCurrentView ().HideAsync ();
            cam = new Camera2D (new Vector2 (GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), GraphicsDevice);
            //offset = 0f;
            
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft;
            testCharacter = new BasicCharacter ();
            currentState = GameState.Menu;
            adapterSectionLeft = new Vector2 [4] { 
                new Vector2 (150, 418), 
                new Vector2 (250, 518),
                new Vector2 (150, 618), 
                new Vector2 (50, 518) 
                
            };
            adapterSectionRight = new Vector2[4] {
                new Vector2 (250, 518),
                new Vector2 (350, 618),
                new Vector2 (250, 718),
                new Vector2 (150, 618)
                
            };
            movingControl = new Control (new Vector2 (50f, 418f), 75f, 150f);
            base.Initialize ();
        }

        protected override void LoadContent () {
            spriteBatch = new SpriteBatch (GraphicsDevice);
            testCharacter.SpriteModel = Content.Load<Texture2D> ("spriteAction");
            movingControl.SpriteModel = Content.Load<Texture2D> ("movingControlSprite");
            bgAdapter = Content.Load<Texture2D> ("bg_adapter_c");
            bgScreen = Content.Load<Texture2D> ("bg_sprite");
            bgMenu = Content.Load<Texture2D> ("bgMenu");
            point = Content.Load<Texture2D> ("point");
        }

        protected override void Update (GameTime gameTime) {
          
            switch (currentState) { 

                case GameState.Menu:
                    TouchCollection currentTouch = TouchPanel.GetState ();
                    foreach (TouchLocation element in currentTouch) {
                        if (element.State == TouchLocationState.Pressed || element.State == TouchLocationState.Moved) {
                            if (element.Position.X >= 192 && element.Position.X <= 640 && element.Position.Y >= 384 && element.Position.Y <= 573) {
                                currentState = GameState.Fight;
                                break;
                            } 
                        }
                    }
                    break;

                case GameState.Fight:
                   // testCharacter.update (gameTime, adapterSectionLeft, adapterSectionRight, cam, bgScreen);
                    testCharacter.update (gameTime, adapterSectionLeft, adapterSectionRight, cam, bgScreen, movingControl);
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
                    int width = 250;
                    int height = 470;
                    int row = testCharacter.currentAction;
                    int column = (int) testCharacter.currentFrame;
                    Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
                    spriteBatch.Draw (testCharacter.SpriteModel, testCharacter.PositionOnDisplay, sourceRectangle, Color.White);
                    //spriteBatch.Draw (bgAdapter, cam.Offset, Color.White);
                    spriteBatch.Draw (movingControl.SpriteModel, movingControl.PositionOnDisplay + cam.Offset, Color.White);
                    float d = 1.41f * 150f / 2;
                    spriteBatch.Draw (point, movingControl.centerPosition - new Vector2 (2, 2), Color.White);
                    spriteBatch.Draw (point, movingControl.centerPosition - new Vector2 (2, 2) + new Vector2 (d, d), Color.White);
                    spriteBatch.Draw (point, movingControl.centerPosition - new Vector2 (2, 2) + new Vector2 (-d, d), Color.White);

                    spriteBatch.Draw (point, movingControl.centerPosition - new Vector2 (2, 2) + new Vector2 (d, d), Color.White);
                    spriteBatch.Draw (point, movingControl.centerPosition - new Vector2 (2, 2) + new Vector2 (-d, d), Color.White);
                    spriteBatch.Draw (point, movingControl.centerPosition - new Vector2 (2, 2) + new Vector2 (0, 150), Color.White);
                    break;
            }
            spriteBatch.End ();
            base.Draw (gameTime);
        }
    }
}
