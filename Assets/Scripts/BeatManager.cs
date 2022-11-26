using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour
{
    private static BeatManager instance;
    public static BeatManager Instance { get { return instance; } }

    [SerializeField] private float beatInterval = 1.0f;
    public float BeatInterval { get { return beatInterval; }}
    [SerializeField] private float beatOffset = 0.5f;
    public float BeatOffset { get { return beatOffset; }}
    [SerializeField] private float tollerance = 0.1f;

    [SerializeField] private AudioClip currentStageMusic;
    [SerializeField] private GameObject beatTestObject;

    private AudioSource aud;

    private float timer = 0;
    public float Timer { get { return timer; } }
    private bool isBeatCountStarted = false;
    public bool IsBeatCountStarted { get { return isBeatCountStarted; } }

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void StartBeatSeq()
    {
        isBeatCountStarted = true;
        aud.clip = currentStageMusic;
        aud.Play();
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        if (isBeatCountStarted)
        {
            timer += Time.fixedDeltaTime;
        }

        if (beatTestObject != null)
        {
            if (IsCurrentInputValidBeat())
            {
                if(!beatTestObject.activeInHierarchy)
                beatTestObject.SetActive(true);
            }
            else
            {
                if(beatTestObject.activeInHierarchy)
                beatTestObject.SetActive(false);
            }
        }
    }

    public bool IsCurrentInputValidBeat()
    {
        if (((timer+beatOffset) % beatInterval) < tollerance || ((timer+beatOffset) % beatInterval) > (beatInterval - tollerance))
            return true;
        else
            return false;
    }
}
