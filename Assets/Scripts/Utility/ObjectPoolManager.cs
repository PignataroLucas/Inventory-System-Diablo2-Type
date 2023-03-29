using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public GameObject prefab;
        private Stack<GameObject> _inactiveInstances = new Stack<GameObject>();

        public GameObject GetObject()
        {
            GameObject spawnedGameObject;
            if (_inactiveInstances.Count > 0)
            {
                spawnedGameObject = _inactiveInstances.Pop();
            }
            else
            {
                spawnedGameObject = Instantiate(prefab);

                PooledObject pooledObject = spawnedGameObject.AddComponent<PooledObject>();
                pooledObject.pool = this;
            }
            spawnedGameObject.transform.SetParent(null);
            spawnedGameObject.SetActive(true);

            return spawnedGameObject;
        }
        public void ReturnObject(GameObject toReturn)
        {
            PooledObject pooledObject = toReturn.GetComponent<PooledObject>();

            if (pooledObject != null && pooledObject.pool == this)
            {
                toReturn.transform.SetParent(transform);
                toReturn.transform.position = this.transform.position;
                toReturn.SetActive(false);
                
                _inactiveInstances.Push(toReturn);
            }
            else
            {
                Debug.LogWarning(toReturn.name + "Was returned to a pool it wasn't spawned from.");
                Destroy(toReturn);
            }
        }
    }
    public class PooledObject : MonoBehaviour
    {
        public ObjectPoolManager pool;
    }
    
}


