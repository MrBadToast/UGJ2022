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
    [SerializeField] private float drag;

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
        if(Input.GetKey(key_right))
        {

        }
    }

}

