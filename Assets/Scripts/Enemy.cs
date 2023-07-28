using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 적대 캐릭터 제어 스크립트
public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive = false;

    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;

    WaitForFixedUpdate wait;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        //anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }

    void FixedUpdate()
    {
        //if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) // 0 -> 애니메이션 레이어 표기
        if (!isLive)
            return;

        // 플레이어 캐릭터를 따라 이동
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;  // 충돌로 인하여 밀려나는 물리 속도를 0으로 만들어 이동에 영향이 없게끔 함
    }
    void LateUpdate()
    {
        if (!isLive)
            return;
        // 항상 플레이어 방향을 보도록 설정
        spriter.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable()
    {
        //target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;            // 콜라이더 활성화
        rigid.simulated = true;         // 리지드바디 물리적 활성화
        //spriter.sortingOrder = 2;       // 이미지 가림 조정용 값을 원래값으로 변경, 객체의 Sprite Renderer -> Additional Settings -> order in Layer
        //anim.SetBool("Dead", false);    // 애니메이션 파라미터
        health = maxHealth;
    }

    // 캐릭터 생성
    public void Init(SpawnData data)
    {
        //anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    // 충돌 처리
    void OnTriggerEnter2D(Collider2D collision)
    {
        //if (!collision.CompareTag("Bullet") || !isLive)
        if (!isLive)
            return;

        //health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());    // 넉백 코루틴 적용

        if (health > 0)
        {
            //anim.SetTrigger("Hit");     // 애니메이션 파라미터 
        }
        else
        {
            isLive = false;
            coll.enabled = false;       // 콜라이더 비활성화
            rigid.simulated = false;    // 리지드바디 물리적 비활성화
            //spriter.sortingOrder = 1;   // 이미지 가림 조정용 값 1로 변경, 객체의 Sprite Renderer -> Additional Settings -> order in Layer
            //anim.SetBool("Dead", true); // 애니메이션 파라미터 
            GameManager.Instance.kill++;
            GameManager.Instance.GetExp();
        }
    }

    // 넉백 효과 적용 코루틴 작성
    IEnumerator KnockBack()
    {
        /*
        yield return null;  // 1 프레임 쉬기
        yield return new WaitForSeconds(2f);  // 2초 쉬기
        yield return new WaitForFixedUpdate(); // 다음 하나의 물리 프레임 딜레이
        */
        yield return wait;
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse); // ForceMode: 가해지는 힘의 종류, Impulse: 순간적인 힘
    }

    // 캐릭터가 죽었을 때
    void Dead()     // 9강 20분 쯤에 나옴, 유니티 애니메이션 설정에서 사용됨
    {
        gameObject.SetActive(false);
    }
}
