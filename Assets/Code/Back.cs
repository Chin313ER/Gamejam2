using UnityEngine;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour
{
    public void restart()
    {
        Debug.Log("Play Button Clicked");
        SceneManager.LoadScene("MainSceneNIG");
    }
    public void Backmenu()
    {
        Debug.Log("Play Button Clicked");
        SceneManager.LoadScene("StartGame");
    }
}