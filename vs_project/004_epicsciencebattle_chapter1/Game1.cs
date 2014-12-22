﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;
using Windows.UI.ViewManagement;
using System.Diagnostics;
using System.Collections.Generic;

namespace _004_epicsciencebattle_chapter1 {

    enum GameState { Menu, Fight };

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
        int prevAction = 0;
       // int offset = 0;
        enum CharacterActions { Nothing, Up, Right, Down, Left };
        enum Momentum { Negative, Neutral, Positive };
        GameState currentState;
        Camera2D cam;
        float offset;
        public Game1 () {
            graphics = new GraphicsDeviceManager (this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize () {
            StatusBar.GetForCurrentView ().HideAsync ();
            cam = new Camera2D ();
            offset = 0f;
            cam.Pos = new Vector2 (640f, 384f); // camera on center 
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
            testCharacter.TextureCharacter = Content.Load <Texture2D> ("spriteAction");
            bgAdapter = Content.Load<Texture2D> ("bg_adapter2");
            bgScreen = Content.Load<Texture2D> ("bg_sprite");
            bgMenu = Content.Load<Texture2D> ("bgMenu");
        }

        public void activationMovementCamera (int _type, float _speedMoving) {
            float boundaryDistance = 50f;
            switch (_type) {
                case 0:
                    float leftBoundaryCamera = 0f + offset;
                    float leftBoundaryCharacter = testCharacter.positionOnDisplay.X;
                    if (leftBoundaryCamera - _speedMoving < 0f) {
                        _speedMoving = leftBoundaryCamera;
                    }
                    if ((leftBoundaryCharacter - leftBoundaryCamera) <= boundaryDistance) {
                        offset -= _speedMoving;
                        cam.Move (new Vector2 (-1 * _speedMoving, 0f));
                    }
                    break;
                case 1:
                    float rightBoundaryCamera = 1280f + offset;
                    float rightBoundaryCharacter = testCharacter.positionOnDisplay.X + 250;
                    if ((rightBoundaryCamera + _speedMoving) > 2560) {
                        _speedMoving = 2560 - rightBoundaryCamera;
                    }
                    if ((rightBoundaryCamera - rightBoundaryCharacter) <= boundaryDistance) {
                        offset += _speedMoving;
                        cam.Move (new Vector2 (_speedMoving, 0f));
                    }
                    break;
            }
        }

        protected override void Update (GameTime gameTime) {
            GameState a = GameState.Fight;
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
                                    if ((testCharacter.positionOnDisplay.X + 250 > 2560))
                                        break;
                                    testCharacter.positionOnDisplay.X += speedCharacterMoving;
                                    testCharacter.currentAction = 2;
                                    activationMovementCamera (1, speedCharacterMoving);
                                    break;
                                case 2:
                                    testCharacter.currentAction = 3;
                               
                                    break;
                                case 3:
                                    if (testCharacter.positionOnDisplay.X < 0f)
                                        break;
                                    testCharacter.positionOnDisplay.X -= speedCharacterMoving;
                                    testCharacter.currentAction = 4;
                                    activationMovementCamera (0, speedCharacterMoving);
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
                        testCharacter.positionOnDisplay.Y -= speedCharacterMoving;
                        if ((testCharacter.defaultY - testCharacter.positionOnDisplay.Y) >= testCharacter.verticalDelta) {
                            testCharacter.verticalMomentum = (int) Momentum.Negative;
                        }
                    }
                    if (testCharacter.verticalMomentum == (int) Momentum.Negative) {
                        testCharacter.positionOnDisplay.Y += speedCharacterMoving;
                        if (testCharacter.positionOnDisplay.Y >= testCharacter.defaultY) {
                            testCharacter.positionOnDisplay.Y = testCharacter.defaultY;
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
                        cam.getTransformation (GraphicsDevice));

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
                    //Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

                    // номер версии в меню
                    //spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);


                    spriteBatch.Draw (testCharacter.TextureCharacter, testCharacter.positionOnDisplay, sourceRectangle, Color.White);
                    spriteBatch.Draw (bgAdapter, new Vector2 (offset, 0), Color.White);
                    //spriteBatch.Draw ()
                    break;
            }
            
            spriteBatch.End ();
            base.Draw (gameTime);
        }
    }
}
