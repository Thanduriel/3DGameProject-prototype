using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3DPrototype.Game
{
	class Player : Actor
	{
		public Player(Vector3 position, Model model)
			: base(position, model)
		{
			_circumference = BoundingRadius * 2 * (float)Math.PI;
		}

		float _circumference;

		public override void Update(GameTime gameTime)
		{
			// slow down movement in y dir
			Velocity.Y -= Velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;

			base.Update(gameTime);

			Vector3 axis = Vector3.Cross(new  Vector3(0.2f, 0f, 1f)/*Vector3.UnitZ*/, Velocity);
			axis.Normalize();
			AngularVelocity = axis * (Velocity.Length() / _circumference * 2 * (float)Math.PI);
	//		if (Math.Abs(Velocity.Y) > Math.Abs(Velocity.X)) AngularVelocity *= -1f;
		}
	}
}
