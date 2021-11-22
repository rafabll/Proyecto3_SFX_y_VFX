using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 30;

    private PlayerController playerControllerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").
            GetComponent<PlayerController>();
    }

    
    void Update()
    {
        if (!playerControllerScript.gameOver)
        {
            transform.Translate(Vector3.left * 
                                speed * Time.deltaTime);
        }

        if (transform.position.y < -1)
        {
            Destroy(gameObject);
        }
    }
}
