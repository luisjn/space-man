using System;
using Enums;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private static GameManager _gameManager;
    
    private Rigidbody2D _rigidbody;
    private Vector3 _startPosition;
    
    public float runningSpeed = 1.5f;
    public bool facingRight;
    public int enemyDamage = 10;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _startPosition = transform.position;
    }

    private void Start()
    {
        transform.position = _startPosition;
        _gameManager = GameManager.Instance;
    }

    private void FixedUpdate()
    {
        float currentRunningSpeed = runningSpeed;

        if (facingRight)
        {
            currentRunningSpeed = runningSpeed;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            currentRunningSpeed = -runningSpeed;
            transform.eulerAngles = Vector3.zero;
        }

        if (_gameManager.currentGameState.Equals(GameState.InGame))
        {
            _rigidbody.velocity = new Vector2(currentRunningSpeed, _rigidbody.velocity.y);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().CollectHealth(-enemyDamage);
            return;
        }

        facingRight = !facingRight;
    }
}
