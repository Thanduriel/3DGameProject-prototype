using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPrototype.Game
{
	class World
	{
		public World()
		{
			Player = new Actor(new Vector3(0,0, 5), Globals.ContentManager.Load<Model>("sphere"));
			_gameObjects.Add(Player);
			_gameObjects.Add(new Actor(new Vector3(2, 2, 0), Globals.ContentManager.Load<Model>("sphere")));
		}

		public void Draw(GameTime gameTime)
		{
			foreach (var obj in _gameObjects) obj.Draw();
		}

		public void Update(GameTime gameTime)
		{
			foreach (var obj in _gameObjects) obj.Update(gameTime);

			// apply gravity
			Player.Velocity += new Vector3(-0.2f, 0, -9.81f) * (float)gameTime.ElapsedGameTime.TotalSeconds;

			// collision: only the player can interact with other objects
			float plane = Player.Position.X * 0.2f;
			if (Player.Position.Z < plane) Player.Position = new Vector3(Player.Position.X, Player.Position.Y, plane);
		}

		protected List<Actor> _gameObjects = new List<Actor>();
		public Actor Player;
	}
}
