using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace _004_epicsciencebattle_chapter1 {
    public class BasicModel {
        protected Vector2 positionOnDisplay;
        protected Vector2 defaultPosition;
        protected Vector2 sizeTexture;
        protected Texture2D spriteModel;

        public Vector2 PositionOnDisplay {
            get { return positionOnDisplay; }
            set { positionOnDisplay = value; }
        }

        public void PositionOnDisplayAdd (Vector2 _position) {
            positionOnDisplay += _position;
        }

        public virtual Texture2D SpriteModel {
            get { return spriteModel; }
            set { spriteModel = value; }
        }

        public Vector2 SizeTexture {
            get { return sizeTexture; }
        }

        public Vector2 DefaultPosition {
            get { return defaultPosition; }
        }
    }
}
