using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia; 

public class SpokesPerson : MonoBehaviour, IVirtualButtonEventHandler
{

    public AudioSource audioData;

    public GameObject piggy;

    public GameObject vButtonSpeak;

    bool isPlaying; 

    private void Awake()
    {
        audioData = piggy.GetComponent<AudioSource>();
        piggy = GameObject.Find("SpokesPerson");
        vButtonSpeak = GameObject.Find("ARButton");
        vButtonSpeak.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);

        isPlaying = false;


    }

    private void Start()
    {
        piggy.SetActive(false);
    }

    private void Update()
    {
        if (isPlaying && !audioData.isPlaying)
        {
            isPlaying = false;
            audioData.Stop();
            piggy.SetActive(false); 
        }
    }

    /// <summary>
    /// Called when the virtual button has just been pressed:
    /// </summary>
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {


        if (!isPlaying)
        {
            isPlaying = true;
            piggy.SetActive(true); 
            Debug.Log("button Pressed");
            audioData.Play();
            if (audioData.isPlaying)
                Debug.Log("Sound Playing"); 

        }
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {

    }
}

