using UnityEngine;
using UnityEngine.InputSystem;

using MIST.Managers;
using MIST.Camera;
using MIST.Animations;

namespace MIST.Characters.Controllers
{
    public class PlayerController : BaseController
    {
        protected override void Awake()
        {
            base.Awake();

            #region input_events
            // performed input events 
            InputManager.Instance.inputActions.Gameplay.Attack.performed += AttackAnimStart;

            // canceled input events
            InputManager.Instance.inputActions.Gameplay.Attack.canceled += ResetAnim;
            InputManager.Instance.inputActions.Gameplay.Movement.canceled += ResetAnim;
            #endregion
        }

        private void OnDisable()
        {
            #region input_events
            // performed input events 
            InputManager.Instance.inputActions.Gameplay.Attack.performed -= AttackAnimStart;

            // canceled input events
            InputManager.Instance.inputActions.Gameplay.Attack.canceled -= ResetAnim;
            InputManager.Instance.inputActions.Gameplay.Movement.canceled -= ResetAnim;
            #endregion
        }

        #region movement_and_rotation
        // Handling player movement and rotation
        protected override void HandleMovementAndRotation()
        {
            if (InputManager.Instance.inputActions.Gameplay.enabled)
            {
                // Get the direction of movement and rotation
                Vector2 moveDirection = InputManager.Instance.inputActions.Gameplay.Movement.ReadValue<Vector2>();
                Vector2 rotateDirection = InputManager.Instance.inputActions.Gameplay.Rotation.ReadValue<Vector2>();
                
                // Fix to the rotation direction when using the non-gamepad control scheme.
                // The rotation direction was not read from player position on the screen, but bottom left corner of the screen.
                if (!InputManager.Instance.IsUsingGamepadScheme())
                    rotateDirection -= CameraController.WorldToScreenPoint(transform.position);

                // Perform movement and rotation
                Move(moveDirection);
                Rotate(rotateDirection);
            }
        }

        // Handle the player movement
        protected override void Move(Vector2 moveDirection)
        {
            base.Move(moveDirection);

            // Check if the attack button is pressed - that prevents 'one shot' attack when running (the Attack animation should loop).
            if (moveDirection != Vector2.zero && !InputManager.Instance.inputActions.Gameplay.Attack.IsPressed())
                animationController.UpdateAnimationType(CharacterAnimationType.Run);
        }

        #endregion

        #region animations_handling
        // Start the Attack animation
        private void AttackAnimStart(InputAction.CallbackContext obj) => animationController.UpdateAnimationType(CharacterAnimationType.Attack);

        // Reset the animation after Attack or Movement InputAction was canceled
        private void ResetAnim(InputAction.CallbackContext obj)
        {
            // Check if attack button is pressed - that prevents canceling the Attack animation when the player stops running
            if (InputManager.Instance.inputActions.Gameplay.Attack.IsPressed())
                return;

            // Check movement input value and set current animation type to Run or Idle
            Vector2 moveDirection = InputManager.Instance.inputActions.Gameplay.Movement.ReadValue<Vector2>();

            if (moveDirection == Vector2.zero)
                animationController.UpdateAnimationType(CharacterAnimationType.Idle);
            else
                animationController.UpdateAnimationType(CharacterAnimationType.Run);
        }
        #endregion
    }
}