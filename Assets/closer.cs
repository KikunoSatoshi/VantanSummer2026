using UnityEngine;
using UnityEngine.Experimental.Rendering;

[RequireComponent(typeof(BoxCollider2D))]
public class closer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        tr.transform.localScale = Vector3.one;
    }
}
