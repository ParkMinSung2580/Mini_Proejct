using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;        //Enemy 이름
    public float health;            //체력
    public float attackDamage;      //공격 데미지
    public float visionRange;       //시야 범위
    public float visionAngle;       //시야 각
    public float moveSpeed;         //움직임 속도
}
