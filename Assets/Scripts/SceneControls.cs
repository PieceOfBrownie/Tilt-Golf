using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneControls : MonoBehaviour
{
    public static SceneControls instance;

    public Image background;

    [SerializeField] float alphaChangeSpeed = 1f;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        background = GameObject.Find("White/Black Background").GetComponent<Image>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            StartCoroutine(FadeWhiteBackground(background, SceneManager.GetActiveScene().buildIndex));
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            StartCoroutine(FadeBlackBackground(background));
        }
    }


    public IEnumerator FadeWhiteBackground(Image whiteBackground, int sceneIndex)
    {
        whiteBackground.color = new Color(1, 1, 1, 0);

        float time = 0f;
        while (time <= 1f)
        {
            time += alphaChangeSpeed * Time.deltaTime;
            Color currentColor = whiteBackground.color;
            float newAlpha = Mathf.Clamp(currentColor.a + alphaChangeSpeed * Time.deltaTime, 0.0f, 1.0f);
            whiteBackground.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
            yield return null;
        }

        SceneManager.LoadScene(sceneIndex);
    }

    public IEnumerator FadeBlackBackground(Image blackBackground)
    {
        blackBackground.color = new Color(0, 0, 0, 0);

        float time = 0f;
        while (time <= 1f)
        {
            time += alphaChangeSpeed * Time.deltaTime;
            Color currentColor = blackBackground.color;
            float newAlpha = Mathf.Clamp(currentColor.a + alphaChangeSpeed * Time.deltaTime, 0.0f, 1.0f);
            blackBackground.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
            yield return null;
        }

        SceneManager.LoadScene(0);
    }
}
