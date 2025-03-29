using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public LayerMask defaultLayer;
    public LayerMask xRayLayer;

    private bool xRayActive;

    void SetLayerAllChildren(Transform root, int layer)
    {
        var children = root.GetComponentsInChildren<Transform>(includeInactive: true);

        foreach (var child in children)
        {
            child.gameObject.layer = layer;
        }
    }

    public void activateXRay()
    {
        if(xRayActive)
        {
            xRayActive = !xRayActive;
            int layerNum = (int)Mathf.Log(defaultLayer.value, 2);
            gameObject.layer = layerNum;

            if(transform.childCount > 0)
            {
                SetLayerAllChildren(transform, layerNum);
            }
        }
        else
        {
            xRayActive = !xRayActive;
            int layerNum = (int)Mathf.Log(xRayLayer.value, 2);
            gameObject.layer = layerNum;

            if(transform.childCount > 0)
            {
                SetLayerAllChildren(transform, layerNum);
            }
        }
        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
