using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class flickerlight : MonoBehaviour
{
    public Light2D lamp;

    void Start()
    {
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            lamp.intensity = Random.Range(0.2f, 1f);

            yield return new WaitForSeconds(
                Random.Range(0.05f, 0.2f)
            );
        }
    }
}