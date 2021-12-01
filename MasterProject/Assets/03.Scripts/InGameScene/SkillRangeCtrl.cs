using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRangeCtrl : MonoBehaviour
{
    List<GameObject> target_List = new List<GameObject>();      // 타겟을 저장할 리스트
    SphereCollider range_Coll;      // 타겟을 체크할 충돌체
    float damage_Time = 0.5f;       // 대미지 적용 딜레이
    bool damage_Bool = false;       // 한 번만 적용되게 체크하는 변수

    void Start()
    {
        range_Coll = this.GetComponent<SphereCollider>();
    }

    void Update()
    {
        if (damage_Time > 0.0f)
        {
            damage_Time -= Time.deltaTime;
            return;
        }

        SkillDamage();
    }

    void SkillDamage()
    {
        if (damage_Bool == true)    // 이미 실행 된 적 있으면 리턴
            return;

        range_Coll.enabled = false;     // 충돌체를 끔
        damage_Bool = true;

        for(int ii = 0; ii < target_List.Count; ii++)
        {
            if (target_List[ii] == null)        // 타겟이 없으면 리턴
                return;

            if (target_List[ii].tag.Contains("TOWER") == true)      // 타겟의 태그가 특정 태그와 같은지 비교
                if (target_List[ii].name.Contains("CommandTower") == true)      // 일반 타워와 기지의 경우 스크립트가 달라 다르게 체크가 필요
                {
                    CommandTowerMgr command = target_List[ii].GetComponent<CommandTowerMgr>();
                    if (command != null)
                        command.TakeDamage(100);    // 해당 오브젝트 스크립트에 있는 대미지 함수 실행
                }
                else
                {
                    TowerCtrl_Team ctrl = target_List[ii].transform.parent.GetComponent<TowerCtrl_Team>();
                    if (ctrl != null)
                        ctrl.TakeDamage(100);    // 해당 오브젝트 스크립트에 있는 대미지 함수 실행
                }



        }

        Destroy(this.gameObject,3f);    // 오브젝트가 3초 뒤 사라지도록 설정
    }

    #region ---------- 사정거리 충돌 체크

    public void OnTriggerEnter(Collider coll)       // 충돌체에 충돌된 오브젝트만 리스트에 추가
    {
        if(coll.tag.Contains("TOWER") == true)
            target_List.Add(coll.gameObject);
    }

    public void OnTriggerExit(Collider coll)        // 충돌체에서 빠져나간 오브젝트는 제외
    {
        if (coll.tag.Contains("TOWER") == true)
            target_List.Remove(coll.gameObject);
    }

    #endregion
}
