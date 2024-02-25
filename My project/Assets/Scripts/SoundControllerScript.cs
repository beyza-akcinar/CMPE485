using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControllerScript : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (isPlaying)
            {
                audioSource.Stop();
            }
            else
            {
                audioSource.Play();
            }
            isPlaying = !isPlaying;
        }
    }
}
