using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : MonoBehaviour
{
    public float damage;
    Rigidbody2D rb;
    Vector3 direction;
    Transform playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = PlayerBehavior.Instance.transform;
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 바라볼 방향 설정
        direction = playerPosition.position - transform.position;

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
