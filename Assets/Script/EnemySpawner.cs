using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//RequireComponentを付けることで、必要なコンポーネントが必ず付くようにし、実行時エラーを防いでいる。
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
    //BoxCollider2Dを使って敵の出現範囲を決めている。
    private BoxCollider2D boxCollider2D;
    private Coroutine spawnCoroutine;

    public float startDelay = 3f;

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
        // ゲーム開始までstartDelay秒待つ
        yield return new WaitForSeconds(startDelay);

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

            // weightの合計を求める
            int totalWeight = 0;

            foreach (EnemyData enemy in spawnableEnemies)
            {
                totalWeight += enemy.weight;
            }

            // 0～合計weight未満の乱数を作る
            int randomValue = Random.Range(0, totalWeight);

            // 出現する敵を決定
            EnemyData selectedEnemy = null;

            foreach (EnemyData enemy in spawnableEnemies)
            {
                randomValue -= enemy.weight;

                if (randomValue < 0)
                {
                    selectedEnemy = enemy;
                    break;
                }
            }
                // 敵を生成し、計算した座標に移動する
                GameObject newObject = Instantiate(selectedEnemy.prefab);
                Debug.Log("敵を生成しました");
                newObject.transform.position = new Vector2(randomX + this.transform.position.x + boxCollider2D.offset.x, randomY + this.transform.position.y + boxCollider2D.offset.y);


                Enemy enemyComponent = newObject.GetComponent<Enemy>();
                aliveEnemies.Add(enemyComponent);
                //Enemyから色,スコアを取得
                enemyComponent.color = selectedEnemy.color;
                enemyComponent.score = selectedEnemy.score;

                //Enemyからcloserへ速度渡し
                closer closerComponent = newObject.GetComponent<closer>();

                if (closerComponent != null)
                {
                    closerComponent.growSpeed = selectedEnemy.growSpeed;
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
