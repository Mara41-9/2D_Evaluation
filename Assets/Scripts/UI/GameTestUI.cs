using UnityEngine;
using UnityEngine.UI;

public class GameTestUI : MonoBehaviour
{
    // [SerializeField] private DaniTechUIButton Button_BBB;

    [SerializeField] private SpawnSpot SpawnSpot_Monster;

    public void OnClick_SelectTestBtn()
    {
        SpawnSpot_Monster.StartSpawn();
    }
}
