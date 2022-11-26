using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PetManager : MonoBehaviour
{ 
    public List<PetFollowing> petList = new List<PetFollowing>(100);
    int prevPetListCount = 0;

    private void Start()
    {
        PlayerBehavior.Instance.OnPlayerDamaged += CallWhenPlayerDamaged;

        // 테스트용 인스턴스
        PetFollowing pet1 = GameObject.Find("pet1").GetComponent<PetFollowing>();
        PetFollowing pet2 = GameObject.Find("pet2").GetComponent<PetFollowing>();
        PetFollowing pet3 = GameObject.Find("pet3").GetComponent<PetFollowing>();
        petList.Add(pet1);
        petList.Add(pet2);
        petList.Add(pet3);
        //CallWhenPlayerDamaged();
        //CallWhenPlayerDamaged();
    }

    private void OnDestroy()
    {
        PlayerBehavior.Instance.OnPlayerDamaged -= CallWhenPlayerDamaged;
    }

    void Update()
    {
        // 펫이 있을 경우, 펫에게 따라갈 대상의 위치 할당
        // 리소스를 위해 펫 리스트에 변화가 생겼을 경우에만 실행
        if (petList.Count > 0 && petList.Count != prevPetListCount)
        {
            // transform을 대입하는 것은 주소를 주는 것. 벨류를 주는 게 아니기 때문에 최초 1회만 주면 됨
            // 첫 번째 펫에게는 플레이어의 위치 할당
            petList[0].seniorTransform = PlayerBehavior.Instance.transform;
            // 두 번째 펫부터는 그 앞의 펫 위치 할당 
            for (int i = 1; i < petList.Count; i++)
            {   
                petList[i].seniorTransform = petList[i - 1].transform;
            }
            prevPetListCount = petList.Count;
        }
    }
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
            return true;
        }
        // 남아있는 펫이 없다면 return false
        else return false;
    }
    // 플레이어가 펫을 획득 성공했을 때
    public bool OnPlayerGained(PetFollowing pet)
    { 
        petList.Add(pet);
        // 혹시 몰라서 bool타입 return 했으나, 아직 쓸 데는 없음 
        return true;
    }
}
