using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class materialIntensityChanger : MonoBehaviour
{



    [SerializeField] float speed;
    [SerializeField] float StartRange;
    [SerializeField] float EndRange;
    [SerializeField] Material material;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //material.SetFloat("_EmissiveIntensity", Mathf.PingPong(Time.time * speed, range));
        
        material.color = new Vector4(
            Mathf.PingPong(Time.time * speed, EndRange - StartRange) + StartRange,
            Mathf.PingPong(Time.time * speed, EndRange - StartRange) + StartRange, 
            Mathf.PingPong(Time.time * speed, EndRange - StartRange) + StartRange, 1);
    }
}
