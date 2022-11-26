using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGoal : MonoBehaviour
{
    public string happyendScene;
    public string badendScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(PetManager.Instance.PetCount >= 3)
            {
                SceneLoader.Instance.LoadScene(happyendScene);
            }
            else
            {
                SceneLoader.Instance.LoadScene(badendScene);
            }
        }
    }
}
