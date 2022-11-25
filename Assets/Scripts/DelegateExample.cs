using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateExample : MonoBehaviour
{
    private void Start()
    {

        PlayerBehavior.Instance.OnPlayerDamaged += CallWhenPlayerDamaged;
        
    }

    private void OnDestroy()
    {
        PlayerBehavior.Instance.OnPlayerDamaged -= CallWhenPlayerDamaged;
    }

    public void CallWhenPlayerDamaged()
    {
        // 맨 앞에 펫 삭제
        // 디큐
        // if 큐 count != 0 (펫이 있으면)
        // 펫 있으면 return true, 없으면 return false
    }
    public void OnPlayerGained(PetFollowing)
    {
        //petfollwing 객체 인큐
        // return true;
    }
}
