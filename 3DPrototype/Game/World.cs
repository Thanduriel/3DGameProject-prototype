using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
			_gameObjects.Add(new Actor(new Vector3(0), Globals.ContentManager.Load<Model>("sphere")));
		}

		public void Draw(GameTime gameTime)
		{
			foreach (var obj in _gameObjects) obj.Draw();
		}

		public void Update(GameTime gameTime)
		{
			foreach (var obj in _gameObjects) obj.Update(gameTime);
		}

		protected List<Actor> _gameObjects = new List<Actor>();
	}
}
