using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3DPrototype.Game
{
	class Bouncer : Actor
	{
		public Bouncer(Vector3 position) : base(position, Globals.ContentManager.Load<Model>("zylinder"))
		{
		//	Rotation = Rotation *= Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), -(float)Math.Atan(0.2f));
			Scale = new Vector3(1.1f);
			BoundingRadius *= 0.8f;
			_color = new Vector3(1, 0, 0);
		}

		public override void Collide(Actor player)
		{
			Vector3 hitNormal = Vector3.Normalize(player.Position - Position);

			float v = Math.Abs(player.Velocity.X); // only downward speed gives points
			if (v > Globals.MinimumVelocity)
			{

				Globals.PlayerScore += (int)(Globals.VelocityToPoints * v * v);
			}

			// set back the player
			player.Position = Position + hitNormal * (player.BoundingRadius + BoundingRadius);

			float len = player.Velocity.Length();
			float α = (float)Math.Acos(hitNormal.X); // angle between hitNormal and UnitX
	//		if (α < 0.5f) player.Velocity = new Vector3(0);
			player.Velocity += (α / (float)Math.PI * 0.0f + 1f) * hitNormal * len * 1.5f;
		}
	}
}
