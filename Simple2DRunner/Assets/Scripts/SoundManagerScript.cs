using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip jump, dead, win;
    public static AudioSource source;
    private static int i;

    void Start()
    {
        i = 1;
        
        jump = Resources.Load<AudioClip>("Jump");
        dead = Resources.Load<AudioClip>("Dead");
        win = Resources.Load<AudioClip>("Win");

        source = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "jump":
                source.PlayOneShot(jump);
                break;
            case "dead":
                source.PlayOneShot(dead);
                break;
            case "win":
                {
                    source.clip = win;
                    if (!source.isPlaying && i == 1)
                    {
                        source.Play();
                        i--;
                    }
                    break;
                }
        }
    }

}
