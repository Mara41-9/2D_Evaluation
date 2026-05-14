using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Text Text_StackCount;
    [SerializeField] private GameUIButton Btn_Slot;
    [SerializeField] private Image Img_Icon;
    [SerializeField] private Image Img_Frame;

    // 슬롯 클릭 시 외부에 알려주는 이벤트 (int 값 전달)
    private event Action<int> OnSelectEvent;

    public int SlotInstanceId { get; private set; }

    private void OnEnable()
    {
        Btn_Slot.BindOnClickButtonEvent(OnClick_SelectItem);
        RandomTestItemSetIcon();
    }

    private void RandomTestItemSetIcon()
    {
        // 데이터 연동 테스트 전용 - 추후에 제거될 메서드
        // 0 ~ 3 사이의 랜덤 숫자 뽑음 -> 0, 1, 2
        int randomIdx = UnityEngine.Random.Range(0, 3);

        // 임시 아이템 ID 저장할 변수 생성
        string temporalItemDataId = string.Empty;

        switch(randomIdx)
        {
            case 0:
                temporalItemDataId = "Item_Potion_1";
                break;
            case 1:
                temporalItemDataId = "Item_Coin_1";
                break;
            case 2:
                temporalItemDataId = "Item_Sword_1";
                break;
        }

        SetIcon(temporalItemDataId);
    }


    // 아이템 ID 받아서 해당 아이템의 아이콘 스프라이트를 찾아 슬롯 이미지에 넣어주는 함수
    private void SetIcon(string itemDataId)
    {
        // GameDataManager에서 해당 아이템 정보 받아옴
        var itemData = GameDataManager.Instance.GetItemData(itemDataId);
        if (itemData == null)
        {
            Debug.LogWarning($"Item 데이터를 불러올 수 없습니다! 경로:{itemDataId}");
            return;
        }

        // 아이템의 경로 받아옴
        string iconPath = itemData.IconPath;
        if(iconPath == null)
        {
            Debug.LogWarning($"Item 데이터에 아이콘 경로가 존재하지 않습니다.");
            return;
        }

        // iconPath에 있는 Sprite 로드하고 로드 완료되면 Img_Icon 이미지에 넣자
        //ResourceManager.Instance.LoadSprite(iconPath, (sprite) =>
        //{
        //    Img_Icon.sprite = sprite;
        //});

        // 어드레서블 적용 -> 비동기로 바뀜
        GameUtil.LoadAndSetSpriteImage(Img_Icon, iconPath).Forget();

        //// 실제 이미지 파일 불러옴
        //var sprite = GameUtil.LoadSpriteCanBeNull(iconPath);
        //if (sprite == null)
        //{
        //    Debug.LogWarning($"Sprite를 불러올 수 없습니다! 경로:{iconPath}");
        //    return;
        //}

        //Img_Icon.sprite = sprite;
    }


    // 이 오브젝트가 비활성될 때
    private void OnDisable()
    {
        // 등록돼있던 이벤트들 전부 제거
        OnSelectEvent = null;
    }


    // 슬롯이 생성된 후, 슬롯의 슬롯의 기본 정보(고유 번호)를 세팅하는 초기화 함수
    public void InitSlot(int slotInstanceId)
    {
        SlotInstanceId = slotInstanceId;
        // 슬롯 번호를 문자열로 바꿔서 텍스트 UI에 출력
        // ToString() -> Text.text는 문자열만 받을 수 있음
        Text_StackCount.text = slotInstanceId.ToString();
    }

    public void OnClick_SelectItem()
    {
        OnSelectEvent?.Invoke(SlotInstanceId);
        Debug.Log($"{SlotInstanceId}눌러졌다");
    }

    public void BindSlotSelectEvent(Action<int> onSelectEvent)
    {
        OnSelectEvent = onSelectEvent;
    }

    
}
