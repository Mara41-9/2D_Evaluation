using UnityEngine;
using UnityEngine.UI;

public class LobbyMainUI : MonoBehaviour
{
    // Layout_Bottom - 맨 아래 부분
    [Header("Layout_Bottom")]
    [SerializeField] private GameUIButton Btn_Shop;
    [SerializeField] private GameUIButton Btn_Skill;
    [SerializeField] private GameUIButton Btn_Battle;
    [SerializeField] private GameUIButton Btn_Ranking;
    [SerializeField] private GameUIButton Btn_Talent;

    // Layout_Top - 맨 윗 부분
    [Header("Layout_Top")]
    [SerializeField] private GameUIButton Btn_Profile;

    // Layout_RightTop - 오른쪽 윗 부분
    [Header("Layout_RightTop")]
    [SerializeField] private GameUIButton Btn_Quest;
    [SerializeField] private GameUIButton Btn_Inventory;

    [Header("Layout_Bottom(2)")]
    [SerializeField] private GameUIButton Btn_AddExp;


    private void OnEnable()
    {
        Btn_Shop.BindOnClickButtonEvent(OnClick_OpenShop);
        Btn_Skill.BindOnClickButtonEvent(OnClick_OpenSKillPopup);
        Btn_Battle.BindOnClickButtonEvent(OnClick_OpenBattle);
        Btn_Ranking.BindOnClickButtonEvent(OnClick_OpenRanking);
        Btn_Talent.BindOnClickButtonEvent(OnClick_OpenTalent);
        Btn_Profile.BindOnClickButtonEvent(OnClick_OpenProfile);
        Btn_Quest.BindOnClickButtonEvent(OnClick_OpenQuest);
        Btn_Inventory.BindOnClickButtonEvent(OnClick_OpenInventory);
        Btn_AddExp.BindOnClickButtonEvent(OnClick_AddExpButton);

    }


    public void OnClick_OpenShop()
    {
        Debug.LogWarning("상점 창이 열렸습니다.");
    }

    public void OnClick_OpenSKillPopup()
    {
        UIManager.Instance.OpenSkillPopup();
        Debug.LogWarning("스킬 창이 열렸습니다.");
    }

    
    public void OnClick_OpenBattle()
    {
        Debug.LogWarning("전투 화면으로 이동합니다.");
    }

    public void OnClick_OpenRanking()
    {
        Debug.LogWarning("랭킹 창이 열렸습니다.");
    }

    public void OnClick_OpenTalent()
    {
        Debug.LogWarning("강화 창이 열렸습니다.");
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

    public void OnClick_AddExpButton()
    {
        Debug.LogWarning("Exp 증가 버튼 클릭!");
    }
    
}
