using UnityEngine;

using MIST.Animations;
using MIST.EQ;
using MIST.Physics;

namespace MIST.Characters.Controllers
{
    public class EnemyController : BaseController
    {
        [Header("Position")]
        [SerializeField] protected bool comeBackToDefaultPos = true;    // true = the enemy returns to the default position
        [SerializeField] protected Transform returnToTransform;         // Can be used when enemy has to come back to starting position

        [Header("Player Detection")]
        [SerializeField] protected SingleColliderDetector colliderDetector; // Detects the transform to follow/look at
        [SerializeField] protected bool lookAtDetectedTransform = true;     // If true then the enemy will look at the player

        [Header("Equipment")]
        [SerializeField] protected EquipmentController equipmentController; // Used to check what type of weapon does the enemy wear

        protected override void Awake()
        {
            base.Awake();

            colliderDetector = GetComponent<SingleColliderDetector>();

            SetDefaultPosition(); // Set the enemy default position
        }

        private void Update()
        {
            HandleAnimations(); // Handle the enemy animations
        }

        #region movement_and_rotation
        /// <summary>
        /// Handling the enemy movement and rotation
        /// </summary>
        protected override void HandleMovementAndRotation()
        {
            Transform destinationTransform = null; // move and rotate destination

            // Checking if the enemy should move towards the player or return to the default position
            if (colliderDetector.detectedTransform == null && comeBackToDefaultPos)
                destinationTransform = returnToTransform;
            else if (colliderDetector.detectedTransform != null)
                destinationTransform = colliderDetector.detectedTransform;

            if (destinationTransform != null)
            {
                // Checking enemy position - stop moving if the enemy is close enough to the player
                if (!IsCloseEnough())
                    Move(destinationTransform); // move towards Transform

                Rotate(destinationTransform);   // rotate towards Transform
            }
        }

        /// <summary>
        /// Set the position that enemy should return to
        /// </summary>
        protected virtual void SetDefaultPosition() => returnToTransform = transform.parent;

        /// <summary>
        /// Check if the enemy is close enough to the player. It's used to stop enemy movement when attacking the player.
        /// </summary>
        /// <returns></returns>
        private bool IsCloseEnough()
        {
            Vector2 relativePos = colliderDetector.detectedTransform.position - transform.position; // Calculate Vector2 value between enemy and the player

            // Check if the enemy is close enough to the player.
            // It's based on the current weapon equipped by the enemy:
            // - Melee - 1.2f,
            // - Range - depends on weapon range.
            if ((equipmentController.IsWeaponMelee() && relativePos.magnitude < 1.2f) || // melee weapon equipped
                (!equipmentController.IsWeaponMelee() && relativePos.magnitude < equipmentController.GetWeaponAsRange().Range)) // range weapon equipped
                return true;
            else
                return false;
        }
        #endregion

        #region animations
        /// <summary>
        /// Handling the enemy animations
        /// </summary>
        private void HandleAnimations()
        {
            // Checking if enemy can perform attack
            if (colliderDetector.detectedTransform != null && IsCloseEnough())
            {
                animationController.UpdateAnimationType(CharacterAnimationType.Attack); // set the Attack animation
                return;
            }

            // Checking if the enemy velocity is high enough to set the Run animation
            if (GetMovementVelocity() <= 0.2f)
                animationController.UpdateAnimationType(CharacterAnimationType.Idle);   // set the Idle animation
            else
                animationController.UpdateAnimationType(CharacterAnimationType.Run);    // set the Run animation
        }
        #endregion
    }
}