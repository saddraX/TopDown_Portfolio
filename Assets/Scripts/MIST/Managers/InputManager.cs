using UnityEngine;
using UnityEngine.InputSystem;

namespace MIST.Managers
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        [Header("Input components")]
        public PlayerInput playerInput;     // Can be used to check current control scheme
        public InputActions inputActions;   // Used to get input from the player

        private void Awake()
        {
            Instance = this;

            // Init inputActions
            inputActions = new InputActions();
            inputActions.UI.Disable();
            inputActions.Gameplay.Enable();
        }

        /// <summary>
        /// Check if Gamepad is current control scheme
        /// </summary>
        /// <returns></returns>
        public bool IsUsingGamepadScheme()
        {
            if (playerInput.currentControlScheme == "Gamepad")
                return true;
            else
                return false;
        }
    }
}