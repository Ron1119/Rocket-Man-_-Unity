﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Oscillator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector;
    [Range(0, 1)] [SerializeField] float movementFactor;

    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}