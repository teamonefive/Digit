﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnStart : MonoBehaviour
{
    public GameObject self;

    // Start is called before the first frame update
    void Start()
    {
        self.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
