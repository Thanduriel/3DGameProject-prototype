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
			Rotation = Quaternion.CreateFromAxisAngle(Vector3.Up, 0f);
			AngularVelocity = new Vector3(0, 0, 0.1f);
			Velocity = new Vector3(0);
		}

		public void Update(GameTime gameTime)
		{
			Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
			float l = AngularVelocity.Length();
			Rotation *= Quaternion.CreateFromAxisAngle(AngularVelocity / l, l * (float)gameTime.ElapsedGameTime.TotalSeconds);
			WorldMatrix = Matrix.CreateFromQuaternion(Rotation) * Matrix.CreateTranslation(Position);
		}

		public Vector3 Position { get; private set; }
		public Quaternion Rotation;

		public Model Model { get; private set; }

		public Matrix WorldMatrix { get; private set; }

		public Vector3 Velocity;
		public Vector3 AngularVelocity;
	}
}
