using UnityEngine;

public class SuccessPopup : MonoBehaviour
{
    [SerializeField] private GameUIButton Btn_GoMainUI;

    public void OnEnable()
    {
        Btn_GoMainUI.BindOnClickButtonEvent(OnClick_GoMainUIBtn);
    }

    private void OnClick_GoMainUIBtn()
    {
        UIManager.Instance.OpenLobbyMainUI();
        Debug.LogWarning("메인UI로 돌아갑니다.");
    }
}
