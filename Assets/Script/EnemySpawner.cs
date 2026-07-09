using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemySpawner : MonoBehaviour
{
    [Header("Object creation")]
    //出現する敵のリスト
    public List<EnemyData> enemyList = new List<EnemyData>();
    //生きている敵のリスト
    public List<Enemy> aliveEnemies = new List<Enemy>();

    [Header("Other options")]

    public float spawnInterval = 1.0f;
    private BoxCollider2D boxCollider2D;
    private Coroutine spawnCoroutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spawnCoroutine = StartCoroutine(SpawnObject());
    }

    // Update is called once per frame
    void OnDisable()
    {
        StopCoroutine(spawnCoroutine);
    }
    IEnumerator SpawnObject()
    {
        while (true)
        {
            Debug.Log("ループ開始");
            // 範囲内でランダムな座標を求める
            float randomX = Random.Range(-boxCollider2D.size.x, boxCollider2D.size.x) * .5f;
            float randomY = Random.Range(-boxCollider2D.size.y, boxCollider2D.size.y) * .5f;

            // 現在出現できる敵だけを入れるリスト
            List<EnemyData> spawnableEnemies = new List<EnemyData>();

            foreach (EnemyData enemy in enemyList)
            {
                if (Time.time >= enemy.spawnStartTime)
                {
                    spawnableEnemies.Add(enemy);
                }
            }

            // 出現可能な敵をランダムに選ぶ
            if (spawnableEnemies.Count == 0)
            {
                yield return new WaitForSeconds(spawnInterval);
                continue;
            }

            int randomIndex = Random.Range(0, spawnableEnemies.Count);
           
            // 敵を生成し、計算した座標に移動する
            GameObject newObject = Instantiate(spawnableEnemies[randomIndex].prefab);
            Debug.Log("敵を生成しました");
            newObject.transform.position = new Vector2(randomX + this.transform.position.x + boxCollider2D.offset.x, randomY + this.transform.position.y + boxCollider2D.offset.y);

            //Enemyから色を取得
            Enemy enemyComponent = newObject.GetComponent<Enemy>();
            aliveEnemies.Add(enemyComponent);
            enemyComponent.color = spawnableEnemies[randomIndex].color;

            //Enemyからcloserへ速度渡し
            closer closerComponent = newObject.GetComponent<closer>();

            if (closerComponent != null)
            {
                closerComponent.growSpeed = spawnableEnemies[randomIndex].growSpeed;
            }

            // 処理をループさせる前に待つ
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public Enemy GetFrontEnemy()
    {
        if (aliveEnemies.Count == 0)
        {
            return null;
        }

        Enemy frontEnemy = aliveEnemies[0];

        foreach (Enemy enemy in aliveEnemies)
        {
            if (enemy.transform.localScale.x > frontEnemy.transform.localScale.x)
            {
                frontEnemy = enemy;
            }
        }

        return frontEnemy;
    }
}
