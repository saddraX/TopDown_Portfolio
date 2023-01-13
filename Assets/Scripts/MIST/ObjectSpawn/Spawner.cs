using System.Collections;
using UnityEngine;

namespace MIST.ObjectSpawn
{
    public abstract class Spawner : MonoBehaviour
    {
        [Header("Object Pooler")]
        [SerializeField] protected ObjectPooler objectPooler; // Contains objects to spawn

        [Header("Spawn parameters")]
        [SerializeField] protected float spawnTimeInterval = 2f;
        [SerializeField] protected Vector2 minSpawnPosition = Vector2.zero;
        [SerializeField] protected Vector2 maxSpawnPosition = Vector2.zero;

        protected virtual void Awake() => StartCoroutine(RandomPositionSpawnLoop());

        #region spawn_handle
        /// <summary>
        /// Object spawn loop with random position
        /// </summary>
        /// <returns></returns>
        protected IEnumerator RandomPositionSpawnLoop()
        {
            yield return new WaitForSeconds(spawnTimeInterval);
            
            GameObject objectToSpawn = objectPooler.GetNextObject(); // Get next object from pooler

            SpawnAtRandomPosition(objectToSpawn); // Spawn object at random position

            StartCoroutine(RandomPositionSpawnLoop()); // Start new spawn coroutine
        }

        /// <summary>
        /// Spawn the object at the given position
        /// </summary>
        /// <param name="objectToSpawn"></param>
        /// <param name="spawnPosition"></param>
        protected virtual void SpawnAtPosition(GameObject objectToSpawn, Vector2 spawnPosition) => SpawnObjectAndSetParameters(objectToSpawn, spawnPosition);

        /// <summary>
        /// Spawn the object at the random position
        /// </summary>
        /// <param name="objectToSpawn"></param>
        protected virtual void SpawnAtRandomPosition(GameObject objectToSpawn)
        {
            Vector2 spawnPosition = new Vector2();
            spawnPosition.x = Random.Range(minSpawnPosition.x, maxSpawnPosition.x);
            spawnPosition.y = Random.Range(minSpawnPosition.y, maxSpawnPosition.y);
            
            SpawnAtPosition(objectToSpawn, spawnPosition);
        }

        /// <summary>
        /// Spawn the object at the given position and set its parameters
        /// </summary>
        /// <param name="objectToSpawn"></param>
        /// <param name="position"></param>
        protected virtual void SpawnObjectAndSetParameters(GameObject objectToSpawn, Vector2 position)
        {
            if (objectToSpawn != null)
            {
                objectToSpawn.SetActive(true);
                objectToSpawn.transform.parent = transform;
                objectToSpawn.transform.position = position;
            }
        }
        #endregion

        #region drawing_gizmos
        private void OnDrawGizmos()
        {
            DrawZone();
        }

        /// <summary>
        /// Draw rectangle using min or max spawn position
        /// </summary>
        private void DrawZone()
        {
            Gizmos.color = new Color(0, 0, 1, 0.1f);

            Gizmos.DrawCube((minSpawnPosition + maxSpawnPosition) / 2, (maxSpawnPosition - minSpawnPosition));
            Gizmos.DrawWireCube((minSpawnPosition + maxSpawnPosition) / 2, (maxSpawnPosition - minSpawnPosition));
        }
        #endregion
    }

}