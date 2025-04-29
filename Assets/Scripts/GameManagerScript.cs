using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject LoseCanvas;
    public GameObject WinCanvas;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        LoseCanvas.SetActive(false);
        WinCanvas.SetActive(false);
    }

}
