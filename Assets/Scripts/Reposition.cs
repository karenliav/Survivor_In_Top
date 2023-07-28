using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Map, 적대 캐릭터 재배치 스크립트
// 플레이어에게서 거리가 멀어질 경우 재배치
public class Reposition : MonoBehaviour
{
    Collider2D coll;

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 myPos = transform.position;
        float diffX = Mathf.Abs(playerPos.x - myPos.x); // Abs = 절대값
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.Instance.player.inputVec;
        float dirX = playerDir.x < 0 ? -1 : 1;      
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":  // 맵이 생성될때 플레이어 이동 방향으로 생김 (playerDir)
                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 80);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 80);
                }
                break;
            case "Enemy":
                if (coll.enabled)
                {
                    if (diffX > diffY)
                    {
                        transform.Translate(Vector3.right * dirX * 39);
                    }
                    else if (diffX < diffY)
                    {
                        transform.Translate(Vector3.up * dirY * 39);
                    }
                }
                break;
        }
    }
}
