using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public GameObject prefab;    // 敵のプレハブ
    public int weight;           // 出現確率
    public float startTime;      // 出現開始時間
}