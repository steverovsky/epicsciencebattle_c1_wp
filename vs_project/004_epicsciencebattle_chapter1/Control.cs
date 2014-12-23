using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;

namespace _004_epicsciencebattle_chapter1 {
    class Control : BasicModel {

        private float innerRadius;
        private float outerRadius;
        public Vector2 centerPosition;

        public override Texture2D SpriteModel {
            set {
                spriteModel = value;
                this.sizeTexture = new Vector2 (this.spriteModel.Width, this.spriteModel.Height);
            }
            get { return spriteModel; }
        }

        public Control (Vector2 _defaultPosition, float _innertRadius, float _outerRadius) {
            this.defaultPosition = _defaultPosition;
            this.positionOnDisplay = this.defaultPosition;     
            this.innerRadius = _innertRadius;
            this.outerRadius = _outerRadius;
            this.centerPosition = new Vector2 (this.positionOnDisplay.X + this.outerRadius, this.positionOnDisplay.Y + this.outerRadius);
        }

        public float CrossProduct (Vector2 a, Vector2 b) {
            return a.X * b.Y - a.Y * b.X;
        }

        public float dot (Vector2 a, Vector2 b) {
            return a.X * b.X + a.Y * b.Y;
        }

        public bool f (Vector2 A, Vector2 B, Vector2 C, Vector2 P) {
            // Compute vectors  
            Vector2 v0 = C - A;
            Vector2 v1 = B - A;
            Vector2 v2 = P - A;

            // Compute dot products
            float dot00 = dot(v0, v0);
            float dot01 = dot(v0, v1);
            float dot02 = dot(v0, v2);
            float dot11 = dot(v1, v1);
            float dot12 = dot (v1, v2);

            // Compute barycentric coordinates
            float invDenom = 1 / (dot00 * dot11 - dot01 * dot01);
            float u = (dot11 * dot02 - dot01 * dot12) * invDenom;
            float v = (dot00 * dot12 - dot01 * dot02) * invDenom;

            return (u >= 0) && (v >= 0) && (u + v < 1);
        }

        float sign (Vector2 p1, Vector2 p2, Vector2 p3) {
            return (p1.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p1.Y - p3.Y);
        }

        bool PointInTriangle (Vector2 pt, Vector2 v1, Vector2 v2, Vector2 v3) {
            bool b1, b2, b3;

            b1 = sign (pt, v1, v2) < 0.0f;
            b2 = sign (pt, v2, v3) < 0.0f;
            b3 = sign (pt, v3, v1) < 0.0f;

            return ((b1 == b2) && (b2 == b3));
        }

        public bool EntryIntoTriangle (Vector2 _a, Vector2 _b, Vector2 _c, Vector2 _point) {
            float a = (_a.X - _point.X) * (_b.Y - _a.Y) - (_b.X - _a.X) * (_a.Y - _point.Y);
            float b = (_b.X - _point.X) * (_c.Y - _b.Y) - (_c.X - _b.X) * (_b.Y - _point.Y);
            float c = (_c.X - _point.X) * (_a.Y - _c.Y) - (_a.X - _c.X) * (_c.Y - _point.Y);
            // проверить случай, когда точка на линии треугольника
            if ((a >= 0 && b >= 0 && c >= 0) || (a <= 0 && b <= 0 && c <= 0)) 
                return true;
            return false;
        }

        public int getSectionNumber (Vector2 _point) {
            bool pointInTriangle = false;
            float d = 1.41f * outerRadius / 2;

            for (int section = 1; section <= 4; ++section) {
                switch (section) {
                    case 1:
                        //System.Diagnostics.Debug.WriteLine (centerPosition.X + " " + centerPosition.Y);
                        pointInTriangle = PointInTriangle (_point, centerPosition, 
                                           new Vector2 (centerPosition.X + d, centerPosition.Y + d), 
                                           new Vector2 (centerPosition.X - d, centerPosition.Y + d)
                                           ) || PointInTriangle (_point, new Vector2 (centerPosition.X + d, centerPosition.Y + d), 
                                                                         new Vector2 (centerPosition.X - d, centerPosition.Y - d),
                                                                         new Vector2 (centerPosition.X, centerPosition.Y + outerRadius)
                                                                         );
                        // вынести проверку вхождения не в маленький круг вне свитча
                        // просто запомнить секцию и брейкаться
                        if (pointInTriangle &&
                           !((Math.Sqrt (Math.Pow (_point.X - centerPosition.X, 2) + Math.Pow (_point.Y - centerPosition.Y, 2))) < innerRadius)) {
                            // избавиться от корня — результат в квадрат
                            return 1;
                        }
                        break;
                }
            }
            return 0;
        }
    }
}
