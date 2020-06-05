using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelController : MonoBehaviour
{
    //"랜덤한 장애물들"이 팝업한다 -> 랜덤한 위치지만 규칙적인 간격하에 놓여짐  5m -> 10m ->15m  
    // 왼쪽라인, 중간라인, 오른쪽라인 중 어디에 나올지 고른다
    // 몇개가 나오는건지 고른다

    public LevelPiece[] obstacles;  //장애물 
    List<int> probabilityList= new List<int>();
    //int numberOfObstacles;  // 팝업 개수        
    //int indexOfObstacles;   // 장애물의 종류

    public LevelPiece[] levelPieces;
    public Transform _camera;
    public int drawDistance;

    public float pieceLength;
    public float speed;

    Queue<GameObject> activePieces =new Queue<GameObject>();
    Queue<GameObject> activeObstacles = new Queue<GameObject>();
    int currentCamStep = 0;
    int lastCamStep = 0;


    private void Start()
    {
        BuidProbabilityList();
        for (int i=0; i<drawDistance; i++)
        {
            SpawnNewLevelPiece();
            SpawnObstacles();
        }

        currentCamStep = (int)(_camera.transform.position.z /pieceLength);
        lastCamStep = currentCamStep;
    }

    private void Update()
    {
        _camera.transform.position = Vector3.MoveTowards(_camera.transform.position,_camera.transform.position+Vector3.forward,Time.deltaTime *speed);

        currentCamStep = (int)(_camera.transform.position.z / pieceLength);
        if(currentCamStep != lastCamStep)
        {
            lastCamStep = currentCamStep;
            DespawnLevelPiece();
            SpawnNewLevelPiece();
            SpawnObstacles();
        }
    
    }

    void SpawnObstacles()   //장애물 배치
    {
        int pieceIndex = probabilityList[Random.Range(0, probabilityList.Count)];
        GameObject hurdle = Instantiate(obstacles[pieceIndex].prefab, new Vector3(0f, 0f, pieceLength * (currentCamStep + activePieces.Count)), Quaternion.identity);
        activeObstacles.Enqueue(hurdle);
    }

    void SpawnNewLevelPiece()     // 지면 배치 -> 현재 끝없이 나오는버전
    {
        GameObject newLevelPiece = Instantiate(levelPieces[0].prefab, new Vector3(0f, 0f, pieceLength * (currentCamStep +activePieces.Count)), Quaternion.identity);
        activePieces.Enqueue(newLevelPiece);
    }

    void DespawnLevelPiece()    // 뒤에떨 없애주고 앞애껄 그만큼 만드는 방식
    {
        GameObject oldLevelPiece = activePieces.Dequeue();
        GameObject oldLevelObstacles= activeObstacles.Dequeue();
        Destroy(oldLevelObstacles);
        Destroy(oldLevelPiece);
    }

    void BuidProbabilityList()      //장애물 확률 조정 함수
    {
        int index = 0;
        foreach(LevelPiece piece in obstacles)
        {
            for(int i=0; i<piece.probability; i++)
            {
                probabilityList.Add(index);
            }
            index++;
        }
    }

}


[System.Serializable]
public class LevelPiece
{
    public string name;
    public GameObject prefab;
    public int probability=1;
}
