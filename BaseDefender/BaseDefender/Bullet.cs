using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace BaseDefender
{
    public class Bullet : Sprite
    {
        private int damage;
        private int age;

        private int speed;
        private ContentManager content;
        private SoundEffect bulletSound;

        public int Damage
        {
            get { return damage; }
        }

        public void Kill()
        {
            this.age = 200;
        }

        public bool IsDead()
        {
            return age > 100;
        }

        public override void Update(GameTime gameTime)
        {
            age++;
            if (age == 5)
            {
                bulletSound.Play();
            }
            position += velocity;

            base.Update(gameTime);
        }

        public Bullet(Texture2D texture, Vector2 position, float rotation, int speed, int damage,ContentManager content)
            : base(texture, position)
        {
            this.rotation = rotation;
            this.damage = damage;
            this.speed = speed;
            this.content = content;
            bulletSound = content.Load<SoundEffect>("gunShot");
        }

        public Bullet(Texture2D texture, Vector2 position, Vector2 velocity, int speed, int damage, ContentManager content)
            : base(texture, position)
        {
            this.rotation = rotation;
            this.damage = damage;

            this.speed = speed;

            this.velocity = velocity * speed;
            bulletSound = content.Load<SoundEffect>("gunshot");
        }

        public void SetRotation(float value)
        {
            rotation = value;

            velocity = Vector2.Transform(new Vector2(0, -speed), Matrix.CreateRotationZ(rotation));
        }
    }
}
