using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPrototype.Game
{
	class World01 : World
	{
		public World01() : base()
		{
			_gameObjects.Add(new Bouncer(AlignedPosition(-10, 0)));
			_gameObjects.Add(new Bouncer(AlignedPosition(-12, 3)));
			_gameObjects.Add(new Bouncer(AlignedPosition(-12, -3)));

			_gameObjects.Add(new Bouncer(AlignedPosition(-30, -6)));
			//		_gameObjects.Add(new Bouncer(AlignedPosition(-31, -4)));
			_gameObjects.Add(new Bouncer(AlignedPosition(-32, -2)));

			_gameObjects.Add(new Bouncer(AlignedPosition(-63, 0)));

			_gameObjects.Add(new Obstacle(AlignedPosition(-35, 3), new Vector3(0.5f, 2f, 1f), (float)Math.PI * -0.25f));
			_gameObjects.Add(new Obstacle(AlignedPosition(-39, -7), new Vector3(2f, 0.5f, 1f), (float)Math.PI));
		}
	}

	class World02 : World
	{
		public World02() : base(14)
		{
			_gameObjects.Add(new Bouncer(AlignedPosition(-10, 5)));
			_gameObjects.Add(new Bouncer(AlignedPosition(-10, -5)));

			_gameObjects.Add(new Bouncer(AlignedPosition(-14, 5)));
			_gameObjects.Add(new Bouncer(AlignedPosition(-14, -5)));

			_gameObjects.Add(new Bouncer(AlignedPosition(-19, 5)));
			_gameObjects.Add(new Bouncer(AlignedPosition(-19, -5)));

			_gameObjects.Add(new Bouncer(AlignedPosition(-24, 5)));

			_gameObjects.Add(new Bouncer(AlignedPosition(-29, 5)));
			_gameObjects.Add(new Bouncer(AlignedPosition(-29, -5)));

			_gameObjects.Add(new Bouncer(AlignedPosition(-35, -5)));

			//	_gameObjects.Add(new Obstacle(AlignedPosition(-35, 3), new Vector3(0.5f, 2f, 1f), (float)Math.PI * -0.25f));
			_gameObjects.Add(new Obstacle(AlignedPosition(-46, 10), new Vector3(2f, 0.5f, 1f), (float)Math.PI * 0.25f));
			_gameObjects.Add(new Obstacle(AlignedPosition(-46, -10), new Vector3(2f, 0.5f, 1f), (float)Math.PI * -1.25f));

			_gameObjects.Add(new Bouncer(AlignedPosition(-64, 6)));
		}
	}
}
