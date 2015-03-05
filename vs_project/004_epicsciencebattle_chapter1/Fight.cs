using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;
using Windows.UI.ViewManagement;
using System.Diagnostics;
using System.Collections.Generic;

namespace _004_epicsciencebattle_chapter1 {
    enum FightState { StartFight, Fight, EndingFight, Pause, Combo };
    class Fight {
        private int leftCharacterID;
        private int rightCharacterID;
        private int roomID;
        private Texture2D backgroundRoom;
        private BasicCharacter leftCharacter;
        Control movingControl;
        Camera2D camera;
        public Fight (int _leftCharacterID, int _rightCharacterID, int _roomID, Texture2D _backgroundRoom) {
            this.leftCharacterID = _leftCharacterID;
            this.rightCharacterID = _rightCharacterID;
            this.roomID = _roomID;
            this.backgroundRoom = _backgroundRoom;
            this.leftCharacter = new BasicCharacter();
            leftCharacter.SpriteModel = Tools.bgCharacter;

            this.camera = Tools.camera;
            this.movingControl = new Control(new Vector2(50f, 418f), 75f, 150f);
            movingControl.SpriteModel = Tools.bgControl;
        }
        public void Update (GameTime _gameTime) {
            leftCharacter.update(_gameTime, Tools.camera, backgroundRoom, movingControl);
        }
        public void Draw (SpriteBatch _spriteBatch) {
            _spriteBatch.Draw (backgroundRoom, Vector2.Zero, Color.White);
            leftCharacter.Draw(_spriteBatch);
            _spriteBatch.Draw(movingControl.SpriteModel, movingControl.PositionOnDisplay + Tools.camera.Offset, Color.White);
        }
    }
}
