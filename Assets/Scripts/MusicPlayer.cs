using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Scott
{
    public class MusicPlayer : MonoBehaviour
    {

        private int audSwitcher = 1;
        public AudioSource aud1;
        public AudioSource aud2;
        public AudioClip setClip;
        private float vol1;
        private float vol2;
        public float crossfadeTime=3;
        private float musicTriggerTime;
        private bool fading;

        void Update()
        {
            if(fading)
            {
                if (audSwitcher == 1)
                {
                    if (vol1 < 1)
                    {
                        vol1 = (Time.time - musicTriggerTime) / crossfadeTime;
                        aud1.volume = vol1;
                        vol2 = 1 - ((Time.time - musicTriggerTime) / crossfadeTime);
                        aud2.volume = vol2;
                    }
                    else if (vol1 >= 1)
                    {
                        fading = false;
                        aud2.gameObject.SetActive(false);
                    }
                    else
                    {
                        Debug.Log("Music switch broke somehow1");
                    }
                }
                else if (audSwitcher == 2)
                {
                    if (vol2 < 1)
                    {
                        vol2 = (Time.time - musicTriggerTime) / crossfadeTime;
                        aud2.volume = vol2;
                        vol1 = 1 - ((Time.time - musicTriggerTime) / crossfadeTime);
                        aud1.volume = vol1;
                    }
                    else if (vol2 >= 1)
                    {
                        fading = false;
                        aud1.gameObject.SetActive(false);
                    }
                    else
                    {
                        Debug.Log("Music switch broke somehow2");
                    }
                }
            }
        }

        
        public void OnTriggerEnter()
        {
            if (aud2.gameObject.activeInHierarchy)
            {
                if (aud2.clip == setClip)
                    return;
                else
                {
                    fading = true;
                    audSwitcher = 1;
                    musicTriggerTime = Time.time;
                    aud1.gameObject.SetActive(true);
                    aud1.clip = setClip;
                    aud1.volume = 0f;
                    aud2.volume = 0f;
                    vol1 = 0f;
                    vol2 = 0f;
                    aud1.Play();
                }
            }
            else if (aud1.gameObject.activeInHierarchy)
            {
                if (aud1.clip == setClip)
                    return;
                else
                {
                    fading = true;
                    audSwitcher = 2;
                    musicTriggerTime = Time.time;
                    aud2.gameObject.SetActive(true);
                    aud2.clip = setClip;
                    aud2.volume = 0f;
                    aud1.volume = 0f;
                    vol1 = 0f;
                    vol2 = 0f;
                    aud2.Play();
                }
            }
        }
    }
}