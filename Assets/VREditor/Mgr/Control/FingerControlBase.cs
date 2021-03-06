using UnityEngine;
using System.Collections;
using U3DSceneEditor;

public class FingerControlBase : MonoBehaviour 
{
    public class KeyEvent
    {
        public KeyCode kc = KeyCode.Backspace;
        public bool IsPress = false;
    }

    public KeyEvent[] mKeyCharactors = new KeyEvent[(int)KeyCode.Menu - (int)KeyCode.Backspace];

    void Start()
    {
        for ( int i = 0; i < (int)KeyCode.Menu - (int)KeyCode.Backspace; ++i)
        {
            mKeyCharactors[i] = new KeyEvent();
            mKeyCharactors[i].kc = (KeyCode)(i + (int)KeyCode.Backspace);
        }
    }

    #region Gesture event registration/unregistration
    void OnEnable()
    {
        Debuger.Log("Registering finger gesture events from C# script");

        // register input events
        FingerGestures.OnFingerLongPress += FingerGestures_OnFingerLongPress;
        FingerGestures.OnFingerTap += FingerGestures_OnFingerTap;
        FingerGestures.OnFingerSwipe += FingerGestures_OnFingerSwipe;
        FingerGestures.OnFingerDragBegin += FingerGestures_OnFingerDragBegin;
        FingerGestures.OnFingerDragMove += FingerGestures_OnFingerDragMove;
        FingerGestures.OnFingerDragEnd += FingerGestures_OnFingerDragEnd;
        FingerGestures.OnFingerUp += FingerGestures_OnFingerUp;
        FingerGestures.OnFingerDown += FingerGestures_OnFingerDown;

        FingerGestures.OnPinchBegin += FingerGestures_OnPinchBegin;
        FingerGestures.OnPinchMove += FingerGestures_OnPinchMove;
        FingerGestures.OnPinchEnd += FingerGestures_OnPinchEnd;
    }

    void OnDisable()
    {
        // unregister finger gesture events
        FingerGestures.OnFingerLongPress -= FingerGestures_OnFingerLongPress;
        FingerGestures.OnFingerTap -= FingerGestures_OnFingerTap;
        FingerGestures.OnFingerSwipe -= FingerGestures_OnFingerSwipe;
        FingerGestures.OnFingerDragBegin -= FingerGestures_OnFingerDragBegin;
        FingerGestures.OnFingerDragMove -= FingerGestures_OnFingerDragMove;
        FingerGestures.OnFingerDragEnd -= FingerGestures_OnFingerDragEnd;
        FingerGestures.OnFingerUp -= FingerGestures_OnFingerUp;
        FingerGestures.OnFingerDown -= FingerGestures_OnFingerDown;

        FingerGestures.OnPinchBegin -= FingerGestures_OnPinchBegin;
        FingerGestures.OnPinchMove -= FingerGestures_OnPinchMove;
        FingerGestures.OnPinchEnd -= FingerGestures_OnPinchEnd;
    }

    protected bool Key_Mid = false;
    protected bool Key_Rotation = false;
    protected bool Key_Alt = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) Key_Rotation = true;
        if (Input.GetKeyUp(KeyCode.LeftControl)) Key_Rotation = false;

        if (Input.GetKeyDown(KeyCode.LeftAlt)) Key_Alt = true;
        if (Input.GetKeyUp(KeyCode.LeftAlt)) Key_Alt = false;

        /// 
        for (int i = 0; i < mKeyCharactors.Length; ++i)
        {
            if (Input.GetKeyDown(mKeyCharactors[i].kc))
                mKeyCharactors[i].IsPress = true;

            if (Input.GetKeyUp(mKeyCharactors[i].kc))
            {
                if (mKeyCharactors[i].IsPress)
                    KeyUp(mKeyCharactors[i].kc);
                
                mKeyCharactors[i].IsPress = false;
            }

            if (mKeyCharactors[i].IsPress)
                KeyDown(mKeyCharactors[i].kc);
        }
    }

    #endregion

    #region Reaction to gesture events

    virtual public void FingerGestures_OnFingerLongPress(int fingerIndex, Vector2 fingerPos)
    {
        if (IsUILayer()) return;
    }

    virtual public void FingerGestures_OnFingerTap(int fingerIndex, Vector2 fingerPos, int tapCount)
    {
        if (IsUILayer()) return;
    }

    virtual public void FingerGestures_OnFingerSwipe(int fingerIndex, Vector2 startPos, FingerGestures.SwipeDirection direction, float velocity)
    {
        if (IsUILayer()) return;
    }

    virtual public void FingerGestures_OnFingerUp(int fingerIndex, Vector2 fingerPos, float timeHeldDown)
    {
        if (IsUILayer()) return;
    }

    virtual public void FingerGestures_OnFingerDown(int fingerIndex, Vector2 fingerPos)
    {
        if (IsUILayer()) return;
    }


    #region Drag & Drop Gesture

    virtual public void FingerGestures_OnFingerDragBegin(int fingerIndex, Vector2 fingerPos, Vector2 startPos)
    {
        if (IsUILayer()) return;
    }

    virtual public void FingerGestures_OnFingerDragMove(int fingerIndex, Vector2 fingerPos, Vector2 delta)
    {
        if (IsUILayer()) return;
    }

    virtual public void FingerGestures_OnFingerDragEnd(int fingerIndex, Vector2 fingerPos)
    {
        if (IsUILayer()) return;
    }

    #endregion

    #region Pinch & Pinch Gesture
    virtual public void FingerGestures_OnPinchBegin(Vector2 fingerPos1, Vector2 fingerPos2)
    {

    }

    virtual public void FingerGestures_OnPinchMove(Vector2 fingerPos1, Vector2 fingerPos2, float delta)
    {
    }

    virtual public void FingerGestures_OnPinchEnd(Vector2 fingerPos1, Vector2 fingerPos2)
    {

    }
    #endregion  //  Pinch & Pinch Gesture

    #region KeyBoard
    virtual public void KeyDown(KeyCode kc)
    {
        Debuger.Log("KeyDown " + kc.ToString());
    }

    virtual public void KeyUp(KeyCode kc)
    {
        Debuger.Log("KeyUp " + kc.ToString());
    }

    #endregion  // KeyBoard

    #endregion

    #region Utils

    // attempt to pick the scene object at the given finger position and compare it to the given requiredObject. 
    // If it's this object spawn its particles.
    bool CheckSpawnParticles(Vector2 fingerPos, GameObject requiredObject)
    {
        GameObject selection = PickObject(fingerPos);

        if (!selection || selection != requiredObject)
            return false;

        return true;
    }

    public Camera currentCamera;
    Ray ray;

    virtual public bool IsUILayer()
    {
        return false;
    }

    public bool IsInScreen(Vector3 touchPos)
    {
        if (touchPos.x < 0) return false;
        if (touchPos.x > Screen.width) return false;
        if (touchPos.y < 0) return false;
        if (touchPos.y > Screen.height) return false;

        return true;
    }

    // Convert from screen-space coordinates to world-space coordinates on the Z = 0 plane
    public Vector3 GetWorldPos(Vector2 screenPos)
    {
        Ray ray = currentCamera.ScreenPointToRay(screenPos);

        // we solve for intersection with z = 0 plane
        float t = -ray.origin.z / ray.direction.z;

        return ray.GetPoint(t);
    }

    // Return the GameObject at the given screen position, or null if no valid object was found
    public GameObject PickObject(Vector2 screenPos)
    {
        //  
        Ray ray = currentCamera.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            return hit.collider.gameObject;

        return null;
    }

    public GameObject[] PickObjects(Vector2 screenPos)
    {
        //  
        Ray ray = currentCamera.ScreenPointToRay(screenPos);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        if (hits.Length == 0)
            return null;

        GameObject[] objs = new GameObject[hits.Length];
        for(int i = 0; i < hits.Length; ++i)
        {
            RaycastHit e = (RaycastHit)hits[i];
            objs[i] = e.collider.gameObject;
        }

        return objs;
    }

    public bool IsPickObject(Vector2 screenPos, GameObject target)
    {
        Ray ray = currentCamera.ScreenPointToRay(screenPos);
        RaycastHit hit;

        int mask = 1 << target.layer;

        if (Physics.Raycast(ray, out hit, mask))
        {
            Debuger.Log(hit.collider.name);
            return true;
        }

        return false;
    }

    public Vector3 PickObjectPos(Vector2 screenPos)
    {
        Ray ray = currentCamera.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            return hit.point;

        return Vector3.zero;
    }
    #endregion
}
