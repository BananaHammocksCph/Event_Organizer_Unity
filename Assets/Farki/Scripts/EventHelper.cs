using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;

public class EventHelper : MonoBehaviour
{
    [SerializeField] private TMP_InputField e_Name, e_Description, e_Date, c_Description, e_Location;
    [SerializeField] private TMP_Dropdown e_Type;

    [SerializeField] private GameObject _eventPrefab, _content;

    RestController _restController;

    private string _errorMessage;
    private bool _displayError;

    const int kDialogWidth = 300;
    const int kDialogHeight = 100;

    private bool _isCatering;

    #region Properties

    public bool IsCatering
    {
        get => _isCatering;
        set => _isCatering = value;
    }

    #endregion

    #region ErrorHandling

    private void CheckContents()
    {
        if (e_Name.text.Length < 1)
        {
            _errorMessage = "You need to fill out the name";
            _displayError = true;
        }

        if (e_Description.text.Length < 1)
        {
            _errorMessage = "You need to fill out the description";
            _displayError = true;
        }

        if(e_Date.text == "")
        {
            _errorMessage = "You need to fill out the date";
            _displayError = true;
        }

        if(IsCatering && c_Description.text == "")
        {
            _errorMessage = "You need to fill out Catering Description";
            _displayError = true;
        }
    }

    void Window(int windowID)
    {
        GUI.Label(new Rect(10, 20, kDialogWidth - 20, kDialogHeight - 50), _errorMessage);

        if (GUI.Button(new Rect(kDialogWidth - 110, kDialogHeight - 30, 100, 20), "Okay"))
        {
            _displayError = false;
        }

    }

    private void OnGUI()
    {
        if (_displayError)
        {
            Rect rect = new Rect((Screen.width / 2) - (kDialogWidth / 2), (Screen.height / 2) - (kDialogHeight / 2), kDialogWidth, kDialogHeight);
            GUI.ModalWindow(0, rect, Window, "Error");
        }
    }

    #endregion


    private void Start()
    {
        _restController = FindObjectOfType<RestController>();
    }

    public void CreateEvent()
    {
        //CheckContents();

        Event _event = new Event
        {
            Name = e_Name.text,
            Description = e_Description.text,
            Type = e_Type.options[e_Type.value].text,
            Catering = _isCatering,
            email = FindObjectOfType<UserController>().User.Email,
        //    Created = DateTime.Now.ToString("DD-mm-YYYY"),
            Catering_Desc = c_Description.text,
            Location = e_Location.text,
            Date = e_Date.text
        };

        _restController.PostEvent(_event);

        string path = Application.dataPath + "/Test.json";
        using(StreamWriter writer = new StreamWriter(path))
        {
            string j = JsonUtility.ToJson(_event,true);
            writer.Write(j);
        }
    }


    public void GetAllEvents(Event[] _events)
    {
        foreach(Event e in _events)
        {
            GameObject go = Instantiate(_eventPrefab, _content.transform);
            go.GetComponent<EventButtonHandler>().Event = e;
        }
    }
}
