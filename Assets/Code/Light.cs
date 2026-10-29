using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Light : MonoBehaviour
{
    public Light2D light2D;

    void Update()
    {
        light2D.intensity = Random.Range(0.8f, 1.2f);
    }
}