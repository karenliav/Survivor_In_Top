using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 적대 캐릭터 Spawn Pool 관리 스크립트
public class PoolManager : MonoBehaviour
{
    // 프리팹들을 보관할 변수
    public GameObject[] prefabs;
    // 풀을 담당하는 리스트들
    List<GameObject>[] pools;
        
    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    void Start()
    {
        GameManager.Instance.pool = this.GetComponent<PoolManager>();
    }

    // Pool에서 적대 캐릭터 생성
    public GameObject Get(int index)
    {
        GameObject select = null;

        // 선택한 풀의 비활성화 게임오브젝트 접근
        // 발견하면 select 변수에 할당
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf) // 비활성화 게임오브젝트 찾기
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // 못 찾으면 새롭게 생성하고 select 변수에 할당
        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}
