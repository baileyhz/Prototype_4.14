﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using Unity.Mathematics;
using System.Linq;

public class PlayerContorller2 : MonoBehaviour
{
	private Animator animator;
	private Transform playerTransform;
	private CharacterController characterController;
	private Quaternion targetRotation;
	

	[SerializeField] private Transform enemyTrans;

	[SerializeField] private Transform camTransform;

	[SerializeField] private float maxAngle = 120f;
	[SerializeField] private float turnSpeed = 100f;
	[SerializeField] private float walkSpeed = 1.5f;
	[SerializeField] private float runSpeed = 5.2f;
	[SerializeField] private float turnDelay = 2f;
	[SerializeField] private float BattleSpeed = 3.8f;

	[Header("虛擬相機")]
	[SerializeField] CinemachineFreeLook thirPersonCam;
	[SerializeField] CinemachineVirtualCamera lockOnTargetCam;
	[SerializeField] CinemachineFreeLook BatteleCam;

	[SerializeField] private float rayLength = 5f;
	[SerializeField] private float searchAngle = 45f;
	[SerializeField] private int accurate;

	private bool isJumping;
	private bool isRunning;
	private bool isTurning;
	private bool isAir;
	private bool canTurn = true;
	private bool playerMoveInput = false;

	public static bool isBattle = false;

	//玩家輸入全域
	public static Vector2 moveInput;
	public static Vector3 playerInput;

	//Hash
	private int SpeedHash;
	private int TurnHash;
	private int BattleHash;
	private int BattleSpeedXHash;
	private int BattleSpeedZHash;
	private int AttackTriggerYHash;

	//index
	private int BattleLayer;



	private void OnEnable()
	{
		CameraSwitcher.Register(thirPersonCam);
		CameraSwitcher.Register(lockOnTargetCam);
		CameraSwitcher.SwitchCamera(thirPersonCam);
	}
	private void OnDisable()
	{
		CameraSwitcher.UnRegister(thirPersonCam);
		CameraSwitcher.UnRegister(lockOnTargetCam);
	}


	private void Start()
	{
		animator = GetComponent<Animator>();
		playerTransform = transform;
		characterController = GetComponent<CharacterController>();
		SpeedHash = Animator.StringToHash("MoveSpeed");
		TurnHash = Animator.StringToHash("Turn");
		BattleHash = Animator.StringToHash("isBattle");
		BattleSpeedXHash = Animator.StringToHash("BattleSpeedX");
		BattleSpeedZHash = Animator.StringToHash("BattleSpeedZ");
		AttackTriggerYHash = Animator.StringToHash("Attack");
		BattleLayer = animator.GetLayerIndex("BattleMove");
		Cursor.lockState = CursorLockMode.Locked;

	}

	private void Update()
	{

		if (isBattle)
		{
			BattleMove();
		}
		else
		{
			// 設定動畫參數
			AnimatorSet();
			// 計算視角旋轉並轉向視角目標
			AngleCalculationRotation();
		}



	}

	#region GetInput

	// 取得移動輸入
	public void GetMoveInput(InputAction.CallbackContext ctx)
	{
		moveInput = ctx.ReadValue<Vector2>();
	}

	// 取得跳躍輸入
	public void GetJumpInput(InputAction.CallbackContext ctx)
	{
		isJumping = ctx.ReadValueAsButton();
	}

	// 取得奔跑輸入
	public void GetRunInput(InputAction.CallbackContext ctx)
	{
		isRunning = ctx.ReadValueAsButton();
	}

	public void GetBattleInput(InputAction.CallbackContext ctx)
	{
		if (ctx.ReadValue<float>() == 0)
		{
			isBattle = !isBattle;
			float battleFlot = isBattle ? 1f : 0;
			animator.SetLayerWeight(BattleLayer, battleFlot);
			//animator.SetBool(BattleHash,isBattle);
			if (CameraSwitcher.IsActivaCamera(thirPersonCam))
			{
				CameraSwitcher.SwitchCamera(BatteleCam);
			}
			else if (CameraSwitcher.IsActivaCamera(BatteleCam))
			{
				CameraSwitcher.SwitchCamera(thirPersonCam);
			}
		}
	}



	#endregion

	// 計算旋轉
	private void AngleCalculationRotation()
	{
		// 如果沒有移動輸入，則返回
		if (moveInput == Vector2.zero)
		{
			return;
		}

		// 取得相機的forward向量
		Vector3 cameraDirection = camTransform.forward;
		cameraDirection.y = 0f;

		// 取得玩家前進方向的向量
		Vector3 playerDirection = playerTransform.forward;
		playerDirection.y = 0f;

		// 計算相機與玩家前進方向的夾角
		float angle = Vector3.Angle(cameraDirection, playerDirection);

		// 計算目標旋轉角度
		Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);
		moveDirection = Quaternion.LookRotation(cameraDirection) * moveDirection;
		moveDirection.y = 0f;
		Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

		// 旋轉至目標角度
		playerTransform.rotation = Quaternion.RotateTowards(playerTransform.rotation, targetRotation, 
															turnSpeed * Time.deltaTime);

	}




	// 設定動畫參數
	private void AnimatorSet()
	{
		// 設定移動速度
		float speed = isRunning ? runSpeed : walkSpeed;


		animator.SetFloat(SpeedHash, moveInput.magnitude * speed, 0.1f, Time.deltaTime);

	}

	private void BattleMove()
	{



		Vector3 camForwardProjection = new Vector3(camTransform.forward.x, 0, camTransform.forward.z).normalized;
		playerInput = camForwardProjection * moveInput.y + camTransform.right * moveInput.x;
		

		Debug.DrawRay(playerTransform.position, playerInput, Color.green);
		Debug.DrawRay(playerTransform.position, playerTransform.forward, Color.red);



		if (playerInput.magnitude > 0)
		{
			MotionLockTarget.inputDirEnemies.Clear();
			Dichotomy(Quaternion.AngleAxis(searchAngle * -0.5f, transform.up) * playerInput,
					  Quaternion.AngleAxis(searchAngle * 0.5f, transform.up) * playerInput,
					  accurate,MotionLockTarget.inputDirEnemies);
		}
		else
		{
			MotionLockTarget.frontEnemies.Clear();
			Dichotomy(Quaternion.AngleAxis(searchAngle * -0.5f, transform.up) * playerTransform.forward,
					  Quaternion.AngleAxis(searchAngle * 0.5f, transform.up) * playerTransform.forward,
					  accurate,MotionLockTarget.frontEnemies);
		}


		//playerInput = playerTransform.InverseTransformVector(playerInput);

		animator.SetFloat(BattleSpeedZHash, playerTransform.InverseTransformVector(playerInput).x 
						* BattleSpeed * 2, 0.1f, Time.deltaTime);
		animator.SetFloat(BattleSpeedXHash, playerTransform.InverseTransformVector(playerInput).z 
						* BattleSpeed * 2, 0.1f, Time.deltaTime);

	}

	

	private void Dichotomy(Vector3 leftVec, Vector3 rightVec, int acc,List<GameObject> obj)
	{

		if (acc == 0) return;


		Vector3 middleDirection = (leftVec + rightVec).normalized;
		Debug.DrawRay(new Vector3(playerTransform.position.x, 1f, playerTransform.position.z), 
					  middleDirection * rayLength, Color.blue);
		RaycastHit[] hits = Physics.RaycastAll(new Vector3(playerTransform.position.x,2f,playerTransform.position.z),
											   middleDirection * rayLength);
		//取得射線檢測物件
		foreach (RaycastHit hit in hits)
		{
			GameObject enemy = hit.collider.gameObject;
			if (enemy.tag == "Enemy") obj.Add(enemy);
		}
		IEnumerable<GameObject> distinc = obj.Distinct();
		
		--acc;
		Dichotomy(leftVec, middleDirection, acc,obj);
		Dichotomy(middleDirection, rightVec, acc, obj);
	}



}
