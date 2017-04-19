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
			Scale = Vector3.One;

			_color = new Vector3(0.5f);

			// todo: move this to a more central position
			foreach (ModelMesh mesh in Model.Meshes)
			{
				BasicEffect effect = (BasicEffect)mesh.Effects[0];
			//	effect.LightingEnabled = true;
			//	effect.PreferPerPixelLighting = true;
				effect.EnableDefaultLighting();
			}
			/*		if(model != null && Globals.MeshEffect != null)
					{
						foreach (ModelMesh mesh in model.Meshes)
						{
							foreach (ModelMeshPart part in mesh.MeshParts)
							{
								part.Effect = Globals.MeshEffect;
							}
						}
					}*/
			BoundingRadius = Model.Meshes[0].BoundingSphere.Radius;
			Rotation = Quaternion.CreateFromAxisAngle(Vector3.Up, 0f);
			AngularVelocity = new Vector3(0, 0, 0);
			Velocity = new Vector3(0);
		}

		public virtual void Update(GameTime gameTime)
		{
			float dT = (float)gameTime.ElapsedGameTime.TotalSeconds;
			Position += Velocity * dT;
			
			float l = AngularVelocity.Length();
			if (l > 0.001f)
			{
				Quaternion deltaRot = new Quaternion();
				Vector3 theta = AngularVelocity * dT;
				float thetaMagSq = theta.LengthSquared();
				float s;
				if (thetaMagSq * thetaMagSq / 24.0f < 1e-6f)
				{
					deltaRot.W = 1.0f - thetaMagSq / 2.0f;
					s = 1.0f - thetaMagSq / 6.0f;
				}
				else
				{
					float thetaMag = (float)Math.Sqrt(thetaMagSq);
					deltaRot.W = (float)Math.Cos(thetaMag);
					s = (float)Math.Sin(thetaMag) / thetaMag;
				}
				deltaRot.X = theta.X * s;
				deltaRot.Y = theta.Y * s;
				deltaRot.Z = theta.Z * s;
				Rotation = deltaRot * Rotation;
			}
		}

		public virtual void Draw()
		{
			WorldMatrix = Matrix.CreateScale(Scale)
				* Matrix.CreateFromQuaternion(Rotation)
				* Matrix.CreateTranslation(Position);

			foreach (ModelMesh mesh in Model.Meshes)
			{
				BasicEffect effect = (BasicEffect)mesh.Effects[0];
				effect.World = WorldMatrix;
				Globals.Camera.Set(effect);
				effect.DiffuseColor = _color;
				effect.PreferPerPixelLighting = PerPixelLightning;
				mesh.Draw();
			}
		}

		public virtual void Collide(Actor player)
		{
		}

		public Vector3 Position { get; set; }
		public Quaternion Rotation;
		public Vector3 Scale;
		public float BoundingRadius;

		public Model Model { get; private set; }
		protected Vector3 _color;
		public bool PerPixelLightning = false;

		public Matrix WorldMatrix { get; private set; }

		public Vector3 Velocity;
		public Vector3 AngularVelocity;
	}
}
