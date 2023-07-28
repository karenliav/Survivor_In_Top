using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 제어 스크립트
public class Player : MonoBehaviour
{
    [Header("# Player Move")]
    public Vector2 inputVec;
    public float speed;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        //anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 플레이어 이동 설정
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // 플레이어 이동
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }
    void LateUpdate()
    {
        // 플레이어 방향전환 제어
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }
}
