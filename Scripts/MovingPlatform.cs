using System;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Animator animator = GetComponent<Animator>();
        animator.enabled = true;
    }
}
