using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPrototype.game
{
	class Actor
	{
		public Actor( Vector3 position, Model model = null )
		{
			Position = position;
			Model = model;
		}
		public Vector3 Position { get; private set; }

		public Model Model { get; private set; }
	}
}
