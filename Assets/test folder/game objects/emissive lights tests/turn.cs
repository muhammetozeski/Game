using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turn : MonoBehaviour
{
    [SerializeField] float speed = 3;
    [SerializeField] float acceleration = 1;
    [SerializeField] Transform[] lights;
    [SerializeField] Transform center;
    float currentSpeed;
    float to;
    List<Material> materials = new List<Material>();
    List<Light>lightOfLights = new List<Light>();
     // Start is called before the first frame update
    void Start()
    {
        currentSpeed = speed;
        to = -speed;
        foreach (Transform light in lights)
        {
            Material mat =  light.GetComponent<Renderer>().material;
            light.GetComponent<Renderer>().material = new Material(mat.shader);
            materials.Add (light.GetComponent<Renderer>().material);
        }
        foreach (Transform light in lights)
        {
            lightOfLights.Add(light.GetComponent<Light>());
        }
        foreach (Material mat in materials)
        {
            mat.EnableKeyword("_EMISSION");
            mat.EnableKeyword("_EmissionMap");
            mat.EnableKeyword("_EmissionColor");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSpeed < -speed + 0.5f)
            to = speed;
        else if (currentSpeed > speed - 0.5f)
            to = -speed;

        currentSpeed = Mathf.Lerp(currentSpeed, to, acceleration);
        foreach (Transform light in lights)
        {
            light.Rotate(Vector3.right * currentSpeed);
        }
        center.Rotate(Vector3.right * currentSpeed);

        for (int i = 0; i < lights.Length; i++)
        {
            Color color = Random.ColorHSV();
            materials[i].SetColor("_EmissionColor", color);
            lightOfLights[i].color = color;
        }
    }
}
