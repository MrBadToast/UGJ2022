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
        //
    }
}
