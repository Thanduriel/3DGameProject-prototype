using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPrototype
{
	class Globals
	{
		// some evil globals
		public static Microsoft.Xna.Framework.Content.ContentManager ContentManager;
		public static Microsoft.Xna.Framework.Graphics.BasicEffect MeshEffect;
		public static Graphic.Camera Camera;

		// player score
		public static int PlayerScore;
		public const float MinimumVelocity = 3.5f; // minimal velocity that gives points
		public const float VelocityToPoints = 5; // ratio for points per [m/s] on collision
	}
}
