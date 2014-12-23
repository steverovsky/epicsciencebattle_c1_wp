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
using Microsoft.Xna.Framework.Input.Touch;

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

        public bool EntryIntoSection (int _numberSection, Vector2 _point) {
            // point вместо vector2?
            float PointDistaneToCenter = (float) (Math.Pow (_point.X - centerPosition.X, 2) + Math.Pow (_point.Y - centerPosition.Y, 2));
            bool entryIntoInnerCircle = PointDistaneToCenter < innerRadius * innerRadius;
            if (_numberSection == 0) {
                return entryIntoInnerCircle;
            }
            float k = 1.41f * outerRadius / 2;
            Vector2[] vertices = new Vector2 [4];
            vertices[0] = new Vector2 (0, 0);
            switch (_numberSection) {
                case 1:
                    vertices[1] = new Vector2 (-k, -k);
                    vertices[2] = new Vector2 (0, -outerRadius);
                    vertices[3] = new Vector2 (k, -k);
                    break;
                case 2:
                    vertices[1] = new Vector2 (-k, -k);
                    vertices[2] = new Vector2 (outerRadius, 0);
                    vertices[3] = new Vector2 (k, k);
                    break;
                case 3:
                    vertices[1] = new Vector2 (k, k);
                    vertices[2] = new Vector2 (0, outerRadius);
                    vertices[3] = new Vector2 (-k, k);
                    break;
                case 4:
                    vertices[1] = new Vector2 (-k, k);
                    vertices[2] = new Vector2 (-outerRadius, 0);
                    vertices[3] = new Vector2 (-k, -k);
                    break;
            }
            for (int i = 0; i < vertices.Length; ++i) {
               vertices[i] += centerPosition;
            }
            return (PointInTriangle (_point, vertices[0], vertices[1], vertices[3]) ||
                   PointInTriangle (_point, vertices[1], vertices[2], vertices[3])) &&
                   !entryIntoInnerCircle;
        }

        public int getSectionNumber () {
            TouchCollection touchCollection = TouchPanel.GetState ();
            foreach (TouchLocation pressure in touchCollection) {
                if ((pressure.State == TouchLocationState.Pressed) || (pressure.State == TouchLocationState.Moved)) {
                    for (int i = 0; i < 5; ++i) {
                        if (EntryIntoSection (i, pressure.Position)) {
                            return i;
                        }
                    }
                }
            }
            return -1;
        }
    }
}
