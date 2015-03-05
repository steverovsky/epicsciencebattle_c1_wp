using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;
using Windows.UI.ViewManagement;
using System.Diagnostics;
using System.Collections.Generic;

namespace _004_epicsciencebattle_chapter1 {
    
    public enum Momentum { Negative, Neutral, Positive };

    public class BasicCharacter : BasicModel {
        private Vector2 delta;
        private Vector2 momentum;
        private float currentFrame;
        private CharacterActions currentAction;
        private CharacterActions previousAction;
        private int[] numberFramesAction;
        private bool[] stateAction;
        private float health;
        private string characterName;

        public BasicCharacter () {
            this.defaultPosition = new Vector2 (0, 245f);
            this.positionOnDisplay = defaultPosition;
            this.delta = new Vector2 (500f, 250f);
            this.momentum = new Vector2 (0f, 0f);
            this.currentFrame = 0;
            this.currentAction = CharacterActions.Nothing;
            this.numberFramesAction = new int[6] { 5, 4, 3, 1, 3, 5};
            sizeTexture = new Vector2 (250f, 470f);
            stateAction = new bool[5] { false, false, false, false, false };
            previousAction = CharacterActions.Nothing;
        }

        public void calculationFrame (GameTime _gameTime) {
            if (currentAction != previousAction) {
                currentFrame = 0;
            }
            previousAction = currentAction;
            currentFrame += 0.005f * (float) _gameTime.ElapsedGameTime.TotalMilliseconds;
            if ((int) currentFrame == numberFramesAction[(int) currentAction])
                currentFrame = 0;
        }

        public void performingJjump (float _speedCharacterMoving) {
            if (momentum.Y == (int) Momentum.Positive) {
                PositionOnDisplayAdd (new Vector2 (0f, -1 * _speedCharacterMoving));
                if ((DefaultPosition.Y - PositionOnDisplay.Y) >= delta.Y) {
                    momentum.Y = (int) Momentum.Negative;
                }
            }
            if (momentum.Y == (int) Momentum.Negative) {
                PositionOnDisplayAdd (new Vector2 (0f, _speedCharacterMoving));
                if (PositionOnDisplay.Y >= DefaultPosition.Y) {
                    PositionOnDisplay = new Vector2 (PositionOnDisplay.X, DefaultPosition.Y);
                    momentum.Y = (int) Momentum.Neutral;
                    stateAction[(int) CharacterActions.Up] = false;
                }
            }
        }

        public void update (GameTime _gameTime, Camera2D cam, Texture2D bgScreen, Control _movingControl) {
            currentAction = CharacterActions.Nothing;
            float speedCharacterMoving = 1f * (float) _gameTime.ElapsedGameTime.TotalMilliseconds;
            switch (currentAction = _movingControl.getSectionNumber ()) {
                case CharacterActions.Up:
                    if (!stateAction[(int) CharacterActions.Up]) {
                        stateAction[(int) CharacterActions.Up] = true;
                        momentum.Y = (int) Momentum.Positive;
                    }
                    break;
                case CharacterActions.Right:
                    if ((PositionOnDisplay.X + 250 > 2560))
                        break;
                    PositionOnDisplayAdd (new Vector2 (speedCharacterMoving, 0f));
                    cam.activationMovementCamera (CharacterActions.Right, speedCharacterMoving, this, bgScreen.Width);
                    break;
               case CharacterActions.Left:
                   if (PositionOnDisplay.X < 0f)
                       break;
                   PositionOnDisplayAdd (new Vector2 (-1 * speedCharacterMoving, 0f));
                   cam.activationMovementCamera (CharacterActions.Left, speedCharacterMoving, this, bgScreen.Width);
                   break;
            }
            
            performingJjump (speedCharacterMoving);
            calculationFrame (_gameTime);
        }

        public void Draw (SpriteBatch _spriteBatch) {
            Point startTexture = new Point ((int) sizeTexture.X * (int) currentFrame, (int) sizeTexture.Y * (int) currentAction);
            Rectangle sourceRectangle = new Rectangle (startTexture.X, startTexture.Y, (int) sizeTexture.X, (int) sizeTexture.Y);
            _spriteBatch.Draw (spriteModel, positionOnDisplay, sourceRectangle, Color.White);
        }

    }
}
