using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour
{
    [SerializeField] private float beatInterval = 1.0f;
    [SerializeField] private float beatOffset = 0.5f;
    [SerializeField] private float tollerance = 0.1f;

    [SerializeField] private AudioClip currentStageMusic;
    [SerializeField] private GameObject beatTestObject;

    private AudioSource aud;

    private float timer = 0;
    private bool isBeatCountStarted = false;
    public bool IsBeatCountStarted { get { return isBeatCountStarted; } }

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
    }

    public void StartBeatSeq()
    {
        isBeatCountStarted = true;
        aud.clip = currentStageMusic;
        aud.Play();
    }

    public void Update()
    {
        if (isBeatCountStarted)
        {
            timer += Time.deltaTime;
            
        }
    }

    public void FixedUpdate()
    {

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
