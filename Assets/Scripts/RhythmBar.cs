using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmBar : MonoBehaviour
{
    [SerializeField] private Transform barAnchor;

    public bool BarMoving = true;

    private void Update()
    {
        if (BarMoving)
        {
            BeatManager beat = BeatManager.Instance;
            barAnchor.rotation = Quaternion.Euler(new Vector3(0f, 0f,
                Mathf.Sin((beat.Timer + 0.5f) * Mathf.PI) * 80f));
        }
    }
}
