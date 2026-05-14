using UnityEngine;


// 어떤 컴포넌트가 필수로 필요하다는 것을 강제
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("이동 설정")]
    [SerializeField] private float _moveSpeed = 8f;   // 움직임 속도
    [SerializeField] private float _jumpForce = 8f;  // 점프 힘

    // 지면 체크를 안 하면 적, 아이템 등도 바닥으로 인식할 수 있음!
    [Header("지면 체크 설정")]
    [SerializeField] private Transform _groundCheck;     // 발 밑에 배치할 빈 오브젝트
    [SerializeField] private float _checkRadius = 0.5f;  // 체크 범위
    [SerializeField] private LayerMask _groundLayer;     // 지면으로 인식할 레이어 - 어떤 오브젝트를 바닥으로?

    [Header("애니메이터")]
    [SerializeField] private EntityAnimController AnimatorController_Entity;

    // 우선 직접 들고 있다가 추후에 UI매니저한테 요청하도록 개선해볼 것
    [SerializeField] private ScoreUI _scoreUI;

    private Rigidbody2D _rigidbody;
    private bool _isGrounded;
    private float _horizontalInput;  // 플레이어의 좌우 입력값을 저장하는 변수
    private bool _lookRight = true;

    private int _currentScore;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        // 2D 캐릭터가 물리 충돌 시, 회전해서 넘어지는 것 방지
        // constraints : 움직임 제한 설정
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        // 좌우 입력값 받아서 _horizontalInput 변수에 저장
        // A키 / <- 방향키 = -1 , 입력 없음 = 0 , D 키 / → 방향키 = 1
        _horizontalInput = Input.GetAxisRaw("Horizontal");

        // 점프 입력
        if(Input.GetButtonDown("Jump") && _isGrounded)
        {
            Jump();
        }

        // _horizontalInput가 0이 아니면 움직이는 중!
        bool isMoving = (_horizontalInput != 0);

        // 컨트롤 키를 누르면
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {   
            // 공격해라
            ChangePlayerState(EntityAnimState.Atk);
        }
        else  // 그게 아니라면
        {
            // 걷거나 가만히 있어라
            ChangePlayerState(isMoving ? EntityAnimState.Walk : EntityAnimState.Idle);
        }

        // 캐릭터 방향 전환 
        if (_horizontalInput > 0 && !_lookRight)
        {
            Flip();
        }
        else if (_horizontalInput < 0 && _lookRight)
        {
            Flip();
        }

        
    }

    private void FixedUpdate()
    {
        // Physics2D.OverlapCircle : 원 모양 범위 안에 특정 오브젝트가 있는지 검사
        //                            -> 반환값 : bool
        // (원의 중심 위치, 검사 범위 크기, 어떤 레이어)
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _groundLayer);

        // Rigidbody 이동은 FixedUpdate 함수에서!
        Move();
        
    }

    private void ChangePlayerState(EntityAnimState newState)
    {
        AnimatorController_Entity.SetState(newState);
    }

    private void Move()
    {
        // Y축 속도는 유지, X축 속도만 변경 -> 좌우 이동!
        // _rigidbody.linearVelocity : Rigidbody2D의 현재 속도
        _rigidbody.linearVelocity = new Vector2(_horizontalInput * _moveSpeed, _rigidbody.linearVelocity.y);
    }

    private void Jump()
    {
        // X축 속도 유지, 위쪽 속도를 점프 힘으로 변경
        _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _jumpForce);
    }


    // 캐릭터 방향 반대로 뒤집는 함수
    private void Flip()
    {
        _lookRight = !_lookRight;                // true -> false , false -> true
        Vector3 scaler = transform.localScale;   // 현재 오브젝트의 크기 정보 가져오기 (Scale)
        scaler.x *= -1;                          // Unity에서 Scale X가 음수가 되면 스프라이트가 좌우 반전
        transform.localScale = scaler;           // 마지막으로 바뀐 값을 실제 오브젝트에 적용
    }

    private void OnDrawGizmos()
    {
        if(_groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_groundCheck.position, _checkRadius);
        }
    }

    // 적 충돌 시 처리하는 함수
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어의 콜리전에 충돌한 객체가 Enemy 태그가 아니라면
        if(collision.gameObject.CompareTag("Enemy") == false)
        {
            return;
        }

        // 충돌한 몬스터의 정보를 받아오자
        var enemyComponent = collision.gameObject.GetComponent<Monster2D>();

        if(enemyComponent == null)
        {
            Debug.Log($"충돌한 적 객체에서 컴포넌트를 찾을 수 없습니다 : {gameObject.name}");
            return;
        }

        // 충돌된 오브젝트를 플레이어가 직접 제거하는게 아니라, Id로 게임오브젝트 매니저한테 삭제 요청
        GameObjectManager.Instance.DestroyMonster(enemyComponent._monsterInstanceId);
        // 피그마를 잡으면 스코어를 올려주자!
        AddGameScore();
    }

    private void AddGameScore()
    {
        _currentScore++;
        _scoreUI.AddGameScore(_currentScore);
    }
}
