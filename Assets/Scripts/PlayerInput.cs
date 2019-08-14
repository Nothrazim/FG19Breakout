using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Camera playerCamera; //Default value null

    private void Awake()
    {
        playerCamera = Camera.main;
    }

    void Start()
    {
        Debug.Log("Initializing player input");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
