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
    void Up();



    void Down();



    void Left();



    void Right();



    void WiseClock();



    void Clock();
    

                  

    #endregion  // KeyBoard
}
