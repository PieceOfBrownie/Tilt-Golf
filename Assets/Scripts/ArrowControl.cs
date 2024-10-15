using UnityEngine;

public class ArrowControl : MonoBehaviour
{
    void LateUpdate()
    {
        Vector3 cameraLocalRotation = CameraControl.instance.transform.localEulerAngles;
        
        transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, cameraLocalRotation.y);
    }
}
