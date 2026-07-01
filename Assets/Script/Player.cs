using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.W))
        //{
           
        //}
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
            foreach (Enemy enemy in enemies)
            {
                enemy.TakeDamage(EnemyColor.Red);
            }
        }

        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            DestroyObjectsWithTag("Bule");
        }

        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            DestroyObjectsWithTag("Yellow");
        }
    }

    void DestroyObjectsWithTag(string tagName)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tagName);

        //Debug.Log(tagName + " : " + objects.Length);

        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
    }
}
