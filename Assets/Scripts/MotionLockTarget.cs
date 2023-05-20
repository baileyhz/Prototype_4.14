using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionLockTarget : MonoBehaviour
{
    [SerializeField] private GameObject headTans;
    [SerializeField] private List<GameObject> enemiesInRange = new List<GameObject>();

    [SerializeField] private Transform playerTrans;

    [SerializeField] private float rotationSpeed = 3f;

    private GameObject closestEnemy;



    #region GetInput



    #endregion

    private void Start()
    {
        
    }
    private void Update()
    {
        MinEnemyDistance();
        PlayerTurnToEnemy();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
            enemiesInRange.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
            enemiesInRange.Remove(other.gameObject);
    }

   
    private void MinEnemyDistance()
    {
        float pEdis_1 = 0;
        float pEdis_2 = 10;

        foreach (GameObject enemy in enemiesInRange)
        {
            pEdis_1 = (playerTrans.position - enemy.transform.position).magnitude;
            if (pEdis_2 > pEdis_1)
            {
                closestEnemy = enemy;
                pEdis_2 = pEdis_1;
                Debug.Log("最近的敵人：" + closestEnemy.name);
            }
        }

       
    }

    private void PlayerTurnToEnemy()
    {
        if (enemiesInRange == null || !PlayerContorller2.isBattle)
        {
            return;
        }
        Vector3 target = closestEnemy.transform.position;
        Vector3 targetDirection = target - playerTrans.position;
        targetDirection.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        playerTrans.rotation = Quaternion.Slerp(playerTrans.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

}
