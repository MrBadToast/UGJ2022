using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSceneLoader : MonoBehaviour
{
    public void LoadScene()
    {
        SceneLoader.Instance.LoadScene("Title");
    }

    float keyT = 0f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            keyT += Time.deltaTime;
        }
        else
        {
            keyT = 0f;
        }
        if(keyT > 3.0f)
        {
            Application.Quit();
        }
    }
}
