using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using _3DPrototype.Graphic;
using Microsoft.Xna.Framework.Input;

namespace _3DPrototype.Game.GameState
{
	class MainState : AGameState
	{
		public MainState(Camera camera, int world)
		{
			if (world == 0)
				_world = new World01();
			else _world = new World02();

			_camera = camera;
			_camera.Attach(_world.Player);
			_effectHolder = Globals.ContentManager.Load<Model>("cube");

			Globals.MeshEffect = (BasicEffect)_effectHolder.Meshes[0].Effects[0];
			Globals.PlayerScore = 0;
		}

		public override void Draw(GameTime gameTime)
		{
			// mesh rendering
			_world.Draw(gameTime);
		}

		public override void Update(GameTime gameTime)
		{
			_world.Update(gameTime);

			if (_world.IsCompleted)
				IsFinished = true;
		}

		Camera _camera;
		protected World _world;
		private Model _effectHolder;
	}
}
