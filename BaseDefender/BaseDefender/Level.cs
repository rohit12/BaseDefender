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
        int[,] map = new int[,] 
        {
            {0,0,1,0,0,0,0,0,0,},
            {0,0,1,0,0,0,0,0,0,},
            {0,0,1,1,1,1,1,1,1,},
            {0,0,0,0,0,0,0,0,1,},
            {1,1,1,1,1,1,1,1,1,},
            {1,0,0,0,0,0,0,0,0,},
            {1,1,1,1,1,1,1,1,1,},
            {0,0,0,0,0,0,0,0,1,},
            {1,1,1,1,1,1,1,1,1,},
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

        public Level()
        {
            waypoints.Enqueue(new Vector2(2, 0) * 32);
            waypoints.Enqueue(new Vector2(2, 2) * 32);
            waypoints.Enqueue(new Vector2(8, 2) * 32);
            waypoints.Enqueue(new Vector2(8, 4) * 32);
            waypoints.Enqueue(new Vector2(0, 4) * 32);
            waypoints.Enqueue(new Vector2(0, 6) * 32);
            waypoints.Enqueue(new Vector2(8, 6) * 32);
            waypoints.Enqueue(new Vector2(8, 8) * 32);
            waypoints.Enqueue(new Vector2(0, 8) * 32);
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
                    batch.Draw(texture, new Rectangle(x * 32, y * 32, 32, 32), Color.White);
                }
            }
        }
    }
}
