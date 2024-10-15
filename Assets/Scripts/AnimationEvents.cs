using System;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public void EnableScript()
    {
        gameObject.GetComponent<SceneControls>().enabled = true;
    }

    public void DisableImage()
    {
        gameObject.GetComponent<UnityEngine.UI.Image>().enabled = false;
    }

    public void DisableAnimator()
    {
        gameObject.GetComponent<Animator>().enabled = false;
    }

    public void DoNothing() 
    {
        Debug.Log("This is a placeholder method in case you do not want to attach an Animation Event.");
    }
}
