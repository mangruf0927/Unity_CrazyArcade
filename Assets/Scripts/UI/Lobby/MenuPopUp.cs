using UnityEngine;

public class MenuPopUp : MonoBehaviour
{
    public GameObject optionPopUp;
    public GameObject exitPopUp;

    // Option Button
    public void OnClickOption()
    {
        SoundManager.Instance.PlaySFX("Select");
        optionPopUp.SetActive(true);
        this.gameObject.SetActive(false);
    }

    // ExitButton
    public void OnClickExit()
    {
        SoundManager.Instance.PlaySFX("Select");
        exitPopUp.SetActive(true);
        this.gameObject.SetActive(false);
    }

}
