using UnityEngine;
using UnityEngine.Experimental.Rendering;

[RequireComponent(typeof(BoxCollider2D))]
public class closer : MonoBehaviour
{
    [Header("closeSpeed")]
    public float growSpeed = 1f;
    [Header("maxSize")]
    public Vector3 maxSize;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var tr = GetComponent<Transform>();
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
