using UnityEngine;
using System.Collections;

public class pilka : MonoBehaviour {
	public GameObject boisko;
	public GameObject wynik_P1;
	public GameObject wynik_P2;
	public GameObject wynik_P1w;
	public GameObject wynik_P2w;

	public GameObject set_P1;
	public GameObject set_P2;
	public GameObject set_P1w;
	public GameObject set_P2w;

	public GameObject wynik1;
	public GameObject wynik2;

	public int punktyGracz1 = 0;
	public int punktyGracz2 = 0;

	public int odbiciaGracz1 = 0;
	public int odbiciaGracz2 = 0;

	public int numerSeta = 0;
	public int setyGracz1 = 0;
	public int setyGracz2 = 0;




	public bool waitForStart = true;

	//variable initialized on start
	private Rigidbody rb;
	private float ground;
	private Vector3 gracz1startPos;
	private Vector3 gracz2startPos;
	//AudioSource
	AudioSource s_whistle;
	AudioSource s_wood;

	// Use this for initialization
	void Start () {
		rb = GetComponent <Rigidbody>();
		gracz1startPos = new Vector3 (boisko.transform.position.x, boisko.transform.position.y + 6.0f, boisko.transform.position.z + 6.0f);
		gracz2startPos = new Vector3 (boisko.transform.position.x, boisko.transform.position.y + 6.0f, boisko.transform.position.z - 6.0f);
		ground = boisko.transform.position.y + 1.0f;

		s_whistle = GetComponents<AudioSource> ()[1];
		s_wood = GetComponents<AudioSource> ()[0];
	}
	
	// Update is called once per frame
	void Update () {

		if (isGrounded()) {
			s_whistle.Play ();
			if (transform.position.z < boisko.transform.position.z) {
				addPoint (1);
			} else {
				addPoint (2);
			}
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

	void OnCollisionEnter (Collision col){
		s_wood.Play ();
		if(col.gameObject.name=="Gracz 1"){
			odbiciaGracz2 = 0;
			odbiciaGracz1++;
			if (odbiciaGracz1 > 3) {
				odbiciaGracz1 = 0;
				s_whistle.Play ();
				addPoint (2);
			}
		}
		if(col.gameObject.name=="Gracz 2"){
			odbiciaGracz1 = 0;
			odbiciaGracz2++;
			if (odbiciaGracz2 > 3) {
				odbiciaGracz2 = 0;
				s_whistle.Play ();
				addPoint (1);
			}
		}

	}

	bool isGrounded(){
		if (transform.position.y < ground + 0.10) {
			return true;
		}
		return false;
	}


	private void addPoint(int playerNum){
		if (playerNum == 1) {
			punktyGracz1++;
			transform.position = gracz1startPos;
		} else {
			punktyGracz2++;
			transform.position = gracz2startPos;

		}

		if ((punktyGracz1 >= 25 || punktyGracz2 >= 25) && (punktyGracz1-punktyGracz2 >= 2 || punktyGracz2-punktyGracz1 >= 2) && numerSeta < 5)
		{	
			if (punktyGracz1 > punktyGracz2) {
			
				setyGracz1 += 1;
			} else {
				setyGracz2 += 1;
			}

			punktyGracz1 = 0;
			punktyGracz2 = 0;
			numerSeta += 1;
			}

		if ((punktyGracz1 >= 15 || punktyGracz2 >= 15) && (punktyGracz1-punktyGracz2 >= 2 || punktyGracz2-punktyGracz1 >= 2) && numerSeta == 5)
		{	
			if (punktyGracz1 > punktyGracz2) {

				setyGracz1 += 1;
			} else {
				setyGracz2 += 1;
			}

			punktyGracz1 = 0;
			punktyGracz2 = 0;
			numerSeta += 1;
		}



			

		odbiciaGracz1 = 0;
		odbiciaGracz2 = 0;
		waitForStart = true;
		showPoitns ();
	}

	private void showPoitns(){
		

			TextMesh tm1 = wynik_P1.GetComponent<TextMesh> ();
			TextMesh tm2 = wynik_P2.GetComponent<TextMesh> ();
			TextMesh tm1w = wynik_P1w.GetComponent<TextMesh> ();
			TextMesh tm2w = wynik_P2w.GetComponent<TextMesh> ();


			TextMesh set1 = set_P1.GetComponent<TextMesh> ();
			TextMesh set2 = set_P2.GetComponent<TextMesh> ();
			TextMesh set1w = set_P1w.GetComponent<TextMesh> ();
			TextMesh set2w = set_P2w.GetComponent<TextMesh> ();

			TextMesh wynik11 = wynik1.GetComponent<TextMesh> ();
			TextMesh wynik22 = wynik2.GetComponent<TextMesh> ();

		if (setyGracz2 >= 3 || setyGracz1 >= 3) {
			if (setyGracz1 > setyGracz2) {
				wynik11.text = "Wygral Gracz 1";
				wynik22.text = "Wygral Gracz 1";
			} else {
				wynik11.text = "Wygral Gracz 2";
				wynik22.text = "Wygral Gracz 2";
			
			}
			tm1.text ="";
			tm2.text ="";
			tm1w.text ="";
			tm2w.text ="";
			set1.text ="";
			set2.text ="";
			set1w.text ="";
			set2w.text ="";
			setyGracz1 = 0;
			setyGracz2 = 0;

		}
		else{

			tm1.text = punktyGracz1 + "";
			tm2.text = punktyGracz2 + "";
			tm1w.text = punktyGracz1 + "";
			tm2w.text = punktyGracz2 + "";
			set1.text = setyGracz1 + "";
			set2.text = setyGracz2 + "";
			set1w.text = setyGracz1 + "";
			set2w.text = setyGracz2 + "";
			wynik11.text = ":";
			wynik22.text = ":";
		}
	}
		
}
