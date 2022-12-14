using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Prefabs
{

    public interface IPrefabsProvider
    {
        public GameObject GetGamePrefab(PrefabType prefabType);
    }

    [CreateAssetMenu(fileName = "PrefabsProviderSO", menuName = "SO/PrefabsProviderSO")]
    public class PrefabsProviderSO : ScriptableObject, IPrefabsProvider
    {
        [SerializeField] private List<VatraPrefab> _gamePrefabs = new List<VatraPrefab>();

         public GameObject GetGamePrefab(PrefabType prefabType)
        {
            return _gamePrefabs.FirstOrDefault(x => x.PrefabType == prefabType).Prefab;
        } 
    }


    [System.Serializable]
    public class VatraPrefab
    {
        public PrefabType PrefabType;
        public GameObject Prefab;
    }
}
 