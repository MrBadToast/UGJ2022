using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.Rendering.Universal;

public class PetManager : MonoBehaviour
{
    static private PetManager instance;
    static public PetManager Instance { get { return instance; } }

    public CinemachineVirtualCamera vCam2;
    public CinemachineVirtualCamera vCam3;

    public List<PetFollowing> petList;
    int prevPetListCount = 0;
    public int PetCount { get { return petList.Count; } }

    private void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(gameObject); }

        petList = new List<PetFollowing>();
    }

    private void Start()
    {
        PlayerBehavior.Instance.OnPlayerDamaged += CallWhenPlayerDamaged;
        
    }

    private void OnDestroy()
    {
        PlayerBehavior.Instance.OnPlayerDamaged -= CallWhenPlayerDamaged;
    }

    private void FixedUpdate()
    {
        var light = PlayerBehavior.Instance.sightLight;
        light.pointLightOuterRadius = Mathf.Lerp(light.pointLightOuterRadius, lightTarget, 0.1f);
    }

    //void Update()
    //{
    //    // 펫이 있을 경우, 펫에게 따라갈 대상의 위치 할당
    //    // 리소스를 위해 펫 리스트에 변화가 생겼을 경우에만 실행
    //    if (petList.Count > 0 && petList.Count != prevPetListCount)
    //    {
    //        // transform을 대입하는 것은 주소를 주는 것. 벨류를 주는 게 아니기 때문에 최초 1회만 주면 됨
    //        // 첫 번째 펫에게는 플레이어의 위치 할당
    //        petList[0].seniorTransform = PlayerBehavior.Instance.transform;
    //        // 두 번째 펫부터는 그 앞의 펫 위치 할당 
    //        for (int i = 1; i < petList.Count; i++)
    //        {   
    //            petList[i].seniorTransform = petList[i - 1].transform;
    //        }
    //        prevPetListCount = petList.Count;
    //    }
    //}
    // 플레이어가 데미지를 입었을 때

    public bool CallWhenPlayerDamaged()
    {

        Debug.Log("펫 제거 함수 호출");
        // 펫이 남아있다면
        if (petList.Count != 0)
        {   // 펫을 하나 지우고 return true
            // 펫 오브젝트 자체를 destroy 
            Destroy(petList[0].gameObject);
            petList.RemoveAt(0);
            if(petList.Count >= 1)
            petList[0].seniorTransform = PlayerBehavior.Instance.transform;
            return true;
        }
        // 남아있는 펫이 없다면 return false
        else return false;
    }


    float lightTarget = 5.0f;
    // 플레이어가 펫을 획득 성공했을 때
    public bool OnPlayerGained(PetFollowing pet)
    {
       // PlayerBehavior.Instantiate(PlayerBehavior.Instance.petGainParticle, PlayerBehavior.Instance.transform.position, Quaternion.identity);
        if(petList.Count == 0) pet.seniorTransform = PlayerBehavior.Instance.transform;
        else
        { pet.seniorTransform = petList[petList.Count-1].transform; }
        
        petList.Add(pet);

        if (petList.Count == 0)
        { vCam2.gameObject.SetActive(false); vCam3.gameObject.SetActive(false); }
        if (petList.Count == 1)
        { vCam2.gameObject.SetActive(true); vCam3.gameObject.SetActive(false); }
        if (petList.Count == 3) vCam3.gameObject.SetActive(true);

        lightTarget = (petList.Count * 5f) + 5f;

   

        // 혹시 몰라서 bool타입 return 했으나, 아직 쓸 데는 없음 
        return true;
    }
}
