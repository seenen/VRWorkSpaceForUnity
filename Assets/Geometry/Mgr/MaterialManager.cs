﻿using UnityEngine;
using System.Collections;

public sealed class MaterialManager
{
    static Material beginMat;
    static Material endMat;
    static Material matPhongDisp;


    static MaterialManager()
    {
        beginMat = (Material)Resources.Load("begin_Mat");
        endMat = (Material)Resources.Load("end_Mat");
        matPhongDisp = (Material)Resources.Load("matPhongDisp");
    }

    static Color cur = Color.white;

    public static Material GetBeginMat()
    {
        //return matPhongDisp;

        cur = new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f));
        Material ins = (Material)GameObject.Instantiate(beginMat);
        ins.SetColor("_Color", cur);

        return ins;
    }

    public static Material GetEndMat()
    {
        //return matPhongDisp;

        Material ins = (Material)GameObject.Instantiate(endMat);
        ins.SetColor("_Color", cur);

        return ins;
    }
}
