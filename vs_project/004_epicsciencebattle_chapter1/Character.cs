using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _004_epicsciencebattle_chapter1 {
    enum CharacterState { Alive, Dies };
    class Character : BasicCharacter {
        private bool mainPosition;
        private CharacterActions currentAction;
        private CharacterActions previousAction;
        public Character (bool _mainPosition) {
            mainPosition = _mainPosition;
            if (mainPosition) {
                defaultPosition = new Vector2(0, 245f);
            } else {
                defaultPosition = new Vector2(500f, 245f);
            }
            positionOnDisplay = defaultPosition;
        }

    }
}
