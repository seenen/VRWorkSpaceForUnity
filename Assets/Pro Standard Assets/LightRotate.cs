﻿using UnityEngine;
using System.Collections;

public class LightRotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 60 * Time.deltaTime, 0));
	}
}
