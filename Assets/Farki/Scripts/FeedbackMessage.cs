using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Author: István Farkas
/// <para>
/// </para>
/// </summary>

public class FeedbackMessage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _feedbackText;
    [SerializeField] private GameObject _feedbackObject;

    // Start is called before the first frame update
    void Start()
    {
        _feedbackObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowFeedback(string _message)
    {
        _feedbackObject.SetActive(true);
        _feedbackText.text = _message;
    }
}
