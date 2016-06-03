using UnityEngine;
using System.Collections;
using U3DSceneEditor;

public class FingerControl : FingerControlBase 
{
    public GameObject mSelection;
    public D3Object mD3Object;

    #region Reaction to gesture events

    override public void FingerGestures_OnFingerTap(int fingerIndex, Vector2 fingerPos, int tapCount)
    {
        if (IsUILayer()) return;

        GameObject[] obj = PickObjects(fingerPos);
        if (obj != null)    mSelection = VBOBufferSingleMgr.Instance.Selection(obj);

        if (mSelection != null)
            mD3Object = mSelection.GetComponent<D3Object>();
        else
            mD3Object = null;
    }

    #region Drag & Drop Gesture

    int dragFingerIndex = -1;

    override public void FingerGestures_OnFingerDragBegin(int fingerIndex, Vector2 fingerPos, Vector2 startPos)
    {
        if (IsUILayer()) return;

        mD3Object = null;

        GameObject[] obj = PickObjects(fingerPos);
        if (obj != null) mSelection = VBOBufferSingleMgr.Instance.Selection(obj);

        if (null != mSelection)
        {
            // remember which finger is dragging dragObject
            dragFingerIndex = fingerIndex;

            mD3Object = mSelection.GetComponent<D3Object>();
        }
    }

    override public void FingerGestures_OnFingerDragMove(int fingerIndex, Vector2 fingerPos, Vector2 delta)
    {
        if (IsUILayer()) return;

        // we make sure that this event comes from the finger that is dragging our dragObject
        if (fingerIndex == dragFingerIndex)
        {
            // update the position by converting the current screen position of the finger to a world position on the Z = 0 plane
            if (mD3Object != null)
            {
                Vector3 newPos = GetWorldPos(fingerPos);

                mD3Object.Draging(fingerPos);
            }
        }
    }

    override public void FingerGestures_OnFingerDragEnd(int fingerIndex, Vector2 fingerPos)
    {
        if (IsUILayer()) return;

        // we make sure that this event comes from the finger that is dragging our dragObject
        if (fingerIndex == dragFingerIndex)
        {
            // reset our drag finger index
            dragFingerIndex = -1;

            mSelection.SendMessage("DragEnd");

        }
    }

    #endregion

    #region Pinch & Pinch Gesture

    override public void FingerGestures_OnPinchMove(Vector2 fingerPos1, Vector2 fingerPos2, float delta)
    {
        base.FingerGestures_OnPinchMove(fingerPos1, fingerPos2, delta);

        if (mD3Object != null)
            mD3Object.Rotation(Vector2.one * delta);
    }

    #endregion //  Pinch & Pinch Gesture

    #endregion

    #region KeyBoard
    public IKeyBoardControl handleLeftKeyBoard;
    public IKeyBoardControl handleRightKeyBoard;
    override public void KeyDown(KeyCode kc)
    {
        switch(kc)
        {
            case KeyCode.LeftShift:
            case KeyCode.RightShift:
                handleLeftKeyBoard.Shift(true);
                handleRightKeyBoard.Shift(true);
                break;
            case KeyCode.LeftAlt:
            case KeyCode.RightAlt:
                handleLeftKeyBoard.Alt(true);
                handleRightKeyBoard.Alt(true);
                break;
            case KeyCode.LeftControl:
            case KeyCode.RightControl:
                handleLeftKeyBoard.Ctrl(true);
                handleRightKeyBoard.Ctrl(true);
                break;
            case KeyCode.W:
                handleLeftKeyBoard.Up();
                break;
            case KeyCode.S:
                handleLeftKeyBoard.Down();
                break;
            case KeyCode.A:
                handleLeftKeyBoard.Left();
                break;
            case KeyCode.D:
                handleLeftKeyBoard.Right();
                break;
            case KeyCode.Q:
                handleLeftKeyBoard.WiseClock();
                break;
            case KeyCode.E:
                handleLeftKeyBoard.Clock();
                break;

            case KeyCode.I:
                handleRightKeyBoard.Up();
                break;
            case KeyCode.K:
                handleRightKeyBoard.Down();
                break;
            case KeyCode.J:
                handleRightKeyBoard.Left();
                break;
            case KeyCode.L:
                handleRightKeyBoard.Right();
                break;
            case KeyCode.U:
                handleRightKeyBoard.WiseClock();
                break;
            case KeyCode.O:
                handleRightKeyBoard.Clock();
                break;
        }
    }
    override public void KeyUp(KeyCode kc)
    {
        switch(kc)
        {
            case KeyCode.LeftShift:
            case KeyCode.RightShift:
                handleLeftKeyBoard.Shift(false);
                handleRightKeyBoard.Shift(false);
                break;
            case KeyCode.LeftAlt:
            case KeyCode.RightAlt:
                handleLeftKeyBoard.Alt(false);
                handleRightKeyBoard.Alt(false);
                break;
            case KeyCode.LeftControl:
            case KeyCode.RightControl:
                handleLeftKeyBoard.Ctrl(false);
                handleRightKeyBoard.Ctrl(false);
                break;
        }
    }
    #endregion  // KeyBoard

}
