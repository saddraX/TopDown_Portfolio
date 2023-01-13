using UnityEngine;

using MIST.EQ.Instances;
using MIST.EQ.Items;
using MIST.Managers;

namespace MIST.ObjectSpawn
{
    public class ItemSpawner : Spawner
    {
        #region spawn_handle
        protected override void SpawnObjectAndSetParameters(GameObject objectToSpawn, Vector2 position)
        {
            base.SpawnObjectAndSetParameters(objectToSpawn, position);

            if (objectToSpawn != null)
            {
                // Get PickableItemInstance and set up random item
                PickableItemInstance itemInstance = objectToSpawn.GetComponent<PickableItemInstance>();

                if (itemInstance != null)
                {
                    Item item = ItemManager.Instance.GetRandomItem();
                    itemInstance.SetUpItem(item, true);
                }    
            }
        }
        #endregion
    }
}