using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEffectScript : MonoBehaviour
{
    //============================================ Variables ===============================================
    public float smokeSize = 2f;
    public float durationTime = 5f;
    public AudioSource sound;

    //============================================ Start ===============================================
    // Sets the size of the smoke bomb and starts the self destruct timer
    void Start()
    {
        transform.localScale = new Vector3(smokeSize, smokeSize, smokeSize);
        StartCoroutine(SelfDestruct());
        sound.Play();
    }

    //============================================ Self-Destruct Functions ===============================================
    // Destroys self after timer runs out
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(durationTime);
        Destroy(gameObject);
    }
}
