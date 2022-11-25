using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class TopViewCharacterBehavior : MonoBehaviour
{
    static private TopViewCharacterBehavior instance;
    static public TopViewCharacterBehavior Instance;

    public delegate void ONPlayerDied();
    public ONPlayerDied OnPlayerDied;

    public delegate void ONPlayerGetFellow();
    public ONPlayerGetFellow OnPlayerGetFellow;

    [Title("Controls")]
    [SerializeField] private KeyCode key_right;
    [SerializeField] private KeyCode key_left;
    [SerializeField] private KeyCode key_up;
    [SerializeField] private KeyCode key_down;

    [Title("Properties")]
    [SerializeField] private float moveSpeed;
    [SerializeField,Range(0f,1f)] private float drag;

    private bool isControlActive = true;
    public bool IsControlActive { get { return isControlActive; } }

    private Rigidbody2D rbody;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        rbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(key_right))
        {
            transform.right = Vector2.right;
            rbody.velocity = new Vector2(moveSpeed,rbody.velocity.y);
        }
        else if (Input.GetKey(key_left))
        {
            transform.right = Vector2.left;
            rbody.velocity = new Vector2(-moveSpeed, rbody.velocity.y);
        }

        if (Input.GetKey(key_up))
        {
            rbody.velocity = new Vector2(rbody.velocity.x, moveSpeed);
        }
        else if (Input.GetKey(key_down))
        {
            rbody.velocity = new Vector2(rbody.velocity.x, -moveSpeed);
        }

        rbody.velocity = Vector2.Lerp(rbody.velocity, Vector2.zero, drag);
    }

}

