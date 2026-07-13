using UnityEngine;

public enum EnemyColor
{
    Red, Blue, Yellow
}
// Inspectorに表示できるクラス
[System.Serializable]
public class EnemyData
{
    ///<summary>敵のプレハブ</summary>
    public GameObject prefab;
    ///<summary>出現確率</summary>
    public int weight;
    ///<summary>出現開始時間</summary>
    public float spawnStartTime;
    ///<summary>色の分類enumでRed,Blue,Yellow</summary>
    public EnemyColor color;
    ///<summary>拡大速度</summary>
    public float growSpeed = 1f;
    ///<summary>倒したときのスコア</summary>
    public int score = 100;
}