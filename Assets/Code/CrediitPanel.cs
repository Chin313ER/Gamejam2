using UnityEngine;

public class CrediitPanel : MonoBehaviour
{
    public GameObject Credit;

    public void OpenPanel()
    {
        Credit.SetActive(true);
    }

    public void ClosePanel()
    {
        Credit.SetActive(false);
    }
}

