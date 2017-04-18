using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPrototype.Game.GameState
{
	abstract class AGameState
	{
		public abstract void Draw(GameTime gameTime);
		public abstract void Update(GameTime gameTime);

		public bool IsFinished { get; set; }
		public AGameState NewState { get; set; }
	}
}
