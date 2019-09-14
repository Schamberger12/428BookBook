using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{

    public AudioSource audioData;

    private void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

}
