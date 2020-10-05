using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonBlocker : MonoBehaviour
{
    private bool isinteractable;
    public Button thisButton;
    // Start is called before the first frame update
    void Start()
    {
        isinteractable = true;
    }

    public void SwitchButtonState()
    {
        isinteractable = !isinteractable;
        thisButton.interactable = isinteractable;
    }

    public void SwitchButtonState(bool isInt)
    {
        isinteractable = isInt;
        thisButton.interactable = isInt;
    }
}
