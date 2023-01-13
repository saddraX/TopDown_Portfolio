using UnityEngine;

namespace MIST.EQ.Items
{
    public abstract class Weapon : EquipableItem
    {
        [SerializeField] private float _attackSpeed = 1f;
        [SerializeField] private int _baseDamage = 10;

        public float AttackSpeed { get { return _attackSpeed; } }
        public int BaseDamage { get { return _baseDamage; } }
    }
}