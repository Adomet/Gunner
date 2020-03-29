using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Player MyPlayer;
    public ObjectPooler Pooler;
    public GunTypes MyGunType;
    public float speed=1f;
    public Player pl;
    public Vector3 StartPos;
    public float range=100f;
    public float bulletdamage=1f;
    // Update is called once per frame

    public void OnEnable()
    {
        StartPos = transform.position;
    }

        void Update()
    {
        if(range<Vector3.Distance(StartPos, transform.position))
        {

        Pooler.DestroyToPool(gameObject);
        }
        else
        {
        transform.position += transform.forward * Time.deltaTime * speed;

        }
    
    }

    private void OnTriggerEnter(Collider other)
    {

            Player pl = other.transform.root.GetComponent<Player>();
        if (other.tag != "Pike" && pl !=MyPlayer)
        {

            if (pl != null)
            {
                    pl.AddHealth(-bulletdamage);
                if(pl.Health-bulletdamage<=0f)
                    MyPlayer.AddPointToPlayer(10);
                Pooler.DestroyToPool(gameObject);
            }
            else
            {
                Pooler.DestroyToPool(other.transform.gameObject);
            }
        }

    }
}
