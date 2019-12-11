using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventInformationDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI e_Name, e_Description, e_Type, e_Created, e_Date, e_Location, e_Users, c_Description;

    private Event _currentEvent;

    private string[] _uIds;

    RestController _restController;

    #region Properties

    public string[] UserIds
    {
        get {return _uIds; }
        set
        {
            if(value != null)
            {
                _uIds = value;

                var sb = new System.Text.StringBuilder();
                foreach(string _id in _uIds)
                {
                    sb.AppendLine(_id);
                }

                e_Users.text = sb.ToString();
            }

        }
    }

    #endregion

    private void Start()
    {
        _restController = FindObjectOfType<RestController>();
    }

    public void DisplayInfo(Event _event)
    {
        _currentEvent = _event;
        FindObjectOfType<RestController>().GetAllUsersFromEvent(_event._id);

        e_Name.text = _event.Name;
        e_Description.text = _event.Description;
        e_Type.text = _event.Type;
        e_Created.text = _event.Created;
        e_Date.text = _event.Date;
        e_Location.text = _event.Location;

        if (_event.Catering)
        {
            c_Description.text = _event.Catering_Desc;
        }

        else
        {
            c_Description.text = "No catering available";
        }
    }

    public void Register()
    {
        _restController.AddUserToEvent(_currentEvent._id, FindObjectOfType<UserController>().User);
    }

    public void OnExit()
    {
        Destroy(gameObject);
    }
}
