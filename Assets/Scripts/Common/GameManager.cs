using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    // 현재 플레이 중인 데이터를 메모리에 들고 있는 변수
    private PlayerModel _playerModel = new PlayerModel();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // 게임 시작하자마자 자동으로 세이브 데이터 불러옴
        LoadSaveData();
    }

    // 현재 플레이 데이터를 저장하는 함수
    public void SaveData()
    {
        // 현재 _playerViewModel 데이터를 넘겨서 JSON 저장 요청
        NetworkManager.Instance.RequestSaveData(_playerModel);
    }

    // 게임 저장 -> 종료
    public void SaveAndEndGame()
    {
        SaveData();
        // 유니티 게임 프로그램 종료 요청 함수 -> 현재 실행 중인 게임만 종료
        Application.Quit();
    }

    // 세이브 데이터 불러오기
    public void LoadSaveData()
    {
        // 저장 파일에서 읽어온 데이터를 현재 메모리 데이터로 넣기
        _playerModel = NetworkManager.Instance.RequestLoadSaveData();
    }

    // 플레이어의 현재 총 Exp 가져오기
    public int GetPlayerExp()
    {
        return _playerModel.PlayerTotalExp;
    }

    // 플레이어 경험치 증가
    public void IncreasePlayerExp(int exp)
    {
        // 현재 경험치에 추가 경험치 더하기
        // 추후에 한곳에서 관리할 수 있게 익스텐션(확장메서드)으로 빼도 된다
        _playerModel.PlayerTotalExp += exp;
        Debug.LogWarning($"현재 누적 Exp: {_playerModel.PlayerTotalExp}");
    }
}
