using UnityEngine;

namespace MIST.EQ.Items
{
    [CreateAssetMenu(menuName = "CustomAssets/Weapons/MeleeWeapon")]
    public class MeleeWeapon : Weapon
    {
        [SerializeField] private bool _isOneHanded;

        public bool IsOneHanded { get { return _isOneHanded; } }
    }
}

