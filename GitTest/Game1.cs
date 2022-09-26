using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        bool personHit;

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
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            GraphicsDevice device = _graphics.GraphicsDevice;

            KeyboardState keyboard = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (keyboard.IsKeyDown(Keys.Left))
            {
                charPosition.X = charPosition.X - 2;
            }
            if (keyboard.IsKeyDown(Keys.Right))
            {
                charPosition.X = charPosition.X + 2;
            }
            Rectangle charRectangle = new Rectangle((int)charPosition.X, (int)charPosition.Y, 32, 48);
            Rectangle blockRectangle = new Rectangle((int)ballPosition.X, (int)ballPosition.Y, 24, 24);
            if (charRectangle.Intersects(blockRectangle) == true)
            {
                personHit = true;
            }
            else if(charRectangle.Intersects(blockRectangle) == false)
{
                personHit = false;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice device = _graphics.GraphicsDevice;
            if (personHit == true)
            { 
                device.Clear(Color.Red); 
            }
            else
            { 
                device.Clear(Color.CornflowerBlue); 
            }
            
            _spriteBatch.Begin();
            _spriteBatch.Draw(ballTexture, ballPosition, new Rectangle(24, 0, 24, 24), Color.White); 
            _spriteBatch.Draw(charTexture, charPosition, new Rectangle(32, 0, 32, 48), Color.White);
            _spriteBatch.End(); base.Draw(gameTime);
        }
    }
}
