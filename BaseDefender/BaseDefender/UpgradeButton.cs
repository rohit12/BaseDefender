using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BaseDefender
{
    public class UpgradeButton
    {
        private bool clicked = false;
        private bool upgraded = false;
        private MouseState previousState;
        private Rectangle bounds;
        private Texture2D upgradeTexture;

        public UpgradeButton()
        {

        }

        public UpgradeButton(Texture2D texture, Vector2 position)
        {
            upgradeTexture = texture;
            bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public bool Clicked
        {
            get { return clicked; }
            set { clicked = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!upgraded)
                spriteBatch.Draw(upgradeTexture, bounds, Color.White);
            
            //Update();       
        }

        public void Update()
        {
            MouseState mouseState = Mouse.GetState();
            
            int mouseX = mouseState.X;
            int mouseY = mouseState.Y;
            bool isMouseOver = bounds.Contains(mouseX, mouseY);
          
            if (mouseState.LeftButton == ButtonState.Pressed && previousState.LeftButton == ButtonState.Released)
            {
                if (isMouseOver == true)
                {
                    clicked = true;
                    upgraded = true;
                }
            }
            previousState = mouseState;
        }
    }
}
