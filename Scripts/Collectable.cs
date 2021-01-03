using System;
using Enums;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private static GameManager _gameManager;
    
    private SpriteRenderer sprite;
    private CircleCollider2D itemCollider;
    // private bool hasBeenCollected;
    private GameObject player;

    public int value = 1;
    public CollectableType type = CollectableType.Money;
    
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        itemCollider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    private void Show()
    {
        sprite.enabled = true;
        itemCollider.enabled = true;
        // hasBeenCollected = false;
    }
    
    private void Hide()
    {
        sprite.enabled = false;
        itemCollider.enabled = false;
    }
    
    private void Collect()
    {
        Hide();
        // hasBeenCollected = true;

        switch (type)
        {
            case CollectableType.Money:
                _gameManager.CollectObject(this);
                GetComponent<AudioSource>().Play();
                break;
            case CollectableType.HealthPotion:
                player.GetComponent<PlayerController>().CollectHealth(value);
                break;
            case CollectableType.ManaPotion:
                player.GetComponent<PlayerController>().CollectMana(value);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
