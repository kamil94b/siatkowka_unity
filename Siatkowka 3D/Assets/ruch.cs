using UnityEngine;
using System.Collections;

public class ruch : MonoBehaviour {

	private Rigidbody rb;
	public pilka pilka;
	public GameObject boisko;
	public GameObject gracz;

	private float actualScale = 1.6f;
	private float actualScaleCel= 1.6f;

	private float moveTime = 0;

	private float playerSpeed = 6.0f;
	private float jumpHeight = 10.0f;
	public bool isComputerPlayer = false;

	private float ogranicznikPolaLeft;
	private float ogranicznikPolaRight;
	private float ogranicznikPolaTop;
	private float ogranicznikPolaBottom;
	private int graczPrzedSiatka = -1;

	void Start () {
		rb = GetComponent<Rigidbody>();

		ogranicznikPolaLeft = boisko.transform.position.x - 5;
		ogranicznikPolaRight =boisko.transform.position.x + 5;

		if (transform.position.z < boisko.transform.position.z) {
			graczPrzedSiatka  = 1;
		}

		if (graczPrzedSiatka == 1) {
			ogranicznikPolaTop = boisko.transform.position.z;
			ogranicznikPolaBottom = boisko.transform.position.z - 10;
		} else {
			ogranicznikPolaTop = boisko.transform.position.z + 10 ;
			ogranicznikPolaBottom = boisko.transform.position.z;
		}
			
	}


	void Update() {

		if (isComputerPlayer == false) {
			float rochPrzodTyl = 0;
			float rochLewoPrawo = 0;
			//STEROWANIE RECZNE
			if (gracz.name == "Gracz 1") {
				rochPrzodTyl = Input.GetAxis ("Vertical") * playerSpeed * 1.5f;
				rochLewoPrawo = Input.GetAxis ("Horizontal") * playerSpeed * 1.5f;
			} else {
				rochPrzodTyl = Input.GetAxis ("Vertical2") * playerSpeed * 1.5f;
				rochLewoPrawo = Input.GetAxis ("Horizontal2") * playerSpeed * 1.5f;
			}

			//odwrocone sterownie dla gracza po drugiej stronie
			if (graczPrzedSiatka == -1) {
				rochPrzodTyl = rochPrzodTyl * -1.0f;
				rochLewoPrawo = rochLewoPrawo*-1.0f;
			}
				
			rochLewoPrawo = getLimiters (rochLewoPrawo,true);
			rochPrzodTyl = getLimiters (rochPrzodTyl, false);
			float jumpSpeed = 0;	
			if (gracz.name == "Gracz 1") {
				jumpSpeed = getJump (Input.GetButton ("Jump"));
			} else {
				jumpSpeed = getJump (Input.GetButton ("Jump2"));
			}
			Vector3 ruch = new Vector3 (rochLewoPrawo, jumpSpeed, rochPrzodTyl);

			bool isMoving = true;
			if (rochLewoPrawo == 0 && rochPrzodTyl == 0) {
				isMoving = false;
			}
			bool isJump = true;
			if (rb.position.y < getZiemia() + 0.2) {
				isJump = false;
			}

			transform.localScale = getActualScale (isMoving,isJump);

			rb.velocity = ruch;
		} else {
			//KOMPUTER

			float celX = 0.0f;
			float celZ = 0.0f;

			int pilkaPrzedSiatka = -1;

			if (pilka.transform.position.z < boisko.transform.position.z) {
				pilkaPrzedSiatka  = 1;
			}
				
			if (graczPrzedSiatka * pilkaPrzedSiatka == 1) {
				celX = pilka.transform.position.x;
				if (graczPrzedSiatka == 1) {
					celZ = pilka.transform.position.z - 1.1f;
				} else {
					celZ = pilka.transform.position.z + 1.1f;
				}
			} else {
				if (graczPrzedSiatka == 1) {
					celZ = boisko.transform.position.z - 6.0f;
				} else {
					celZ = boisko.transform.position.z + 6.0f;
				}
				celX = boisko.transform.position.x;
			}

			float actualSpeed = playerSpeed * Time.deltaTime;

			float moveLeftRight = 0.0f;
			float moveTopBottom= 0.0f;

			if (Time.deltaTime != 0) {
				moveLeftRight = getMoveFromTo (rb.position.x, celX, actualSpeed) / Time.deltaTime;
				moveTopBottom = getMoveFromTo (rb.position.z, celZ, actualSpeed) / Time.deltaTime;
			}


			bool virtualJumpButton = false;

			if (graczPrzedSiatka * pilkaPrzedSiatka == 1) {
				float actualSpeed2 = actualSpeed / Time.deltaTime;
				if (moveLeftRight != actualSpeed2 && moveLeftRight != -actualSpeed2 && moveTopBottom != actualSpeed2 && moveTopBottom != -actualSpeed2) {
					//The player is under the ball and can Jump
					virtualJumpButton = true;
				}
			}

			float jumpSpeed = getJump(virtualJumpButton);

			moveLeftRight = getLimiters (moveLeftRight, true);
			moveTopBottom = getLimiters (moveTopBottom, false);

			Vector3 ruch = new Vector3 (moveLeftRight, jumpSpeed ,moveTopBottom);
			rb.velocity = ruch;

			bool isMoving = true;
			if (moveLeftRight == 0 && moveTopBottom == 0) {
				isMoving = false;
			}
			bool isJump = true;
			if (rb.position.y < getZiemia() + 0.2) {
				isJump = false;
			}

			transform.localScale = getActualScale (isMoving,isJump);
		}
	}

	void OnTriggerEnter(Collider other){
		pilka.waitForStart = false;
	}


	private float getMoveFromTo(float start,float end,float speedT){
		if (start > end) {
			if ((start - end) > speedT) {
				return - speedT;
			} else {
				return end - start;
			}
		} else {
			if ((end - start) > speedT) {
				return speedT;
			} else {
				return - start + end;
			}
		}
	}
		
	private float getJump(bool buttomJump){
		float jumpSpeed = 0.0f;
		if (buttomJump == true) {
			if (rb.position.y < getZiemia() + 0.5) {
				jumpSpeed = jumpHeight;
			} else {
				jumpSpeed = rb.velocity.y + Physics.gravity.y * Time.deltaTime * 1.2f;
			}
		} else {
			jumpSpeed = rb.velocity.y + Physics.gravity.y * Time.deltaTime * 2.0f;
		}
		return jumpSpeed;
	}

	private float getLimiters(float speed, bool isLeftRight){
		if (isLeftRight) {
			if (rb.position.x < ogranicznikPolaLeft + 1.2f && speed < 0) {
				return 0;
			}
			if (rb.position.x > ogranicznikPolaRight - 1.2f && speed > 0) {
				return 0;
			}
		} else {
			if (rb.position.z > ogranicznikPolaTop -1.2f  && speed > 0) {
				return 0;
			}
			if (rb.position.z < ogranicznikPolaBottom + 1.2f && speed < 0) {
				return 0;
			}
		}
		return speed;
	}


	private Vector3 getActualScale(bool isMoving,bool isJump){

		if (isJump == true) {
			actualScaleCel = 1.8f;
		} else {
			if (isMoving == true && actualScaleCel == 1.6f) {
				moveTime += Time.deltaTime;
				actualScale = 1.6f + 0.2f * Mathf.Sin (moveTime * 20);
				return new Vector3 (2.0f, actualScale, 2.0f);
			} else {
				moveTime = 0;
			}
			if (actualScaleCel == 1.8f) {
				actualScaleCel = 1.3f;
			}
			if (actualScaleCel == 1.3f && actualScale < 1.35f) {
				actualScaleCel = 1.6f;	
			}
		}
		actualScale += (actualScaleCel - actualScale) / 2.0f;

		return new Vector3 (2.0f, actualScale, 2.0f);

	}

	private float getZiemia(){
		return boisko.transform.position.y + transform.localScale.y;
	}

}
