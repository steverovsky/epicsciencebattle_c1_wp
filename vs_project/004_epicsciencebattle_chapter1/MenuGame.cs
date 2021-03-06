﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;
using Windows.UI.ViewManagement;
using System.Diagnostics;
using System.Collections.Generic;

namespace _004_epicsciencebattle_chapter1 {
    enum MenuState { Wait = -1, InitStory, ContinueStory, RandomFight, Options, Break };
    class MenuGame {
        MenuState currentMenuState;
        float marginRight;
        Vector2 startPosition0;
        Vector2 itemSize;
        int menulength;
        private Texture2D backgroundMenu;
        Fight fight;
        public MenuGame (Texture2D _backgroundMenu) {
            backgroundMenu = _backgroundMenu;
            currentMenuState = MenuState.Wait;
            marginRight = 25.0f;
            startPosition0 = new Vector2(90f, 472f);
            itemSize = new Vector2(200f, 100f);
            menulength = 5;
        }

        public int GetStateFromTouch () {
            int index;
            TouchCollection currentTouch = TouchPanel.GetState();
            foreach (TouchLocation element in currentTouch) {
                if (element.State == TouchLocationState.Pressed || element.State == TouchLocationState.Moved) {
                    for (index = 0; index < menulength; ++index) {
                        if (Tools.EntryIntoRectangle(element.Position,
                                new Vector2(startPosition0.X + (itemSize.X + marginRight) * index, startPosition0.Y),
                                new Vector2(startPosition0.X + itemSize.X + (itemSize.X + marginRight) * index, startPosition0.Y + itemSize.Y))) 
                                {
                            return index;
                        }
                    }
                }
            }
            return -1;
        }

        public void Draw (SpriteBatch _spriteBatch) {
            switch (currentMenuState) {
                case MenuState.Wait:
                    _spriteBatch.Draw(backgroundMenu, Vector2.Zero, Color.White);
                    break;
                case MenuState.InitStory:
                    break;
                case MenuState.ContinueStory:
                    break;
                case MenuState.RandomFight:
                    fight.Draw(_spriteBatch);
                    break;
                case MenuState.Options:
                    break;
                case MenuState.Break:
                    break;
            }
        }

        public bool Update (GameTime _gameTime) {
            switch (currentMenuState) {
                case MenuState.Wait:
                   // System.Diagnostics.Debug.WriteLine(GetStateFromTouch());
                    currentMenuState = (MenuState)GetStateFromTouch();
                    break;
                case MenuState.InitStory:
                    break;
                case MenuState.ContinueStory:
                    break;
                case MenuState.RandomFight:
                    if (fight == null)
                        fight = new Fight(0, 0, 0, Tools.bgScreen);
                    fight.Update(_gameTime);
                    break;
                case MenuState.Options:
                    break;
                case MenuState.Break:
                    return false;
            }
            return true;
        }
    }
}
