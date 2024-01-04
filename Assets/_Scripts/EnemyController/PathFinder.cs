using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PathFinder : MonoBehaviour
{
    EnemySpawner enemySpawner;
    WaveConfig waveConfig;
    List<Transform> waypoints;
    [SerializeField] int waypointIndex = 0;

    [SerializeField] int indexNumber = 0;

    [SerializeField] int countNumberMove = 0;
    
    public void setIndexNumber(int index)
    {
        indexNumber = index;
    }
    void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }
    void Start()
    {
        waveConfig = enemySpawner.GetCurrentPath();
        waypoints = waveConfig.GetWayPoints(0);
        transform.position = waypoints[waypointIndex].position;
    }


    void LateUpdate()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if (waypointIndex <= indexNumber && enemySpawner.GetStep() == 0 && enemySpawner.GetCurrentPathIndex() == 0) 
        {
            
            Vector3 targetPositon = waypoints[waypointIndex].position;
            
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPositon, delta );

            

            if (transform.position.x == targetPositon.x && transform.position.y == targetPositon.y)
            {
             
                waypointIndex++;
                if (waypointIndex == waypoints.Count)
                {
                    enemySpawner.SetStep() ;

                }

            }

            

            //Debug.Log(waypointIndex);
        }
         if ( countNumberMove <= 12 && enemySpawner.GetStep() == 1 && enemySpawner.GetCurrentPathIndex() == 0)
         {   

            Vector3 targetPositon = waypoints[waypointIndex  % 12 ].position;
            
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPositon, delta);

            
            if (transform.position.x == targetPositon.x && transform.position.y == targetPositon.y)
            {
                waypointIndex++;
                countNumberMove++; 
                
                if (countNumberMove == 12)
                {
                    enemySpawner.SetNextPath() ;
                    enemySpawner.ResetStep();
                    countNumberMove = 0 ;
                }
            }
         }

         if (enemySpawner.GetCurrentPathIndex() == 1 && isNeedToMove == true)
        {
            waypoints = waveConfig.GetWayPoints(1);
            Vector3 targetPositon = waypoints[indexNumber].position;

            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPositon, delta);

            if (transform.position.x == targetPositon.x && transform.position.y == targetPositon.y)
            {   
                //isNeedToMove = false;
                timer += Time.deltaTime;

                
                if (timer >= delayTime && !showNotification)
                {
                    enemySpawner.StartMoveUpDown();
                    isNeedToMove = false;
                    showNotification = true;
                }

                //enemySpawner.StartMoveUpDown();

                //isNeedToMove = false;
            }
        }
        //else if (enemySpawner.GetCurrentPathIndex() == 1  && enemySpawner.GetCountGetPositon() >= 12)
        //{
            
        //    Vector3 newTargetPosition = transform.position;
        //    newTargetPosition.y -= distanceToMove;

        //    float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
        //    transform.position = Vector2.MoveTowards(transform.position, newTargetPosition, delta);

        //    if (transform.position.x == newTargetPosition.x && transform.position.y == newTargetPosition.y)
        //    {
        //        isMoveDown = false;

        //    }
        //}
    }


    //private float distanceToMove = 3f;
    private bool isNeedToMove = true;
    private float timer = 0f;
    private bool showNotification = false;
    public float delayTime = 2f;
}
