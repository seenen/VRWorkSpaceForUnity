using UnityEngine;
using System.Collections;
using LibraryGeometryFormat;
using System;
using System.Globalization;
using System.Collections.Generic;

public class ObjModelRawAnly
{
    /* OBJ file tags */
    private const string O = "o";
    private const string G = "g";
    private const string V = "v";
    private const string VT = "vt";
    private const string VN = "vn";
    private const string F = "f";
    private const string MTL = "mtllib";
    private const string UML = "usemtl";

    /* MTL file tags */
    private const string NML = "newmtl";
    private const string NS = "Ns"; // Shininess
    private const string KA = "Ka"; // Ambient component (not supported)
    private const string KD = "Kd"; // Diffuse component
    private const string KS = "Ks"; // Specular component
    private const string D = "d";   // Transparency (not supported)
    private const string TR = "Tr"; // Same as 'd'
    private const string ILLUM = "illum"; // Illumination model. 1 - diffuse, 2 - specular
    private const string MAP_KD = "map_Kd"; // Diffuse texture (other textures are not supported)

    private string basepath;
    private string mtllib;
    public GeometryBuffer buffer;

    ObjModelRaw mObjModelRaw = null;

    public ObjModelRawAnly(ObjModelRaw omr)
    {
        mObjModelRaw = omr;

        buffer = new GeometryBuffer();
    }

    public void SetGeometryData(string data)
    {
        string[] lines = data.Split("\n\r".ToCharArray());

        for (int i = 0; i < lines.Length; i++)
        {
            string l = lines[i];

            if (l.IndexOf("#") != -1) l = l.Substring(0, l.IndexOf("#"));
            string[] p = l.Split(" ".ToCharArray());

            switch (p[0])
            {
                case O:
                    buffer.PushObject(p[1].Trim());
                    break;
                case G:
                    buffer.PushGroup(p[1].Trim());
                    break;
                case V:
                    buffer.PushVertex(new Vector3(cf(p[1]), cf(p[2]), cf(p[3])));
                    break;
                case VT:
                    buffer.PushUV(new Vector2(cf(p[1]), cf(p[2])));
                    break;
                case VN:
                    buffer.PushNormal(new Vector3(cf(p[1]), cf(p[2]), cf(p[3])));
                    break;
                case F:
                    for (int j = 1; j < p.Length; j++)
                    {
                        string[] c = p[j].Trim().Split("/".ToCharArray());
                        FaceIndices fi = new FaceIndices();
                        fi.vi = ci(c[0]) - 1;
                        if (c.Length > 1 && c[1] != "") fi.vu = ci(c[1]) - 1;
                        if (c.Length > 2 && c[2] != "") fi.vn = ci(c[2]) - 1;
                        buffer.PushFace(fi);
                    }
                    break;
                case MTL:
                    mtllib = p[1].Trim();
                    break;
                case UML:
                    buffer.PushMaterialName(p[1].Trim());
                    break;
            }
        }

        // buffer.Trace();
    }

    private float cf(string v)
    {
        return Convert.ToSingle(v.Trim(), new CultureInfo("en-US"));
    }

    private int ci(string v)
    {
        return Convert.ToInt32(v.Trim(), new CultureInfo("en-US"));
    }

    public bool hasMaterials
    {
        get
        {
            return mtllib != null;
        }
    }

    /* ############## MATERIALS */
    public List<MaterialData> materialData;
    public class MaterialData
    {
        public string name;
        public Color ambient;
        public Color diffuse;
        public Color specular;
        public float shininess;
        public float alpha;
        public int illumType;
        public string diffuseTexPath;
        public Texture2D diffuseTex;
    }

    private void SetMaterialData(string data)
    {
        string[] lines = data.Split("\n".ToCharArray());

        materialData = new List<MaterialData>();
        MaterialData current = new MaterialData();

        for (int i = 0; i < lines.Length; i++)
        {
            string l = lines[i];

            if (l.IndexOf("#") != -1) l = l.Substring(0, l.IndexOf("#"));
            string[] p = l.Split(" ".ToCharArray());

            switch (p[0])
            {
                case NML:
                    current = new MaterialData();
                    current.name = p[1].Trim();
                    materialData.Add(current);
                    break;
                case KA:
                    current.ambient = gc(p);
                    break;
                case KD:
                    current.diffuse = gc(p);
                    break;
                case KS:
                    current.specular = gc(p);
                    break;
                case NS:
                    current.shininess = cf(p[1]) / 1000;
                    break;
                case D:
                case TR:
                    current.alpha = cf(p[1]);
                    break;
                case MAP_KD:
                    current.diffuseTexPath = p[1].Trim();
                    break;
                case ILLUM:
                    current.illumType = ci(p[1]);
                    break;

            }
        }
    }

    public Material GetMaterial(MaterialData md)
    {
        Material m;

        if (md.illumType == 2)
        {
            m = new Material(Shader.Find("Specular"));
            m.SetColor("_SpecColor", md.specular);
            m.SetFloat("_Shininess", md.shininess);
        }
        else
        {
            m = new Material(Shader.Find("Diffuse"));
        }

        m.SetColor("_Color", md.diffuse);

        if (md.diffuseTex != null) m.SetTexture("_MainTex", md.diffuseTex);

        return m;
    }

    private Color gc(string[] p)
    {
        return new Color(cf(p[1]), cf(p[2]), cf(p[3]));
    }

}
