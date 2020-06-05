using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelController : MonoBehaviour
{
    //"랜덤한 장애물들"이 팝업한다 -> 랜덤한 위치지만 규칙적인 간격하에 놓여짐  5m -> 10m ->15m  
    // 왼쪽라인, 중간라인, 오른쪽라인 중 어디에 나올지 고른다
    // 몇개가 나오는건지 고른다

    public GameObject[] obstacles;
    int numberOfObstacles;  // 팝업 개수        => 
    int indexOfObstacles;   // 장애물의 종류

    
}

[System.Serializable]
public class LevelPiece
{
    public string name;
    public GameObject prefab;
    public int probability=1;
}
