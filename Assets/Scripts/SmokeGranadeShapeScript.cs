using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeGranadeShapeScript : MonoBehaviour
{
    //============================================ Variables ===============================================
    public float durationBeforeExplosion = 1f;
    public GameObject smokeEffect;

    //============================================ Deploy Smoke Functions ===============================================
    // Start timer for spawning smoke after object collides
    void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(SpawnSmoke());
    }

    // After timer runs out self-destroy and spawn smoke effect
    IEnumerator SpawnSmoke()
    {
        yield return new WaitForSeconds(durationBeforeExplosion);
        Instantiate(smokeEffect, transform.position , Quaternion.identity);
        Destroy(gameObject);
    }
}
