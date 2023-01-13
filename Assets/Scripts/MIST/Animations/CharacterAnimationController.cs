using System;
using UnityEngine;

using MIST.Characters.Controllers;
using MIST.Combat;
using MIST.EQ;

namespace MIST.Animations
{
    public class CharacterAnimationController : MonoBehaviour
    {
        [Header("CharacterControllers")]
        [SerializeField] protected BaseController characterController;
        [SerializeField] protected CombatController combatController;
        [SerializeField] protected EquipmentController equipmentController;

        [Header("Animation")]
        [SerializeField] protected Animator animator;
        [SerializeField] protected CharacterAnimationType currentAnimationType = CharacterAnimationType.Idle;

        // events
        public event Action<CharacterAnimationType> OnAnimationTypeChanged;

        private void Awake() => equipmentController.OnEquipmentChanged += UpdateWeaponParameters;

        private void OnDestroy() => equipmentController.OnEquipmentChanged -= UpdateWeaponParameters;
        
        private void OnEnable() => UpdateAnimationType(CharacterAnimationType.Idle);  // set animation type to default (Idle)

        #region animation_type_handling
        /// <summary>
        /// Getting current animation type
        /// </summary>
        public virtual CharacterAnimationType GetCurrentAnimationType() => currentAnimationType;

        /// <summary>
        /// Update current animation type
        /// </summary>
        /// <param name="animationType"></param>
        public virtual void UpdateAnimationType(CharacterAnimationType animationType)
        {
            // Check if the new animation type is equal to current animation type
            if (animationType == currentAnimationType)
                return;

            // Prevent setting animation type to Attack when the character has no ammo to shoot
            if (animationType == CharacterAnimationType.Attack && !equipmentController.IsWeaponMelee() && !equipmentController.IsAmmoEquipped())
                return;

            currentAnimationType = animationType;           // update currentAnimationType
            OnAnimationTypeChanged?.Invoke(animationType);  // invoke OnAnimationTypeChanged if someone's subscribing
                                                            // (for example in situation, where enemy should update
                                                            // behaviour when player animation change)

            SetParameterValue("AnimationType", (int)animationType); // set animation type in animator 
        }
        #endregion

        #region animator_parameters_handling
        // Set new parameter values (int, float, bool)
        public virtual void SetParameterValue(string parameter, int value) => animator.SetInteger(parameter, value);
        public virtual void SetParameterValue(string parameter, float value) => animator.SetFloat(parameter, value);
        public virtual void SetParameterValue(string parameter, bool value) => animator.SetBool(parameter, value);

        /// <summary>
        /// Update animator parameters such as WeaponType and AttackSpeed. Values depends on stats of character equipped weapon.
        /// </summary>
        public virtual void UpdateWeaponParameters()
        {
            float weaponType = equipmentController.IsWeaponMelee() ? 0 : 1; // 0 - melee weapon | 1 - range weapon
            SetParameterValue("WeaponType", weaponType); // set WeaponType parameter

            SetParameterValue("AttackSpeed", equipmentController.equipment.Weapon.AttackSpeed); // set AttackSpeed parameter to current weapon attack speed
        }
        #endregion

        #region animation_events
        public virtual void PerformAttack() => combatController.PerformAttack();
        #endregion
    }
}