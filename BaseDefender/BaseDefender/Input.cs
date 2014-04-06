using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BaseDefender
{
    class Input
    {
        private KeyboardState keyboardState;
        private KeyboardState lastState;

        /*public Input()
        {
            keyboardState = Keyboard.GetState();
            lastState = keyboardState;
        }

        public void Update()
        {
            lastState = keyboardState;
            keyboardState = Keyboard.GetState();

        }
        */
        public bool Enter
        {
            get
            {
                keyboardState = Keyboard.GetState();
                if (keyboardState.IsKeyDown(Keys.Enter) && lastState.IsKeyUp(Keys.Enter))
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }
           
        }

        public bool Escape
        {
            get
            {
                keyboardState = Keyboard.GetState();
                if (keyboardState.IsKeyDown(Keys.Escape) && lastState.IsKeyUp(Keys.Escape))
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }
        }
    }
}
