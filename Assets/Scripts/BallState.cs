using UnityEngine;

public class BallState : MonoBehaviour
{
    public static BallState instance;


    [HideInInspector] public Rigidbody rb;

    Vector3 savedVelocity;

    CameraControl rotateCameraPoint;

    void Awake()
    {
        instance = this;

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!CameraControl.instance.rotateBool)
        {
            rb.WakeUp();
        }
        else
        {
            rb.Sleep();
        }
    }
}
