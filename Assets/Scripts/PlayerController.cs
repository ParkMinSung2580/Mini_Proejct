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

    private Vector2Int cellPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerRb= GetComponent<Rigidbody2D>();
        playerSr= GetComponent<SpriteRenderer>();
    }

    public void Spawn(Vector2Int cell)
    {
        cellPosition = cell;
    }

    public void MoveTo(Vector2Int cell)
    {
        cellPosition = cell;
        //이동시 좌표변환
        transform.position = GridManager.Instance.GridToWorld(cell);
    }


    private void FixedUpdate()
    {
        PlayerInput();
        UpdateCurrentCell();
    }

    private void PlayerInput()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        
        dic = new Vector2(inputX, inputY).normalized;

        //playerRb.velocity = dic.normalized * speed;

        PlayerLookAt();

        Move(dic);
    }

    
    private void PlayerLookAt()
    {
        if (inputX > 0)
            playerSr.flipX = false; // 오른쪽
        else if (inputX < 0)
            playerSr.flipX = true;  // 왼쪽
    }

    //디버그용
    private void UpdateCurrentCell()
    {
        cellPosition = GridManager.Instance.WorldToGrid(transform.position);
    }

    private void Move(Vector2 input)
    {
        if (input == Vector2.zero)
            return;

        Vector3 moveDir = input.normalized;
        Vector3 targetPos =
            transform.position + moveDir * speed * Time.deltaTime;

        Vector2Int targetCell =
            GridManager.Instance.WorldToGrid(targetPos);

        //if (!GridManager.Instance.IsValidCell(targetCell))
        //    return;

        if (!GridManager.Instance.GetCell(targetCell).isWalkable)
            return;

        transform.position = targetPos;
    }
}
