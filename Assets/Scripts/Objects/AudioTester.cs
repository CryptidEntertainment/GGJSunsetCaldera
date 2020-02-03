using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTester : MonoBehaviour
{
    AudioSource audSource;

    void Awake()
    {
        if (GetComponent<AudioSource>() != null)
        {
            audSource = GetComponent<AudioSource>();
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if(audSource!=null)
        {
            Debug.Log("Doing the audio thing.");
            //audSource.Play();
        }
    }
}
