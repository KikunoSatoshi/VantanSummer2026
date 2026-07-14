using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : MonoBehaviour
{
    public EnemyColor attackColor;
    private EnemySpawner spawner;
    private static bool canAttack = true;      // 攻撃できるか(staticによりASD共通で押せなくなる、static≒ゲーム全体で1つだけ存在するもの)
    public float penaltyTime = 1f;      // ペナルティ時間
    void Start()
    {
        spawner = FindAnyObjectByType<EnemySpawner>();
    }

    void Update()
    {
        if (!canAttack)
        {
            return;
        }

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

        if (enemy == null)
        {
            return;
        }

        // 色が違ったらペナルティ開始
        if (enemy.color != attackColor)
        {
            StartCoroutine(Penalty());
        }

        enemy.TakeDamage(attackColor);
    }

    IEnumerator Penalty()
    {
        canAttack = false;                  // 攻撃禁止

        yield return new WaitForSeconds(penaltyTime);

        canAttack = true;                   // 攻撃再開
    }
}