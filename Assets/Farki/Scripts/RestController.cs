using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using System.Linq;
using UnityEditor;

public class RestController : MonoBehaviour
{

    [SerializeField] private string _baseURL = "http://ec2-18-191-80-15.us-east-2.compute.amazonaws.com:3001/api";


    private UID userId;
    #region Properties
    private bool _isValid;
    private string _id;

    [HideInInspector]
    public bool _updatedValue;

    public bool IsValidLogin
    {
        get { return _isValid; }
        set
        {
            _isValid = value;
        }
    }

    #endregion

    private void Start()
    {
        userId = new UID();
    }

    /// <summary>
    /// Used for creating a POST call for creating an event.
    /// </summary>
    /// <param name="_event"> TThe event is created in the EventHelper.cs </param>
    public void PostEvent(Event _event)
    {
        RestClient.Post(_baseURL + "/events", _event).Then(res =>
        {
            FindObjectOfType<FeedbackMessage>().ShowFeedback("Event Created!");
        });
    }

    public void PostLogin(User _user)
    {
        RestClient.Post<UserResponse>(_baseURL + "/login", _user).Then(res =>{
            Debug.Log("EM: " + res.data.Email);
            UserController _uc = FindObjectOfType<UserController>();

        if (res.data.Email == _uc.User.Email && res.data.Password == _uc.User.Password)
            {
                IsValidLogin = true;
                userId.Id =  res.data._id ;
            }

            else IsValidLogin = false;


            _updatedValue = true;
        });

    }

    public void PostRegister(User _user)
    {
        RestClient.Post(_baseURL + "/users", _user).Then(res=> {
            FindObjectOfType<FeedbackMessage>().ShowFeedback("User Registered");
        });

    }

    /// <summary>
    ///  Method to get all events and serialize it to an object
    /// </summary>
    public void GetAllEvents()
    {
        var usersRoute = _baseURL + "/events";

        RestClient.Get<EventResponse>(usersRoute).Then(e =>
        {        
            FindObjectOfType<EventHelper>().GetAllEvents(e.data);           
        });

    }

    public void GetAllUsersFromEvent(string eventId)
    {
        var usersRoute = _baseURL + "/events/" + eventId + "/users";

        RestClient.Get<E_User>(usersRoute).Then(e =>
        {// 22 3
            //e.Text.Substring(22, e.Text.Length - 3)
            FindObjectOfType<EventInformationDisplayer>().UserIds = e.data;
        });
    }

    public void AddUserToEvent(string eventId, User user)
    {
        var eventRoute = _baseURL + "/events/" + eventId + "/users";

        RestClient.Post(eventRoute, userId);
        GetAllUsersFromEvent(eventId);
    }

    public void PostRating(string eventId, Rating _rating)
    {
        var ratingRoute = _baseURL + "/events/" + eventId + "/ratings";

        RestClient.Post(ratingRoute, _rating).Then(res =>
        {
            Debug.Log("RATING RES: " + res.Text);

            FeedbackMessage _feedbackMessage = FindObjectOfType<FeedbackMessage>();
            if (res.Text.Contains("400"))
            {
                _feedbackMessage.ShowFeedback("Invalid location");
            }
            else
            {
                _feedbackMessage.ShowFeedback("Rating Successful");
            }
        });
    }

    //test
    //public IEnumerator GetRatings(string eventId)
    //{
    //    bool _isPopulated = false;
    //    var ratingsRoute = _baseURL + "/events/"+ eventId + "/ratings";

    //    List<int> _scores = new List<int>();

    //    RestClient.Get<RatinResponse>(ratingsRoute).Then(res =>
    //    {
    //        foreach(Rating r in res.data)
    //        {
    //            _scores.Add(r.Score);
    //        }
    //        _isPopulated = true;
    //    });

    //    yield return new WaitUntil(() => _isPopulated);

    //    double _avg = _scores.Average();
    //    FindObjectOfType<EventInformationDisplayer>().GetAvgRating(_avg);
    //}

    public void GetAllRating(string eventId)
    {
        var ratingsRoute = _baseURL + "/events/" + eventId + "/ratings";
        RestClient.Get<RatinResponse>(ratingsRoute).Then(res =>
        {
            FindObjectOfType<EventInformationDisplayer>().GetAllRatings(res.data);
        });
    }
}