using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerBehavior : MonoBehaviour
{
    static private PlayerBehavior instance;
    static public PlayerBehavior Instance { get { return instance; } }

    public delegate bool ONPlayerDamaged();
    public ONPlayerDamaged OnPlayerDamaged;

    public delegate bool ONPlayerDied();
    public ONPlayerDied OnPlayerDied;

    public delegate void ONPlayerGetFellow(PetFollowing pet);
    public ONPlayerGetFellow OnPlayerGetFellow;

    private enum ControlState
    {
        NONE,
        DIRECTION,
        TIMING
    }

    [Title("Controls")]
    [SerializeField] private KeyCode key_tryDirection;
    [SerializeField] private KeyCode key_tryConfirm;
    //[SerializeField] private KeyCode key_right;
    //[SerializeField] private KeyCode key_left;
    //[SerializeField] private KeyCode key_up;
    //[SerializeField] private KeyCode key_down;

    [Title("Properties")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float fullHealth;
    [SerializeField,Range(0f,1f)] private float acceleration;
    [SerializeField,Range(0f,1f)] private float drag;
    [Title("RhythmMechanicProperties")]
    [SerializeField] private float interval = 0.5f;
    [SerializeField] private float tollerance = 0.1f;

    [Title("References")]
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Transform DirectionArrowAnchor;
    [SerializeField] private GameObject DirectionArrowObject;
    [SerializeField] private GameObject DirectionArrowShot;

    private bool isControlActive = true;
    private float currentHealth;
    private float rhythmTimer = 0f;
    private Vector2 forwardingDirection = Vector2.right;
    [SerializeField] private ControlState controlState = ControlState.NONE;
    public bool IsControlValid { get { return isControlActive && Time.timeScale != 0f; } }

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

    private void Start()
    {
        currentHealth = fullHealth;
    }

    private void FixedUpdate()
    {
        {
            //if (Input.GetKey(key_right))
            //{
            //    transform.right = Vector2.right;
            //    rbody.velocity = Vector2.Lerp(rbody.velocity,new Vector2( moveSpeed,rbody.velocity.y),acceleration);
            //}
            //else if (Input.GetKey(key_left))
            //{
            //    transform.right = Vector2.left;
            //    rbody.velocity = Vector2.Lerp(rbody.velocity, new Vector2(-moveSpeed, rbody.velocity.y), acceleration);
            //}

            //if (Input.GetKey(key_up))
            //{
            //    rbody.velocity = Vector2.Lerp(rbody.velocity, new Vector2(rbody.velocity.x, moveSpeed), acceleration);
            //}
            //else if (Input.GetKey(key_down))
            //{
            //    rbody.velocity = Vector2.Lerp(rbody.velocity, new Vector2(rbody.velocity.x, -moveSpeed), acceleration);
            //}

            //rbody.velocity = Vector2.Lerp(rbody.velocity, Vector2.zero, drag);
        }       
    }

    private void Update()
    {
        if (IsControlValid)
        {
            rbody.velocity = Vector2.Lerp(rbody.velocity, forwardingDirection.normalized, acceleration);

            if (controlState == ControlState.NONE)
            {
                if (Input.GetKeyDown(key_tryDirection))
                {
                    controlState = ControlState.DIRECTION;
                    DirectionArrowObject.SetActive(true);
                }
            }
            else if (controlState == ControlState.DIRECTION)
            {
                SetArrowDirection();
                if (Input.GetKeyDown(key_tryConfirm))
                {
                    controlState = ControlState.TIMING;
                }
            }
            else if(controlState == ControlState.TIMING)
            {

            }
        }
    }

    public void DamagePlayer(float damageAmount) 
    {
        if(damageAmount >= currentHealth)
        {
            PlayerDied();
            currentHealth = 0f;
        }
        else
        {
            currentHealth -= damageAmount;
        }

        healthBar.SetHealthbar(currentHealth / fullHealth);
    }

    public void HealPlayer(float healAmount)
    {
        if(healAmount + currentHealth >= fullHealth)
        {
            currentHealth = fullHealth;
        }
        else
        {
            currentHealth += healAmount;
        }

        healthBar.SetHealthbar(currentHealth/fullHealth);
    }

    private void PlayerDied()
    {
        OnPlayerDied.Invoke();
        isControlActive = false;
    }

    private void SetArrowDirection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - transform.position;

        DirectionArrowAnchor.up = direction;
        
    }

}

