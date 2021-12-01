using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBoomCtrl : MonoBehaviour
{
    public GameObject sky_Obj = null;       // 폭격기 오브젝트
    public GameObject boom_Obj = null;      // 폭발 이펙트
    public GameObject range_Obj = null;     // 사거리 체크용 오브젝트
    public GameObject sound_Obj = null;     // 사운드 오브젝트
    float sound_Delay = 0.0f;               // 사운드 딜레이
    float boom_Delay = 0.0f;                // 폭발 이펙트 생성 딜레이
    float target_dist = 0.0f;               // 타겟까지의 거리
    float end_dist = 0.0f;                  // 폭격기 목적지 거리
    Vector3 target_Pos = Vector3.zero;      // 타겟 좌표
    Vector3 start_Pos = Vector3.zero;       // 폭격기 시작 위치
    Vector3 end_Pos = Vector3.zero;         // 폭격기 도착 위치

    bool range_Bool = true;


    void Start()
    {
    }

    void Update()
    {
        if (boom_Delay > 0.0f)
            boom_Delay -= Time.deltaTime;

        if (sound_Delay > 0.0f)
            sound_Delay -= Time.deltaTime;

        Vector3 pos = end_Pos - start_Pos;
        this.transform.Translate(pos * 0.2f * Time.deltaTime);

        target_dist = Vector3.Distance(target_Pos, this.transform.position);
        end_dist = Vector3.Distance(end_Pos, this.transform.position);

        if (target_dist < 3.5)
        {
            BoomCreate();       // 폭발 이펙트 생성
            SoundCreate();      // 폭발 사운드 생성

            if (range_Bool == true)     // 한 번만 발동되도록 체크하는 변수
            {
                range_Bool = false;
                Vector3 range_pos = target_Pos;
                range_pos.y = 1.0f;
                Instantiate(range_Obj, range_pos, Quaternion.identity);     // 타겟 체크용 오브젝트 생성
            }    
        }
            

        if (end_dist < 2.0f)            // 목적지와의 거리가 일정 수치 이하면 오브젝트 삭제
            Destroy(this.gameObject);
    }

    void BoomCreate()
    {
        if (boom_Delay > 0.0f)
            return;

        float randX = Random.Range(-6.0f, 10.0f);       // 일정 범위 내에 랜덤하게 생성
        float randZ = Random.Range(-6.0f, 10.0f);
        Vector3 pos = target_Pos;
        pos.x += randX - 1;
        pos.z += randZ - 1;
        pos.y = 1.0f;   // 높이는 고정

        Instantiate(boom_Obj, pos, sky_Obj.transform.rotation);     // 랜덤한 위치에 폭발 이펙트 생성
        boom_Delay = 0.005f;        // 이펙트 생성 딜레이
    }

    void SoundCreate()
    {
        if (sound_Delay > 0.0f)
            return;

        Instantiate(sound_Obj, this.transform.position, transform.rotation);    // 폭발 사운드를 가진 오브젝트 생성
        sound_Delay = 0.1f;     // 사운드 오브젝트 생성 딜레이
    }

    public void TargetSetting(Vector3 a_Target_Pos)     // 스킬 사용 시 타겟과 초기 값을 설정하는 함수
    {
        start_Pos = this.transform.position;        // 시작 위치를 해당 오브젝트의 위치로 설정
        target_Pos = a_Target_Pos;              // 타겟 위치는 매개변수로 넘어온 값을 이용
        target_Pos.y = start_Pos.y;     // 타겟의 높이는 해당 오브젝트와 동일하게 변경
        end_Pos = target_Pos + (start_Pos - target_Pos) * -1;       // 해당 오브젝트의 위치에서 일정 거리만큼 떨어진 값을 구함
        end_Pos.y = start_Pos.y;        // 높이는 해당 오브젝트와 동일하게 변경
    }
}
