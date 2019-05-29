using UnityEngine;

namespace Platformer_3D.Extensions
{
    public static class CameraExtensions
    {
        public static Vector3 ScaledForward(this Camera cam)
        {
            return Vector3.Scale(cam.transform.forward, new Vector3(1, 0, 1)).normalized;
        }

        public static Vector3 ScaledRight(this Camera cam)
        {
            return Vector3.Scale(cam.transform.right, new Vector3(1, 0, 1)).normalized;
        }
    }
}
