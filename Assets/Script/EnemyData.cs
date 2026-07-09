using UnityEngine;

public enum EnemyColor
{
    Red, Blue, Yellow
}

[System.Serializable]
public class EnemyData
{
    ///<summary>敵のプレハブ</summary>
    public GameObject prefab;
    ///<summary>出現確率</summary>
    public int weight;
    ///<summary>出現開始時間</summary>
    public float spawnStartTime;
    ///<summary></summary>
    public EnemyColor color;
    ///<summary>速度</summary>
    public float growSpeed = 1f;
}