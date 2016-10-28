using UnityEngine;
using System.Collections;

public class pilka : MonoBehaviour {

	public Rigidbody rb;
	private float ground = 6.01f;

	public int punktyGracz1 = 0;
	public int punktyGracz2 = 0;

	public bool waitForStart = true;


	public float x = 0.0f;
	public float z = 0.0f;

	private Vector3 gracz1startPos = new Vector3 (240.0f, 10.0f, 255.0f);

	// Use this for initialization
	void Start () {
		rb = GetComponent <Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

		x = transform.position.x;
		z = transform.position.z;

		if (isGrounded()) {
			transform.position = new Vector3(gracz1startPos.x,gracz1startPos.y,gracz1startPos.z);
			waitForStart = true;
		}


		if (waitForStart == true) {
			rb.useGravity = false;
			rb.velocity = new Vector3 (0, 0, 0);
		} else {
			rb.useGravity = true;
		}

		if (rb.velocity.magnitude > 10) {
			rb.velocity = rb.velocity.normalized * 10;
		}
	}



	bool isGrounded(){
		if (transform.position.y < ground + 0.10) {
			return true;
		}
		return false;
	}
		
}
