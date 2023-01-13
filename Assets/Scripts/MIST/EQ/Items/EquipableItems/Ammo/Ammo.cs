using UnityEngine;

namespace MIST.EQ.Items
{
    [CreateAssetMenu(menuName = "CustomAssets/Ammo")]
    public class Ammo : EquipableItem
    {
        [SerializeField] private float _flySpeed = 1f;
        [SerializeField] private int _damage = 1;

        public float FlySpeed { get { return _flySpeed; } }
        public int Damage { get { return _damage; } }
    }

}
