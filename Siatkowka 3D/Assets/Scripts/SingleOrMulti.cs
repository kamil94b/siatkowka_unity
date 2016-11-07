using UnityEngine;
using System.Collections;

public class SingleOrMulti : MonoBehaviour {
    
    public bool SelectSingle(bool choice)
    {
        Application.LoadLevel(1);

        bool SingleSelect = choice;
        return SingleSelect;
    }
}
