﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContril : MonoBehaviour
{
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, 0, -10f);
    }
}
