using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // ============================================================== Variables =====================================================
    public LayerMask defaultLayer;
    public LayerMask xRayLayer;

    private bool xRayActive;

    // Agression Level
    public float prisonAwarenessLevel;

    // ============================================================== X-Ray Variables =====================================================
    // Applies the change of visibility layer to all the children of the component
    void SetLayerAllChildren(Transform root, int layer)
    {
        var children = root.GetComponentsInChildren<Transform>(includeInactive: true);

        foreach (var child in children)
        {
            child.gameObject.layer = layer;
        }
    }

    // Changes the rendering layer, causing enemies to be visible through the walls
    public void activateXRay()
    {
        // De-Activate X-Ray
        if (xRayActive)
        {
            xRayActive = !xRayActive;
            int layerNum = (int)Mathf.Log(defaultLayer.value, 2);
            gameObject.layer = layerNum;

            if (transform.childCount > 0)
            {
                SetLayerAllChildren(transform, layerNum);
            }
        }
        // Activate X-Ray
        else
        {
            xRayActive = !xRayActive;
            int layerNum = (int)Mathf.Log(xRayLayer.value, 2);
            gameObject.layer = layerNum;

            if (transform.childCount > 0)
            {
                SetLayerAllChildren(transform, layerNum);
            }
        }

    }
}
