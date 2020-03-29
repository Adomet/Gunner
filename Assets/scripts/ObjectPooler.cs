using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }


    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // @TODO Add Start of Polled object

    void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

      
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i< pool.size; i++)
            {

                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);


            }

            poolDictionary.Add(pool.tag +"(Clone)",objectPool);
        }


    }

    public GameObject SpawnFromPool (string tag,GameObject Obj,Vector3 positon ,Quaternion rotation)
    {
        GameObject objectToSpawn;

        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool :" + tag + " doesnt exicst!");
            return null;
        }

        if (poolDictionary[tag].Count != 0)
        {

            objectToSpawn = poolDictionary[tag].Dequeue();

            objectToSpawn.transform.position = positon;
            objectToSpawn.transform.rotation = rotation;

            objectToSpawn.SetActive(true);

        }

        else
        {
          //  Debug.Log( tag + " Instantiated");

             objectToSpawn = Instantiate(Obj,positon,rotation);
        }


        return objectToSpawn;

    }


    public void DestroyToPool (GameObject ObjToDestory)
    {
      //  Debug.Log(ObjToDestory.name + " Got Pooled");

          ObjToDestory.SetActive(false);
        // Animator objAnimator = ObjToDestory.GetComponent<Animator>();
        // if (objAnimator != null)
        //     objAnimator.Play("Run", -1, 0f);
 

        poolDictionary[ObjToDestory.name].Enqueue(ObjToDestory);

    }
}
