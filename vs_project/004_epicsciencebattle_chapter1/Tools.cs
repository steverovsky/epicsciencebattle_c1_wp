using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;
using Windows.UI.ViewManagement;
using System.Diagnostics;
using System.Collections.Generic;

namespace _004_epicsciencebattle_chapter1 {
    static class Tools {

        public static Texture2D bgMenu;
        public static Texture2D bgScreen;
        public static Control control;
        public static Camera2D camera;
        public static Texture2D bgCharacter;
        public static Texture2D bgControl;

        public static bool EntryIntoCircle (Vector2 _point, Vector2 _centerCircle, float _radius) {
            return false;
        }

        public static bool EntryIntoTriangle (Vector2 _point, Vector2 [] _pointTriangle) {
            return false;
        }

        public static bool EntryIntoRectangle (Vector2 _point, Vector2 _pointA, Vector2 _pointB) {
            if (_point.X >= _pointA.X && _point.X < _pointB.X &&
                _point.Y >= _pointA.Y && _point.Y < _pointB.Y)
                return true;
            return false;
        }

        public static void TestFunction () {

        }
    }
}
