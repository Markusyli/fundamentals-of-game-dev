using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform mainCamera;

    private void LateUpdate()
    {
        // If camera is set, turn canvas into billboard
        if (mainCamera)
            transform.LookAt(transform.position + mainCamera.forward);
    }
}
