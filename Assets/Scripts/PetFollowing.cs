using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetFollowing : MonoBehaviour
{
    public float moveSpeed;
    public float distanceLimit;
    // petMovment는 안쓰지만 일단 남겨놓음
    Vector3 petMovment;
    Rigidbody2D rb;

    public Transform seniorTransform;
    

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 에러가 날 바엔 그냥 하지 마라
        if (seniorTransform == null)
            return;

        // 펫이 가야할 direction 설정, normalize()
        Vector3 direction = (seniorTransform.position - transform.position).normalized;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //rb.rotation = angle; -> 캐릭터를 향해 실시간 rotate

        // 혹시 몰라서 한 번 더
        direction.Normalize();

        // FixedUpdate를 통해 펫을 움직이려고 만든 코드이나, FixedUpdate를 안쓰기로 해서 사실 필요는 없음
        petMovment = direction;

        // 펫 캐릭터 방향 전환을 scale의 음수 양수 전환을 통해 수행.
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

        // 목표체와 펫 사이의 거리 계산 
        float distance = Vector3.Distance(seniorTransform.position, transform.position);
        if(distance > distanceLimit)
        {   // 현재 거리가 최소 거리보다 멀 때만 이동
            transform.position += direction * Time.deltaTime * moveSpeed;
        }
    }

    // 혹시 몰라서 남겨놓음. FixedUpdate와 MoveCharacter 둘다.
    private void FixedUpdate()
    {
        //원래 쓰려고 했으나, 안써도 될 것 같아서 안씀
        //MoveCharacter(petMovment);
    }
    void MoveCharacter(Vector3 direction)
    {
        rb.MovePosition((Vector3)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    // 플레이어와 충돌 시 -> 펫 획득
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 리듬게임을 진행하고 성공하면 플레이어 따라가는 코드 
            // TopViewCharacterBehavior.Instance.
        }
    }
}
