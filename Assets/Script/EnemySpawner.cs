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
            // Create some random numbers
            // 範囲内でランダムな座標を求める
            float randomX = Random.Range(-boxCollider2D.size.x, boxCollider2D.size.x) * .5f;
            float randomY = Random.Range(-boxCollider2D.size.y, boxCollider2D.size.y) * .5f;

            // ランダムな敵を選ぶ
            int randomIndex = Random.Range(0, enemyList.Count);

            // Generate the new object
            // オブジェクトを生成し、計算した座標に移動する
            GameObject newObject = Instantiate(enemyList[randomIndex].prefab);
            newObject.transform.position
                = new Vector2(randomX + this.transform.position.x + boxCollider2D.offset.x, randomY + this.transform.position.y + boxCollider2D.offset.y);

            // Wait for some time before spawning another object
            // 処理をループさせる前に待つ
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
