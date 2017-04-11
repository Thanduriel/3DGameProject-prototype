﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using _3DPrototype.Graphic;

namespace _3DPrototype.Game.GameState
{
	class MainState : AGameState
	{
		public MainState(Camera camera)
		{
			_camera = camera;
			_effectHolder = Globals.ContentManager.Load<Model>("sphere");

			Globals.MeshEffect = (BasicEffect)_effectHolder.Meshes[0].Effects[0];
		}

		public override void Draw(GameTime gameTime)
		{
			// mesh rendering
			_camera.Set(Globals.MeshEffect);
			Globals.MeshEffect.EnableDefaultLighting();
		
			_world.Draw(gameTime);

		}

		public override void Update(GameTime gameTime)
		{
			_world.Update(gameTime);
		}

		Camera _camera;
		protected World _world = new World();
		private Model _effectHolder;
	}
}