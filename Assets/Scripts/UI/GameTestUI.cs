using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTestUI : MonoBehaviour
{
    // [SerializeField] private DaniTechUIButton Button_BBB;

    [SerializeField] private SpawnSpot SpawnSpot_Monster;

    [SerializeField] private GameUIButton Btn_Exit;
    [SerializeField] private GameUIButton Btn_Inventory;

    public void OnEnable()
    {
        Btn_Exit.BindOnClickButtonEvent(OnClick_ExitButton);
        Btn_Inventory.BindOnClickButtonEvent(OnClick_InventoryBtn);
        
    }

    public void OnClick_SelectTestBtn()
    {
        SpawnSpot_Monster.StartSpawn();
    }

    public void OnClick_InventoryBtn()
    {
        UIManager.Instance.OpenPopupUI(UIType.InventoryPopup);
    }

    private void OnClick_ExitButton()
    {
        Debug.LogWarning("UI씬 메인UI로 이동합니다.");
        SceneManager.LoadScene("UI_Basic");
    }
}
