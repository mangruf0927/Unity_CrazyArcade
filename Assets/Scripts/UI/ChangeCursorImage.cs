using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursorImage : MonoBehaviour
{
    public Texture2D defaultImage;
    public Texture2D clickImage;

    private void Start() 
    {
        Cursor.SetCursor(defaultImage, Vector2.zero, CursorMode.Auto);
    }

    private void Update() 
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
        {
            Cursor.SetCursor(clickImage, Vector2.zero, CursorMode.Auto); // 클릭 시 커서 변경
        }
        if (Input.GetMouseButtonUp(0)) // 마우스 왼쪽 버튼 떼면
        {
            Cursor.SetCursor(defaultImage, Vector2.zero, CursorMode.Auto); // 기본 커서로 복귀
        }
    }
}
