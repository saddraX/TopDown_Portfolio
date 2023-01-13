using UnityEngine;

using MIST.IngameObject;
using MIST.EQ;

namespace MIST.Characters
{
    public class Character : Hittable
    {
        [Header("Character Stats")]
        public string characterName = string.Empty;

        [Header("Controllers")]
        [SerializeField] protected EquipmentController equipmentController; // used for damage calculations or retrieving info about current weapon
    }
}

