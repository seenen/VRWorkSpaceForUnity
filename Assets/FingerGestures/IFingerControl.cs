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
