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

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Level level = new Level();
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
        
	    public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            menu = new Menu();
            input = new Input();
            graphics.PreferredBackBufferWidth = level.Width * 32;
            graphics.PreferredBackBufferHeight = (level.Height + 1) * 32;
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

            Texture2D towerTexture = Content.Load<Texture2D>("tower");
            Texture2D bulletTexture = Content.Load<Texture2D>("bullet");
            bulletSound = Content.Load<SoundEffect>("gunShot");
            player = new Player(level, towerTexture, bulletTexture,Content);

            waveManager = new WaveManager(player, level, 30, enemyTexture);

            SpriteFont font = Content.Load<SpriteFont>("Arial");

            Texture2D topBar = Content.Load<Texture2D>("toolbar");
            toolbar = new Toolbar(topBar, font, new Vector2(0, level.Height * 32));
           
            Texture2D arrowNormal = Content.Load<Texture2D>("GUI\\Arrow Tower\\arrow normal");
            Texture2D arrowHover = Content.Load<Texture2D>("GUI\\Arrow Tower\\arrow hover");
            Texture2D arrowPressed = Content.Load<Texture2D>("GUI\\Arrow Tower\\arrow pressed");

            arrowButton = new Button(arrowNormal, arrowHover, arrowPressed, new Vector2(0, level.Height * 32));
            arrowButton.Clicked += new EventHandler(arrowButton_Clicked);

	        Texture2D pausetexture = Content.Load<Texture2D>("pause");
            Texture2D playtexture = Content.Load<Texture2D>("play");
	        button1=new Button1(pausetexture, playtexture, new Vector2(35, level.Height * 32 + 3));

        }

        private void arrowButton_Clicked(object sender, EventArgs e)
        {
            player.NewTowerType = "Arrow Tower";
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
                spriteBatch.Draw(texture, new Vector2(0f, 0f), null, Color.White, 0, new Vector2(0f, 0f), 9/8f, SpriteEffects.None, 0);
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
		        button1.Draw(spriteBatch);
              
            }

            else if (gameStates == GameStates.End)
            {
		        Texture2D endscreen = Content.Load<Texture2D>("end_screen");
                menu.DrawEndScreen(spriteBatch, level.Width * 32, lucida,endscreen);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
