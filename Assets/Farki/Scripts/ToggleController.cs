using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleController : MonoBehaviour
{
    private bool _isToggled = false;

    public void HandleToggle(GameObject go)
    {
        _isToggled = !_isToggled;

        if (_isToggled)
            go.SetActive(true);

        else
            go.SetActive(false);
    }
}
