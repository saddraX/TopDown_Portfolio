using UnityEngine;

namespace MIST.EQ.Items
{
    public class EquipableItem : Item
    {
        public enum EquipableItemQuality
        {
            Common,
            Rare,
            Legendary
        }

        [SerializeField] private float _weight;
        [SerializeField] private EquipableItemQuality _itemQuality = EquipableItemQuality.Common;

        public float Weight { get { return _weight; } }
        public EquipableItemQuality ItemQuality { get { return _itemQuality; } }
    }
}

