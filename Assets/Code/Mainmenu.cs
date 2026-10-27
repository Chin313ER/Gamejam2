using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Play Button Clicked");
        SceneManager.LoadScene("cutscene1");
    }
}