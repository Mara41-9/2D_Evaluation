using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    // 생성할 몬스터의 프리팹
    [SerializeField] private GameObject Prefab_Enemy;
    [SerializeField] private Transform Root_Enemy;

    public static GameObjectManager Instance { get; set; }

    // 생성된 오브젝트의 키
    private int _objectInstanceKeyGenerator = 1;

    // 생성된 오브젝트의 생명을 보관
    private Dictionary<int, GameObject> _createdGameObjectContainer = new Dictionary<int, GameObject>();
    private Dictionary<int, FieldObject2D> _fieldObjectContainer = new Dictionary<int, FieldObject2D>();


    private void Awake()
    {
        Instance = this;
    }

    public void RequestSpawnEnemy()
    {
        if(Prefab_Enemy == null)
        {
            Debug.LogWarning("프리팹이 등록되지 않은 오브젝트 입니다.");
            return;
        }

        var gObj = Instantiate(Prefab_Enemy, Root_Enemy);
        if (gObj == null)
        {
            Debug.LogWarning("생성에 실패한 게임 오브젝트 입니다.");
            return;
        }

        // 생성에 성공했다면, 미리 Key를 발급한다.
        _objectInstanceKeyGenerator++;

        // Dictionary에 추가하기 전에 미리 키 검사
        if (_createdGameObjectContainer.ContainsKey(_objectInstanceKeyGenerator) == true)
        {
            Debug.LogWarning("이미 동일한 키가 발급된 게임 오브젝트가 존재합니다");
            return;
        }

        // 동적생성(실체화)된 오브젝트를 게임 오브젝트 매니저의 자료구조(Dictionary)에 보관
        _createdGameObjectContainer.Add(_objectInstanceKeyGenerator, gObj);
        // 동적생성(실체화)된 오브젝트의 고유 ID를 전달해서 초기화
        InitGeneratedEntityObject(_objectInstanceKeyGenerator, gObj);

        Debug.Log($"키: {_objectInstanceKeyGenerator}의 객체 {gObj.name}이 호출되었습니다.");
    }

    // 동적으로 생성된 적(GameObject)에 고유 ID 같은 초기 정보를 세팅
    private void InitGeneratedEntityObject(int generatedId, GameObject gObj)
    {
        // 생성된 GameObject에서 Nara_2DEnemy 컴포넌트 가져오기
        Nara_2DEnemy gameEntity = gObj.GetComponent<Nara_2DEnemy>();

        if(gameEntity == null)
        {
            Debug.LogWarning($"생성된 {gObj.name}의 InstanceId를 대입할 수 있는 컴포넌트를 가져올 수 없습니다!");
            return;
        }

        // 생성된 객체의 정보를 부여
        gameEntity.InitEnemyInfo(generatedId);
    }

    // instanceId로 등록된 GameObject를 찾아서 반환하는 함수
    public GameObject GetEntityObjectCanBeNull(int instanceId)
    {
        // instanceId 가 딕셔너리에 존재하지 않다면
        if (_createdGameObjectContainer.ContainsKey(instanceId) == false)
        {
            Debug.LogWarning($"{instanceId}는 존재하지 않습니다.");
            return null;
        }

        // 해당 키에 저장된 값 가져오기
        return _createdGameObjectContainer[instanceId];
    }

    // instanceId에 해당하는 게임 오브젝트를 찾아서 제거하는 함수
    public void RequestDestroyEntityObject(int instanceId)
    {
        // instanceId에 해당하는 GameObject 가져오기
        var gObj = GetEntityObjectCanBeNull(instanceId);
        if(gObj == null)
        {
            return;
        }

        // 해당 데이터 제거
        _createdGameObjectContainer.Remove(instanceId);
        // 씬에서 GameObject 삭제
        Destroy(gObj);
    }



    //[필드 오브젝트] ====================================================================================================

    // 필드 오브젝트를 생성하는 비동기 함수
    public async UniTaskVoid CreateFieldObject(string fieldObjectDataId, Transform spawnSpot)
    {
        // GameDataManager에서 FieldObject 데이터 가져오기
        var fieldObject = GameDataManager.Instance.GetFieldObjectData(fieldObjectDataId);
        if (fieldObject != null)
        {
            // 어드레서블 기반 비동기 생성
            // fieldObject.PrefabPath: 생성할 프리팹 주소, Root_Enemy: 생성된 오브젝트 부모, true: 월드 좌표 유지
            var createdObj = await ResourceManager.Instance.InstantiateAsync(fieldObject.PrefabPath, Root_Enemy, true);
            // 생성된 오브젝트의 위치 설정
            createdObj.transform.position = spawnSpot.position;
            // 생성된 오브젝트를 관리 시스템에 등록
            AddFieldObjectOnCreate(createdObj, fieldObjectDataId);
        }
    }

    // 생성 완료된 필드 오브젝트를 관리 컨테이너에 등록
    private void AddFieldObjectOnCreate(GameObject createdObject, string fieldObjectDataId)
    {
        // 생성된 오브젝트 키 증가
        _objectInstanceKeyGenerator++;
        // 현재 생성된 고유 ID 저장
        var generatedInstanceId = _objectInstanceKeyGenerator;
        // 생성된 오브젝트에서 FieldObject2D 컴포넌트 가져오기
        var fieldObject = createdObject.GetComponent<FieldObject2D>();

        if (fieldObject != null)
        {
            _fieldObjectContainer.Add(generatedInstanceId, fieldObject);
            // 생성된 오브젝트 내부 데이터 초기화
            fieldObject.InitFieldObjectInfoOnCreated(generatedInstanceId, fieldObjectDataId);
        }
    }

    // 인스턴스 ID로 오브젝트 찾기
    public FieldObject2D GetFieldObjectByInstanceId(int fieldObjectInstanceId)
    {
        // _fieldObjectContainer 안에 해당 키가 없으면
        if (_fieldObjectContainer.ContainsKey(fieldObjectInstanceId) == false)
        {
            Debug.LogError($"{fieldObjectInstanceId} 찾으려는 필드 오브젝트가 유효하지 않습니다");
            return null;
        }

        return _fieldObjectContainer[fieldObjectInstanceId];
    }
}
