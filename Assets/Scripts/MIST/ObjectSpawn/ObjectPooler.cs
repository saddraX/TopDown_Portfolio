using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MIST.ObjectSpawn
{
    public class ObjectPooler : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private GameObject prefab;

        [Header("Pool stats")]
        [SerializeField] private int poolSize = 10;
        public bool initialized = false;

        private List<GameObject> allInstances = new List<GameObject>(); // Contains all object instances

        #region initialize_pool
        void Awake() => InitializePool();

        /// <summary>
        /// Populate pool with prefab
        /// </summary>
        public void InitializePool()
        {
            if (!initialized)
            {
                for (int i = 0; i < poolSize; i++)
                {
                    // Instantiate object and hide it
                    GameObject gObject = Instantiate(prefab, transform);
                    gObject.SetActive(false);

                    // Add object to all instances list
                    allInstances.Add(gObject);
                }

                initialized = true;
            }
        }
        #endregion

        #region retrieving_objects
        /// <summary>
        /// Get inactive object
        /// </summary>
        /// <returns></returns>
        public GameObject GetNextObject()
        {
            foreach (GameObject gObject in allInstances)
            {
                if (!gObject.activeSelf)
                {
                    return gObject;
                }
            }
            return null;
        }

        /// <summary>
        /// Get all prefab instances
        /// </summary>
        /// <returns></returns>
        public List<GameObject> GetAllInstances() => allInstances;
        #endregion
    }
}