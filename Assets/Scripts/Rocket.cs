using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {

    GameObject explosionPrefab;
    // Use this for initialization
    void Start()
    {
      
   
    }
    void OnCollisionEnter(Collision col)
    {
        //Go Boom//
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
      
    }
}
