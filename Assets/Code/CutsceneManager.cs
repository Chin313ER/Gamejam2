using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutsceneManager : MonoBehaviour
{
    public Image cutsceneImage;
    public Sprite[] frames;
    public Image fadeImage;

    int currentFrame = 0;

    void Start()
    {
        cutsceneImage.sprite = frames[0];

        // เริ่มด้วยจอดำ
        fadeImage.color = new Color(0, 0, 0, 1);

        StartCoroutine(FadeInFirstImage());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentFrame++;

            if (currentFrame < frames.Length)
            {
                cutsceneImage.sprite = frames[currentFrame];
            }
            else
            {
                SceneManager.LoadScene("StartGame");
            }
        }
    }

    IEnumerator FadeInFirstImage()
    {
        yield return new WaitForSeconds(1f);

        for (float a = 1; a >= 0; a -= Time.deltaTime)
        {
            fadeImage.color = new Color(0, 0, 0, a);
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 0);
    }
}