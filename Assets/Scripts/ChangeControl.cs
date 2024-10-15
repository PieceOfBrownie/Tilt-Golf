using UnityEngine;
using System.Collections;

public class ChangeControl : MonoBehaviour
{
    public static ChangeControl instance;

    GameObject lastContact,
               currentContact;

    [Header("Change Speeds")]
    [SerializeField] float courseRotation = 1.0f;
    [SerializeField] float cameraPosition = 1.0f;


    Vector3 nextPosition;

    bool isCameraMoving, isCourseRotating = false;
    public bool isFinished = false;


    void Awake()
    {
        instance = this;
    }

    void OnCollisionExit(Collision collision)
    {;
        if (collision.gameObject.tag == "Plane")
        {
            lastContact = collision.transform.root.gameObject;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        currentContact = collider.transform.root.gameObject;
        
        if (collider.CompareTag("Finish"))
        {
            currentContact.GetComponent<MouseTilt>().enabled = false;

            CameraControl.instance.enabled = false;
            
            BallState.instance.rb.Sleep();
            BallState.instance.enabled = false;

            isFinished = true;

            CameraAnimation.instance.finishRotation = -90f;
            CameraAnimation.instance.enabled = true;
            return;
        }

        else if (collider.CompareTag("Plane"))
        {
            collider.enabled = false;

            if (CameraControl.instance.gameObject.transform.position == currentContact.transform.position)
            {
                currentContact.GetComponent<MouseTilt>().enabled = true;
                
                CameraControl.instance.enabled = true;
                return;
            }

            lastContact.GetComponent<MouseTilt>().enabled = false;

            if (!isCameraMoving)
            {
                nextPosition = currentContact.transform.position;
                StartCoroutine(MoveCamera(CameraControl.instance.gameObject.transform.position));
            }

            if (!isCourseRotating)
                StartCoroutine(ResetCourseRotation(lastContact.transform.rotation));

        }
    }

    IEnumerator MoveCamera(Vector3 startPosition)
    {
        isCameraMoving = true;
        CameraControl.instance.enabled = false;

        float time = 0f;
        while (time <= 1f)
        {
            time += cameraPosition * Time.deltaTime;
            CameraControl.instance.gameObject.transform.position = Vector3.Lerp(startPosition, nextPosition, time);
            yield return null;
        }

        isCameraMoving = false;
        CameraControl.instance.enabled = true;
    }

    IEnumerator ResetCourseRotation(Quaternion startRotation)
    {
        isCourseRotating = true;

        float time = 0f;
        while (time <= 1f)
        {
            time += courseRotation * Time.deltaTime;
            lastContact.transform.rotation = Quaternion.Slerp(startRotation, Quaternion.identity, time);
            yield return null;
        }

        isCourseRotating = false;

        currentContact.GetComponent<MouseTilt>().enabled = true;
    }
}