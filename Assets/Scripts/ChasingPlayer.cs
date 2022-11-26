using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingPlayer : MonoBehaviour
{
    public float moveSpeed;
    public float damage;
    Vector3 enemyMovement;
    Rigidbody2D rb;

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
        // 쫓아갈 방향 설정
        Vector3 direction = playerPosition.position - transform.position;
        // 당장은 왼쪽 오른쪽 방향전환밖에 없어서 주석처리했지만, 혹시 몰라서 남겨둠
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //rb.rotation = angle;

        // 적 캐릭터 방향 전환을 scale의 음수 양수 전환을 통해 수행.
        Vector3 scale = transform.localScale;
        if(direction.x > 0)
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

    // PetFollowing에서는 쓰지 않았던 FixedUpdate를 여기선 쓴다. 더 고치기 불안해서 그냥 냅둠.
    private void FixedUpdate()
    {
        MoveCharacter(enemyMovement);
    }
    void MoveCharacter(Vector3 direction)
    {
        rb.MovePosition((Vector3)transform.position + (direction * moveSpeed * Time.deltaTime));
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
