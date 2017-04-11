using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPrototype.Game
{
	class Actor
	{
		public Actor( Vector3 position, Model model = null )
		{
			Position = position;
			Model = model;
			
			// move this to a more central position
			if(model != null && Globals.MeshEffect != null)
			{
				foreach (ModelMesh mesh in model.Meshes)
				{
					foreach (ModelMeshPart part in mesh.MeshParts)
					{
						part.Effect = Globals.MeshEffect;
					}
				}
			}

			Rotation = Quaternion.CreateFromAxisAngle(Vector3.Up, 0f);
			AngularVelocity = new Vector3(0, 0, 0.1f);
			Velocity = new Vector3(0);
		}

		public void Update(GameTime gameTime)
		{
			Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
			float l = AngularVelocity.Length();
			Rotation *= Quaternion.CreateFromAxisAngle(AngularVelocity / l, l * (float)gameTime.ElapsedGameTime.TotalSeconds);
		}

		public void Draw()
		{
			WorldMatrix = Matrix.CreateFromQuaternion(Rotation) * Matrix.CreateTranslation(Position);

			foreach (ModelMesh mesh in Model.Meshes)
			{
				Globals.MeshEffect.World = WorldMatrix;
				mesh.Draw();
			}
		}

		public Vector3 Position { get; set; }
		public Quaternion Rotation;

		public Model Model { get; private set; }

		public Matrix WorldMatrix { get; private set; }

		public Vector3 Velocity;
		public Vector3 AngularVelocity;
	}
}
