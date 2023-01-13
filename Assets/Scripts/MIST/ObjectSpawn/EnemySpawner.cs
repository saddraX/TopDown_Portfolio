using System.Collections.Generic;
using UnityEngine;

using MIST.EQ;
using MIST.Characters;

namespace MIST.ObjectSpawn
{
    public class EnemySpawner : Spawner
    {
        protected virtual void Start() => InitializeAmmoPoolers();

        #region spawn_handle
        /// <summary>
        /// Spawn the enemy at the given position and set its parameters
        /// </summary>
        /// <param name="objectToSpawn"></param>
        /// <param name="position"></param>
        protected override void SpawnObjectAndSetParameters(GameObject objectToSpawn, Vector2 position)
        {
            base.SpawnObjectAndSetParameters(objectToSpawn, position);

            if (objectToSpawn != null)
            {
                // Get enemy character Transform and reset its position
                Transform enemyCharacterTransform = objectToSpawn.transform.GetChild(0);

                if (enemyCharacterTransform != null)
                    enemyCharacterTransform.localPosition = Vector2.zero;

                // Get enemy EquipmentController and set random equipment
                EquipmentController equipmentController = enemyCharacterTransform.GetComponent<EquipmentController>();

                if (equipmentController != null)
                {
                    equipmentController.SetRandomWeapon();

                    if (!equipmentController.IsWeaponMelee())
                        equipmentController.SetRandomAmmo();
                }

                // Get Enemy component and reset values
                Enemy enemy = objectToSpawn.GetComponent<Enemy>();

                if (enemy != null)
                {
                    enemy.ResetParameters();
                    enemy.ResetHealthBar();
                }
            }
        }
        #endregion

        #region initialize_poolers
        /// <summary>
        /// Initialize the ammoPooler mamually. 
        /// Normally the ammoPooler is initializing pool at runtime. That causes preformance spikes.
        /// This method is used to manually initialize the ammoPooler at game startup.
        /// That prevents performance spikes.
        /// </summary>
        private void InitializeAmmoPoolers()
        {
            List<GameObject> poolerInstances = objectPooler.GetAllInstances();
            ObjectPooler ammoPooler;

            foreach (GameObject instance in poolerInstances)
            {
                ammoPooler = instance.transform.Find("Pools").GetComponentInChildren<ObjectPooler>(); // Get ammo pooler

                if (ammoPooler != null)
                    ammoPooler.InitializePool(); // Initialize Ammo pool
                else
                    Debug.LogWarning("AmmoPooler is null!");
            }
        }
        #endregion
    }
}