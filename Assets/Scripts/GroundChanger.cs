using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundChanger : MonoBehaviour {

    public GameObject ground;
    public Text text;

	// Use this for initialization
	void Start ()
    {

        //ground = GetComponentInChildren<Transform>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            text.text = "collsion active";
            ground.SetActive(true);
        }
    }
}
