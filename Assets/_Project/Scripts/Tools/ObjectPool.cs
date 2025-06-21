using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool instance;
    public static ObjectPool Instance => instance;

    [SerializeField] private int poolSize = 10;

    private Dictionary<GameObject, Queue<GameObject>> poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();

    [Header("To Initalize")]
    [SerializeField] GameObject titleMedium;

    public void ReturnObject(GameObject objectToReturn, float delay = 0.001f) => StartCoroutine(this.DelayReturn(delay, objectToReturn));

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        this.InitalizeNewPool(titleMedium);
    }

    private void InitalizeNewPool(GameObject prefab)
    {
        this.poolDictionary[prefab] = new Queue<GameObject>();

        for (int i = 0; i < this.poolSize; i++)
            this.CreateNewObject(prefab);
    }

    private void CreateNewObject(GameObject prefab)
    {
        GameObject newObject = Instantiate(prefab, transform);
        newObject.AddComponent<PooledObject>().originalPrefab = prefab;
        newObject.SetActive(false);

        this.poolDictionary[prefab].Enqueue(newObject);
    }

    public GameObject GetObject(GameObject prefab, Transform target)
    {
        if (!this.poolDictionary.ContainsKey(prefab))
            this.InitalizeNewPool(prefab);

        if (this.poolDictionary[prefab].Count == 0)
            this.CreateNewObject(prefab);

        GameObject objectToGet = this.poolDictionary[prefab].Dequeue();
        objectToGet.transform.position = target.position;
        objectToGet.transform.parent = null;
        objectToGet.SetActive(true);
        objectToGet.GetComponent<TileController>().isActive = true;

        return objectToGet;
    }

    private void ReturnToPool(GameObject objectToReturn)
    {
        objectToReturn.SetActive(false);
        objectToReturn.transform.parent = transform;

        GameObject originalPrefab = objectToReturn.GetComponent<PooledObject>().originalPrefab;
        this.poolDictionary[originalPrefab].Enqueue(objectToReturn);
    }

    private IEnumerator DelayReturn(float delay, GameObject objectToReturn)
    {
        yield return new WaitForSeconds(delay);

        this.ReturnToPool(objectToReturn);
    }
}
