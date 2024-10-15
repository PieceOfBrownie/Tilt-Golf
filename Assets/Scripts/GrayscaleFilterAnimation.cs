using UnityEngine;
using UnityEngine.UI;

public class IncreaseAlpha : MonoBehaviour
{
    Image image;

    CameraControl rotateCameraPoint;

    [SerializeField] float alphaChangeSpeed = 0.14902f;

    float minAlpha = 0.0f;
    float maxAlpha = 0.1490196f;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void LateUpdate()
    {
        if (CameraControl.instance.rotateBool)
        {
            Color currentColor = image.color;
            float newAlpha = Mathf.Clamp(currentColor.a + alphaChangeSpeed * Time.deltaTime, minAlpha, maxAlpha);
            image.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
        }
        else
        {
            Color currentColor = image.color;
            float newAlpha = Mathf.Clamp(currentColor.a - alphaChangeSpeed * Time.deltaTime, minAlpha, maxAlpha);
            image.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
        }
    }
}

