using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CanvasManagerScript : MonoBehaviour
{
    public GameObject GameplayCanvas;
    public GameObject StartCanvas;
    public GameObject LoseCanvas;
    public GameObject WinCanvas;
    public bool CursorOn;
    // Start is called before the first frame update
    void Start()
    {
        StartCanvas.SetActive(true);
        GameplayCanvas.SetActive(false);
        LoseCanvas.SetActive(false);
        WinCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            
        }
    }

    public void PlayButton()
    {
        StartCanvas.SetActive(false);
        GameplayCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitButton()
    {
        EditorApplication.ExitPlaymode();
    }

    public void OpenLoseScreen()
    {
        LoseCanvas.SetActive(true);
        GameplayCanvas.SetActive(false);
    }

    public void OpenWinScreen()
    {
        WinCanvas.SetActive(true);
        GameplayCanvas.SetActive(false);
    }
}
