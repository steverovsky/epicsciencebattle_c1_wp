using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace _004_epicsciencebattle_chapter1 {
    class BasicCharacter {
        private Texture2D textureCharacter;
        public Vector2 positionOnDisplay;
        public float defaultY;
        public float verticalDelta;
        public float horizontalDelta;
        public sbyte verticalMomentum;
        public sbyte horizontalMomentum;
        public float currentFrame;
        public int currentAction;
        public int[] numberFramesAction;
        public int Rows { get; set; }
        public int Columns { get; set; }
        private Vector2 sizeCharacter;

        public Vector2 SizeCharacter {
            get { return sizeCharacter; }
        }

        public Texture2D TextureCharacter {
            set { textureCharacter = value; }
            get { return textureCharacter; }
        }

        public Vector2 PositionOnDisplay {
            set { positionOnDisplay = value; }
            get { return positionOnDisplay; }
        }

        public BasicCharacter () {
            this.defaultY = 245f;
            this.positionOnDisplay = new Vector2 (0, this.defaultY);
            this.verticalDelta = 250f;
            this.verticalMomentum = 0;
            this.horizontalMomentum = 0;
            this.horizontalDelta = 500f;
            this.currentFrame = 0;
            this.currentAction = 0;
            this.numberFramesAction = new int[5] { 5, 4, 3, 1, 3};
            this.sizeCharacter = new Vector2 (250f, 470f);
        }

        

        public void Update () {

        }

    }
}
