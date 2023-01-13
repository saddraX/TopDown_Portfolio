using UnityEngine;

using MIST.EQ.Items;
using MIST.Physics;

namespace MIST.EQ.Instances
{
    public abstract class ItemInstance : MonoBehaviour
    {
        [Header("Item & Presence")]
        public Item item;
        [SerializeField] protected SpriteRenderer itemSpriteRenderer;

        [Header("Physics")]
        [SerializeField] protected Rigidbody2D rigidbody2d;
        [SerializeField] protected Collider2D collider2d;
        [SerializeField] protected SingleColliderDetector colliderDetector;

        protected virtual void Awake() => rigidbody2d = gameObject.GetComponent<Rigidbody2D>();

        protected virtual void OnEnable() => colliderDetector.OnTransformDetected += OnTransformDetected;

        protected virtual void OnDisable() => colliderDetector.OnTransformDetected -= OnTransformDetected;

        #region item_parameters
        /// <summary>
        /// Set Item parameters and its presence
        /// </summary>
        /// <param name="item"></param>
        /// <param name="canInteract"></param>
        public virtual void SetUpItem(Item item, bool canInteract)
        {
            // Set up item
            this.item = item;

            // Set up presence
            itemSpriteRenderer.sprite = item.Sprite; 
            itemSpriteRenderer.color = item.Color;

            // Set up physics
            collider2d.enabled = canInteract; // Enable collider when item should interact
        }
        #endregion

        #region physics
        /// <summary>
        /// Method called when the collider detector detects Transform
        /// </summary>
        /// <param name="detectedTransform"></param>
        protected abstract void OnTransformDetected(Transform detectedTransform);

        /// <summary>
        /// Set LayerMask to detect by collider detector
        /// </summary>
        /// <param name="layerMask"></param>
        public virtual void SetDetectLayerMask(LayerMask layerMask) => colliderDetector.SetDetectLayerMask(layerMask);
        #endregion
    }
}