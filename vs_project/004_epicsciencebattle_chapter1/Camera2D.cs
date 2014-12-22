using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace _004_epicsciencebattle_chapter1 {
    class Camera2D {
        protected float zoom;
        public Matrix transform;
        public Vector2 position;
        protected float rotation;

        public float Zoom {
            get { return zoom; }
            set { zoom = value;
            if (zoom < 0.1f)
                zoom = 0.1f; // негативный зум перевернёт изображение 
            }
        }

        public float Rotation {
            get { return rotation; }
            set { rotation = value; }
        }

        public void Move (Vector2 amount) {
            position += amount;

        }

        public Vector2 Pos {
            get { return position; }
            set { position = value; }
        }

        public Matrix getTransformation (GraphicsDevice _graphicsDevice) {
            transform =
                Matrix.CreateTranslation (new Vector3 (-position.X, -position.Y, 0)) *
                Matrix.CreateScale (new Vector3 (Zoom, Zoom, 1)) *
                Matrix.CreateTranslation (new Vector3 (_graphicsDevice.Viewport.Width * 0.5f, _graphicsDevice.Viewport.Height * 0.5f, 0)); // не кросс
            return transform;
        }

        public Camera2D () {
            this.zoom = 1.0f;
            this.rotation = 0f;
            this.position = Vector2.Zero;
        }
    }
}
