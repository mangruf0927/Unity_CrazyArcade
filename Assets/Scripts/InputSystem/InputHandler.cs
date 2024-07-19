using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public delegate void InputStateHandler();
    public event InputStateHandler OnPlayerIdle;
    public event InputStateHandler OnPlayerMove;

    public delegate void InputBoolHandler(bool value);
    public event InputBoolHandler OnCheckHorizontal; // 대각선 이동 불가함으로 체크해야 함


    private void Update() 
    {
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0 )
            {
                OnPlayerIdle?.Invoke();
            }

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            OnPlayerMove?.Invoke();
        }

        if(Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Vertical"))
        {
            OnCheckHorizontal?.Invoke(true);
        }
        else if(Input.GetButtonDown("Vertical") || Input.GetButtonUp("Horizontal"))
        {
            OnCheckHorizontal?.Invoke(false);
        }

    }
}