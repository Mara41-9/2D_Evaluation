using UnityEngine;
using UnityEngine.UI;

public class LobbyMainUI : MonoBehaviour
{
    // Layout_Top - 맨 윗 부분
    [Header("Layout_Top")]
    [SerializeField] private GameUIButton Btn_Profile;

    // Layout_RightTop - 오른쪽 부분
    [Header("Layout_RightTop")]
    [SerializeField] private GameUIButton Btn_Quest;
    [SerializeField] private GameUIButton Btn_Inventory;

    // Layout_LeftTop - 왼쪽 부분
    [Header("Layout_LeftTop")]
    [SerializeField] private GameUIButton Btn_Skill;


    private void OnEnable()
    {
        Btn_Skill.BindOnClickButtonEvent(OnClick_OpenSKillPopup);
        Btn_Profile.BindOnClickButtonEvent(OnClick_OpenProfile);
        Btn_Quest.BindOnClickButtonEvent(OnClick_OpenQuest);
        Btn_Inventory.BindOnClickButtonEvent(OnClick_OpenInventory);
    }

    public void OnClick_OpenSKillPopup()
    {
        UIManager.Instance.OpenSkillPopup();
        Debug.LogWarning("스킬 창이 열렸습니다.");
    }

    public void OnClick_OpenProfile()
    {
        UIManager.Instance.OpenPropilePopup();
        Debug.LogWarning("프로필 창이 열렸습니다.");
    }

    public void OnClick_OpenQuest()
    {
        UIManager.Instance.OpenQuestPopup();
        Debug.LogWarning("퀘스트 창이 열렸습니다.");
    }

    public void OnClick_OpenInventory()
    {
        UIManager.Instance.OpenInventoryPopup();
        Debug.LogWarning("인벤토리 창이 열렸습니다.");
    }

}
