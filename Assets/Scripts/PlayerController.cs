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

    private Coroutine moveCoroutine;

    [SerializeField] private Vector2Int cellPosition;

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


    private void Update()
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
    }

    void FixedUpdate() 
    {
        playerRb.velocity = dic.normalized * speed;
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
        // 이동은 월드 좌표로 부드럽게
        Vector3 moveDir = (Vector3)dic.normalized;
        transform.position += moveDir * speed * Time.deltaTime;

        /*if (input.normalized == Vector2.zero)
            return;

        Vector2Int currentCell =
            GridManager.Instance.WorldToGrid(transform.position);

        Vector2Int dir = new Vector2Int(
            Mathf.RoundToInt(input.x),
            Mathf.RoundToInt(input.y)
        );

        Vector2Int nextCell = currentCell + dir;

        
        if (!GridManager.Instance.HasCell(nextCell))
            return;

        
        if (!GridManager.Instance.IsWalkable(nextCell))
            return;


        if (moveCoroutine == null) // 이미 이동 중이면 무시
            moveCoroutine = StartCoroutine(MoveToCell(nextCell));*/

    }

    /*private IEnumerator MoveToCell(Vector2Int nextCell) 
    { 
        Vector3 targetPos = GridManager.Instance.GridToWorld(nextCell); 
        while ((transform.position - targetPos).sqrMagnitude > 0.01f) 
        { 
            transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime); 
            yield return null; 
        } 
        transform.position = targetPos;
        moveCoroutine = null;
    }*/
}
