using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

    public GameObject PowerUpPrefab;

    public ObjectPooler ObjectPooler;

    public Vector3 SpPos = new Vector3(0,1,0);

    public float SpSpeed = 1f;

   public void GameOver ()
    {

        /* Warning This file is Just for look ups not a real script*/
        /* Warning This file is Just for look ups not a real script*/
        /* Warning This file is Just for look ups not a real script*/
        /* Warning This file is Just for look ups not a real script*/
        /* Warning This file is Just for look ups not a real script*/

        GameObject SpCube1 = ObjectPooler.SpawnFromPool("PUpCube(Clone)",PowerUpPrefab, (transform.position + SpPos), Random.rotation);
        Vector3 force = -1 * transform.forward * SpSpeed * 1.5f * Random.Range(0.5f,1.2f);
        SpCube1.GetComponent<Rigidbody>().velocity = force;


        GameObject SpCube3 = ObjectPooler.SpawnFromPool("PUpCube(Clone)", PowerUpPrefab, (transform.position + SpPos), Random.rotation);
        Vector3 force3 = -1 * (transform.forward + transform.right) * SpSpeed * Random.Range(0.5f, 1.2f);
        SpCube3.GetComponent<Rigidbody>().velocity = force3;
       
        GameObject SpCube2 = ObjectPooler.SpawnFromPool("PUpCube(Clone)", PowerUpPrefab, (transform.position + SpPos), Random.rotation);
        Vector3 force2 = -1 * (transform.forward - transform.right) * SpSpeed * Random.Range(0.5f, 1.2f);
        SpCube2.GetComponent<Rigidbody>().velocity = force2;


  
        SpCube1.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        SpCube2.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        SpCube3.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);


        Debug.Log("AI GameOver");
    }
}
