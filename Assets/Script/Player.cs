using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private EnemySpawner spawner;

    void Start()
    {
        spawner = FindFirstObjectByType<EnemySpawner>();
    }

    void Update()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            Attack(EnemyColor.Red);
        }

        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            Attack(EnemyColor.Blue);
        }

        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            Attack(EnemyColor.Yellow);
        }
    }

    void Attack(EnemyColor attackColor)
    {
        Enemy enemy = spawner.GetFrontEnemy();

        if (enemy != null)
        {
            enemy.TakeDamage(attackColor);
        }
    }
}