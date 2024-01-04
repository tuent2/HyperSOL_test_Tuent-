using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotting : MonoBehaviour
{

    [SerializeField] GameObject projectTileFrefab;
    [SerializeField] GameObject projectTileFrefab1;
    [SerializeField] GameObject projectTileFrefab2;
    
    [SerializeField] float projectTileLifeTime = 5f;



    //[SerializeField] bool useAI;
     float basefiringRate = 0.2f;
     float firingRateVariance = 0f;
     float minimumFiringRate = 0.1f;
    //[HideInInspector] public bool isFiring;
    Coroutine firingCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        //if (useAI)
        //{
        //    isFiring = true;
        //}
        StartCoroutine(FireContinuously());
    }

    //void Update()
    //{
    //    Fire();
    //}



    IEnumerator FireContinuously()
    {
        while (true)
        {

            //float TimeToNextProjectTile = Random.Range(basefiringRate - firingRateVariance, basefiringRate + firingRateVariance);
            //TimeToNextProjectTile = Mathf.Clamp(TimeToNextProjectTile, minimumFiringRate, float.MaxValue);
            yield return new WaitForSeconds(1f);

            GameObject instance = Instantiate(projectTileFrefab
            , transform.position
            , Quaternion.identity);

            GameObject instance1 = Instantiate(projectTileFrefab1
            , transform.position
            , Quaternion.identity);

            GameObject instance2 = Instantiate(projectTileFrefab2
            , transform.position
            , Quaternion.identity);


            Destroy(instance, projectTileLifeTime);
            Destroy(instance1, projectTileLifeTime);
            Destroy(instance2, projectTileLifeTime);



        }
    }
}
