using UnityEngine;
using UnityEngine.UI;

using MIST.EQ;

namespace MIST.UI
{
    public class UIEquipmentSlot : MonoBehaviour
    {
        enum EquipmentSlotType
        {
            Weapon,
            Ammo
        }

        [SerializeField] private EquipmentSlotType equipmentSlotType;
        [SerializeField] private Image itemImage;

        /// <summary>
        /// Update EQ slot presence
        /// </summary>
        /// <param name="equipment"></param>
        public void UpdateSlotPresence(Equipment equipment)
        {
            Sprite itemSprite = null;
            Color itemColor = Color.white;

            // Chcek for current EquipmentSlotType
            switch (equipmentSlotType)
            {
                case EquipmentSlotType.Weapon:
                    // Get weapon sprite and color
                    if (equipment.Weapon != null)
                    {
                        itemSprite = equipment.Weapon.Sprite;
                        itemColor = equipment.Weapon.Color;
                    }
                    break;
                case EquipmentSlotType.Ammo:
                    // Get ammo sprite and color
                    if (equipment.Ammo != null)
                    {
                        itemSprite = equipment.Ammo.Sprite;
                        itemColor = equipment.Ammo.Color;
                    }
                    break;
            }

            // Update slot presence 
            itemImage.sprite = itemSprite;
            itemImage.color = itemColor;

            itemImage.enabled = itemSprite != null ? true : false; // Check if itemImage should be enabled
        }
    }
}