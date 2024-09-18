using UnityEngine;
using UnityEngine.EventSystems;

public class MenuPopUp : MonoBehaviour
{
    public GameObject optionPopUp;
    public GameObject exitPopUp;

    // Option Button
    public void OnClickOption()
    {
        SoundManager.Instance.PlaySFX("Select");
        optionPopUp.SetActive(true);
        gameObject.SetActive(false);
    }

    // ExitButton
    public void OnClickExit()
    {
        SoundManager.Instance.PlaySFX("Select");

        EventSystem.current.SetSelectedGameObject(null);
        
        exitPopUp.SetActive(true);
        gameObject.SetActive(false);
    }

}
