using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video; 
using Vuforia;

public class ButtonBehavior : MonoBehaviour, IVirtualButtonEventHandler, ITrackableEventHandler
{
    [SerializeField]
    int currentState;

    [SerializeField]
    VideoPlayer gregVideo;

    [SerializeField]
    VideoPlayer katyaVideo;

    [SerializeField]
    GameObject gregVideoSurface;

    [SerializeField]
    GameObject katyaVideoSurface;

    [SerializeField]
    GameObject bookInfo;

    [SerializeField]
    GameObject gregReviews;

    [SerializeField]
    GameObject katyaReviews;

    private TrackableBehaviour mTrackableBehaviour;



    public GameObject vButtonSpeak;

    bool isPlaying;

    private void Awake()
    {
        gregVideoSurface = GameObject.Find("GregPlane");
        katyaVideoSurface = GameObject.Find("KatyaPlane");

        gregVideo = gregVideoSurface.GetComponent<VideoPlayer>();
        katyaVideo = katyaVideoSurface.GetComponent<VideoPlayer>();

        vButtonSpeak = GameObject.Find("ArBackButton");
        vButtonSpeak.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }

        isPlaying = false;


    }

    private void Start()
    {
        BookInfoActive();
    }

    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if ((currentState == 4) &&(newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED))
        {
            // Play audio when target is found
            gregVideo.Play();
        }
        else
        {
            // Stop audio when target is lost
            gregVideo.Stop();
        }

        if ((currentState == 5) && (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED))
        {
            // Play audio when target is found
            katyaVideo.Play();
        }
        else
        {
            // Stop audio when target is lost
            katyaVideo.Stop();
        }
    }



    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        currentState++;
        if (currentState > 5)
            currentState = 1;

        switch (currentState)
        {
            case 1:
                BookInfoActive();
                break;
            case 2:
                GregReivewActive();
                break;
            case 3:
                KatyaReivewActive();
                break;
            case 4:
                GregVideoActive();
                break;
            case 5:
                KatyaVideoActive();
                break;
            default:
                Debug.Log($"{currentState} does not exist!");
                break;
        }
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {

    }

    void BookInfoActive()
    {
        gregVideoSurface.SetActive(false);
        katyaVideoSurface.SetActive(false);
        gregReviews.SetActive(false);
        katyaReviews.SetActive(false);
        bookInfo.SetActive(true);
    }

    void GregReivewActive()
    {
        gregVideoSurface.SetActive(false);
        katyaVideoSurface.SetActive(false);
        gregReviews.SetActive(true);
        katyaReviews.SetActive(false);
        bookInfo.SetActive(false);
    }

    void KatyaReivewActive()
    {
        gregVideoSurface.SetActive(false);
        katyaVideoSurface.SetActive(false);
        gregReviews.SetActive(false);
        katyaReviews.SetActive(true);
        bookInfo.SetActive(false);
    }

    void GregVideoActive()
    {
        gregVideoSurface.SetActive(true);
        katyaVideoSurface.SetActive(false);
        gregReviews.SetActive(false);
        katyaReviews.SetActive(false);
        bookInfo.SetActive(false);

        gregVideo.Play();
    }

    void KatyaVideoActive()
    {
        gregVideoSurface.SetActive(false);
        katyaVideoSurface.SetActive(true);
        gregReviews.SetActive(false);
        katyaReviews.SetActive(false);
        bookInfo.SetActive(false);

        katyaVideo.Play();
    }
}