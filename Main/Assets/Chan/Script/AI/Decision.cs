using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decision 
{
  static  public GameObject LookingPatrolPoint(AIData data)
    {
        int iRand=Random.Range(0, data.ArrWanderPoint.Length);
        GameObject Target= data.ArrWanderPoint[iRand];
        return Target;
    }





    public void BattleNumber(AIData data)
    {
       



    }
}
