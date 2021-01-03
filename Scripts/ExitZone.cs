using UnityEngine;

public class ExitZone : MonoBehaviour
{
    private static LevelManager _levelManager;
    private void Start()
    {
        _levelManager = LevelManager.Instance;
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _levelManager.AddLevelBlock();
            _levelManager.RemoveLevelBlock();
        }
    }
}
