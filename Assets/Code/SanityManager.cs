using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SanityManager : MonoBehaviour
{
    public Image sanityBar;

    public float sanity = 100f;
    public float maxSanity = 100f;

    public float recoverSpeed = 10f;

    public bool shadowActive = false;

    void Start()
    {
        UpdateSanityBar();
    }

    void Update()
    {
        if (!shadowActive && sanity < maxSanity)
        {
            sanity += recoverSpeed * Time.deltaTime;
            sanity = Mathf.Clamp(sanity, 0, maxSanity);
        }

        UpdateSanityBar();

        if (sanity <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void UseShadow()
    {
        sanity -= 20f;
        sanity = Mathf.Clamp(sanity, 0, maxSanity);

        shadowActive = true;
    }

    public void ReturnShadow()
    {
        shadowActive = false;
    }

    void UpdateSanityBar()
    {
        sanityBar.fillAmount = sanity / maxSanity;
    }
}