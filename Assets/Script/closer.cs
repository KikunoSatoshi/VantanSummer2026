using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class closer : MonoBehaviour
{
    [Header("closeSpeed")]
    public float growSpeed = 1f;
    [Header("maxSize")]
    public Vector3 maxSize;

    private PlayerHealth playerHp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHp = FindFirstObjectByType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x <= maxSize.x)
        {
            transform.localScale += Vector3.one * growSpeed * Time.deltaTime;
        }
    }
}
