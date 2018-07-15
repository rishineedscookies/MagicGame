using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour {

    private float lifetime = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        lifetime -= Time.deltaTime;

        if(lifetime < 0f) {
            Destroy(this.gameObject);
        }


	}
}
