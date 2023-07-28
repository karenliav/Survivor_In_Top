using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] MapPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        // 스테이지 시작시 맵 생성
        Instantiate(MapPrefabs[0], Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
