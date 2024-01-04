using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    [SerializeField] List<WaveConfig> waveConfig;
   
    [SerializeField] bool isLooping;
    WaveConfig currentWave;

    

    [SerializeField] int indexMove = 0;
    [SerializeField] int stepMove = 0;
    [SerializeField] bool isStartMoveUpDown = false;
    [SerializeField] bool isMovingUp = false;
    private float distanceToMove = 1f;
    void Start()
    {
        SpawnEnemyWaves();
    }

   

    public void StartMoveUpDown()
    {
        //isStartMoveUpDown = true;
        StartCoroutine(WaitToMove());
    }
    IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(1.5f);
        if (isStartMoveUpDown == false)
        {
            isStartMoveUpDown = true;
            
        }


    }

    public void StopMoveUpDown()
    {
        isStartMoveUpDown = false;
    }

    private void LateUpdate()
    {
        if (isStartMoveUpDown == true )
        {
            if (isMovingUp)
            {
                
                transform.Translate(Vector3.up * Time.deltaTime * distanceToMove);
            }
            else
            {
                
                transform.Translate(Vector3.down * Time.deltaTime * distanceToMove);
            }

           
            if (transform.position.y >= 1.0f) 
            {
                isMovingUp = false;
            }
            else if (transform.position.y <= -1f) 
            {
                isMovingUp = true; 
            }

        }
    }

    public int GetStep()
    {
        return stepMove;
    }

    public void SetStep()
    {
        stepMove++;
    }

    public void ResetStep()
    {
        stepMove = 0;
    }

    public WaveConfig GetCurrentPath()
    {
        return currentWave;
    }

    public int GetCurrentPathIndex()
    {
        return indexMove;
    }

    public void SetNextPath()
    {
        indexMove++;
    }
    void SpawnEnemyWaves()
    {
        do
        {
            currentWave = waveConfig[indexMove];
            for (int i = 0; i < currentWave.GetEnemyCount(); i++)
            {
                GameObject enemy = Instantiate(currentWave.GetEnemyPrefab(i)
                , currentWave.GetStartedWaypoint(0).position,
                Quaternion.identity,
                transform);
                enemy.GetComponent<PathFinder>().setIndexNumber(i);


            }
            //yield return new WaitForSeconds(TimeBetweenWaves);

        }
        while (isLooping == true);
    }
}
