using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{

    public Player[] Players;
    public PowerUp[] PUps;

    // @TODO Make a system that automaticly tracks objects from the pool
    void Start()
    {
        Players = GameObject.FindObjectsOfType<Player>();
        PUps = GameObject.FindObjectsOfType<PowerUp>();

        InvokeRepeating("UpdateLists", 0.5f, 0.5f);
    }

   
    public void UpdateLists ()
    {
        Players =  GameObject.FindObjectsOfType<Player>();
        PUps    =  GameObject.FindObjectsOfType<PowerUp>();
    }
}
