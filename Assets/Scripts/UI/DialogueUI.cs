using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : UIBase
{
    [SerializeField] private Text Text_Description;

    public void StartDialogue(string dialogueId)
    {
        var dialogueData = GameDataManager.Instance.GetDialogueData(dialogueId);
        if(dialogueData == null)
        {
            Debug.LogWarning($"다이얼로그 데이터가 존재하지 않습니다 {dialogueData}");
            return;
        }
       
        Text_Description.text = dialogueData.Description;
            
    }
}
