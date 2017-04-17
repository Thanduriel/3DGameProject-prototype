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
		const float GroundGradient = 0.2f;

		public World()
		{
			Player = new Player(new Vector3(0,0, 5), Globals.ContentManager.Load<Model>("sphere"));
			Player.AngularVelocity = -Vector3.UnitY;
			_playerController = new Controller(Player);
			_gameObjects.Add(Player);

			// ground plane
			Actor ground = new Actor(new Vector3(0, 0, 0), Globals.ContentManager.Load<Model>("cube"));
			//		ground.AngularVelocity = Vector3.UnitY;
			ground.Rotation *= Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), -(float)Math.Atan(GroundGradient));
			ground.Scale = new Vector3(1000f, 1f, 0.1f);
			_gameObjects.Add(ground);

			_gameObjects.Add(new Actor(new Vector3(-10, 0, PlaneZ(-10)), Globals.ContentManager.Load<Model>("sphere")));

		}

		public void Draw(GameTime gameTime)
		{
			foreach (var obj in _gameObjects) obj.Draw();
		}

		public void Update(GameTime gameTime)
		{
			_playerController.Update(gameTime);
			foreach (var obj in _gameObjects) obj.Update(gameTime);

			// collision: only the player may collide; the player is always [0]
			for (int i = 2; i < _gameObjects.Count; ++i)
			{
				float r = Player.BoundingRadius + _gameObjects[i].BoundingRadius;
				if (r * r > Vector3.DistanceSquared(Player.Position, _gameObjects[i].Position))
				{
					_gameObjects[i].Collide(Player);
				}
			}

			// apply gravity
			Player.Velocity += new Vector3(-0.6f, 0, -9.81f) * (float)gameTime.ElapsedGameTime.TotalSeconds;

			// collision: only the player can interact with other objects
			float plane = PlaneZ(Player.Position.X);
			if (Player.Position.Z < plane)
			{
				Player.Position = new Vector3(Player.Position.X, Player.Position.Y, plane);
				Player.Velocity.Z = 0f;
			}
		}

		public static float PlaneZ(float x)
		{
			return x * GroundGradient;
		}

		protected List<Actor> _gameObjects = new List<Actor>();
		public Actor Player { get; private set; }
		Controller _playerController;
	}
}
