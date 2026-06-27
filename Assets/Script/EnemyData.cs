using UnityEngine;

[System.Serializable]
public class EnemyData
{
    ///<summary>敵のプレハブ</summary>
    public GameObject prefab;
    ///<summary>出現確率</summary>
    public int weight;
    ///<summary>出現開始時間</summary>
    public float spawnStartTime;      
}