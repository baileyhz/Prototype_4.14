using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public ComboManager comboManager;

    public void EnableDetection()
    {
        comboManager.currentWeapon.ToggleDetection(true);
        MotionLockTarget.isAttacking = true;
        //Debug.Log("EnableDetection");
    }

    public void DisableDetection()
    {
        comboManager.currentWeapon.ToggleDetection(false);
		MotionLockTarget.isAttacking = false;
		//Debug.Log("DisableDetection");
	}

	public void NowCanTurn()
	{
        MotionLockTarget.isTurn = true;
	}

    public void NowCannotTurn()
    {
        MotionLockTarget.isTurn = false;
    }
}
