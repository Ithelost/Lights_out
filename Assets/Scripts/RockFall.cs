using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFall : MonoBehaviour {

    public GameObject Rock;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody rigidRock = Rock.gameObject.GetComponent<Rigidbody>();
            rigidRock.useGravity = true;
        }
    }
}
