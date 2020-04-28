using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//追擊判斷
public class EnterInto
{
    /// <summary>
    ///追擊範圍的判斷並且確認是否追擊
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    static public bool EnterRange(AIData data)
    {
        //抓取怪物位置
        GameObject ObjEnemy = data.m_ObjEnemy;
        Vector3 cpos = ObjEnemy.transform.position;

        //抓取怪物與目標的距離並轉換成長度
        Vector3 vec = data.m_ArrayVTarget - cpos;
        float m_fVec = vec.magnitude;

        //data資料不為空的時候
        if (data != null)
        {
            //目標是否在怪物背後(怪物背後的距離為m_fPursuitRange * 0.3f)
            if (m_fVec < (data.m_fPursuitRange * 0.3f))
            {
                //怪物進入攻擊範圍
                if (m_fVec < 10)
                {//速度要為0最好改成遞減 追擊為false
                    data.m_fMinSpeed = 0.0f;
                    data.m_bChase = false;

                }
                //目標脫離攻擊範圍 進入追擊 速度遞增恢復 追擊為True
                data.m_fMinSpeed = 0.2f;
                data.m_bChase = true;
            }
            //目標不在最怪物背後
            else if (m_fVec > (data.m_fPursuitRange * 0.3f))
            {
                //確認是否有目標進入怪物扇形偵測範圍230度偵測距離m_fPursuitRange*0.6可修改(目前為探針掃描待修*****改成範圍探測*****)
                if (LookRay.Look(data, 10f, 230, data.m_fRotatePerSecond, data.m_fPursuitRange * 0.6f, Color.blue))
                {
                    //追擊為true
                    data.m_bChase = true;
                }
                else
                {
                    //確認是否有目標進入怪物扇形偵測範圍90度偵測距離m_fPursuitRange(目前為探針掃描待修*****改成範圍探測*****)
                    if (LookRay.Look(data, 5f, 90, data.m_fRotatePerSecond, data.m_fPursuitRange, Color.blue))
                    {
                        //追擊為true
                        data.m_bChase = true;
                    }
                    else
                    {
                        //追擊為false
                        data.m_bChase = false;
                    }
                }
            }

        }
        //返回追擊數據
        return data.m_bChase;

    }

}
