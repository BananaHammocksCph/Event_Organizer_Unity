using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserController : MonoBehaviour
{
    [SerializeField] private TMP_InputField u_Name, u_Email, u_Password;

    [SerializeField] private GameObject _loginUI, _eventManagerUI;

    private RestController _restController;

    private string _errorMessage;
    private bool _displayError;

    const int kDialogWidth = 300;
    const int kDialogHeight = 100;

    private User _user;

    #region Properties

    public User User
    {
        get => _user;
        set => _user = value;
    }

    #endregion

    private void Start()
    {
        _restController = FindObjectOfType<RestController>();
    }

    public void OnLogin()
    {
        User = new User
        {
           // Name = u_Name.text,
            Email = u_Email.text,
            Password = u_Password.text
        };

        _restController.PostLogin(User);

        StartCoroutine(CheckLogin());

    }

    IEnumerator CheckLogin()
    {
        yield return new WaitUntil(() =>_restController._updatedValue == true);

        Debug.Log("_restControll " + _restController.IsValidLogin);

        if (_restController.IsValidLogin == true)
        {
            _loginUI.SetActive(false);
            _eventManagerUI.SetActive(true);
        }

        else
        {
            _displayError = true;
            _errorMessage = "Email or Password is wrong";
        }
        _restController._updatedValue = false;
    }

    public void OnRegister()
    {
        User = new User
        {
            Name = u_Name.text,
            Email = u_Email.text,
            Password = u_Password.text
        };

        _restController.PostRegister(User);
    }

    #region ErrorHandling

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
}
