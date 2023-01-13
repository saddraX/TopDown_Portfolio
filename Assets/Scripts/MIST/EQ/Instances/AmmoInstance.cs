using System.Collections;
using UnityEngine;

using MIST.EQ.Items;
using MIST.IngameObject;

namespace MIST.EQ.Instances
{
    public class AmmoInstance : ItemInstance
    {
        [Header("Ammo Parameters")]
        public int totalDamage = 0; // sum of weapon and ammo damage

        protected override void OnDisable()
        {
            base.OnDisable();

            DisableAmmo(); 
        }

        #region parameters_and_movement
        /// <summary>
        /// Calculate total damage the ammo should do
        /// </summary>
        /// <param name="ammo"></param>
        /// <param name="weapon"></param>
        public void SetTotalDamage(Ammo ammo, Weapon weapon) => totalDamage = weapon.BaseDamage + ammo.Damage;

        /// <summary>
        /// Set
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="speed"></param>
        public void Fly(Transform startPoint, float speed)
        {
            // Set starting position and rotation of the 
            transform.position = startPoint.position;
            transform.rotation = startPoint.rotation;

            // Move ammo using physics
            rigidbody2d.velocity = Vector2.zero;
            rigidbody2d.AddForce(transform.right * speed);

            // Wait a certain amount of time and disable ammo
            StartCoroutine(WaitAndDisableAmmo(2f));
        }
        #endregion

        #region ammo_object_handling
        private IEnumerator WaitAndDisableAmmo(float waitDuration)
        {
            yield return new WaitForSeconds(waitDuration);
            DisableAmmo();
        }

        private void DisableAmmo()
        {
            gameObject.SetActive(false);
        }
        #endregion

        #region physics
        protected override void OnTransformDetected(Transform detectedTransform)
        {
            // Get EquipmentController component
            Hittable hittable = detectedTransform.parent.GetComponentInParent<Hittable>();

            if (hittable == null)
            {
                // Set detected transform to null
                colliderDetector.detectedTransform = null;
                return;
            }
            else
            {
                // Damage the hittable and disable ammo instance object
                hittable.TakeDamage(totalDamage);
                DisableAmmo();
            }
        }
        #endregion
    }
}