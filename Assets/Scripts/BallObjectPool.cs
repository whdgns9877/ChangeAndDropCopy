using System.Collections.Generic;
using UnityEngine;

public class BallObjectPool : MonoBehaviour
{
    private static BallObjectPool instance;
    public static BallObjectPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BallObjectPool>();
                if (instance == null)
                {
                    GameObject singleton = new GameObject(typeof(BallObjectPool).Name);
                    instance = singleton.AddComponent<BallObjectPool>();
                }
            }
            return instance;
        }
    }

    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private int initialSize;

    private Queue<GameObject> pool;
    public Queue<GameObject> Pool { get { return pool; } }

    private List<Transform> activeBallList = new List<Transform>();
    public List<Transform> ActiveBallList { get { return activeBallList; } }

    private void Awake()
    {
        pool = new Queue<GameObject>();
        CreatePool(initialSize);
    }

    private void CreatePool(int CreateCount)
    {
        for (int i = 0; i < CreateCount; i++)
        {
            GameObject obj = InstantiatePrefab();
            pool.Enqueue(obj);
        }
    }

    public GameObject SpawnFromPool(Vector3 position, Quaternion rotation)
    {
        if (pool.Count == 0)
        {
            CreatePool(1);
        }

        GameObject obj = pool.Dequeue();
        activeBallList.Add(obj.transform);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);
        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
        activeBallList.Remove(obj.transform);
    }

    private GameObject InstantiatePrefab()
    {
        GameObject obj = Instantiate(ballPrefab);
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        return obj;
    }
}