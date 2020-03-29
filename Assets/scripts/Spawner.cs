using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{


    [System.Serializable]
    public class Spawn
    {
        public string tag;
        public GameObject Obj;
        public int spSize;
    }

    public List<Spawn> Spawns;

    public ObjectPooler objPooler;

    public float rndmaxX = 0f;
    public float rndmaxZ = 0f;

    public Tracker tracker;
    


    // Start is called before the first frame update
    void Start()
    {
        foreach (Spawn sp in Spawns)
        {
            for (int i = 0; i < sp.spSize; i++)
            {
                GameObject SpObj = objPooler.SpawnFromPool(sp.tag,sp.Obj, new Vector3(Random.Range(-rndmaxX, rndmaxX), transform.position.y, Random.Range(-rndmaxZ, rndmaxZ)), Quaternion.identity);
                    
            }
        }

        InvokeRepeating("AutoSpawn",1f,1f);

    }


    // the function that spawns pooled objects back to intended number of objects
    // @TODO Make a system that automaticly makes the spawn
    void AutoSpawn ()
    {
        foreach (Spawn sp in Spawns)
        {
            if (sp.tag == "AI(Clone)")
            {
                int a = tracker.Players.Length;
                for (int i = a; i < sp.spSize; i++)
                {
                       GameObject SpObj = objPooler.SpawnFromPool(sp.tag, sp.Obj, new Vector3(Random.Range(-rndmaxX, rndmaxX), transform.position.y, Random.Range(-rndmaxZ, rndmaxZ)), Quaternion.identity);
                       Player player = SpObj.GetComponent<Player>();
                       AIMovement aIMovement = SpObj.GetComponent<AIMovement>();

                    player.PlayerPoint = 1;
                    player.KillCount = 0;
                    player.pUpCount = 0;
                    player.Gun.GunTypeindex = 0;


                    aIMovement.range = 10;
                    aIMovement.AIRotInputValue = 3;
                    aIMovement.AISpeed = 6;


                }
            }


            if (sp.tag == "PUpCube(Clone)")
            {
                int a = tracker.PUps.Length;
                for (int i = a; i < sp.spSize; i++)
                {
                    GameObject SpObj = objPooler.SpawnFromPool(sp.tag, sp.Obj, new Vector3(Random.Range(-rndmaxX, rndmaxX), transform.position.y, Random.Range(-rndmaxZ, rndmaxZ)), Quaternion.identity);

                }
            }
        }

    }

}
