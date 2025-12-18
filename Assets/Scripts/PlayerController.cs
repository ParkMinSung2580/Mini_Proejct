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
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();       
    }

    private void PlayerInput()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        
        dic = new Vector2(inputX, inputY);

        playerRb.velocity = dic.normalized * speed;
    }

    /*
    private void PlayerMove()
    {
        if (inputX < 0)
        {
            playerSr.flipX = true;
        }
        else
        {
            playerSr.flipX = false;
        }
    }
    */
}
