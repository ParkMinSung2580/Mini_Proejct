using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    private float inputX;
    private float inputY;

    private Vector2 dic;
    private Rigidbody2D playerRb;

    private SpriteRenderer playerSr;

    // Start is called before the first frame update
    void Start()
    {
        playerRb= GetComponent<Rigidbody2D>();
        playerSr= GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        
        dic = new Vector2(inputX, inputY);

        playerRb.velocity = dic.normalized * speed;

        PlayerLookAt();
    }

    
    private void PlayerLookAt()
    {
        if (inputX > 0)
            playerSr.flipX = false; // ¿À¸¥ÂÊ
        else if (inputX < 0)
            playerSr.flipX = true;  // ¿ÞÂÊ
    } 
}
