using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinBoxScript : MonoBehaviour
{
    public GameObject WinCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Win");
            WinCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
