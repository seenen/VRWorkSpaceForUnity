using UnityEngine;
using System.Collections;
using U3DSceneEditor;

public class FingerControlBase : MonoBehaviour 
{

    #region Gesture event registration/unregistration
    void OnEnable()
    {
        Debug.Log("Registering finger gesture events from C# script");

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

    bool Key_W = false;
    bool Key_S = false;
    bool Key_D = false;
    bool Key_A = false;
    protected bool Key_Mid = false;
    protected bool Key_Rotation = false;
    protected bool Key_Alt = false;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) Key_Rotation = true;
        if (Input.GetKeyUp(KeyCode.LeftControl)) Key_Rotation = false;

        if (Input.GetKeyDown(KeyCode.LeftAlt)) Key_Alt = true;
        if (Input.GetKeyUp(KeyCode.LeftAlt)) Key_Alt = false;

        if (Input.GetKeyDown(KeyCode.W)) Key_W =(true);
        if (Input.GetKeyUp(KeyCode.W)) Key_W =(false);

        if (Input.GetKeyDown(KeyCode.S)) Key_S = (true);
        if (Input.GetKeyUp(KeyCode.S)) Key_S = (false);

        if (Input.GetKeyDown(KeyCode.A)) Key_A = (true);
        if (Input.GetKeyUp(KeyCode.A)) Key_A = (false);

        if (Input.GetKeyDown(KeyCode.D)) Key_D = (true);
        if (Input.GetKeyUp(KeyCode.D)) Key_D = (false);

        if (Input.GetKeyDown(KeyCode.Mouse2)) Key_Mid = (true);
        if (Input.GetKeyUp(KeyCode.Mouse2)) Key_Mid = (false);

        if (Key_W)  Up();
        if (Key_S)  Down();
        if (Key_A)  Left();
        if (Key_D)  Right();
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
    virtual public void Up()
    {

    }
    virtual public void Down()
    {

    }
    virtual public void Left()
    {

    }
    virtual public void Right()
    {

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
            Debug.Log(hit.collider.name);
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
