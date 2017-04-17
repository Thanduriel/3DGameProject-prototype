using _3DPrototype.Game.GameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace _3DPrototype
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
		Graphic.Camera camera;

		Stack<AGameState> gameStates = new Stack<AGameState>();

		Game.Actor testActor;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = 1366;
			graphics.PreferredBackBufferHeight = 768;
			graphics.ApplyChanges();

			Content.RootDirectory = "Content";

			Globals.ContentManager = Content;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
			Globals.Camera = new Graphic.Camera(graphics);
			testActor = new Game.Actor(new Vector3(0f), Content.Load<Model>("sphere"));
			gameStates.Push(new MainState(Globals.Camera));

			base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
			
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) || gameStates.Count == 0)
                Exit();

			AGameState state = gameStates.Peek();
			state.Update(gameTime);
			Globals.Camera.Update(gameTime);

			AGameState newState = state.NewState;
			if (state.IsFinished) gameStates.Pop();
			if (newState != null) gameStates.Push(newState);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green);

			gameStates.Peek().Draw(gameTime);

/*			foreach (ModelMesh mesh in testActor.Model.Meshes)
			{
				foreach (BasicEffect effect in mesh.Effects)
				{
					camera.Set(effect);
					effect.World = testActor.WorldMatrix;
					effect.EnableDefaultLighting();
				}
				mesh.Draw();
			}*/

			base.Draw(gameTime);
        }
    }
}
