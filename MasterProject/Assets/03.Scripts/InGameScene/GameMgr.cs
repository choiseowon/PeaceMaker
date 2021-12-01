using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class GameMgr : MonoBehaviour
{
    public static GameMgr Inst = null;

    [Header("----- Boom Skill -----")]
    public Button skill_Btn = null;  // 폭격 스킬 버튼
    public Image skill_Img = null;
    public Text skill_Txt = null;
    public GameObject boom_Obj = null;  // 폭격기 오브젝트
    public GameObject pick_Obj = null;  // 범위 표시 오브젝트
    float skill_Delay = 0.0f;
    float skill_Time = 5.0f;
    GameObject target_Pick;
    Vector3 mouse_Pos = Vector3.zero;

    #region

    public static int[] tankLevel = {1, 1, 1, 1, 1};

    [HideInInspector] public int m_GetGold = 0; // 게임중 유저가 획득한 골드
    string DefenceUrl = "http://pmaker.dothome.co.kr/TeamProject/MyRoomScene/MapSettingLoad.php";
    string GetUserAttItemUrl = "http://pmaker.dothome.co.kr/TeamProject/StoreScene/AttGetItem.php";
    string GetAttItemUrl = "http://pmaker.dothome.co.kr/TeamProject/StoreScene/AttGetDefaultItem.php";
    string LoadTowerInfoUrl = "http://pmaker.dothome.co.kr//TeamProject/MyRoomScene/MyRoomLoadTowerInfo.php";
    [HideInInspector] public List<GameObject> tower_List = new List<GameObject>();

    // 메인 BGM관련 스크립트
    AudioSource m_BGMaudioSource = null;
    public AudioClip[] m_BGMaudioClip = null;
    // 메인 BGM관련 스크립트

    [Header("====== GameObject ======")]
    public GameObject SpawnPointGroup = null;
    public GameObject[] m_Tower;
    public GameObject m_TowerGroup = null;

    MeshRenderer[] m_TowerSpawnPointList;                           // 포탑이 생성될 위치
    List<GameObject> m_SpawnPointList = new List<GameObject>();     // 

    int m_VsUserNum = -1;
    string m_VsUserNick = "";

    [HideInInspector] public int m_TankNumbers = 0;
    [HideInInspector] public int m_TowerNumbers = 0;
    int m_UserSellMap = 0;

    public static List<AttUnit> m_UserTankList = new List<AttUnit>();     // 유저 아이템 정보를 받을 변수    

    [Header("----- UI -----")]
    public Transform m_Canvas = null;
    public Text m_MyName = null;
    public Text m_VsName = null;
    public Text m_GoldTxt = null;
    public Image m_VsHpImg = null;
    public Button m_ConfigBtn = null;
    public GameObject m_ConfigRoot = null;
    public GameObject m_ConfigObj = null;
    public Button m_LobbyBtn = null;
    public Button m_OptionBtn = null;
    public Button m_CloseBtn = null;
    public GameObject m_DlgBox = null;

    // Start is called before the first frame update
    private void Awake()
    {
        Inst = this;
        m_UserSellMap = (int)GlobarValue.g_UserMap;
        Application.targetFrameRate = 60;
        StartCoroutine(GetUserTankInfo());
        StartCoroutine(GetVsUserTowerInfo());
        //StartCoroutine();
    }

    #endregion

    void Start()
    {

        if (skill_Btn != null)          // 스킬 버튼 클릭 시
            skill_Btn.onClick.AddListener(() =>
            {
                if (StartEndCtrl.Inst.g_GameState != GameState.GS_Playing)      // 게임이 진행 중이 아니면 리턴
                    return;

                if (skill_Delay > 0.0f)     // 스킬 쿨타임이 안됬으면 리턴
                    return;

                SkillPickFunc();    // 스킬 범위 표시 함수
            });

        #region

        if (m_ConfigBtn != null)
            m_ConfigBtn.onClick.AddListener(() =>
            {
                if (target_Pick != null)
                {
                    Destroy(target_Pick);
                    target_Pick = null;
                }
                if(StartEndCtrl.Inst.g_GameState == GameState.GS_Playing)
                    m_ConfigRoot.SetActive(true);
            });

        if (m_LobbyBtn != null)
            m_LobbyBtn.onClick.AddListener(() =>
            {
                m_ConfigRoot.SetActive(false);
                GameObject dlg = Instantiate(m_DlgBox);
                dlg.transform.SetParent(m_Canvas);
                dlg.transform.localPosition = new Vector3(0, 0, 0);
                dlg.GetComponent<DialogBoxCtrl>().MsgDlg("로비로 이동하시겠습니까?\n(패배로 기록됩니다)", ReturnLobby);
            });

        if (m_OptionBtn != null)
            m_OptionBtn.onClick.AddListener(() =>
            {
                m_ConfigRoot.SetActive(false);
                Instantiate(m_ConfigObj);
            });

        if (m_CloseBtn != null)
            m_CloseBtn.onClick.AddListener(() =>
            {
                m_ConfigRoot.SetActive(false);
            });

        // 메인 BGM관련 스크립트
        m_BGMaudioSource = Camera.main.GetComponent<AudioSource>();
        m_BGMaudioSource.clip = m_BGMaudioClip[Random.Range(0, m_BGMaudioClip.Length)];
        m_BGMaudioSource.volume = 0.3f;
        m_BGMaudioSource.Play();
        // 메인 BGM관련 스크립트

        m_VsUserNum = GlobalValue.SO_Info.m_No;
        m_MyName.text = MyInfo.m_Nick;
        m_VsName.text = GlobalValue.SO_Info.m_Nick;
        GoldTextSett(100);
        GlobarValue.MakeMapSave();
        UserMapListLoad();

        #endregion
    }

    void Update()
    {
        if (StartEndCtrl.Inst.g_GameState != GameState.GS_Playing)
            return;

        if (target_Pick != null)        // 범위 표시 오브젝트가 있다면
        {
            mouse_Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);        // 마우스의 좌표를 변수에 저장
            mouse_Pos.y = 1.1f;             // 높이값 고정
            target_Pick.transform.position = mouse_Pos;     // 범위 표시 오브젝트가 마우스를 따라 다니도록 설정

            if (!EventSystem.current.IsPointerOverGameObject())             // UI를 클릭해도 작동 할 수 있도록 체크
            {
                if (Input.GetMouseButtonDown(0))        // 마우스 왼쪽 버튼 클릭 시
                {
                    Vector3 start_Pos = target_Pick.transform.position;         // 범위 표시 오브젝트의 좌표를 변수에 저장
                    start_Pos += new Vector3(-20, 0, -20);          // 폭격기 오브젝트 생성 위치 설정
                    start_Pos.y = 20;               // 높이 값 고정
                    GameObject obj = Instantiate(boom_Obj, start_Pos, transform.rotation);      // 폭격기 오브젝트 생성
                    obj.GetComponent<SkillBoomCtrl>().TargetSetting(target_Pick.transform.position);        // 스킬 사용 시 타겟과 초기 값을 설정하는 함수
                    Instantiate(pick_Obj, target_Pick.transform.position, Quaternion.Euler(90, 0, 0));      // 범위 표시 오브젝트를 해당 위치에 추가로 생성
                    Destroy(target_Pick);       // 마우스를 따라 다니던 범위 표시 오브젝트는 삭제
                    target_Pick = null;
                    skill_Delay = skill_Time;       // 스킬 딜레이 설정
                }
                else if (Input.GetMouseButtonDown(1))       // 마우스 오른쪽 버튼 클릭 시
                {
                    Destroy(target_Pick);       // 범위 표시 오브젝트 삭제
                    target_Pick = null;
                }
            }
        }

        if (skill_Delay > 0.0f)             // 스킬이 딜레이 상태 일 때
        {
            skill_Delay -= Time.deltaTime;
            skill_Txt.text = skill_Delay.ToString("F1");        // 스킬 버튼 위에 남은 시간 표시 소수점 첫 번째자리까지
            skill_Img.fillAmount = skill_Delay / skill_Time;    // 남은 시간 만큼 쿨타임 이미지 추가
        }

        if (skill_Delay <= 0.0f)        // 스킬이 사용 가능 할 때
        {
            skill_Delay = 0.0f;         // 스킬 딜레이 0으로 설정
            skill_Txt.text = "";        // 남은 시간 공백으로 표시
        }
            
    }

    public void GoldTextSett(int a_Gold)
    {
        m_GetGold += a_Gold;
        m_GoldTxt.text = "획득 골드 : + " + m_GetGold.ToString() + "G";
    }

    void SkillPickFunc()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);          // 마우스 위치를 받아오는 변수
        target_Pick = Instantiate(pick_Obj, pos, Quaternion.Euler(90, 0, 0));       // 범위 표시 오브젝트 생성 및 각도 조절
        target_Pick.GetComponent<EffDeathCtrl>().enabled = false;           // 범위 표시 오브젝트에 붙은 스크립트를 꺼둠
    }

    void SetSpawnPoint()
    {
        for (int i = 0; i < m_TowerSpawnPointList.Length; i++)
        {
            m_SpawnPointList.Add(m_TowerSpawnPointList[i].gameObject);
        }
    }

    void SpawnTower()
    {
        for (int i = 0; i < GlobarValue.g_MapList[m_UserSellMap].m_SpawnPoint.Length; i++)      // static으로 저장된 맵리스트 중 현재 맵의 스폰 포인트 만큼 반복
        {
            if (GlobarValue.g_MapList[m_UserSellMap].m_SpawnPoint[i] == true)           // 해당 스폰 포인트에 타워가 설치되 있다면
            {
                GameObject a_Tower = Instantiate(m_Tower[(int)GlobarValue.g_MapList[m_UserSellMap].m_TowerType[i]]);        // 맵 리스트에 저장된 타입의 타워 생성
                a_Tower.gameObject.transform.SetParent(m_TowerGroup.transform, false);
                tower_List.Add(a_Tower);                                            // 타워 리스트에 해당 타워 추가
                Vector3 pos = m_TowerSpawnPointList[i].transform.position;          // 타워 위치를 변경하기 위해 스폰 좌표 값을 변수에 저장
                pos.y += 1.0f;
                a_Tower.transform.position = pos;               // 저장한 스폰 좌표로 타워 이동
                TowerCtrl_Team a_WowerCtrl_Team = a_Tower.GetComponent<TowerCtrl_Team>();           // 해당 타워에 붙어있는 스크립트
                a_WowerCtrl_Team.m_TowerNumber = i;
                a_WowerCtrl_Team.m_TowerType = GlobarValue.g_MapList[m_UserSellMap].m_TowerType[i];     // 해당 타워의 타입 값을 넘겨줌
            }
        }
    }

    void ReturnLobby()
    {
        StartEndCtrl.Inst.GameEndCall("lose");
        UnityEngine.SceneManagement.SceneManager.LoadScene("LobbyScene");
    }

    void UserMapListLoad()
    {
        StartCoroutine(LoadMapInfoCo());
    }

    IEnumerator LoadMapInfoCo()
    {
        if (MyInfo.m_No < 0)
        {
            yield break;            //로그인 실패 상태라면 그냥 리턴
        }

        WWWForm form = new WWWForm();
        form.AddField("Input_usernumber", m_VsUserNum);             // 상대방 유저 넘버를 넘겨줌

        UnityWebRequest a_www = UnityWebRequest.Post(DefenceUrl, form);     // form 값을 해당 Url 주소의 php 로 넘겨 줌
        yield return a_www.SendWebRequest();            // 해당 데이터를 받아 올 때까지 대기함
        if (a_www.error == null)
        {
            System.Text.Encoding enc = System.Text.Encoding.UTF8;           // UTF8 형식으로 변환
            string a_ReStr = enc.GetString(a_www.downloadHandler.data);     // 변환한 메세지를 변수에 저장
            if (a_ReStr.Contains("MapLoadDone") == true)        // 해당 메세지에 해당 텍스트가 있는지 확인
                LoadDataSetUserMap(a_ReStr);                // 해당하면 받아온 데이터를 저장하는 함수 호출
        }
        else
        {
            Debug.Log(a_www.error);         // 에러가 뜨면 해당 메세지 출력
        }

        for (int i = 0; i < 2; i++)
        {
            GlobarValue.g_MapList[i].SaveMapInfo();
        }

    }

    void LoadDataSetUserMap(string strJosnData)
    {
        if (strJosnData.Contains("MapList") == false)       // JSON 
            return;

        var N = JSON.Parse(strJosnData);    // var 타입으로 변환

        MapSetting a_MapSetting;    // 클래스 생성

        int UserMapNum = 0;
        for (int i = 0; i < 3; i++)
        {
            string _map = N["MapList"][0]["maptype"][i];                    // DB에 저장된 maptype 값을 변수에 저장
            string _userselltower = N["MapList"][0]["userselltower"][i];    // DB에 저장된 userselltower 값을 변수에 저장
            string _towertype = N["MapList"][0]["towertype"][i];            // DB에 저장된 towertype 값을 변수에 저장
            string[] _userSetTowerList = _userselltower.Split(' ');         // 공백을 기준으로 문자열 분리
            string[] _TowerTyep = _towertype.Split(' ');                    // 공백을 기준으로 문자열 분리
            int ListNum = _userSetTowerList.Length - 1;
            UserMapNum = 0;
            if (_map == UserMap.MAP1.ToString())
            {
                UserMapNum = (int)UserMap.MAP1;
            }
            else if (_map == UserMap.MAP2.ToString())
            {
                UserMapNum = (int)UserMap.MAP2;
            }
            else if (_map == UserMap.MAP3.ToString())
            {
                UserMapNum = (int)UserMap.MAP3;
            }

            if (_userSetTowerList.Length < 10 || _TowerTyep.Length < 10)
            {
                GlobarValue.g_MapList[UserMapNum].m_SetMapCheck = false;
                continue;
            }

            int a_CheckTower = 0;
            for (int ii = 0; ii < GlobarValue.g_MapList[UserMapNum].m_SpawnPoint.Length; ii++)
            {
                if (_userSetTowerList[ii] == "0")
                {
                    GlobarValue.g_MapList[UserMapNum].m_SpawnPoint[ii] = true; 
                    a_CheckTower++;
                }
                else
                {
                    GlobarValue.g_MapList[UserMapNum].m_SpawnPoint[ii] = false;
                }
            }

            for (int ii = 0; ii < GlobarValue.g_MapList[UserMapNum].m_TowerType.Length; ii++)
            {
                int _TypeCint = int.Parse(_TowerTyep[ii]);

                GlobarValue.g_MapList[UserMapNum].m_TowerType[ii] = (TowerType)_TypeCint;
            }

            if (a_CheckTower == 0)
                GlobarValue.g_MapList[UserMapNum].m_SetMapCheck = false;
            else
                GlobarValue.g_MapList[UserMapNum].m_SetMapCheck = true;
        }

        m_TowerSpawnPointList = SpawnPointGroup.transform.GetComponentsInChildren<MeshRenderer>();
        SetSpawnPoint();
        SpawnTower();
    }

    bool isNetworkLock = false;
    bool isDataInit = false;
    IEnumerator GetUserTankInfo()
    {
        isNetworkLock = true;
        WWWForm form = new WWWForm();
        form.AddField("Input_ID", MyInfo.m_No);
        form.AddField("Input_itemType", "1", System.Text.Encoding.UTF8);    // 공격 아이템만 가져오기
        UnityWebRequest request = UnityWebRequest.Post(GetUserAttItemUrl, form);
        yield return request.SendWebRequest();

        if (request.error == null)
        {
            System.Text.Encoding enc = System.Text.Encoding.UTF8;
            string a_ReStr = enc.GetString(request.downloadHandler.data);

            if (a_ReStr.Contains("Get_Item_Success~") == true)
            {
                // 확인 부분
                if (a_ReStr.Contains("UnitList") == false)
                {
                    Debug.Log("데이터 저장 실패");
                    isDataInit = false;
                    yield break;
                }

                // 파싱된 결과를 바탕으로 아이템 초기화
                AttUnit a_UserUt;
                var N = JSON.Parse(a_ReStr);
                m_UserTankList.Clear();
                // 아이템을 전체적으로 초기화한다.
                // 먼저 JSON에 저장되어 있던 정보 초기화
                for (int i = 0; i < N["UnitList"].Count; i++)
                {
                    int itemNo = N["UnitList"][i]["ItemNo"].AsInt;
                    string itemName = N["UnitList"][i]["ItemName"];
                    int Level = N["UnitList"][i]["Level"].AsInt;
                    int isBuy = N["UnitList"][i]["isBuy"].AsInt;
                    int KindOfItem = N["UnitList"][i]["KindOfItem"].AsInt - 1;
                    int ItemUsable = N["UnitList"][i]["ItemUsable"].AsInt;

                    float UnitAtt = N["UnitList"][i]["UnitAttack"].AsFloat;
                    float UnitDef = N["UnitList"][i]["UnitDefence"].AsFloat;
                    int UnitHP = N["UnitList"][i]["UnitHP"].AsInt;
                    float UnitAttSpeed = N["UnitList"][i]["UnitAttSpeed"].AsFloat;
                    float UnitMoveSpeed = N["UnitList"][i]["UnitMoveSpeed"].AsFloat;
                    int Unitprice = N["UnitList"][i]["UnitPrice"].AsInt;
                    int UnitUprice = N["UnitList"][i]["UnitUpPrice"].AsInt;
                    int UnitRange = N["UnitList"][i]["UnitRange"].AsInt;
                    int UnitSkill = N["UnitList"][i]["UnitSkill"].AsInt;

                    a_UserUt = new AttUnit();
                    a_UserUt.m_UnitNo = itemNo;
                    a_UserUt.m_Name = itemName;
                    a_UserUt.m_Level = Level;
                    a_UserUt.m_isBuy = isBuy;
                    a_UserUt.m_unitkind = (AttUnitkind)KindOfItem;
                    a_UserUt.ItemUsable = ItemUsable;

                    a_UserUt.m_Att = UnitAtt;
                    a_UserUt.m_Def = UnitDef;
                    a_UserUt.m_Hp = UnitHP;
                    a_UserUt.m_AttSpeed = UnitAttSpeed;
                    a_UserUt.m_Speed = UnitMoveSpeed;
                    a_UserUt.m_Price = Unitprice;
                    a_UserUt.m_UpPrice = UnitUprice;
                    a_UserUt.m_Range = UnitRange;
                    a_UserUt.m_SkillTime = UnitSkill;

                    m_UserTankList.Add(a_UserUt);
                }//for (int i = 0; i < N["UnitList"].Count; i++)

                bool isInsert = false;
                AttUnit a_UserUtNew;
                // 유저가 가지고 있지 않은 아이템은 초기 아이템 정보로 초기화한다.
                // 다시 URL 통신
                WWWForm form2 = new WWWForm();
                UnityWebRequest request2 = UnityWebRequest.Post(GetAttItemUrl, form2);
                yield return request2.SendWebRequest();

                if (request2.error == null)
                {
                    string a_ReStr2 = enc.GetString(request2.downloadHandler.data);
                    var N2 = JSON.Parse(a_ReStr2);

                    for (int i = 0; i < N2["UnitList"].Count; i++)
                    {
                        foreach (var tpitem in m_UserTankList)
                        {
                            if (tpitem.m_unitkind == (AttUnitkind)i)
                                isInsert = true;
                        }

                        if (isInsert == true)
                        {
                            isInsert = false;
                            continue;
                        }
                        else
                        {
                            isInsert = false;
                            string UnitName = N2["UnitList"][i]["UnitName"];
                            int UnitAtt = N2["UnitList"][i]["UnitAttack"].AsInt;
                            int UnitDef = N2["UnitList"][i]["UnitDefence"].AsInt;
                            int UnitHP = N2["UnitList"][i]["UnitHP"].AsInt;
                            float UnitAttSpeed = N2["UnitList"][i]["UnitAttSpeed"].AsFloat;
                            float UnitMoveSpeed = N2["UnitList"][i]["UnitMoveSpeed"].AsFloat;
                            int UnitRange = N2["UnitList"][i]["UnitRange"].AsInt;
                            int UnitSkill = N2["UnitList"][i]["UnitSkill"].AsInt;
                            int Unitprice = N2["UnitList"][i]["UnitPrice"].AsInt;
                            int UnitUprice = N2["UnitList"][i]["UnitUpPrice"].AsInt;
                            int UnitUsable = N2["UnitList"][i]["UnitUseable"].AsInt;

                            a_UserUtNew = new AttUnit();
                            a_UserUtNew.m_UnitNo = 0; //기본값
                            a_UserUtNew.m_Name = UnitName;
                            a_UserUtNew.m_Level = 1;    // 구매 안함
                            a_UserUtNew.m_isBuy = 0;
                            a_UserUtNew.m_unitkind = (AttUnitkind)i;
                            a_UserUtNew.ItemUsable = UnitUsable;
                            a_UserUtNew.m_Att = UnitAtt;
                            a_UserUtNew.m_Def = UnitDef;
                            a_UserUtNew.m_Hp = UnitHP;
                            a_UserUtNew.m_AttSpeed = UnitAttSpeed;
                            a_UserUtNew.m_Speed = UnitMoveSpeed;
                            a_UserUtNew.m_Price = Unitprice;
                            a_UserUtNew.m_Range = UnitRange;
                            a_UserUtNew.m_UpPrice = UnitUprice;
                            a_UserUtNew.m_SkillTime = UnitSkill;

                            m_UserTankList.Add(a_UserUtNew);
                        }
                    } // for (int i = 0; i < N2["UnitList"].Count; i++)
                } // if (request2.error == null)
                else
                {
                    isDataInit = false; // 데이터 불러오기 실패                    
                }
                isDataInit = true;   // 데이터 저장 성공
            }//if (a_ReStr.Contains("Get_Item_Success~") == true)
            else
            {
                // 값이 없을 경우
                AttUnit a_UserUtNew;
                // 유저가 가지고 있지 않은 아이템은 초기 아이템 정보로 초기화한다.
                // 다시 URL 통신
                WWWForm form3 = new WWWForm();
                UnityWebRequest request3 = UnityWebRequest.Post(GetAttItemUrl, form3);
                yield return request3.SendWebRequest();

                if (request3.error == null)
                {
                    string a_ReStr2 = enc.GetString(request3.downloadHandler.data);
                    var N2 = JSON.Parse(a_ReStr2);
                    for (int i = 0; i < N2["UnitList"].Count; i++)
                    {
                        string UnitName = N2["UnitList"][i]["UnitName"];
                        int UnitAtt = N2["UnitList"][i]["UnitAttack"].AsInt;
                        int UnitDef = N2["UnitList"][i]["UnitDefence"].AsInt;
                        int UnitHP = N2["UnitList"][i]["UnitHP"].AsInt;
                        float UnitAttSpeed = N2["UnitList"][i]["UnitAttSpeed"].AsFloat;
                        float UnitMoveSpeed = N2["UnitList"][i]["UnitMoveSpeed"].AsFloat;
                        int Unitprice = N2["UnitList"][i]["UnitPrice"].AsInt;
                        int UnitUprice = N2["UnitList"][i]["UnitUpPrice"].AsInt;
                        int UnitRange = N2["UnitList"][i]["UnitRange"].AsInt;
                        int UnitSkill = N2["UnitList"][i]["UnitSkill"].AsInt;

                        a_UserUtNew = new AttUnit();
                        a_UserUtNew.m_UnitNo = 0; //기본값
                        a_UserUtNew.m_Name = UnitName;
                        a_UserUtNew.m_Level = 1;    // 구매 안함
                        a_UserUtNew.m_isBuy = 0;
                        a_UserUtNew.m_unitkind = (AttUnitkind)i;
                        a_UserUtNew.m_Att = UnitAtt;
                        a_UserUtNew.m_Def = UnitDef;
                        a_UserUtNew.m_Hp = UnitHP;
                        a_UserUtNew.m_AttSpeed = UnitAttSpeed;
                        a_UserUtNew.m_Speed = UnitMoveSpeed;
                        a_UserUtNew.m_Price = Unitprice;
                        a_UserUtNew.m_UpPrice = UnitUprice;
                        a_UserUtNew.m_Range = UnitRange;
                        a_UserUtNew.m_SkillTime = UnitSkill;

                        m_UserTankList.Add(a_UserUtNew);
                    }//for (int i = 0; i < N["UnitList"].Count; i++)
                }//if (request2.error == null) 
                isDataInit = true; // 데이터 받아오기 성공
            }
            isNetworkLock = false; // 네트워크 종료
        } // if (request.error == null)
        else
        {
            Debug.Log(request.error);
            isDataInit = false;   // 데이터 저장 실패
            isNetworkLock = false; // 실패했을 시 네트워크 상태 해제
        }
    } // IEnumerator GetUserTankInfo()

    IEnumerator GetVsUserTowerInfo()
    {
        WWWForm form = new WWWForm();
        form.AddField("U_ID", GlobalValue.SO_Info.m_No);

        //------- 신버전     // using UnityEngine.Networking;
        UnityWebRequest a_www = UnityWebRequest.Post(LoadTowerInfoUrl, form);
        yield return a_www.SendWebRequest(); //응답이 올때까지 대기하기...
        if (a_www.error == null) //에러가 나지 않았을 때 동작
        {
            System.Text.Encoding enc = System.Text.Encoding.UTF8;
            string sz = enc.GetString(a_www.downloadHandler.data);

            if (sz.Contains("Load-Success") == false)
                yield break;

            var N = JSON.Parse(sz);

            //if (N == null)
            //    yield break;

            GlobarValue.g_VsUserTowerList.Clear();
            UserTower a_Tower;
            int a_UnitCount = N["UnitList"][0]["Count"].AsInt;

            for (int i = 0; i < a_UnitCount; i++)
            {
                a_Tower = new UserTower();
                GlobarValue.g_VsUserTowerList.Add(a_Tower);
            }

            for (int i = 0; i < a_UnitCount; i++)
            {

                if (N["UnitList"][0]["UnitName"][i] != null)
                    GlobarValue.g_VsUserTowerList[i].m_TowerName = N["UnitList"][0]["UnitName"][i];

                if (N["UnitList"][0]["UnitAttack"][i] != null)
                    GlobarValue.g_VsUserTowerList[i].m_TowerAttack = N["UnitList"][0]["UnitAttack"][i].AsInt;

                if (N["UnitList"][0]["UnitDefence"][i] != null)
                    GlobarValue.g_VsUserTowerList[i].m_TowerDefence = N["UnitList"][0]["UnitDefence"][i].AsInt;

                if (N["UnitList"][0]["UnitHP"][i] != null)
                    GlobarValue.g_VsUserTowerList[i].m_TowerHP = N["UnitList"][0]["UnitHP"][i].AsInt;

                if (N["UnitList"][0]["UnitAttSpeed"][i] != null)
                    GlobarValue.g_VsUserTowerList[i].m_TowerAttSpeed = N["UnitList"][0]["UnitAttSpeed"][i].AsFloat;

                if (N["UnitList"][0]["UnitPrice"][i] != null)
                    GlobarValue.g_VsUserTowerList[i].m_TowerPrice = N["UnitList"][0]["UnitPrice"][i].AsInt;

                if (N["UnitList"][0]["UnitUpPrice"][i] != null)
                    GlobarValue.g_VsUserTowerList[i].m_TowerUpPrice = N["UnitList"][0]["UnitUpPrice"][i].AsInt;

                if (N["UnitList"][0]["UnitKind"][i] != null)
                    GlobarValue.g_VsUserTowerList[i].m_TowerKind = N["UnitList"][0]["UnitKind"][i].AsInt;

                if (N["UnitList"][0]["UnitRange"][i] != null)
                    GlobarValue.g_VsUserTowerList[i].m_TowerRange = N["UnitList"][0]["UnitRange"][i].AsInt;

                if (N["UnitList"][0]["UnitLevel"][i] != null)
                    GlobarValue.g_VsUserTowerList[i].m_UnitLevel = N["UnitList"][0]["UnitLevel"][i].AsInt;

                GlobarValue.g_VsUserTowerList[i].m_TowerType = GlobarValue.g_VsUserTowerList[i].SetTowerType(GlobarValue.g_VsUserTowerList[i].m_TowerName);
            }
        }
        else
        {
            Debug.Log(a_www.error);
        }
    }
}
