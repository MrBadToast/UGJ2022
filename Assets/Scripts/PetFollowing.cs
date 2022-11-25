using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetFollowing : MonoBehaviour
{
    public float moveSpeed;
    Vector3 petMovment;
    Rigidbody2D rb;

    Transform playerPosition;

    void Start()
    {
        playerPosition = PlayerBehavior.Instance.transform;
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 direction = playerPosition.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // rb.rotation = angle;
        if (direction.x > 0)
        {
            transform.right = Vector3.right;
        }
        else if (direction.x < 0)
        {
            transform.right = Vector3.left;
        }
        direction.Normalize();
        petMovment = direction;
    }

    private void FixedUpdate()
    {
        MoveCharacter(petMovment);
    }
    void MoveCharacter(Vector3 direction)
    {
        rb.MovePosition((Vector3)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    // 플레이어와 충돌 시
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 리듬게임을 진행하고 성공하면 플레이어 따라가는 코드 
            // TopViewCharacterBehavior.Instance.
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // 그 앞의 펫을 따라가는 코드 
        }
    }
}
