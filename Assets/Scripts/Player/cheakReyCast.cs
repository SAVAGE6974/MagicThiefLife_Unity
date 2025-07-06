using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class cheakReyCast : MonoBehaviour
{
    [SerializeField] private OpenStorePannel openStorePannel;
    
    public float maxDistance = 20f;

    private void Update()
    {
        
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            if (hit.collider.CompareTag("store"))
            {
                openStorePannel.OpenStore();
            }
        }
    }
}
