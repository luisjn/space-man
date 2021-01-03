using Enums;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static GameManager _gameManager;
    
    private const string StateAlive = "isAlive", StateONTheGround = "isOnTheGround", StateIsFalling = "isFalling";
    public const int InitialHealth = 100, InitialMana = 15, MaxHealth = 200, MaxMana = 30, MinHealth = 10, MinMana = 0;
    public const int SuperJumpCost = 5;
    public const float SuperJumpForce = 1.5f;

    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _startPosition;
    [SerializeField]
    private int healthPoints, manaPoints;

    public float jumpForce = 8f;
    public float walkingSpeed = 6f;
    public LayerMask groundMask;
    public float jumpRaicastDistance = 2f;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _startPosition = transform.position;
    }

    private void Update()
    {
        _animator.SetBool(StateONTheGround, IsTouchingTheGround());
        _animator.SetBool(StateIsFalling, IsFalling());
        
        if (Input.GetButtonDown("Jump"))
        {
            Jump(false);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Jump(true);
        }

        // gizmo
        Debug.DrawRay(transform.position, Vector2.down * jumpRaicastDistance, Color.gray);
    }

    private void FixedUpdate()
    {
        if (_gameManager.currentGameState.Equals(GameState.InGame))
        {
            if (_rigidBody.velocity.x < walkingSpeed)
            {
                Move();
            }
        }
        else
        {
            _rigidBody.velocity = new Vector2(0, _rigidBody.velocity.y);
        }
    }

    private void Jump(bool isSuperJump)
    {
        float jumpForceFactor = jumpForce;
        if (isSuperJump && manaPoints >= SuperJumpCost)
        {
            manaPoints -= SuperJumpCost;
            jumpForceFactor *= SuperJumpForce;
        }
        if (_gameManager.currentGameState.Equals(GameState.InGame))
        {
            if (IsTouchingTheGround())
            {
                GetComponent<AudioSource>().Play();
                _rigidBody.AddForce(Vector2.up * jumpForceFactor, ForceMode2D.Impulse);
            }
        }
    }

    private bool IsTouchingTheGround()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, jumpRaicastDistance, groundMask);
    }

    private bool IsFalling()
    {
        return _rigidBody.velocity.y < 0;
    }

    private void Move()
    {
        _rigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * walkingSpeed, _rigidBody.velocity.y);
        if (Input.GetAxis("Horizontal") < 0)
        {
            _spriteRenderer.flipX = true;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            _spriteRenderer.flipX = false;
        }
    }

    public void Die()
    {
        float travelledDistance = GetTravelledDistance();
        float previousMaxDistance = PlayerPrefs.GetFloat("maxscore", 0f);
        if (travelledDistance > previousMaxDistance)
        {
            PlayerPrefs.SetFloat("maxscore", travelledDistance);
        }
        
        _animator.SetBool(StateAlive, false);
        _gameManager.GameOver();
    }

    public void StartGame()
    {
        _animator.SetBool(StateAlive, true);    
        _animator.SetBool(StateONTheGround, false);    
        _animator.SetBool(StateIsFalling, true);

        healthPoints = InitialHealth;
        manaPoints = InitialMana;
        
        Invoke("RestartPosition", 0.1f);
    }

    private void RestartPosition()
    {
        transform.position = _startPosition;
        _rigidBody.velocity = Vector2.zero;
        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<CameraFollow>().ResetCameraPosition();
    }

    public void CollectHealth(int points)
    {
        healthPoints = healthPoints >= MaxHealth ? MaxHealth : healthPoints += points;

        if (healthPoints <= 0)
        {
            Die();
        }
    }
    
    public void CollectMana(int points)
    {
        manaPoints = manaPoints >= MaxMana ? MaxMana : manaPoints += points;
    }

    public int GetHealth()
    {
        return healthPoints;
    }

    public int GetMana()
    {
        return manaPoints;
    }

    public float GetTravelledDistance()
    {
        return transform.position.x - _startPosition.x;
    }
}
