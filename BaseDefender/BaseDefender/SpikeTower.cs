using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseDefender
{
    public class SpikeTower : Tower
    {
        private Vector2[] directions = new Vector2[8];
        private List<Enemy> targets = new List<Enemy>();

        public SpikeTower(Texture2D texture, Texture2D bulletTexture, Vector2 position) 
            : base(texture,bulletTexture,position)
        {
            this.damage = 20;
            this.cost = 20;
            this.radius = 50;
            directions = new Vector2[]
            {
                new Vector2(-1,-1),
                new Vector2(0,-1),
                new Vector2(1,-1),
                new Vector2(-1,0),
                new Vector2(1,0),
                new Vector2(-1,1),
                new Vector2(0,1),
                new Vector2(1,1),
            };
        }

        public override void Update(GameTime gameTime)
        {
            bulletTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (bulletTimer >= 1.0f && targets.Count != 0)
            {
                for (int i = 0; i < directions.Length; i++)
                {
                    Bullet bullet = new Bullet(bulletTexture, Vector2.Subtract(center, new Vector2(bulletTexture.Width / 2)), directions[i], 6, damage);
                    bulletList.Add(bullet);
                }
                bulletTimer = 0;
            }

            for (int i = 0; i < bulletList.Count; i++)
            {
                Bullet bullet = bulletList[i];
                bullet.Update(gameTime);

                if (!IsInRange(bullet.Center))
                {
                    bullet.Kill();
                }

                for (int j = 0; j < targets.Count; j++)
                {
                    if (targets[j] != null && Vector2.Distance(bullet.Center, targets[j].Center) < 48)
                    {
                        targets[j].CurrentHealth -= bullet.Damage;
                        bullet.Kill();
                        break;
                    }
                }

                if (bullet.IsDead())
                {
                    bulletList.Remove(bullet);
                    i--;
                }
            }
            //base.Update(gameTime);
        }

        public override void GetClosestEnemy(List<Enemy> enemies)
        {
            targets.Clear();
            foreach (Enemy enemy in enemies)
            {
                if (IsInRange(enemy.Center))
                {
                    targets.Add(enemy);
                }
            }
        }
    }
}
