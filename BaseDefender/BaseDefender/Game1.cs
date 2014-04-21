using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BaseDefender
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public enum GameStates
        {
            Menu,
            Running,
            End,
        }

        string endText;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Level level = new Level(50);
        //Tower tower;
        Player player;
        //Enemy enemy1;
        //Wave wave;
        WaveManager waveManager;
        Toolbar toolbar;
        Button arrowButton;
        public static UpgradeButton upgradeButton;
        public static bool buttonVisible;
        SpriteFont arial;
        SpriteFont lucida;
        Menu menu;
        Input input;
        public static GameStates gameStates;
        Texture2D[] enemyTexture = new Texture2D[2];
        public SoundEffect bulletSound;
        Button1 button1;
        Button spikeButton;
        
	    public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            menu = new Menu();
            input = new Input();
            graphics.PreferredBackBufferWidth = level.Width * 50;
            graphics.PreferredBackBufferHeight = (level.Height + 1) * 50;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
            gameStates = GameStates.Menu;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D grass = Content.Load<Texture2D>("grass");
            Texture2D path = Content.Load<Texture2D>("path");

            level.AddTexture(grass);
            level.AddTexture(path);

            enemyTexture[0] = Content.Load<Texture2D>("enemy"); 
            enemyTexture[1]=Content.Load<Texture2D>("enemy2");

            arial = Content.Load<SpriteFont>("Arial");
            lucida = Content.Load<SpriteFont>("Lucida");

            Texture2D[] towerTextures = new Texture2D[]
            {
                Content.Load<Texture2D>("Arrow Tower"),
                Content.Load<Texture2D>("Spike Tower")
            };
            Texture2D bulletTexture = Content.Load<Texture2D>("bullet");
            bulletSound = Content.Load<SoundEffect>("gunShot");
            player = new Player(level, towerTextures, bulletTexture,Content);

            waveManager = new WaveManager(player, level, 30, enemyTexture);

            SpriteFont font = Content.Load<SpriteFont>("Arial");

            Texture2D topBar = Content.Load<Texture2D>("toolbar");
            toolbar = new Toolbar(topBar, font, new Vector2(0, level.Height * 50));
            Texture2D arrowNormal = Content.Load<Texture2D>("GUI\\Arrow Tower\\arrow normal");
            Texture2D arrowHover = Content.Load<Texture2D>("GUI\\Arrow Tower\\arrow hover");
            Texture2D arrowPressed = Content.Load<Texture2D>("GUI\\Arrow Tower\\arrow pressed");

            arrowButton = new Button(arrowNormal, arrowHover, arrowPressed, new Vector2(0, level.Height * 50));
            arrowButton.Clicked += new EventHandler(arrowButton_Clicked);

            Texture2D spikeNormal = Content.Load<Texture2D>("GUI\\Spike Tower\\spike normal");
            Texture2D spikeHover = Content.Load<Texture2D>("GUI\\Spike Tower\\spike hover");
            Texture2D spikePressed = Content.Load<Texture2D>("GUI\\Spike Tower\\spike pressed");

            spikeButton = new Button(spikeNormal, spikeHover,spikePressed, new Vector2(50, level.Height *50));
            spikeButton.Clicked += new EventHandler(spikeButton_Clicked);

	        Texture2D pausetexture = Content.Load<Texture2D>("pause");
            Texture2D playtexture = Content.Load<Texture2D>("play");
	        button1=new Button1(pausetexture, playtexture, new Vector2(120, level.Height * 50 + 3));

        }

        private void arrowButton_Clicked(object sender, EventArgs e)
        {
            player.NewTowerType = "Arrow Tower";
        }

        private void spikeButton_Clicked(object sender, EventArgs e)
        {
            player.NewTowerType = "Spike Tower";
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            //enemy1.Update(gameTime);
            //List<Enemy> enemies = new List<Enemy>();
            //enemies.Add(enemy1);
            //wave.Update(gameTime);
            //player.Update(gameTime, wave.enemies);

            if (gameStates == GameStates.Menu)
            {

                if (input.Enter)
                {
                    gameStates = GameStates.Running;
                }
                if (input.Escape)
                {
                    this.Exit();
                }
            }

            else if (gameStates == GameStates.Running)
            {
                GameUpdate(gameTime);
            }

            else if (gameStates == GameStates.End)
            {
                if (input.Enter)
                {
                    this.Exit();
                }
            }

            base.Update(gameTime);
        }

        protected void GameUpdate(GameTime gameTime)
        {
	        button1.Update(gameTime);
            bool pause = button1.checkPause();
            if (!pause)
	        {
            	waveManager.Update(gameTime);
            	player.Update(gameTime, waveManager.Enemies);
            	arrowButton.Update(gameTime);
                spikeButton.Update(gameTime);
	        }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            if (gameStates == GameStates.Menu)
            {
                Texture2D texture = Content.Load<Texture2D>("start_screen");
                //spriteBatch.Draw(texture, new Vector2(0f, 0f), Color.White);
                spriteBatch.Draw(texture, new Vector2(0f, 0f), null, Color.White, 0, new Vector2(0f, 0f), 1f, SpriteEffects.None, 0);
            }

            else if (gameStates == GameStates.Running)
            {
                level.Draw(spriteBatch);
                //enemy1.Draw(spriteBatch);
                //wave.Draw(spriteBatch);
                player.Draw(spriteBatch);
                waveManager.Draw(spriteBatch);
                toolbar.Draw(spriteBatch, player);
                //tower.Draw(spriteBatch);
                arrowButton.Draw(spriteBatch);
                spikeButton.Draw(spriteBatch);
		        button1.Draw(spriteBatch);
              
            }

            else if (gameStates == GameStates.End)
            {
		        Texture2D endscreen = Content.Load<Texture2D>("end_screen");
                menu.DrawEndScreen(spriteBatch, level.Width * 50, lucida,endscreen);
                endText = String.Format("Your Score is: {0}",player.Money);
                spriteBatch.DrawString(arial, endText, new Vector2(250,250), Color.Black);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
