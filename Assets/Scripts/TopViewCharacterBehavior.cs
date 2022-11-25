using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class TopViewCharacterBehavior : MonoBehaviour
{
    static private TopViewCharacterBehavior instance;
    static public TopViewCharacterBehavior Instance { get { return instance; } }

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
    [SerializeField,Range(0f,1f)] private float acceleration;
    [SerializeField,Range(0f,1f)] private float drag;

    private bool isControlActive = true;
    public bool IsControlActive { get { return isControlActive; } }

    public void DamagePlayer(float damageAmount) { }

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
            rbody.velocity = Vector2.Lerp(rbody.velocity,new Vector2( moveSpeed,rbody.velocity.y),acceleration);
        }
        else if (Input.GetKey(key_left))
        {
            transform.right = Vector2.left;
            rbody.velocity = Vector2.Lerp(rbody.velocity, new Vector2(-moveSpeed, rbody.velocity.y), acceleration);
        }

        if (Input.GetKey(key_up))
        {
            rbody.velocity = Vector2.Lerp(rbody.velocity, new Vector2(rbody.velocity.x, moveSpeed), acceleration);
        }
        else if (Input.GetKey(key_down))
        {
            rbody.velocity = Vector2.Lerp(rbody.velocity, new Vector2(rbody.velocity.x, -moveSpeed), acceleration);
        }

        rbody.velocity = Vector2.Lerp(rbody.velocity, Vector2.zero, drag);

    }



}

