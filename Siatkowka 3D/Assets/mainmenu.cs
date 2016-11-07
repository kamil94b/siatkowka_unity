using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class mainmenu : MonoBehaviour {

    public GameObject button1, button2, button3, button4, button5, button6;

    private int selected = 0;
	// Use this for initialization
	void Start () {
        setSelection(0);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selected++;
            if (selected > 5)
                selected = 0;
            setSelection(selected);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selected = (selected+3) % 6;
            setSelection(selected);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selected--;
            if (selected < 0)
                selected = 5;
            setSelection(selected);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
             Application.LoadLevel(selected+1);  
        }

	}

    private void setSelection(int pos)
    {
        Image im1 = (button1.GetComponent<Image>());
        im1.color = Color.white;
        (button2.GetComponent<Image>()).color = Color.white;
        (button3.GetComponent<Image>()).color = Color.white;
        (button4.GetComponent<Image>()).color = Color.white;
        (button5.GetComponent<Image>()).color = Color.white;
        (button6.GetComponent<Image>()).color = Color.white;

        switch (pos)
        {
            case 0:
                (button1.GetComponent<Image>()).color = Color.cyan;
                break;
            case 1:
                (button2.GetComponent<Image>()).color = Color.cyan;
                break;
            case 2:
                (button3.GetComponent<Image>()).color = Color.cyan;
                break;
            case 3:
                (button4.GetComponent<Image>()).color = Color.cyan;
                break;
            case 4:
                (button5.GetComponent<Image>()).color = Color.cyan;
                break;
            case 5:
                (button6.GetComponent<Image>()).color = Color.cyan;
                break;
        }
    }
}
