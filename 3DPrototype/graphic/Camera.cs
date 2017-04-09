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
			worldMatrix = Matrix.CreateWorld(camTarget, Vector3.
						  Forward, Vector3.Up);
		}

		public void Set(BasicEffect effect)
		{
			effect.AmbientLightColor = new Vector3(1f, 0, 0);
			effect.View = viewMatrix;
			effect.World = worldMatrix;
			effect.Projection = projectionMatrix;
		}

		//Camera
		Vector3 camTarget;
		Vector3 camPosition;
		Matrix projectionMatrix;
		Matrix viewMatrix;
		Matrix worldMatrix;
	}
}
