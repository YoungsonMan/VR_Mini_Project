using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();

    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation)
    {

        PooledObjectInfo pool = ObjectPools.Find(p => p.lookupString == objectToSpawn.name);
        
        // 존재하지않는 POOL이면 만들기
        if (pool == null)
        {
            pool = new PooledObjectInfo() { lookupString = objectToSpawn.name };
            ObjectPools.Add(pool);
        }

        // POOL에 inactive object 있나 확인
        GameObject spawnableObj = pool.InativeObjects.FirstOrDefault();
        // 있으면, 활성화
        if (spawnableObj != null)
        {
            spawnableObj.transform.position = spawnPosition;
            spawnableObj.transform.rotation = spawnRotation;
            pool.InativeObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }
        // 없으면, 만들기
        else
        {
            spawnableObj = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
        }

        return spawnableObj;

    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        string goName = obj.name.Substring(0, obj.name.Length - 7);

        PooledObjectInfo pool = ObjectPools.Find(p => p.lookupString == goName);
        if (pool != null)
        {
            obj.SetActive(false);
            pool.InativeObjects.Add(obj);
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
        }
        else
        {
            Debug.LogWarning($"Trying to release unpooled object,{obj.name}");
        }


    }

    public class PooledObjectInfo
    {
        public string lookupString;
        public List<GameObject> InativeObjects = new List<GameObject>();
    }
}
