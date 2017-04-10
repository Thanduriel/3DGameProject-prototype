using _3DPrototype.game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPrototype.graphic
{
	class Camera
	{
		public Camera (GraphicsDeviceManager graphics)
		{
			camTarget = new Vector3(0f, 0f, 0f);
			camPosition = new Vector3(0f, 0f, -5);
			projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
							   MathHelper.ToRadians(45f), graphics.
							   GraphicsDevice.Viewport.AspectRatio,
				1f, 1000f);
			viewMatrix = Matrix.CreateLookAt(camPosition, camTarget,
						 new Vector3(0f, 1f, 0f));// Y up
		}

		public void Update()
		{
			if (!isAttached) return;

			camTarget = target.Position;
			camPosition = camTarget + new Vector3(0f, 0f, -5f);

			viewMatrix = Matrix.CreateLookAt(camPosition, camTarget,
						 new Vector3(0f, 1f, 0f));// Y up
		}

		public void Set(BasicEffect effect)
		{
			effect.AmbientLightColor = new Vector3(1f, 0, 0);
			effect.View = viewMatrix;
			effect.Projection = projectionMatrix;
		}

		public void Attach(Actor actor)
		{
			target = actor;
			isAttached = true;
		}

		
		Vector3 camTarget;
		Vector3 camPosition;
		Matrix projectionMatrix;
		Matrix viewMatrix;

		Actor target;
		bool isAttached;
	}
}
