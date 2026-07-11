using UnityEngine;
using UnityEngine.Experimental.Rendering;

[RequireComponent(typeof(BoxCollider2D))]
public class closer : MonoBehaviour
{
    [Header("closeSpeed")]
    public float growSpeed = 1f;
    [Header("maxSize")]
    public Vector3 maxSize;
    [Header("Damage")]
    public float damageInterval = 3f;
    private PlayerHealth playerHp;

    private float damageTimer = 0f;

    private bool reachedMaxSize = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHp = FindAnyObjectByType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x <= maxSize.x)
        {
            transform.localScale += Vector3.one * growSpeed * Time.deltaTime;
        }
        else
        {
            reachedMaxSize = true;
        }

        // 最大サイズ後の処理
        if (reachedMaxSize)
        {
            damageTimer -= Time.deltaTime;


            if (damageTimer <= 0)
            {
                playerHp.TakeDamage(1);

                damageTimer = damageInterval;
            }
        }
    }
}
