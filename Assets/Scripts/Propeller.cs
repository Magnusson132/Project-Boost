using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{

    [SerializeField] float xAngle = 0;
    // [SerializeField] float yAngle = 0;
    [SerializeField] float zAngle = 0;
    [SerializeField] float spinnerRotation = 0;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(xAngle, spinnerRotation *Time.deltaTime, zAngle);
    }
}
