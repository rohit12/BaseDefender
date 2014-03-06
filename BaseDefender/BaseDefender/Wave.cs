using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseDefender
{
    class Wave
    {
        private int numOfEnemies;
        private int waveNumber;
        private float spawnTimer;
        private int enemiesSpawned;
        private bool enemyAtEnd;
        private bool spawningEnemies;
        private Level level;
        //private Texture2D enemyTexture;
        private Texture2D[] enemyTexture;
        private float[] enemyHealthList;
        public List<Enemy> enemies = new List<Enemy>();
        private Player player;

        public bool RoundOver
        {
            get { return enemies.Count == 0 && enemiesSpawned == numOfEnemies; }
        }

        public int RoundNumber
        {
            get { return waveNumber; }
        }

        public bool EnemyAtEnd
        {
            get { return enemyAtEnd; }
            set { enemyAtEnd = value; }
        }

        public List<Enemy> Enemies
        {
            get { return enemies; }
        }

        //public Wave(int waveNumber, int numOfEnemies, Player player, Level level, Texture2D enemyTexture)
        public Wave(int waveNumber, int numOfEnemies, Player player, Level level, Texture2D[] enemyTextureList, float[] enemyHealthList)
        {
            this.waveNumber = waveNumber;
            this.numOfEnemies = numOfEnemies;
            this.player = player;
            this.level = level;
            //this.enemyTexture = enemyTexture;
            this.enemyTexture = enemyTextureList;
            this.enemyHealthList = enemyHealthList;
        }

        private int SelectEnemy(int enemyTypeCount)
        {
            Random rand = new Random();
            int randomIndex = rand.Next() % enemyTypeCount;
            return randomIndex;
        }

        private void AddEnemy()
        {
           // Enemy enemy = new Enemy(enemyTexture, level.Waypoints.Peek(), 50 + waveNumber * 20, (waveNumber + 1) * 2, (waveNumber + 1) * 0.5f);
            int randomEnemyIndex = SelectEnemy(enemyTexture.Count());
            Enemy enemy = new Enemy(enemyTexture[randomEnemyIndex], level.Waypoints.Peek(),/*enemyHealthList [randomEnemyIndex ]*/ 50 + waveNumber * 20, (waveNumber + 1) * 2, (waveNumber + 1) * 0.5f);
            enemy.SetWaypoints(level.Waypoints);
            enemies.Add(enemy);
            spawnTimer = 0;
            enemiesSpawned++;
        }

        public void Start()
        {
            spawningEnemies = true;
        }

        public void Update(GameTime gameTime)
        {
            if (enemiesSpawned == numOfEnemies)
                spawningEnemies = false;
            if (spawningEnemies)
            {
                spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (spawnTimer > 1)
                    AddEnemy();
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                Enemy enemy = enemies[i];
                enemy.Update(gameTime);
                if (enemy.IsDead)
                {
                    if (enemy.CurrentHealth > 0)
                    {
                        enemyAtEnd = true;
                        player.Lives -= 1;
                        if (player.Lives == 0)
                        {
                            Game1.gameStates = Game1.GameStates.End;
                        }
                    }
                    else
                    {
                        player.Money += enemy.BountyGiven;
                    }

                    enemies.Remove(enemy);
                    i--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in enemies)
                enemy.Draw(spriteBatch);
        }
    }
}
