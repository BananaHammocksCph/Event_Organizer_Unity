using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DateHelper : MonoBehaviour
{
    private TMP_InputField _inputfield;

    private void Start()
    {
        _inputfield = GetComponent<TMP_InputField>();
    }

    public void CheckChar(string str)
    {
        Debug.Log("STR IS: " + str);
        if(str.Length == 2 || str.Length == 5)
        {
            _inputfield.text = str + "-";
            _inputfield.caretPosition = str.Length +1;
        }
    }
}
