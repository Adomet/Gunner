using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public ObjectPooler Pooler;

    public float BoostBarIncrease = 1f;

    public float AIBoostIncrease = 0.5f;

    private void OnEnable()
    {
        Pooler = GameObject.FindObjectOfType<ObjectPooler>();

        this.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }



    private void OnTriggerEnter(Collider other)
    {
        Player pl = other.transform.root.GetComponent<Player>();
        if (pl != null)
        {

            pl.pUpCount++;
            if (!pl.IsAI)
            {

                pl.AddHealth(1f);
               
            }
            //else if () TODO: add aı power up
            else
            {

                pl.AddHealth(1f);

            }
            Pooler = GameObject.FindObjectOfType<ObjectPooler>();
            Pooler.DestroyToPool(this.gameObject);

        }



    }
}
