using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPrototype.Game
{
	struct Line
	{
		public Vector3 begin;
		public Vector3 end;
		public Vector3 normal;
	}

	class Obstacle : Actor
	{
		public Obstacle(Vector3 position, Vector3 size, float rotation) : base(position, Globals.ContentManager.Load<Model>("cube"))
		{
			//	Rotation = Rotation *= Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), -(float)Math.Atan(0.2f));
			Scale = size;
			_color = new Vector3(0, 0, 1);

			BoundingRadius *= Math.Max(Scale.X, Scale.Y);

			Matrix rot = Matrix.CreateRotationZ(rotation);

			_lines[0].begin = new Vector3(size.X, -size.Y, 0f);
			_lines[0].end = new Vector3(size.X, size.Y, 0f);
			_lines[1].begin = new Vector3(size.X, -size.Y, 0f);
			_lines[1].end = new Vector3(-size.X, -size.Y, 0f);
			_lines[2].begin = new Vector3(size.X, size.Y, 0f);
			_lines[2].end = new Vector3(-size.X, size.Y, 0f);
			_lines[3].begin = new Vector3(-size.X, -size.Y, 0f);
			_lines[3].end = new Vector3(-size.X, size.Y, 0f);

			for (int i = 0; i < 4; ++i)
			{
				Transform(ref _lines[i].begin, ref rot);
				Transform(ref _lines[i].end, ref rot);

				_lines[i].normal = Vector3.Cross(_lines[i].end - _lines[i].begin, Vector3.UnitZ);
				_lines[i].normal.Normalize();
			}
			_lines[1].normal *= -1f;
		//	_lines[2].normal *= -1f;
			_lines[3].normal *= -1f;

			Rotation = Quaternion.CreateFromRotationMatrix(rot);
		}

		static float projectionFactor;

		static float minDist(Vector3 v, Vector3 w, Vector3 p)
		{
			float l2 = (v- w).LengthSquared();
			if (l2 == 0.0) return (p- v).Length();

			projectionFactor = Math.Max(0f, Math.Min(1f, Vector3.Dot((p - v), (w - v)) / l2));

			Vector3 projection = v + projectionFactor * (w - v);  // Projection falls on the segment
			return (p - projection).Length();
		}

		public override void Collide(Actor player)
		{
			foreach(Line l in _lines)
			{
				Vector3 hitNormal = l.normal;
				float d = minDist(l.begin, l.end, player.Position);
		
				if (d < player.BoundingRadius)
				{
					// when closer to the edges use different normals
					if (projectionFactor <= 0.1f)
					{
						hitNormal = player.Position - l.begin;
						hitNormal.Normalize();
					}
					else if (d >= 0.9f)
					{
						hitNormal = player.Position - l.end;
						hitNormal.Normalize();
					}

					// set back the player
					player.Position -= hitNormal * (d - player.BoundingRadius);

					player.Velocity = hitNormal * player.Velocity.Length() * 0.95f;
					break;
				}
			}
		}

		private void Transform(ref Vector3 vec, ref Matrix rot)
		{
			vec = Vector3.Transform(vec, rot);
			vec += Position;
		}

		private Line[] _lines = new Line[4];
	}
}
