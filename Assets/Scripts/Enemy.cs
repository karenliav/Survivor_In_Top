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
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }

    void FixedUpdate()
    {
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) // 0 -> 애니메이션 레이어 표기
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
        
    }

    // 캐릭터 생성
    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    // 충돌 처리
    void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    // 캐릭터가 죽었을 때
    void Dead()     // 9강 20분 쯤에 나옴, 유니티 애니메이션 설정에서 사용됨
    {
        gameObject.SetActive(false);
    }
}
