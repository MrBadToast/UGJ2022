using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PatrolEnemy : MonoBehaviour
{
    public float moveSpeed;
    public float damage;
    public Transform patrolSection_A;
    public Transform patrolSection_B;

    private int rotate_dir = 1;
    Vector3 direction;
    private float distance;

    Vector3 enemyMovement;
    Rigidbody2D rb;

    Transform playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 진행 방향 설정
        if(rotate_dir == 0)
        {
            Debug.Log("dir = 0");
            direction = patrolSection_A.position - transform.position;
            distance = Vector3.Distance(patrolSection_A.position, transform.position);
        }
        else
        {
            Debug.Log("dir = 1");
            direction = patrolSection_B.position - transform.position;
            distance = Vector3.Distance(patrolSection_B.position, transform.position);
        }
        
        if (distance > 0.1f)
        {   
            transform.position += Time.deltaTime * moveSpeed * direction;
        }
        else
        {
            Debug.Log("방향전환");
            rotate_dir = (rotate_dir + 1) % 2;
        }


            // 적 캐릭터 방향 전환을 scale의 음수 양수 전환을 통해 수행.
        Vector3 scale = transform.localScale;
        if (direction.x > 0)
        {
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        else if (direction.x < 0)
        {
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        direction.Normalize();
        enemyMovement = direction;

    }

    // 플레이어와 충돌 시
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {   // 적 캐릭터가 가진 damage값을 return
            PlayerBehavior.Instance.DamagePlayer(damage);
        }
    }
}
