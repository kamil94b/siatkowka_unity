using UnityEngine;
using System.Collections;

public class ruch : MonoBehaviour {

	//Obiekt odpowiedzialny za ruch gracza.
	//private CharacterController characterControler;
	private Rigidbody rb;
	public pilka pilka;

	private float predkoscPoruszania = 9.0f;
	private float wysokoscSkoku = 10.5f;
	private float aktualnaWysokoscSkoku = 0f;
	private float predkoscBiegania = 7.0f;

	private float speed = 3.0f;
	private float polowaBoiskaZ = 251;

	public bool isComputerPlayer = false;

	private float ziemia = 6.62f;
	private float jumpSpeed = 0.0f;


	public float ogranicznikPolaLeft;
	public float ogranicznikPolaRight;
	public float ogranicznikPolaTop;
	public float ogranicznikPolaBottom;

	void Start () {
		rb = GetComponent<Rigidbody>();
	}


	void Update() {

		if (isComputerPlayer == false) {

			//sterownie gracza

			float rochPrzodTyl = Input.GetAxis ("Vertical") * predkoscPoruszania;
			float rochLewoPrawo = Input.GetAxis ("Horizontal") * predkoscPoruszania;


			if (transform.position.z > polowaBoiskaZ) {
				rochPrzodTyl = rochPrzodTyl * -1.0f;
				rochLewoPrawo = rochLewoPrawo*-1.0f;
			}

			if (rb.position.x < ogranicznikPolaLeft + 1.1f && rochLewoPrawo < 0) {
				rochLewoPrawo = 0;
			}
			if (rb.position.x > ogranicznikPolaRight - 1.1f && rochLewoPrawo > 0) {
				rochLewoPrawo = 0;
			}
			if (rb.position.z > ogranicznikPolaTop -1.1f  && rochPrzodTyl > 0) {
				rochPrzodTyl = 0;
			}
			if (rb.position.z < ogranicznikPolaBottom + 1.1f && rochPrzodTyl < 0) {
				rochPrzodTyl = 0;
			}

			aktualnaWysokoscSkoku = 0;


			if (Input.GetButton ("Jump")) {
				if (rb.position.y < ziemia + 0.5) {
					jumpSpeed = 10;
				} else {
					jumpSpeed = rb.velocity.y + Physics.gravity.y * Time.deltaTime * 1.2f;
				}
			} else {
				jumpSpeed = rb.velocity.y + Physics.gravity.y * Time.deltaTime * 2.0f;
			}

				
			//jumpSpeed *= 1.5f;
			Vector3 ruch = new Vector3 (rochLewoPrawo, jumpSpeed, rochPrzodTyl);

			rb.velocity = ruch;

			//ruch = transform.rotation * ruch;

			//characterControler.Move (ruch * Time.deltaTime);
		} else {

			//komputer

			float celX = 0.0f;
			float celZ = 0.0f;

			//int graczPrzedSiatka = -1;
			//int pilkaPrzedSiatka = -1;
			//
			//if (transform.position.z < polowaBoiskaZ) {
			//	graczPrzedSiatka  = 1;
			//}
			//if (pilka.transform.position.z < polowaBoiskaZ) {
			//	pilkaPrzedSiatka  = 1;
			//}


			//if (graczPrzedSiatka * pilkaPrzedSiatka == 1) {
				//celX = pilka.transform.position.x;
				//celZ = pilka.transform.position.z;
			//} else {
				celX = pilka.transform.position.x;
				celZ = pilka.transform.position.z;
			//}
		
			//Vector ruch = new Vector3(celX - transform.position.x,0,0);
			//Vector3 ruch;
			//if (celX > transform.position.x) {
			//	
			//} else {
			//	ruch = new Vector3(celZ - transform.position.z,0,0);
			//}



			Vector3 ruch = new Vector3 (getMoveFromTo(rb.position.x,celX,30.0f), 0.0f, getMoveFromTo(rb.position.z,celZ,30.0f));
			rb.velocity = ruch;

			//transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z+);
			//c/haracterControler.Move (ruch);

		}
	}

	void OnTriggerEnter(Collider other){
		//other.gameObject.SetActive (false);
		pilka.waitForStart = false;
	}


	private float getMoveFromTo(float start,float end,float speedT){
		if (start > end) {
			if (start - end > speedT) {
				//return - speedT;
				return 0;
			} else {
				return end - start;
			}
		} else {
			if (end - start > speedT) {
				//return speedT;
				return 0;
			} else {
				return - start + end;
			}
		}
	}

}
