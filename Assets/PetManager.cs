using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PetManager : MonoBehaviour
{
    private Queue<PetFollowing> petQueue;

    private void Start()
    {
        PlayerBehavior.Instance.OnPlayerDamaged += CallWhenPlayerDamaged;
    }

    private void OnDestroy()
    {
        PlayerBehavior.Instance.OnPlayerDamaged -= CallWhenPlayerDamaged;
    }

    public bool CallWhenPlayerDamaged()
    {
        // 맨 앞에 펫 삭제
        // 디큐

        // if 큐 count != 0 (펫이 있으면)
        if (petQueue.Count != 0)
        {
            petQueue.Dequeue();
            return true;
        }
        else return false;
        // 펫 있으면 return true, 없으면 return false
    }
    public bool OnPlayerGained(PetFollowing pet)
    {
        //petfollwing 객체 인큐
        petQueue.Enqueue(pet);
        return true;
        // return true;
    }
}
