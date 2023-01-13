using UnityEngine;
using MIST.Animations;

namespace MIST.Characters.Controllers
{
    public abstract class BaseController : MonoBehaviour
    {
        [Header("Controllers")]
        [SerializeField] protected CharacterAnimationController animationController; // Character animation controller

        [Header("Physics")]
        protected Rigidbody2D rigidbody2d; // Character rigidbody

        [Header("Movement Stats")]
        [SerializeField] protected float movementSpeed = 1f; // Speed of character movement
        [SerializeField] [Range(0f, 1f)] protected float rotationSmooth = 0.5f; // The Quaternion.Slerp 't' argument used to smooth the character rotation

        protected virtual void Awake() => rigidbody2d = GetComponent<Rigidbody2D>();

        protected virtual void FixedUpdate() => HandleMovementAndRotation();

        #region movement_and_rotation
        /// <summary>
        /// Handling the character movement and rotation
        /// </summary>
        protected abstract void HandleMovementAndRotation();

        #region movement
        /// <summary>
        /// Move towards Vector2 (using physics)
        /// </summary>
        /// <param name="moveDirection"></param>
        protected virtual void Move(Vector2 moveDirection)
        {
            if (moveDirection != Vector2.zero)
                rigidbody2d.AddForce(moveDirection * movementSpeed);    // move character using physics
        }

        /// <summary>
        /// Move towards Transform
        /// </summary>
        /// <param name="moveDirectionTransform"></param>
        protected virtual void Move(Transform moveDirectionTransform)
        {
            if (moveDirectionTransform == null)
                return;

            Vector2 relativePos = moveDirectionTransform.position - transform.position;

            if (relativePos.magnitude > 1.2f)
                Move(relativePos.normalized);
        }

        /// <summary>
        /// Get movement velocity from rigidbody
        /// </summary>
        /// <returns></returns>
        protected virtual float GetMovementVelocity() => rigidbody2d.velocity.magnitude;
        #endregion

        #region rotation
        /// <summary>
        /// Rotate towards Vector2
        /// </summary>
        /// <param name="rotateDirection"></param>
        protected virtual void Rotate(Vector2 rotateDirection)
        {
            if (rotateDirection != Vector2.zero)
                LookAtDirection(rotateDirection);                      // look at rotate direction
        }

        /// <summary>
        /// Rotate towards Transform
        /// </summary>
        /// <param name="rotateDirectionTransform"></param>
        protected virtual void Rotate(Transform rotateDirectionTransform)
        {
            if (rotateDirectionTransform == null)
                return;

            Vector2 relativePos = rotateDirectionTransform.position - transform.position;

            if (relativePos != Vector2.zero)
                LookAtDirection(relativePos);
        }

        /// <summary>
        /// Look towards Vector2
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="smooth"></param>
        protected virtual void LookAtDirection(Vector2 direction, bool smooth = true)
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (smooth)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotationSmooth);
            else
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        /// <summary>
        /// Look towards Transform
        /// </summary>
        /// <param name="destTransform"></param>
        public void LookAtTransform(Transform destTransform)
        {
            if (destTransform == null)
                return;

            var direction = destTransform.position - transform.position;

            LookAtDirection(direction, true);
        }
        #endregion

        #endregion
    }
}