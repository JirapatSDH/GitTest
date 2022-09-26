using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GitTest
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D ballTexture;
        Texture2D charTexture;
        Vector2 charPosition = new Vector2(0, 250);
        Vector2 ballPosition = new Vector2(250, 250);

        Vector2[] ballPos = new Vector2[4];

        Random rand;
        Color[] bgColor;
        bool personHit;

        int frame, chaState, framePerSec;
        int[] ballColor;
        float totalElapsed;
        float timePerFrame;

        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ballTexture = Content.Load<Texture2D>("ball");
            charTexture = Content.Load<Texture2D>("Char01");

                  //Animate
            framePerSec = 5;
            timePerFrame = (float)1 / framePerSec;
            frame = 0;
            totalElapsed = 0;


            rand = new Random();
            ballColor = new int[6];

            //bgNum = new int[6];
            bgColor = new Color[6];
            #region BGColor
            bgColor[0] = Color.Red;
            bgColor[1] = Color.Purple; 
            bgColor[2] = Color.LightGreen;
            bgColor[3] = Color.Yellow;
            bgColor[4] = Color.Pink;
            bgColor[5] = Color.SkyBlue;
            #endregion

            for(int i = 0; i <= 3; i++)
            {
                ballPos[i].X = rand.Next(0, _graphics.GraphicsDevice.Viewport.Width - 24);
                ballPos[i].Y = rand.Next(0, _graphics.GraphicsDevice.Viewport.Height - 24);
                ballColor[i] = rand.Next(0, 5);
            }
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            GraphicsDevice device = _graphics.GraphicsDevice;

            UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);

            KeyboardState keyboard = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (keyboard.IsKeyDown(Keys.Up))
            {
                chaState = 3;
                charPosition.Y -= 2;
            }
            if (keyboard.IsKeyDown(Keys.Left))
            {
                chaState = 1;
                charPosition.X = charPosition.X - 2;
            }
            if (keyboard.IsKeyDown(Keys.Right))
            {
                chaState = 2;
                charPosition.X = charPosition.X + 2;
            }
            if (keyboard.IsKeyDown(Keys.Down))
            {
                chaState = 0;
                charPosition.Y += 2;
            }
            if (keyboard.IsKeyUp(Keys.Up) && keyboard.IsKeyUp(Keys.Left) && keyboard.IsKeyUp(Keys.Right) && keyboard.IsKeyUp(Keys.Down))
            {
                frame = 0;
            }

            Rectangle charRectangle = new Rectangle((int)charPosition.X, (int)charPosition.Y, 32, 48);
            Rectangle[] ballRec = new Rectangle[4];

             for(int x = 0; x < 4; x++)
            {
                ballRec[x] = new Rectangle((int)ballPos[x].X, (int)ballPos[x].Y, 24, 24);

                if (charRectangle.Intersects(ballRec[x]))
                {
                    personHit = true;
                    ballPos[x] = new Vector2(rand.Next(24, _graphics.GraphicsDevice.Viewport.Width - ballTexture.Width), rand.Next(0, _graphics.GraphicsDevice.Viewport.Height - 24));
                    ballColor[x] = rand.Next(0, 5);
                    break;
                    
                }
                else if (!charRectangle.Intersects(ballRec[x]))
                {
                    personHit = false;
                }

            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice device = _graphics.GraphicsDevice;
          
            _spriteBatch.Begin();
            for(int i = 0; i<4 ; i++)
            {
                if (personHit)
                {
                    device.Clear(bgColor[ballColor[i]]);
                }
                else
                {
                    device.Clear(Color.CornflowerBlue);
                }
                _spriteBatch.Draw(ballTexture, ballPos[i], new Rectangle(24*ballColor[i], 0, 24, 24), Color.White);

            }
            _spriteBatch.Draw(charTexture, charPosition, new Rectangle(32*frame, 48*chaState, 32, 48), Color.White);
            _spriteBatch.End(); 
            base.Draw(gameTime);
        }

        void UpdateFrame(float elapsed)
        {
            totalElapsed += elapsed;
            if (totalElapsed > timePerFrame)
            {
                frame = (frame + 1) % 4;
                totalElapsed -= timePerFrame;
            }
        }
    }
}
