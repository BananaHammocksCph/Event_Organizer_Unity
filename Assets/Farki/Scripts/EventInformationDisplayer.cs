using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class EventInformationDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI e_Name, e_Description, e_Type, e_Created, e_Date, e_Location, e_Rating, e_Users, c_Description;

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

    private void Awake()
    {
        _restController = FindObjectOfType<RestController>();
    }
    private void Start()
    {

    }

    public void DisplayInfo(Event _event)
    {
        _currentEvent = _event;
        _restController.GetAllUsersFromEvent(_event._id);

        e_Name.text = _event.Name;
        e_Description.text = _event.Description;
        e_Type.text = _event.Type;
        e_Created.text = _event.Created;
        e_Date.text = _event.Date;
        e_Location.text = _event.Location;
        // StartCoroutine(_restController.GetRatings(_event._id));
        _restController.GetAllRating(_event._id);

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

    public void RateEvent(TMP_Dropdown _ratingDropdown)
    {
        LocationHandler _locationHandler = FindObjectOfType<LocationHandler>();

        Rating _rating = new Rating
        {
            Score = _ratingDropdown.value,
            lat = _locationHandler.Latitude,
            lon = _locationHandler.Longitude
        };

        _restController.PostRating(_currentEvent._id, _rating);
        _restController.GetAllRating(_currentEvent._id);
    }

    public void GetAvgRating(double _ratings)
    {
        e_Rating.text = _ratings.ToString("0.00");
    }

    public void GetAllRatings(Rating[] ratings)
    {
        List<int> _scores = new List<int>();


        foreach (Rating r in ratings)
        {
            _scores.Add(r.Score);
        }

        e_Rating.text = _scores.Average().ToString("0.00");
    }

    public void OnExit()
    {
        Destroy(gameObject);
    }
}
