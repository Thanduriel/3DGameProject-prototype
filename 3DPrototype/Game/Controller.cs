using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPrototype.Game
{
	class Controller
	{
		public Controller(Actor pawn)
		{
			_pawn = pawn;
		}

		const float MOVEMENTVELOCITY = 10f;
		const float MAXVELOCITY = 8f;

		public void Update(GameTime gameTime)
		{
			KeyboardState state = Keyboard.GetState();

			Vector3 mov = new Vector3(0);

			if (state.IsKeyDown(Keys.U)) mov.Y += MOVEMENTVELOCITY;
			if (state.IsKeyDown(Keys.A)) mov.Y -= MOVEMENTVELOCITY;
			if (state.IsKeyDown(Keys.Space)) _pawn.Velocity = new Vector3(0);

			if(Math.Abs(_pawn.Velocity.Y) < MAXVELOCITY)_pawn.Velocity += mov * (float)gameTime.ElapsedGameTime.TotalSeconds;
		}

		private Actor _pawn;
	}
}
