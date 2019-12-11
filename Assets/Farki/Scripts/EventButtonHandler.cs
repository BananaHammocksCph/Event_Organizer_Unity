using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventButtonHandler : MonoBehaviour
{
    private Event _event;

    [SerializeField] private TextMeshProUGUI _buttonText;


    private void Start()
    {
       _buttonText.text = Event.Name;
    }

    #region Properties

    public Event Event
    {
        get => _event;
        set
        {            
            _event = value;
        }
    }

    #endregion

    public void OnButtonClick(GameObject prefab)
    {
        GameObject _infoGo = Instantiate(prefab, GameObject.FindGameObjectWithTag("EventPanel").transform);

        EventInformationDisplayer _eInfo = _infoGo.GetComponent<EventInformationDisplayer>();
        _eInfo.DisplayInfo(Event);

        //if(Event.Catering == true)
        //{
        //    _eInfo.DisplayInfo("Catering", "Catering will be available");
        //    _eInfo.DisplayInfo("Catering Description", Event.Catering_Desc);
        //}
        
    }
}
