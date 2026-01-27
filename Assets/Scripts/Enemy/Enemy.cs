using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public EnemyData data;
    public EnemyData Data => data;

    public Vector2 HomePosition { get; private set; }

    [SerializeField] Vector3 eyeOffset = new Vector2(0, 1);
    [SerializeField] public Animator animator;

    public SpriteRenderer sr;
    public Transform Player { get; private set; }

    public StateMachine FSM { get; private set; }

    [SerializeField] string currentStateName; // 현재 상태 디버깅
    public EnemyContext Context { get; private set; }

    void Awake()
    {
        FSM = GetComponent<StateMachine>();
        FSM.ChangeState(new IdleState(this, FSM));
        Context = new EnemyContext(this);
    }


    private void Start()
    {
        if(Player == null) Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update() { FSM.Update(); currentStateName = FSM.GetStateName(); }


    public bool CanSeePlayer()
    {
        Vector2 dirToPlayer = Player.position - transform.position;
        float distance = dirToPlayer.magnitude;

        if (distance > data.visionRange)
            return false;

        // Gizmos와 동일한 방향 벡터 사용
        Vector2 fov = sr.flipX ? -transform.right : transform.right;

        float angle = Vector2.Angle(fov, dirToPlayer);
        return angle <= data.visionAngle * 0.5f;
    }

    private void OnDrawGizmosSelected()
    {
        if (data == null) return;

        Vector3 origin = transform.position + eyeOffset; 
        float halfAngle = data.visionAngle * 0.5f;

        Vector2 fov;
        if (sr.flipX)
        {
            // 아이작 시점: 캐릭터가 바라보는 방향을 기준으로
            fov = -transform.right;
        }
        else
        {
            fov = transform.right;
        }

        Quaternion leftRot = Quaternion.AngleAxis(-halfAngle, Vector3.forward);
        Quaternion rightRot = Quaternion.AngleAxis(halfAngle, Vector3.forward);

        Vector2 leftDir = leftRot * fov;
        Vector2 rightDir = rightRot * fov;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(origin, origin + (Vector3)leftDir * data.visionRange);
        Gizmos.DrawLine(origin, origin + (Vector3)rightDir * data.visionRange);
        Gizmos.DrawLine(origin + (Vector3)leftDir * data.visionRange,
                        origin + (Vector3)rightDir * data.visionRange);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(origin, origin + (Vector3)fov * data.visionRange);
    }

    public Coroutine RunCoroutine(IEnumerator routine)
    {
        return StartCoroutine(routine);
    }

    public void StopRunningCoroutine(Coroutine coroutine)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
    }

    //Enemy 추격 전 Pos 저장
    public void SavePreChasePosition()
    {
        HomePosition = transform.position;
    }

    // A* ClosePath를 반대로 되돌아가야겠는데
    public void Return()
    {

    }

}

