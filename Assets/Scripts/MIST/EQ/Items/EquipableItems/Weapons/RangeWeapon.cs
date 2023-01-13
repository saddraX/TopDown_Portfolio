using UnityEngine;

namespace MIST.EQ.Items
{
    [CreateAssetMenu(menuName = "CustomAssets/Weapons/RangeWeapon")]
    public class RangeWeapon : Weapon
    {
        [SerializeField] private float _range = 2f;

        public float Range { get { return _range; } }
    }
}

