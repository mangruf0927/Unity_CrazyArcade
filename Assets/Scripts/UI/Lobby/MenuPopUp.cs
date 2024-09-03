using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPopUp : MonoBehaviour
{
    public GameObject optionPopUp;
    public GameObject exitPopUp;

    // Option Button
    public void OnClickOption()
    {
        optionPopUp.SetActive(true);
        this.gameObject.SetActive(false);
    }

    // ExitButton
    public void OnClickExit()
    {
        exitPopUp.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
