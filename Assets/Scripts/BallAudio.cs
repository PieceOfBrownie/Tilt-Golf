using UnityEngine;

public class BallSound : MonoBehaviour
{
    AudioSource dropAudio;
    AudioSource rollAudio;
    AudioSource cupHoleAudio;

    Rigidbody rb;
    bool isRolling,
         playOnce;

    [SerializeField] float minimumImpactVelocity  = 1.0f;
    [SerializeField] float minimumRollingVelocity = 1.0f;
    [SerializeField] float rollingCheckInterval   = 1.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        playOnce = false;

        AudioSource[] audioSources = GetComponents<AudioSource>();
        if (audioSources.Length >= 2)
        {
            dropAudio    = audioSources[0]; //The AudioSource for "Golf Ball Dropping"
            rollAudio    = audioSources[1]; //The AudioSource for "Golf Ball Rolling"
            cupHoleAudio = audioSources[2]; //The AudioSource for "Golf Ball Falling into the Cup Hole"
        }

        InvokeRepeating(nameof(RollCheck), rollingCheckInterval, rollingCheckInterval); //Call RollCheck periodically to check if the ball rolls
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > minimumImpactVelocity)
            if (!dropAudio.isPlaying)
                dropAudio.Play();
    }

    void Update()
    {
        if (ChangeControl.instance.isFinished == true)
            if (!cupHoleAudio.isPlaying && !playOnce)
            {
                cupHoleAudio.Play();
                playOnce = true;
            }
    }


    void RollCheck()
    {
        if (rb.velocity.magnitude > minimumRollingVelocity && rb.angularVelocity.magnitude > minimumRollingVelocity)
        {
            if (!rollAudio.isPlaying)
                rollAudio.Play();
            isRolling = true;
        }
        else
        {
            if (isRolling && rollAudio.isPlaying)
                rollAudio.Stop();
            isRolling = false;
        }
    }
}

