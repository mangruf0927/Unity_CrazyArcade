using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public delegate void InputStateHandler();
    public event InputStateHandler OnPlayerIdle;
    public event InputStateHandler OnPlayerMove;
    public event InputStateHandler OnWaterBalloonSet;

    public delegate void InputBoolHandler(bool value);
    public event InputBoolHandler OnCheckHorizontal; // 대각선 이동 불가함으로 체크해야 함

    public delegate void InputVectorHandler(Vector2 value);
    public event InputVectorHandler OnCheckDirection; 

    private void Update() 
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal == 0 && vertical == 0 )
        {
            OnPlayerIdle?.Invoke();
        }

        if (horizontal != 0 || vertical != 0)
        {
            OnPlayerMove?.Invoke();
            OnCheckDirection?.Invoke(new Vector2(horizontal, vertical));
        }

        if(Input.GetButtonDown("Horizontal") || (Input.GetButtonUp("Vertical") && horizontal != 0))
        {
            OnCheckHorizontal?.Invoke(true);
        }
        
        if(Input.GetButtonDown("Vertical") || (Input.GetButtonUp("Horizontal") && vertical != 0))
        {
            OnCheckHorizontal?.Invoke(false);
        }

        if(Input.GetKey(KeyCode.Space))
        {
            OnWaterBalloonSet?.Invoke();
        }
    }
}

