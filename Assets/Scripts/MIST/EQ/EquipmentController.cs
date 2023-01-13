using System;
using UnityEngine;

using MIST.EQ.Items;
using MIST.Managers;

namespace MIST.EQ
{
    public class EquipmentController : MonoBehaviour
    {
        [Header("Equipment")]
        public Equipment equipment; // Contains character equipment

        [Header("Presence")]
        // Equipment sprite renderers
        [SerializeField] private SpriteRenderer weaponSpriteRenderer;

        // Invoke this event to make sure that the character equipment sprites was updated.
        // It's used also to update animator parameters and UI (if this is the player equipment)
        public event Action OnEquipmentChanged; 

        private void Awake() => OnEquipmentChanged += UpdateCharacterSprites;

        private void OnDestroy() => OnEquipmentChanged -= UpdateCharacterSprites;

        private void OnEnable() => OnEquipmentChanged?.Invoke(); 

        private void Update()
        {
            // In debug mode - equip random weapon after pressing Space
            if (Input.GetKeyDown(KeyCode.Space) && GameManager.Instance.debugMode)
                SetRandomWeapon();
        }

        #region weapon_and_ammo
        /// <summary>
        /// Check if the character has equipped any Ammo
        /// </summary>
        /// <returns></returns>
        public bool IsAmmoEquipped() => equipment.Ammo != null;

        /// <summary>
        /// Check if the character has equipped any Weapon
        /// </summary>
        /// <returns></returns>
        public bool IsWeaponEquipped() => equipment.Weapon != null;

        /// <summary>
        /// Check if the character has equipped the melee weapon
        /// </summary>
        /// <returns></returns>
        public bool IsWeaponMelee()
        {
            if (IsWeaponEquipped())
                return equipment.Weapon.GetType() == typeof(MeleeWeapon);
            else
                return true; // Return true when using fists (they are melee weapon)
        }

        /// <summary>
        /// Get current weapon as MeleeWeapon type
        /// </summary>
        /// <returns></returns>
        public MeleeWeapon GetWeaponAsMelee()
        {
            if (IsWeaponMelee())
                return (MeleeWeapon)equipment.Weapon;
            else
                return null;
        }

        /// <summary>
        /// Get current weapon as RangeWeapon type
        /// </summary>
        /// <returns></returns>
        public RangeWeapon GetWeaponAsRange()
        {
            if (!IsWeaponMelee())
                return (RangeWeapon)equipment.Weapon;
            else
                return null;
        }

        /// <summary>
        /// Equip random weapon
        /// </summary>
        public void SetRandomWeapon()
        {
            Weapon randomWeapon = ItemManager.Instance.GetRandomWeapon();
            EquipOrPickUpItem(randomWeapon);
        }
        
        /// <summary>
        /// Equip random ammo
        /// </summary>
        public void SetRandomAmmo()
        {
            Ammo randomAmmo = ItemManager.Instance.GetRandomAmmo();
            EquipOrPickUpItem(randomAmmo);
        }
        #endregion

        #region equipment_presence
        /// <summary>
        /// Update character sprites and colors
        /// </summary>
        public void UpdateCharacterSprites()
        {
            // Update weapon presence
            weaponSpriteRenderer.sprite = equipment.Weapon.Sprite;
            weaponSpriteRenderer.color = equipment.Weapon.Color;
        }
        #endregion

        #region equipment_management
        /// <summary>
        /// Equip or pick up item. Equip if the item is equipable or pick up non equipable (coins, buffs etc.)
        /// </summary>
        /// <param name="item"></param>
        public void EquipOrPickUpItem(Item item)
        {
            if (item == null)
            {
                Debug.LogWarning("Item to equip is null!");
                return;
            }

            Type itemType = item.GetType(); // Get item type

            //Debug.Log(itemType.ToString());

            // Checking the item type
            // Equip EquipableItem
            if (itemType.BaseType.BaseType == typeof(EquipableItem) || itemType.BaseType == typeof(EquipableItem))
            {
                // Equip Weapon
                if (itemType.BaseType == typeof(Weapon))
                {
                    // Equip MeleeWeapon
                    if (itemType == typeof(MeleeWeapon))
                        equipment.Weapon = (MeleeWeapon)item;
                    // Equip RangeWeapon
                    else if (itemType == typeof(RangeWeapon))
                        equipment.Weapon = (RangeWeapon)item;
                }
                // Equip Ammo
                else if (itemType == typeof(Ammo))
                    equipment.Ammo = (Ammo)item;

                // Invoke event
                OnEquipmentChanged?.Invoke();
            }
            // Pick up NonEquipableItem
            else if (itemType.BaseType.BaseType == typeof(NonEquipableItem))
            {
                // TODO: Pick up items such as coins, buffs or questItems
            }
        }
        #endregion
    }
}

