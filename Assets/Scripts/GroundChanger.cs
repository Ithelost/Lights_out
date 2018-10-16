using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundChanger : MonoBehaviour {

    public GameObject ground;

	// Use this for initialization
	void Start ()
    {

	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ground.SetActive(true);
        }
    }
}
