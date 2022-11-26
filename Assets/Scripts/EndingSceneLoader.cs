using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSceneLoader : MonoBehaviour
{
    public void LoadScene()
    {
        SceneLoader.Instance.LoadScene("Title");
    }
}
