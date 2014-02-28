using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace BaseDefender
{
    public class ArrowTower : Tower
    {
        private ContentManager content;
        
        public ArrowTower(Texture2D texture, Texture2D bulletTexture, Vector2 position,ContentManager content)
            : base(texture, bulletTexture, position)
        {
            this.damage = 15;
            this.cost = 15;

            this.radius = 80; // Set the radius
            this.content = content;
        }

        public void Upgrade(int previousLevel)
        {
            this.damage = this.damage * (previousLevel + 1);
            this.radius = this.radius + ((previousLevel + 1) * 5);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (bulletTimer >= 0.75f && target != null)
            {
                Bullet bullet = new Bullet(bulletTexture, Vector2.Subtract(center,
                    new Vector2(bulletTexture.Width / 2)), rotation, 6, damage,content);

                bulletList.Add(bullet);
                bulletTimer = 0;
            }

            for (int i = 0; i < bulletList.Count; i++)
            {
                Bullet bullet = bulletList[i];

                bullet.SetRotation(rotation);
                bullet.Update(gameTime);

                if (!IsInRange(bullet.Center))
                    bullet.Kill();

                if (target != null && Vector2.Distance(bullet.Center, target.Center) < 12)
                {
                    target.CurrentHealth -= bullet.Damage;
                    bullet.Kill();
                }

                if (bullet.IsDead())
                {
                    bulletList.Remove(bullet);
                    i--;
                }
            }
        }
    }
}
