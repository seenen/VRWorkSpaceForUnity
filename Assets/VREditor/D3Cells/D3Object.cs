using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace U3DSceneEditor
{

    public class D3Object : MonoBehaviour, IFingerControl, ITriggerEvent
    {
        public BoxCollider boxCollider;
        //public MeshCollider meshCollider;

        /// <summary>
        /// 设置碰撞盒
        /// </summary>
        public void SetCollider()
        {
            //  添加碰撞盒子 
            //meshCollider = gameObject.AddComponent<MeshCollider>();
            boxCollider = gameObject.AddComponent<BoxCollider>();
            //boxCollider.center = Vector3.zero;
            //boxCollider.size = Vector3.one;
        }

        Vector3 namepos = Vector3.zero;

#region Mono
        public D3DataBase mData;

        public List<string> mLinks = new List<string>();

        virtual public void Start()
        {
            gameObject.layer = D3Config.Layer_Vbo;

        }

        virtual public void Update()
        {
            UpdateData();

            //namepos = Global.instance.mUniqueCamera.WorldToScreenPoint(transform.position + transform.up * 2);
        }

        virtual public void OnGUI()
        {
            GUI.Label(new Rect(namepos.x - 50, Screen.height - namepos.y, 150, 100), transform.name);
        }

        virtual public void InitData(D3DataBase data)
        {
            SetCollider();

            mData = data;

            //
            name = mData.Name;

            ////  位置 
            transform.position = mData.Pos;

            //
            Vector3 groudpos = GetRealGourdPos(mData.Pos);

            mData.Pos.y = groudpos.y;

            transform.position = mData.Pos;
        }

        virtual public void UninitData()
        {
            
        }

        float SphereCircle = 1;

        virtual public void UpdateData()
        {
            //mData.Pos = transform.position;

            if (m_sel && boxCollider)
            {
                //GLGizmos.DrawSphere(transform.position + Vector3.up * 0.01f, SphereCircle, Color.gray);

                GLGizmos.DrawCube(transform, boxCollider.bounds.size, Color.white);
            }

            if (Physics.Raycast(transform.position + transform.up * 10000, transform.up * -1, out hit, Mathf.Infinity, 1 << D3Config.Layer_Nav | 1 << D3Config.Layer_MedicalDevices))
            {
                Vector3 v = transform.position;
                v.y = hit.point.y;
                transform.position = v;
            }
        }

        Vector3 BoxColliderSize = Vector3.one;

        protected void SetBoxColliderSize(Vector3 size)
        {
            boxCollider.size = GetBoxColliderSize(size);
            boxCollider.center = new Vector3(0, size.y / 2, 0);

            SphereCircle = Mathf.Max(1, boxCollider.size.x / 2);
        }

        Vector3 GetBoxColliderSize(Vector3 size)
        {
            Vector3 tmp = size;

            tmp.x = Mathf.Max(1, size.x);
            tmp.y = Mathf.Max(1, size.y);
            tmp.z = Mathf.Max(1, size.z);

            return tmp;
        }

        public GameObject getZonetypeObj()
        {
            Transform regiontrans = transform.FindChild(D3Config.REGION_MODEL_NAME);

            return regiontrans.gameObject;
        }

        /// <summary>
        /// 设置模型
        /// </summary>
        /// <param name="res"></param>
        public GameObject SetAttachObj(UnityEngine.GameObject res, Vector3 scale, bool bColor, Color color, bool bAnim, string AnimName)
        {
            if (res == null)
                return null;

            {
                //  删除老旧的 
                Transform regiontrans = transform.FindChild(D3Config.REGION_MODEL_NAME);
                if (regiontrans != null)
                {
                    GameObject zonetypeobj = regiontrans.gameObject;
                    GameObject.DestroyImmediate(zonetypeobj);
                }
            }

            {
                //  新的
                GameObject zonetypeobj = (GameObject)Instantiate(res);
                zonetypeobj.transform.position = transform.position;
                zonetypeobj.transform.localScale = scale;
                zonetypeobj.transform.localRotation = Quaternion.identity;
                zonetypeobj.transform.parent = transform;
                zonetypeobj.name = D3Config.REGION_MODEL_NAME;

                //  如果有颜色，则设置颜色 
                if (bColor)
                {
                    SetColor(zonetypeobj, color);
                }

                //  如果有动画，则循环播放动画 
                if (bAnim)
                {
                    SetAnim(zonetypeobj, AnimName);
                }

                return zonetypeobj;
            }
        }

        /// <summary>
        /// 设置颜色 
        /// </summary>
        /// <param name="color"></param>
        void SetColor(GameObject obj, Color color)
        {
            //  模型 
            {
                MeshRenderer ren = obj.GetComponentInChildren<MeshRenderer>();
                if (ren != null)
                {
                    ren.material = (UnityEngine.Material)GameObject.Instantiate((Material)Resources.Load("Template_M"));
                    ren.material.SetColor("_Color", color);
                }
            }

            //  蒙皮 
            {
                SkinnedMeshRenderer ren = obj.GetComponentInChildren<SkinnedMeshRenderer>();
                if (ren != null)
                {
                    ren.material = (UnityEngine.Material)GameObject.Instantiate((Material)Resources.Load("Template_M"));
                    ren.material.SetColor("_Color", color);
                }
            }
        }

        /// <summary>
        /// 设置默认循环动画
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="AnimName"></param>
        void SetAnim(GameObject obj, string AnimName)
        {
            if (obj.GetComponent<Animation>() == null)
                return;

            if (obj.GetComponent<Animation>()[AnimName] == null)
                return;

            obj.GetComponent<Animation>()[AnimName].wrapMode = WrapMode.Loop;
            obj.GetComponent<Animation>().Play(AnimName);
        }

        RaycastHit hit;

        protected Vector3 GetGroudPos(Vector2 screenPos)
        {
            Ray ray = Global.instance.mUniqueCamera.ScreenPointToRay(screenPos);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << D3Config.Layer_Nav))
            {
                Debug.Log(screenPos + " " + hit.point);

                return hit.point;
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << D3Config.Layer_MedicalDevices))
            {
                Debug.Log(screenPos + " " + hit.point);

                return hit.point;
            }

            return transform.position;
        }

        protected Vector3 GetRealGourdPos(Vector3 pos)
        {
            if (Physics.Raycast(pos + Vector3.up * 1000, -Vector3.up, out hit, Mathf.Infinity, 1 << D3Config.Layer_Nav))
                return hit.point;

            if (Physics.Raycast(pos + Vector3.up * 1000, -Vector3.up, out hit, Mathf.Infinity, 1 << D3Config.Layer_MedicalDevices))
                return hit.point;

            return transform.position;
        }

#endregion

#region IFingerControl
        bool m_bSelection;
        virtual public bool bSelection
        {
            set { m_bSelection = (bool)value; }
            get { return m_bSelection; }
        }

        bool m_bDrag;
        virtual public bool bDrag
        {
            set { m_bDrag = (bool)value; }
            get { return m_bDrag; }
        }

        bool m_bRotation;
        virtual public bool bRotation
        {
            set { m_bRotation = (bool)value; }
            get { return m_bRotation; }
        }

        bool m_bShowShape;
        virtual public bool bShowShape
        {
            set { m_bShowShape = (bool)value; }
            get { return m_bShowShape; }
        }

        virtual public void Draging(Vector2 newpos)
        {
            if (!bDrag) return;

            Vector3 pos = GetGroudPos(newpos);

            transform.position = pos;

            return;
        }

        virtual public void DragEnd()
        {
            if (!bDrag) return;

            //WinFormData.instance.UpdatePosition(gameObject.name, transform.position);

            if (!bRotation) return;

            //WinFormData.instance.UpdateDirection(gameObject.name, transform.rotation);
        }

        virtual public void Rotation(Vector2 delta)
        {
            if (!bRotation) return;

            transform.RotateAround(Vector3.up, delta.x * Time.deltaTime);

            //WinFormData.instance.UpdateDirection(gameObject.name, transform.rotation);
        }
#endregion

#region ITriggerEvent
        virtual public void Remove(string removename)
        {
            if (removename == gameObject.name)
            {
                Debuger.Log("GameObject Destroy " + gameObject.name);

                Destroy(gameObject);
            }

            //  删除连接着的节点
            if (mLinks.Contains(removename))
            {
                Debuger.Log("Links Remove " + removename + " linking from " + gameObject.name);

                mLinks.Remove(removename);
            }
        }

        virtual public void Rename(string oldname, string newname)
        {
            if (oldname == gameObject.name)
            {
                Debuger.Log("GameObject Name From " + oldname + " to " + newname);
               
                gameObject.name = newname;
            }
        }

        virtual public void Link(string root, string target)
        {
            if (root == gameObject.name)
                mLinks.Add(target);
        }

        public bool m_sel = false;

        virtual public void Selection(string selname)
        {
            if (selname == gameObject.name)
            {
                m_sel = true;

                //WinFormData.instance.UpdateSelection(gameObject.name);

                //  测试代码
                //Global.instance.mD3Camera.Look(transform.position);

                Debuger.Log("GameObject Selection " + gameObject.name);

            }
            else
                m_sel = false;
        }

#endregion

    }
}