using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabFactory : MonoBehaviour
{
    public static PrefabFactory Instance;

    private Dictionary<string, Object> prefabCache = new Dictionary<string, Object>();

    void Awake()
    {
        if (PrefabFactory.Instance == null)
        {
            PrefabFactory.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    public GameObject InstantiatePrefab(string name, Vector3 position, Quaternion rotation, Transform parent)
    {
        Object prefabObject;
        if (!this.prefabCache.TryGetValue(name, out prefabObject))
        {
            prefabObject = Resources.Load($"Prefabs/{name}");
            this.prefabCache.Add(name, prefabObject);
        }

        return Instantiate(prefabObject, position, rotation, parent) as GameObject;
    }
}
