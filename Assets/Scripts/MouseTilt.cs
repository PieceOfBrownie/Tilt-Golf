using UnityEngine;

public class MouseTilt : MonoBehaviour
{
    float xRotation,
          yRotation,
          zRotation;

    [Header("Controls")]
    [SerializeField] float speed          = 100f;
    [SerializeField] float clampThreshold = 90f;

    void Start()
    {
        xRotation = yRotation = zRotation = 0f;
    }

    void Update()
    {
        if (!CameraControl.instance.rotateBool)
        {
            //Player's tilt controls
            float tiltHorizontal = Input.GetAxisRaw("Mouse X") * speed * Time.deltaTime;
            float tiltVertical   = Input.GetAxisRaw("Mouse Y") * speed * Time.deltaTime;

            //Player's stage rotation threshold
            xRotation = Mathf.Clamp(xRotation + tiltVertical,   -clampThreshold, clampThreshold);
            zRotation = Mathf.Clamp(zRotation + tiltHorizontal, -clampThreshold, clampThreshold);

            transform.localRotation = Quaternion.Euler(xRotation, yRotation, -zRotation);
        }
    }
}
