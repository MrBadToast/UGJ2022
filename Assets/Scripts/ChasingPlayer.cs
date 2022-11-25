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
        playerPosition = TopViewCharacterBehavior.Instance.transform;
        Debug.Log(TopViewCharacterBehavior.Instance.transform.position);
        rb = this.GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = playerPosition.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //rb.rotation = angle;
        direction.Normalize();
        enemyMovement = direction;
    }
    private void FixedUpdate()
    {
        MoveCharacter(enemyMovement);
    }
    void MoveCharacter(Vector3 direction)
    {
        rb.MovePosition((Vector3)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
    private void OnTriggerEnter2D(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TopViewCharacterBehavior.Instance.DamagePlayer(damage);
        }
    }
}
