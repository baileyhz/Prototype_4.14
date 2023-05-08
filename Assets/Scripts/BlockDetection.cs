using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




public class BlockDetection : MonoBehaviour
{
    
    private Collider unitCol;
    private bool isFrontCol, isMidCol, isHindCol = false;
    public string currentBlock;
    private float fontTime, MidTime, hindTime = 0;

    //Timer
    private float timeElapsed = 0.0f;
    
    public class Block
    {
        public const string FrontBlock = "FrontBlock";
        public const string MidBlock = "MidBlock";
        public const string HindBlock = "HindBlock";
    }


    private void Start()
    {
        
        unitCol = GetComponent<Collider>();

    }

    private void Update()
    {
 
        timeElapsed += Time.deltaTime;

        BlockDecide();
        
        Debug.Log("F:" + fontTime + " , " + "M:" + MidTime + " , " + "H:" + hindTime );
     }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Enter");
        switch (other.tag)
        {
            case Block.FrontBlock:
                fontTime = timeElapsed;
                isFrontCol = true;
                break;
            case Block.MidBlock:
                MidTime = timeElapsed;
                isMidCol = true;
                break;
            case Block.HindBlock:
                hindTime = timeElapsed;
                isHindCol = true;
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Stay");
        switch (other.tag)
        {
            case Block.FrontBlock:
                isFrontCol = true;
                break;
            case Block.MidBlock:
                isMidCol = true;
                break;
            case Block.HindBlock:
                isHindCol = true;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Exit");
        switch (other.tag)
        {
            case Block.FrontBlock:
                fontTime = 0.0f;
                isFrontCol = false;
                break;
            case Block.MidBlock:
                MidTime = 0.0f;
                isMidCol = false;
                break;
            case Block.HindBlock:
                hindTime = 0.0f;
                isHindCol = false;
                break;
        }
    }

    private void BlockDecide()
    {
        

        if (isFrontCol && isMidCol)
        {
            if (fontTime>MidTime)
            {
                currentBlock = Block.FrontBlock;
            }
            else
            {
                currentBlock = Block.MidBlock;
            }
            timeElapsed = 0.0f;
            return;
        }

        if (isMidCol && isHindCol)
        {
            if (MidTime>hindTime)
            {
                currentBlock = Block.MidBlock;
            }
            else
            {
                currentBlock = Block.HindBlock;
            }
            timeElapsed = 0.0f;
            return;
        }

        if (isFrontCol)
            currentBlock = Block.FrontBlock;
        if (isMidCol)
            currentBlock = Block.MidBlock;
        if (isHindCol)
            currentBlock = Block.HindBlock;


        
    }

}
