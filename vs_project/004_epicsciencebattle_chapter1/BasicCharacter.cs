using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace _004_epicsciencebattle_chapter1 {
    
    class BasicCharacter : BasicModel {
        public float verticalDelta;
        public float horizontalDelta;
        public sbyte verticalMomentum;
        public sbyte horizontalMomentum;
        public float currentFrame;
        public int currentAction;
        public int[] numberFramesAction;
        public int Rows { get; set; }
        public int Columns { get; set; }
        bool[] stateAction;
        int prevAction = 0;

        

        public BasicCharacter () {
            this.defaultPosition = new Vector2 (0, 245f);
            this.positionOnDisplay = defaultPosition;
            this.verticalDelta = 250f;
            this.verticalMomentum = 0;
            this.horizontalMomentum = 0;
            this.horizontalDelta = 500f;
            this.currentFrame = 0;
            this.currentAction = 0;
            this.numberFramesAction = new int[5] { 5, 4, 3, 1, 3};
            sizeTexture = new Vector2 (250f, 470f);
            stateAction = new bool[5] { false, false, false, false, false };
        }

        public int getSectionNumberLeftAdapter (TouchCollection _touchCollection) {
            bool inAdapter = false;

            foreach (TouchLocation tl in _touchCollection) {
                if ((tl.State == TouchLocationState.Pressed) || (tl.State == TouchLocationState.Moved)) {
                    if (!(Math.Sqrt (Math.Pow (tl.Position.X - 192, 2) + Math.Pow (tl.Position.Y - 568, 2)) < 75) &&
                        Math.Sqrt (Math.Pow (tl.Position.X - 192, 2) + Math.Pow (tl.Position.Y - 568, 2)) < 150) {
                            //System.Diagnostics.Debug.WriteLine ("x: " + tl.Position.X + " y: " + tl.Position.Y);
                            if (tl.Position.X > 200 && tl.Position.Y > 568)
                                return 1;
                            if (tl.Position.X < 200 && tl.Position.Y > 568)
                                return 2;
                            if (tl.Position.X < 200 && tl.Position.Y < 568)
                                return 3;
                            if (tl.Position.X > 200 && tl.Position.Y < 568)
                                return 4;
                    }
                }
            }
            return 0;
        }

        public void update (GameTime _gameTime, Vector2[] adapterSectionLeft, Vector2[] adapterSectionRight, Camera2D cam, Texture2D bgScreen, Control _movingControl) {
            currentAction = 0;

            int it;
            
            float speedCharacterMoving = 1f * (float) _gameTime.ElapsedGameTime.TotalMilliseconds;

            TouchCollection touchCollection = TouchPanel.GetState ();


            switch (_movingControl.getSectionNumber ()) {
                        case 1:

                            if (!stateAction[(int) CharacterActions.Up]) {
                                stateAction[(int) CharacterActions.Up] = true;
                                verticalMomentum = (int) Momentum.Positive;

                            }
                            currentAction = 1;

                            break;
                        case 2:
                            if ((PositionOnDisplay.X + 250 > 2560))
                                break;
                            PositionOnDisplayAdd (new Vector2 (speedCharacterMoving, 0f));
                            currentAction = 2;
                            cam.activationMovementCamera (CharacterActions.Right, speedCharacterMoving, this, bgScreen.Width);
                            break;
                        case 3:
                            currentAction = 3;

                            break;
                        case 4:
                            if (PositionOnDisplay.X < 0f)
                                break;
                            PositionOnDisplayAdd (new Vector2 (-1 * speedCharacterMoving, 0f));
                            currentAction = 4;
                            cam.activationMovementCamera (CharacterActions.Left, speedCharacterMoving, this, bgScreen.Width);
                            break;

                    }
                
  

            if (currentAction != prevAction) {
                currentFrame = 0;
            }
            prevAction = currentAction;

            currentFrame += 0.005f * (float) _gameTime.ElapsedGameTime.TotalMilliseconds;
            if ((int) currentFrame == numberFramesAction[currentAction])
                currentFrame = 0;

            if (verticalMomentum == (int) Momentum.Positive) {
                PositionOnDisplayAdd (new Vector2 (0f, -1 * speedCharacterMoving));
                if ((DefaultPosition.Y - PositionOnDisplay.Y) >= verticalDelta) {
                    verticalMomentum = (int) Momentum.Negative;
                }
            }
            if (verticalMomentum == (int) Momentum.Negative) {
                PositionOnDisplayAdd (new Vector2 (0f, speedCharacterMoving));
                if (PositionOnDisplay.Y >= DefaultPosition.Y) {
                    PositionOnDisplay = new Vector2 (PositionOnDisplay.X, DefaultPosition.Y);
                    verticalMomentum = (int) Momentum.Neutral;
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
        }

    }
}
