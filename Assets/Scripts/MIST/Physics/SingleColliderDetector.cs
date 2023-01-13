using System;
using UnityEngine;

namespace MIST.Physics
{
    public class SingleColliderDetector : MonoBehaviour
    {
        [Header("Collision detect parameters")]
        [SerializeField] private LayerMask detectLayer; // Layers on which the collisions will be detected
        public Transform detectedTransform; // Transform that script has detected using collisions

        public event Action<Transform> OnTransformDetected; // Invoked when the transform is detected

        #region collision_detection
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if ((detectLayer.value & (1 << collision.gameObject.layer)) > 0 && detectedTransform == null)
            {
                detectedTransform = collision.transform;
                OnTransformDetected?.Invoke(detectedTransform);

                //Debug.Log($"{collision.gameObject.name} detected by: {gameObject.name}");
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            if ((detectLayer.value & (1 << collision.gameObject.layer)) > 0 && detectedTransform != null)
            {
                detectedTransform = null;

                //Debug.Log($"{collision.gameObject.name} lost by: {gameObject.name}");
            }
        }
        #endregion

        /// <summary>
        /// Update detected LayerMask by given value
        /// </summary>
        /// <param name="layerMask"></param>
        public virtual void SetDetectLayerMask(LayerMask layerMask) => detectLayer = layerMask;
    }
}