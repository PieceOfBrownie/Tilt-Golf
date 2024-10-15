using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraAnimation : MonoBehaviour
{
    public static CameraAnimation instance;


    [SerializeField] float minRotationSpeed      = 5.0f;     //Min speed (degrees per second)
    [SerializeField] float maxRotationSpeed      = 30.0f;    // Max speed (degrees per second)
    [SerializeField] float rotationAcceleration  = 10.0f;    // Acceleration (degrees per second per second)
    [SerializeField] public float finishRotation = 25.0f;    // Target rotation on the X-axis

    [SerializeField] GameObject golfBall;

    float currentSpeed = 0f;    // Current rotation speed

    void Awake()
    {
        instance = this;
    }

    void OnEnable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        StartCoroutine(AnimateCamera(transform.localRotation));
    }

    IEnumerator AnimateCamera(Quaternion startRotation)
    {
        yield return new WaitForSeconds(0.5f);

        Quaternion targetRotation = Quaternion.Euler(finishRotation, 0f, 0f);

        while (Quaternion.Angle(transform.localRotation, targetRotation) > 0.01f)
        {
            float remainingAngle = Quaternion.Angle(transform.localRotation, targetRotation);

            currentSpeed = remainingAngle < 10f 
                ? Mathf.Max(currentSpeed - rotationAcceleration * Time.deltaTime, minRotationSpeed)     // Slow down
                : Mathf.Min(currentSpeed + rotationAcceleration * Time.deltaTime, maxRotationSpeed);    // Speed up

            // Rotate towards the target
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, currentSpeed * Time.deltaTime);
            yield return null;
        }
        transform.localRotation = targetRotation;


        golfBall.SetActive(true);
        if (ChangeControl.instance.isFinished == true)
            if (SceneManager.GetActiveScene().buildIndex >= 10)
                StartCoroutine(SceneControls.instance.FadeBlackBackground(SceneControls.instance.background));
            else
                StartCoroutine(SceneControls.instance.FadeWhiteBackground(SceneControls.instance.background, SceneManager.GetActiveScene().buildIndex + 1));
        else
            enabled = false;
    }
}
