using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveTest : MonoBehaviour
{
    public Rigidbody2D rigid;
    public float speed;

    private bool isHorizonMove;
    private float h;
    private float v;

    private void Update() 
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        if(hDown || vUp)
        {
            isHorizonMove = true;
        }
        else if( vDown || hUp)
        {
            isHorizonMove = false;
        }
    }
    private void FixedUpdate() 
    {
        Vector2 moveVector = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVector * speed;
    }
}
