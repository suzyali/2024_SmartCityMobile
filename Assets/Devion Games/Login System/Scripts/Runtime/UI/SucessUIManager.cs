using UnityEngine;
public class SucessUIManager : MonoBehaviour
{
    public static SucessUIManager Instance { get; private set; }
    public GameObject sucessUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void DisplaySucessUI()
    {
        sucessUI.SetActive(true);
        Invoke("DisableSucessUI", 3f);
    }

    void DisableSucessUI()
    {
        sucessUI.SetActive(false);
    }
}
