﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public GameObject model;

    // Make these into constants of another class later...
    float stageWidth = 1500f;
    float stageHeight = 950f;
    float moveSpeed = 10f;
    float precisionSpeed = 5f;
    float shipTilt = 0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // Get input from controller.
        float hSpeed = Mathf.Round(Input.GetAxisRaw("Horizontal"));
        float vSpeed = Mathf.Round(Input.GetAxisRaw("Vertical"));

        // Slightly slower when moving diagonally.
        if (hSpeed != 0 && vSpeed != 0){
            hSpeed *= 0.7f;
            vSpeed *= 0.7f;
        }

        // Adjust for precision mode.
        if (Input.GetButton("Precision"))
        {
            hSpeed *= precisionSpeed;
            vSpeed *= precisionSpeed;
        }
        else
        {
            hSpeed *= moveSpeed;
            vSpeed *= moveSpeed;
        }

        shipTilt = shipTilt * 0.8f - (2 * hSpeed) * 0.2f;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x + hSpeed, -stageWidth / 2, stageWidth / 2),
            Mathf.Clamp(transform.position.y + vSpeed, -stageHeight / 2, stageHeight / 2),
            0
        );

        model.transform.rotation = Quaternion.Euler(0f, shipTilt, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit!");
    }
}