using UnityEngine;

public interface IFingerControl
{
    bool bSelection { set; get; }
    bool bRotation { set; get; }
    bool bDrag { set; get; }

    void Draging(Vector2 newpos);
    void DragEnd();
    void Rotation(Vector2 delta);

}

public interface IKeyBoardControl
{
    #region KeyBoard
    /// <summary>
    /// 左边
    /// </summary>
    void Up();
    void Down();
    void Left();
    void Right();
    void WiseClock();
    void Clock();
    
    ///// <summary>
    ///// 右边
    ///// </summary>
    //void I();
    //void K();
    //void J();
    //void L();
    //void U();
    //void O();
    

                  

    #endregion  // KeyBoard
}
