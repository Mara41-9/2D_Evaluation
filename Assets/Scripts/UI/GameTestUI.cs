using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTestUI : MonoBehaviour
{
    // [SerializeField] private DaniTechUIButton Button_BBB;

    [SerializeField] private SpawnSpot SpawnSpot_Monster;

    [SerializeField] private GameUIButton Btn_Exit;

    public void OnEnable()
    {
        Btn_Exit.BindOnClickButtonEvent(OnClick_ExitButton);
        
    }

    public void OnClick_SelectTestBtn()
    {
        SpawnSpot_Monster.StartSpawn();
    }

    private void OnClick_ExitButton()
    {
        Debug.LogWarning("UI씬 메인UI로 이동합니다.");
        SceneManager.LoadScene("UI_Basic");
    }
}
