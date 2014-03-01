using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BaseDefender
{
    class Button1
    {
        private Texture2D pausetexture;
        private Texture2D playtexture;
        private MouseState previousState;
        private Rectangle bounds;
        private bool paused = false;

        public Button1(Texture2D pausetexture, Texture2D playtexture, Vector2 position)
        {
            this.pausetexture = pausetexture;
            this.playtexture = playtexture;
            this.bounds = new Rectangle((int)position.X, (int)position.Y, pausetexture.Width, pausetexture.Height);
        }

        public bool checkPause()
        {
            return paused;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (paused)
                spriteBatch.Draw(playtexture, bounds, Color.White);
            else
                spriteBatch.Draw(pausetexture, bounds, Color.White);
        }

        public void Update(GameTime gametime)
        {
            MouseState mouseState = Mouse.GetState();

            int mouseX = mouseState.X;
            int mouseY = mouseState.Y;
            bool isMouseOver = bounds.Contains(mouseX, mouseY);

            if (mouseState.LeftButton == ButtonState.Pressed && previousState.LeftButton == ButtonState.Released)
            {
                if (isMouseOver == true)
                {
                    if (paused)
                        paused = false;
                    else
                        paused = true;
                }
            }
        }

    }
}
