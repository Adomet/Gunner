using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{

    public Camera target;
    public GameObject text;


    private void Start()
    {
        target = Camera.main;

       

        text.GetComponent<TextMesh>().text = transform.parent.GetComponent<Player>().playerName;
        
    }
    // Start is called before the first frame update
    private void Update()
    {
        transform.LookAt(target.transform);
    }
}
