using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXButtons : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip onClick;
    public AudioClip onHover;

    public void HoverSound()
    {
        if(onHover != null || audioSource != null)
        {
            audioSource.PlayOneShot(onHover);
        }
    }

    public void ClickSound()
    {
        if(onClick != null|| audioSource !=null)
        {
            audioSource.PlayOneShot(onClick);
        }
    }
}
