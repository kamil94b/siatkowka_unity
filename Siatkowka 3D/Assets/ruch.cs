using UnityEngine;
using System.Collections;

public class ruch : MonoBehaviour {

	//Obiekt odpowiedzialny za ruch gracza.
	private CharacterController characterControler;
	public pilka pilka;

	private float predkoscPoruszania = 9.0f;
	private float wysokoscSkoku = 10.5f;
	private float aktualnaWysokoscSkoku = 0f;
	private float predkoscBiegania = 7.0f;

	private float speed = 3.0f;
	private float polowaBoiskaZ = 251;

	public bool isComputerPlayer = false;


	void Start () {
		characterControler = GetComponent<CharacterController>();
	}


	void Update() {

		if (isComputerPlayer == false) {

			//sterownie gracza

			float rochPrzodTyl = Input.GetAxis ("Vertical") * predkoscPoruszania;
			float rochLewoPrawo = Input.GetAxis ("Horizontal") * predkoscPoruszania;


			if (characterControler.isGrounded && Input.GetButton ("Jump")) {
				aktualnaWysokoscSkoku = wysokoscSkoku;
			} else if (!characterControler.isGrounded) {
				if (Input.GetButton ("Jump")) {
					aktualnaWysokoscSkoku += Physics.gravity.y * Time.deltaTime * 1.5f;
				} else {
					aktualnaWysokoscSkoku += Physics.gravity.y * Time.deltaTime * 3;
				}
			}

			Vector3 ruch = new Vector3 (rochLewoPrawo, aktualnaWysokoscSkoku, rochPrzodTyl);

			//ruch = transform.rotation * ruch;

			characterControler.Move (ruch * Time.deltaTime);
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
