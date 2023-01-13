using UnityEngine;

using MIST.EQ.Items;
using MIST.Managers;

namespace MIST.EQ
{
    [System.Serializable]
    public class Equipment
    {
        [Header("Equipment")]
        [SerializeField] private Weapon weapon;
        [SerializeField] private Ammo ammo;

        public Weapon Weapon
        {
            get
            {
                // if the character has no weapon then equip default weapon (fists)
                if (weapon == null)
                    weapon = ItemManager.Instance.GetDefaultWeapon();

                return weapon;
            }
            set { weapon = value; }
        }

        public Ammo Ammo
        {
            get { return ammo; }
            set { ammo = value; }
        }
    }
}
