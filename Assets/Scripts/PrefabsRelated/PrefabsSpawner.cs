using UnityEngine;

namespace Prefabs
{
    public interface IPrefabsSpawner
    {
        GameObject SpawnAtPoint(GameObject _prefab, Vector3 position,   Transform parent = null);
    }

    public class PrefabsSpawner : IPrefabsSpawner
    {
        public GameObject SpawnAtPoint(GameObject _prefab, Vector3 position, Transform parent = null)
        {
            var go = GameObject.Instantiate(_prefab);
            go.transform.position = position;
            if (parent != null)
            {
                go.transform.parent = parent;
            }

            return go;
        }
    }
}