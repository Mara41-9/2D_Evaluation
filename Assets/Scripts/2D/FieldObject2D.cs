using UnityEngine;

public class FieldObject2D : MonoBehaviour
{
    [SerializeField] private string _fieldObjectInstanceId;  // 필드 오브젝트 개별 인스턴스 ID
    [SerializeField] private string _fieldObjectDataId;      // 어떤 종류의 오브젝트인지 ID
    [SerializeField] private string _fieldObjectName;        // 오브젝트 이름

    // 필드 오브젝트 생성 직후 초기화 함수
    public void InitFieldObjectInfoOnCreated(int instanceId, string fieldObjectDataId)
    {
        // GameDataManager에서 해당 ID의 데이터 가져오기
        var fieldObjectData = GameDataManager.Instance.GetFieldObjectData(fieldObjectDataId);
        if(fieldObjectData == null)
        {
            Debug.LogWarning($"유효하지 않은 필드 오브젝트 데이터 입니다! {fieldObjectDataId}");
            return;
        }

        // 현재 오브젝트 데이터 ID 저장
        _fieldObjectDataId = fieldObjectDataId;
    }

    // 현재 오브젝트 데이터 ID 반환 함수
    public string GetFieldObjectDataId()
    {
        return _fieldObjectDataId;
    }

    // 트리거 충돌 시 호출되는 유니티 이벤트 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") == true)
        {

        }
    }
}
