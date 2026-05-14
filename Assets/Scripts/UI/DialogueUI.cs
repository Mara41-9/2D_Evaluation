using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : UIBase
{
    [SerializeField] private Text Text_Description;
    public string _currentDialogueId;

    public void StartDialogue(string dialogueId)
    {
        var dialogueData = GameDataManager.Instance.GetDialogueData(dialogueId);
        if(dialogueData == null)
        {
            Debug.LogWarning($"다이얼로그 데이터가 존재하지 않습니다 {dialogueData}");
            return;
        }

        _currentDialogueId = dialogueId;
    }
}
