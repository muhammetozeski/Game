using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamTest : MonoBehaviour
{

    [SerializeField] GameObject cube;
    int a = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Instantiate(cube);
        print("" + a++ + " cube instantiated");
    }
}
