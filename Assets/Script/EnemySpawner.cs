using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemySpawner : MonoBehaviour
{
    [Header("Object creation")]

    public List<EnemyData> enemyList = new List<EnemyData>();
    
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
            Enemy enemyConponent = newObject.GetComponent<Enemy>();

            enemyConponent.color = spawnableEnemies[randomIndex].color;

            // 処理をループさせる前に待つ
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
