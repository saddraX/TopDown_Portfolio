using UnityEngine;

namespace MIST.Camera
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance; // Camera controller instance

        public static UnityEngine.Camera mainCamera; // Main camera

        [Header("Target")]
        [SerializeField] private Transform target; // Target the camera should follow

        [Header("Camera Movement")]
        // Camera movement variables
        [SerializeField] private float smoothTime = 0.1f;
        [SerializeField] private float maxSpeed = 10f;
        private Vector2 currentVelocity = new Vector2();

        [Header("Movement Boundaries")]
        // Boundaries of the camera movement
        [SerializeField] private Vector2 minPosition;
        [SerializeField] private Vector2 maxPosition;

        private void Awake()
        {
            Instance = this;

            mainCamera = UnityEngine.Camera.main;
        }

        void LateUpdate()
        {
            if (target == null)
                return;

            // Calculate the new position for the camera (with smooth effect)
            var newPos = Vector2.SmoothDamp(transform.position, target.position, ref currentVelocity, smoothTime, maxSpeed);

            // Clamp the camera's position to the defined boundaries
            newPos.x = Mathf.Clamp(newPos.x, minPosition.x, maxPosition.x);
            newPos.y = Mathf.Clamp(newPos.y, minPosition.y, maxPosition.y);

            // Set the camera's position to the new position
            transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        }

        /// <summary>
        /// Get the position of the screen center
        /// </summary>
        /// <returns></returns>
        public static Vector2 GetScreenCenter() => new Vector2(Screen.width / 2, Screen.height / 2);

        public static Vector2 WorldToScreenPoint(Vector3 worldPos) => mainCamera.WorldToScreenPoint(worldPos);

        #region drawing_gizmos
        private void OnDrawGizmos()
        {
            DrawZone();
        }

        /// <summary>
        /// Draw rectangle using camera movement boundaries
        /// </summary>
        private void DrawZone()
        {
            Gizmos.color = new Color(0, 1, 0, 0.1f);

            Gizmos.DrawCube((minPosition + maxPosition) / 2, (maxPosition - minPosition));
            Gizmos.DrawWireCube((minPosition + maxPosition) / 2, (maxPosition - minPosition));
        }
        #endregion
    }
}