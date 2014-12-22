using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace _004_epicsciencebattle_chapter1 {
    class Camera2D {
        private float zoom;
        private Matrix transform;
        private float rotation;
        private Vector2 startPosition;
        private Vector2 offset;
        private GraphicsDevice graphicsDevice;

        public Vector2 Offset {
            get { return offset; }
            set { offset = value; }
        }

        public float Zoom {
            get { return zoom; }
            set { 
                zoom = value;
                if (zoom < 0.1f)
                    zoom = 0.1f; // негативный зум перевернёт изображение 
                }
        }

        public float Rotation {
            get { return rotation; }
            set { rotation = value; }
        }

        public void Move (Vector2 _delta) {
            offset += _delta;
        }

        public Matrix GetTransformation () {
            transform =
                Matrix.CreateTranslation (new Vector3 (-(startPosition + offset).X, -(startPosition + offset).Y, 0)) *
                Matrix.CreateScale (new Vector3 (Zoom, Zoom, 1)) *
                Matrix.CreateTranslation (new Vector3 (graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0)); // не кросс
            return transform;
        }

        public Camera2D (Vector2 _startPosition, GraphicsDevice _graphicsDevice) {
            this.zoom = 1.0f;
            this.rotation = 0f;
            this.startPosition = _startPosition;
            this.graphicsDevice = _graphicsDevice;
        }

        public void activationMovementCamera (CharacterActions _type, float _speedMoving, BasicCharacter _character, int _fieldWidth) {
            float boundaryDistance = 50f;
            switch (_type) {
                case CharacterActions.Left:
                    float leftBoundaryCamera = Offset.X;
                    float leftBoundaryCharacter = _character.PositionOnDisplay.X;
                    if (leftBoundaryCamera - _speedMoving < 0f) {
                        _speedMoving = leftBoundaryCamera;
                    }
                    if ((leftBoundaryCharacter - leftBoundaryCamera) <= boundaryDistance) {
                        offset.X -= _speedMoving;
                    }
                    break;
                case CharacterActions.Right:
                    float rightBoundaryCamera = graphicsDevice.Viewport.Width + Offset.X;
                    float rightBoundaryCharacter = _character.PositionOnDisplay.X + _character.SizeTexture.X;
                    if ((rightBoundaryCamera + _speedMoving) > _fieldWidth) {
                        _speedMoving = _fieldWidth - rightBoundaryCamera;
                    }
                    if ((rightBoundaryCamera - rightBoundaryCharacter) <= boundaryDistance) {
                        offset.X += _speedMoving;
                    }
                    break;
            }
        }
    }
}
