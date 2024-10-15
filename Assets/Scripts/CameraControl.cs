using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl instance;


    [SerializeField] float speed = 100f;


    [HideInInspector] public bool rotateBool;

    float rotationSpeed;

    void Awake()
    {
        instance = this;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            rotateBool    = true;
            rotationSpeed = speed * Time.deltaTime;
        }
        else if (Input.GetMouseButton(1))
        {
            rotateBool    = true;
            rotationSpeed = -speed * Time.deltaTime;
        }
        else
        {
            rotateBool    = false;
            rotationSpeed = 0f;
        }

        transform.Rotate(0, rotationSpeed, 0);
    }
}
