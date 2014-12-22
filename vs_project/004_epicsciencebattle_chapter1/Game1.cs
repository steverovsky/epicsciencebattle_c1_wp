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

    public class Game1 : Game {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D bgAdapter;
        Texture2D bgMenu;
        Texture2D bgScreen;
        BasicCharacter testCharacter;
        Vector2[] adapterSectionLeft;
        Vector2[] adapterSectionRight;
        bool [] stateAction;
        SpriteFont font;
        int prevAction = 0;
        
        enum Momentum { Negative, Neutral, Positive };
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
            stateAction = new bool[5] {false, false, false, false, false};
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
            base.Initialize ();
        }

        protected override void LoadContent () {
            spriteBatch = new SpriteBatch (GraphicsDevice);
            testCharacter.SpriteModel = Content.Load<Texture2D> ("spriteAction");
            bgAdapter = Content.Load<Texture2D> ("bg_adapter2");
            bgScreen = Content.Load<Texture2D> ("bg_sprite");
            bgMenu = Content.Load<Texture2D> ("bgMenu");
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
                    testCharacter.currentAction = 0;

                    int it;
                    
                    float speedCharacterMoving = 1f * (float) gameTime.ElapsedGameTime.TotalMilliseconds;

                    TouchCollection touchCollection = TouchPanel.GetState ();
                    foreach (TouchLocation tl in touchCollection) {
                        if ((tl.State == TouchLocationState.Pressed)|| (tl.State == TouchLocationState.Moved)) {
                            for (it = 0; it < adapterSectionLeft.Length; ++it) {
                                if (tl.Position.X >= adapterSectionLeft[it].X && tl.Position.X <= adapterSectionRight[it].X &&
                                    tl.Position.Y >= adapterSectionLeft[it].Y && tl.Position.Y <= adapterSectionRight[it].Y)  {
                                        break;
                                }
                            }


                           

                            switch (it) {
                                case 0:

                                    if (!stateAction[(int) CharacterActions.Up]) {
                                        stateAction[(int) CharacterActions.Up] = true;
                                        testCharacter.verticalMomentum = (int) Momentum.Positive;
                                        
                                    }
                                    testCharacter.currentAction = 1;
                                    
                                    break;
                                case 1:
                                    if ((testCharacter.PositionOnDisplay.X + 250 > 2560))
                                        break;
                                    //testCharacter.PositionOnDisplay.X += speedCharacterMoving;
                                    testCharacter.PositionOnDisplayAdd (new Vector2 (speedCharacterMoving, 0f));
                                    testCharacter.currentAction = 2;
                                    //activationMovementCamera (1, speedCharacterMoving);
                                    cam.activationMovementCamera (CharacterActions.Right, speedCharacterMoving, testCharacter, bgScreen.Width);
                                    break;
                                case 2:
                                    testCharacter.currentAction = 3;
                               
                                    break;
                                case 3:
                                    if (testCharacter.PositionOnDisplay.X < 0f)
                                        break;
                                    testCharacter.PositionOnDisplayAdd (new Vector2 (-1 * speedCharacterMoving, 0f));
                                    //testCharacter.positionOnDisplay.X -= speedCharacterMoving;
                                    testCharacter.currentAction = 4;
                                    cam.activationMovementCamera (CharacterActions.Left, speedCharacterMoving, testCharacter, bgScreen.Width);
                                    break;
                                
                               

                            }
                        }
                    }

                    

                   // System.Diagnostics.Debug.WriteLine (testCharacter.currentAction + " " +  prevAction);

                    if (testCharacter.currentAction != prevAction) {
                        testCharacter.currentFrame = 0;
                    }
                    prevAction = testCharacter.currentAction;

                    testCharacter.currentFrame += 0.005f * (float) gameTime.ElapsedGameTime.TotalMilliseconds;
                    if ((int) testCharacter.currentFrame == testCharacter.numberFramesAction[testCharacter.currentAction])
                        testCharacter.currentFrame = 0;
 
                    if (testCharacter.verticalMomentum == (int) Momentum.Positive) {
                        testCharacter.PositionOnDisplayAdd (new Vector2 ( 0f, -1 * speedCharacterMoving));
                        //testCharacter.PositionOnDisplay.Y -= speedCharacterMoving;
                        if ((testCharacter.DefaultPosition.Y - testCharacter.PositionOnDisplay.Y) >= testCharacter.verticalDelta) {
                            testCharacter.verticalMomentum = (int) Momentum.Negative;
                        }
                    }
                    if (testCharacter.verticalMomentum == (int) Momentum.Negative) {
                       // testCharacter.positionOnDisplay.Y += speedCharacterMoving;
                        testCharacter.PositionOnDisplayAdd (new Vector2 (0f, speedCharacterMoving));
                        if (testCharacter.PositionOnDisplay.Y >= testCharacter.DefaultPosition.Y) {
                            testCharacter.PositionOnDisplay = new Vector2 (testCharacter.PositionOnDisplay.X, testCharacter.DefaultPosition.Y);
                            testCharacter.verticalMomentum = (int) Momentum.Neutral;
                            stateAction[(int) CharacterActions.Up] = false;
                        }
                    }
                  //  if ((int) GraphicsDevice.Viewport.Width - (int) testCharacter.positionOnDisplay.X - 250 <= 50) {
                   //     offset += (int) testCharacter.positionOnDisplay.X - (int) GraphicsDevice.Viewport.Width;
                   // }
                    //System.Diagnostics.Debug.WriteLine (tt);
                     //)
                    // таймер задержки после прыжка
                    // defaultX вычитает высоту скина
                    // здесь не доходит несколько пилкселей, разобраться с порядком операторов в ифах
                  
                    
                    break;
            }
            
            base.Update (gameTime);
        }

        protected override void Draw (GameTime gameTime) {
            
           

            GraphicsDevice.Clear (Color.Black);
           // spriteBatch.Begin ();

            spriteBatch.Begin (SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        cam.GetTransformation ());

            switch (currentState) {
                case GameState.Menu:

                   // spriteBatch.DrawString (font, "build 002", new Vector2 (10, 10), Color.White);
                    spriteBatch.Draw (bgMenu, Vector2.Zero, Color.White);
                    break;
                case GameState.Fight:

                    spriteBatch.Draw (bgScreen, Vector2.Zero, Color.White);
                    int width = 250;
                    int height = 470;
                    int row = testCharacter.currentAction;
                    int column = (int) testCharacter.currentFrame;
                    
                    Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
                    //Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

                    // номер версии в меню
                    //spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);


                    spriteBatch.Draw (testCharacter.SpriteModel, testCharacter.PositionOnDisplay, sourceRectangle, Color.White);
                    spriteBatch.Draw (bgAdapter, cam.Offset, Color.White);
                    //spriteBatch.Draw ()
                    break;
            }
            
            spriteBatch.End ();
            base.Draw (gameTime);
        }
    }
}
