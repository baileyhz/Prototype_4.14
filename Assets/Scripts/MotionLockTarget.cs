using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionLockTarget : MonoBehaviour
{
    [SerializeField] private GameObject headTans;
	[SerializeField] private Transform playerTrans;
	[SerializeField] private float rotationSpeed = 3f;

	[SerializeField] private List<GameObject> enemiesInCollider = new List<GameObject>();
	public static List<GameObject> frontEnemies = new List<GameObject>();
	public static List<GameObject> inputDirEnemies = new List<GameObject>();

    public static GameObject currntAttackingEnemy;
	private GameObject closestEnemy;

	public static bool isTurn = false;
    public static bool isAttacking = false;


	private void Update()
    {
        if (isAttacking || AttackFocusCam.isAttackFocus || isTurn == false) return;

        if(frontEnemies.Count == 0 && inputDirEnemies.Count == 0) FindClosestEnemy(enemiesInCollider);

        if(PlayerContorller2.playerInput.magnitude == 0)
			inputDirEnemies.Clear();

		if (inputDirEnemies.Count > 0) 
		{
			
			//搜索輸入方向最近的敵人
			frontEnemies.Clear();
			FindClosestEnemy(inputDirEnemies);
			PlayerTurnToClosestEnemy();
		}
        else if(frontEnemies.Count > 0)
        {
			
			//搜索玩家朝向最近的敵人
			inputDirEnemies.Clear();
			FindClosestEnemy(frontEnemies);
			PlayerTurnToClosestEnemy();
		}
       
        if(isTurn) PlayerTurnToInputDir();

	}

	private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
            enemiesInCollider.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
            enemiesInCollider.Remove(other.gameObject);
    }

   
    private void FindClosestEnemy(List<GameObject> enemies)
    {
        if (enemies == null) return;

	    float pEdis_1 = 0;  
        float pEdis_2 = 100;

        foreach (GameObject enemy in enemies)
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

    private void PlayerTurnToInputDir()
	{
        
        //不在戰鬥狀態則return
		if (!PlayerContorller2.isBattle) return;

        //如果沒有敵人轉向玩家輸入的方向
        if (enemiesInCollider.Count == 0 && PlayerContorller2.moveInput.magnitude != 0)
        {
            Vector3 playerInputRotation = PlayerContorller2.playerInput;
			playerInputRotation.y = 0;
			Quaternion playerInputDir = Quaternion.LookRotation(playerInputRotation);
			playerTrans.rotation = Quaternion.Slerp(playerTrans.rotation, playerInputDir, 
                                                            rotationSpeed * Time.deltaTime);
        }

    }

    private void PlayerTurnToClosestEnemy()
    {

		//不在戰鬥狀態則return
		if (!PlayerContorller2.isBattle) return;

		//轉向最靠近的目標
		Vector3 targetDirection = closestEnemy.transform.position - playerTrans.position;
		targetDirection.y = 0f;
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
		playerTrans.rotation = Quaternion.Slerp(playerTrans.rotation, targetRotation, rotationSpeed * Time.deltaTime);
	}

    private void LockAttackingTarget()
    {


        if (currntAttackingEnemy == null || !PlayerContorller2.isBattle)
        {
            return;
        }

       
    }

}
