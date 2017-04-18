using _3DPrototype.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPrototype.Graphic
{
	class Camera
	{
		public Camera (GraphicsDeviceManager graphics)
		{
			camTarget = new Vector3(0f, 0f, 0f);
			camPosition = new Vector3(0f, 0f, 5);
			prevPosition = camPosition;
			projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
							   MathHelper.ToRadians(75f), graphics.
							   GraphicsDevice.Viewport.AspectRatio,
				1f, 1000f);
			viewMatrix = Matrix.CreateLookAt(camPosition, camTarget,
						 new Vector3(0f, 1f, 0f));// Y up
		}
		
		public void Update(GameTime gameTime)
		{
			if (!isAttached) return;

			// interpolate colors
			AmbientColor += 0.5f * (float)gameTime.ElapsedGameTime.TotalSeconds
				* (TargetAmbientColor - AmbientColor);

			camTarget = target.Position + new Vector3(0f, 0f, 5f);
		//	camPosition.Normalize();
			camPosition = prevPosition + (camTarget - prevPosition) * 0.033f * (float)gameTime.TotalGameTime.TotalSeconds;
			prevPosition = camPosition;

			viewMatrix = Matrix.CreateLookAt(camPosition, camPosition - new Vector3(0f,0f,5f),
						 new Vector3(1f, 0f, 0f));// Y up
		}

		public void Set(BasicEffect effect)
		{
			effect.AmbientLightColor = AmbientColor;
			effect.View = viewMatrix;
			effect.Projection = projectionMatrix;
		}

		public void Attach(Actor actor)
		{
			target = actor;
			isAttached = true;
		}

		public Vector3 AmbientColor { get; set; }
		public Vector3 TargetAmbientColor { get; set; }

		Vector3 camTarget;
		Vector3 camPosition;
		Vector3 prevPosition;
		Matrix projectionMatrix;
		Matrix viewMatrix;

		Actor target;
		bool isAttached;
	}
}
