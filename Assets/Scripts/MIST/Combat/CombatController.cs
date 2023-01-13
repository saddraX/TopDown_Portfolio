using UnityEngine;

using MIST.EQ;
using MIST.EQ.Items;
using MIST.EQ.Instances;
using MIST.IngameObject;
using MIST.ObjectSpawn;

namespace MIST.Combat
{
    public class CombatController : MonoBehaviour
    {
        [Header("Controllers")]
        [SerializeField] private EquipmentController equipmentController; // Used to get the equipped items and their values

        [Header("Combat Transforms")]
        [SerializeField] private Transform shootStartPoint;     // The point from where the ammo should start flying
        [SerializeField] private Transform meleeHitTransform;   // The point where melee attack physics should be triggered
        
        [Header("Physics")]
        [SerializeField] LayerMask hittableLayerMask; // Specifies which layer should be hittable by this character

        [Header("Poolers")]
        [SerializeField] private ObjectPooler ammoPooler; // ObjectPooler that contains ammo objects

        #region attack_methods
        /// <summary>
        /// Handle perform attack
        /// </summary>
        public virtual void PerformAttack()
        {
            if (equipmentController.IsWeaponMelee())
                AttackMelee();
            else
                AttackRange();
        }

        /// <summary>
        /// Handle melee attack
        /// </summary>
        public virtual void AttackMelee()
        {
            Collider2D[] hittableColliders = Physics2D.OverlapCircleAll(meleeHitTransform.position, 0.5f, hittableLayerMask); // Get all hittable colliders

            // Check if detected colliders have Hittable component. If so, take damage to that Hittable
            foreach (Collider2D collider2D in hittableColliders)
                if (collider2D.transform.parent.GetComponentInParent<Hittable>() != null)
                    collider2D.transform.parent.GetComponentInParent<Hittable>().TakeDamage(equipmentController.equipment.Weapon.BaseDamage);
        }

        /// <summary>
        /// Handle range attack
        /// </summary>
        public virtual void AttackRange()
        {
            GameObject ammoObject = ammoPooler.GetNextObject(); // Get the next ammo object
            Ammo ammoItem = equipmentController.equipment.Ammo; // Get equipped ammo
            RangeWeapon weaponItem = (RangeWeapon)equipmentController.equipment.Weapon; // Get equipped range weapon

            // Check if previously created instances is not null
            if (ammoObject == null || ammoItem == null || weaponItem == null) 
                return;

            AmmoInstance ammoInstanceScript = ammoObject.GetComponent<AmmoInstance>(); // Get ammo instance script from ammo object

            ammoObject.SetActive(true); // Set ammo object active

            ammoInstanceScript.SetUpItem(ammoItem, true);              // Set up ammo instance
            ammoInstanceScript.SetDetectLayerMask(hittableLayerMask);   // Set hittable LayerMask
            ammoInstanceScript.SetTotalDamage(ammoItem, weaponItem);    // Set total damage of the ammo
            ammoInstanceScript.Fly(shootStartPoint, ammoItem.FlySpeed); // Ammo is flying towards shootStartPoint
        }
        #endregion
    }
}