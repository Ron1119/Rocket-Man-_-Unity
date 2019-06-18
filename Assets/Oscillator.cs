using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector = new Vector3 (20f, 0f, 0f);

    [SerializeField] float period = 2f;
    float movementFactor;

    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;  // get the initial position

    }

    // Update is called once per frame
    void Update()
    {
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau); // -1 to 1
        movementFactor = rawSinWave/2 + 0.5f; // 0 to 1
        Vector3 offset = movementVector * movementFactor;
        transform.position = startPos + offset;
    }
}
