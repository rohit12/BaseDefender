using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BaseDefender
{
    class Menu
    {
        
        public string infoText { get; set; }
        
        public Menu()
        {
            
        }

       
public void DrawEndScreen(SpriteBatch batch, int screenWidth, SpriteFont lucida,Texture2D texture)
        {
            batch.Draw(texture, new Vector2(0f, 0f), Color.White);
            //string str="GAME OVER!";
            //batch.DrawString(lucida, str, new Vector2(screenWidth / 2 - lucida.MeasureString(str).X / 2, 20), Color.Black);
            
            //batch.DrawString(lucida, infoText, new Vector2(screenWidth / 2 - lucida.MeasureString(infoText).X / 2, 100), Color.White);
            
            //string prompt = "Press Enter to Continue";
            //batch.DrawString(lucida, prompt, new Vector2(screenWidth / 2 - lucida.MeasureString(prompt).X / 2, 200), Color.White);
        }


    }
}
