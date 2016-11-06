using UnityEngine;
using System.Collections;

public class pilkatriger : MonoBehaviour {

	public GameObject pilka;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = pilka.transform.position;
	}
}
