using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class menugr : MonoBehaviour {

	public GameObject button1,button2,button3,napis;

	private bool isPaused = false;

	private int selected = 0;

	// Use this for initialization
	void Start () {
		button1.GetComponentInChildren<Text>().text = "Wznów";
		button2.GetComponentInChildren<Text>().text = "Menu";
		button3.GetComponentInChildren<Text>().text = "Wyjdź";
	}
	
	// Update is called once per frame
	void Update () {
			
		if( Input.GetKeyDown(KeyCode.Escape)){
			escEvent ();
		}

		if (isPaused) {
			if (Input.GetKeyDown (KeyCode.DownArrow)) {
				selected++;
				if (selected > 2)
					selected = 0;
				setSelection(selected);
			}
			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				selected--;
				if (selected < 0)
					selected = 2;
				setSelection(selected);
			}


			if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.Return)) {
				switch (selected) {
				case 0:
					escEvent ();
					break;
				case 1:
                    Time.timeScale = 1;
                    Application.LoadLevel(0);
					break;
				case 2:
					Application.Quit ();
					break;
				}
			}
		}
	}

	private void setSelection(int pos){
		Image im1 = (button1.GetComponent<Image> ());
		im1.color = Color.white;
		(button2.GetComponent<Image> ()).color = Color.white;
		(button3.GetComponent<Image> ()).color = Color.white;

		switch (pos) {
		case 0:
			(button1.GetComponent<Image> ()).color = Color.cyan;
			break;
		case 1:
			(button2.GetComponent<Image> ()).color = Color.cyan;
			break;
		case 2:
			(button3.GetComponent<Image> ()).color = Color.cyan;
			break;
		}
	}

	private void escEvent(){
		isPaused = !isPaused;
		button1.gameObject.SetActive(isPaused);
		button2.gameObject.SetActive(isPaused);
		button3.gameObject.SetActive(isPaused);
		napis.gameObject.SetActive(isPaused);
		setSelection(selected);

		if (isPaused == true) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}
}
