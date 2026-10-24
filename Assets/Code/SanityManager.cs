using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SanityManager : MonoBehaviour
{
    public Slider sanityBar;

    public float sanity = 100f;
    public float maxSanity = 100f;

    public float recoverSpeed = 10f;

    public bool shadowActive = false;

    void Start()
    {
        sanityBar.maxValue = maxSanity;
        sanityBar.value = sanity;
    }

    void Update()
    {
        if (!shadowActive && sanity < maxSanity)
        {
            sanity += recoverSpeed * Time.deltaTime;

            if (sanity > maxSanity)
            {
                sanity = maxSanity;
            }
        }

        sanityBar.value = sanity;

        if (sanity <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void UseShadow()
    {
        sanity -= 20f;

        if (sanity < 0)
        {
            sanity = 0;
        }

        shadowActive = true;
    }

    public void ReturnShadow()
    {
        shadowActive = false;
    }
}