using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

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
        }

    }
}
