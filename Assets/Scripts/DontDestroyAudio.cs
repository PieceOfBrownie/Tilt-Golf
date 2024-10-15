using UnityEngine;

public class DontDestroyAudio : MonoBehaviour
{
    static DontDestroyAudio instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
}
