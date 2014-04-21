using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseDefender
{
    public class Level
    {
        int tilesize;
        int[,] map = new int[,] 
        {
            {0,1,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,1,1,1,1,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,1,0,0,0,0,0,0,1,1,1},
            {0,0,0,0,1,0,0,0,0,0,1,1,0,0},
            {0,0,1,1,1,0,0,0,0,0,1,1,0,0},
            {0,0,1,0,0,0,0,0,0,0,1,0,0,0},
            {0,0,1,1,1,0,0,0,0,0,1,0,0,0},
            {0,0,1,1,1,1,1,0,0,0,1,0,0,0},
            {0,0,1,1,1,1,1,0,0,0,1,0,0,0},
            {0,0,0,0,1,1,1,1,1,1,1,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        };
        private Queue<Vector2> waypoints = new Queue<Vector2>();

        public Queue<Vector2> Waypoints
        {
            get { return waypoints; }
        }


        public int Width
        {
            get { return map.GetLength(1); }
        }
        public int Height
        {
            get { return map.GetLength(0); }
        }

        /* public Vector2 ScaleFactor
         {
             get { return scalefactor; }
         }*/
        public Level(int tilesize)
        {
            this.tilesize = tilesize;
            waypoints.Enqueue(new Vector2(1, 0) * tilesize);
            waypoints.Enqueue(new Vector2(1, 1) * tilesize);
            waypoints.Enqueue(new Vector2(4, 1) * tilesize);
            waypoints.Enqueue(new Vector2(4, 4) * tilesize);
            waypoints.Enqueue(new Vector2(2, 4) * tilesize);
            waypoints.Enqueue(new Vector2(2, 6) * tilesize);
            waypoints.Enqueue(new Vector2(3, 6) * tilesize);
            waypoints.Enqueue(new Vector2(4, 7) * tilesize);
            waypoints.Enqueue(new Vector2(6, 7) * tilesize);
            waypoints.Enqueue(new Vector2(6, 9) * tilesize);
            waypoints.Enqueue(new Vector2(10, 9) * tilesize);
            waypoints.Enqueue(new Vector2(10, 4) * tilesize);
            waypoints.Enqueue(new Vector2(11, 3) * tilesize);
            waypoints.Enqueue(new Vector2(12, 2) * tilesize);
            waypoints.Enqueue(new Vector2(13, 2) * tilesize);
        }


        private List<Texture2D> tileTextures = new List<Texture2D>();

        public int GetIndex(int cellX, int cellY)
        {
            if (cellX < 0 || cellX > Width - 1 || cellY < 0 || cellY > Height - 1)
                return 0;

            return map[cellY, cellX];
        }
        public void AddTexture(Texture2D texture)
        {
            tileTextures.Add(texture);
        }

        /* public void ScaleLevelArea(int windowWidth,int windowHeight)
         {
             scalefactor.X =windowWidth/(Width);
             scalefactor.Y=windowHeight/(Height+1);
            
         }*/

        public void Draw(SpriteBatch batch)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    int textureIndex = map[y, x];
                    if (textureIndex == -1)
                        continue;

                    Texture2D texture = tileTextures[textureIndex];

                    batch.Draw(texture, new Rectangle(x * tilesize, y * tilesize, tilesize, tilesize), Color.White);
                    batch.Draw(texture, new Rectangle(x * tilesize, y * tilesize, tilesize, tilesize), Color.White);
                    //batch.Draw(texture, new Rectangle((int)(x * scalefactor.X), (int)(y * scalefactor.Y), (int)scalefactor.X, (int)scalefactor.Y), Color.White);
                }
            }
            // batch.Draw(tileTextures[2], new Rectangle(1 * tilesize, 0 * tilesize, tilesize, tilesize), Color.White);

        }
    }
}
