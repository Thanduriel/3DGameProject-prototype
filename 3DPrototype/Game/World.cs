﻿using Microsoft.Xna.Framework;
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
		const float DefaultSizeX = 64f;
		const float DefaultSizeY = 7f;

		public World(float width = DefaultSizeY)
		{
			_worldSize = new Vector3(DefaultSizeX, width, 10f);

			Player = new Player(new Vector3(0,0, 5), Globals.ContentManager.Load<Model>("sphere"));
			_playerIsDown = false;
			Player.AngularVelocity = -Vector3.UnitY;
			_playerController = new Controller(Player);
			_gameObjects.Add(Player);

			// ground plane
			Actor ground = new Actor(new Vector3(0, 0, 0), Globals.ContentManager.Load<Model>("cube"));
			//		ground.AngularVelocity = Vector3.UnitY;
			ground.Rotation *= Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), -(float)Math.Atan(GroundGradient));
			ground.PerPixelLightning = true;
			float a = PlaneZ(DefaultSizeX);
			a *= a;
			ground.Scale = new Vector3((float)Math.Sqrt(a + DefaultSizeX * DefaultSizeX), width, 0.1f);
			_gameObjects.Add(ground);

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
			Player.Velocity += new Vector3(-2.2f, 0, -9.81f) * (float)gameTime.ElapsedGameTime.TotalSeconds;

			// ground layer
			float plane = PlaneZ(Player.Position.X);
			if (Player.Position.Z < plane 
				&& Math.Abs(Player.Position.Y) < _worldSize.Y
				&& Player.Position.X > -_worldSize.X
				&& !_playerIsDown)
			{
				Player.Position = new Vector3(Player.Position.X, Player.Position.Y, plane);
				Player.Velocity.Z = PlaneZ(Player.Velocity.X);
			}

			if (Player.Position.Z - plane < -1f)
			{
				Globals.Camera.Dettach();
				_playerIsDown = true;
			}

			if (Player.Position.Z - plane < -50f)
				IsCompleted = true;


		}

		public static float PlaneZ(float x)
		{
			return x * GroundGradient;
		}

		public static Vector3 AlignedPosition(float x, float y)
		{
			return new Vector3(x, y, PlaneZ(x) + 0.2f);
		}

		public bool IsCompleted = false;
		protected List<Actor> _gameObjects = new List<Actor>();
		public Actor Player { get; private set; }
		private bool _playerIsDown;
		Controller _playerController;

		Vector3 _worldSize;
	}
}
