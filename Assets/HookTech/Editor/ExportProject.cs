using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class ExportProject : EditorWindow
{
    static ExportProject thisWindow;

    [MenuItem("File/Build Settings... %B", false, 0)]
    public static void Main()
    {
        thisWindow = (ExportProject)EditorWindow.GetWindow(typeof(ExportProject));
        thisWindow.title = "ExportProject";
        thisWindow.ShowPopup();
        thisWindow.Focus();
    }

    public GUISkin skin;

    void OnGUI()
    {
        if (skin == null)
            skin = new GUISkin();

        if (GUILayout.Button("Build_Hook", GUILayout.ExpandWidth(true)))
        {
            //string path = EditorUtility.OpenFolderPanel("Select Saved Path", "", "Heros");

            Debug.Log(EditorApplication.applicationPath);
            Debug.Log(Application.dataPath);

            //if (!string.IsNullOrEmpty(path))
            {
                BuildSend();
                BuildHook();
            }
        }

        if (GUILayout.Button("Build_UserContorl", GUILayout.ExpandWidth(true)))
        {
            BuildUserContorl();
        }

    }

    BuildOptions options = BuildOptions.ShowBuiltPlayer | BuildOptions.AcceptExternalModificationsToPlayer;

    #region BuildUserContorl
    void BuildUserContorl()
    {
        BuildPipeline.BuildPlayer(new string[1] { "Assets/UserContorl/UserContorl.unity" },
                Application.dataPath + "/../../VRWorkSpace/Output/UserControl",
                BuildTarget.WebPlayer,
                options);
    }
    #endregion

    #region BuildHook

    public void BuildSend()
    {
        PlayerSettings.productName = "发送窗体";
        PlayerSettings.runInBackground = true;
        PlayerSettings.displayResolutionDialog = ResolutionDialogSetting.Disabled;

        AssetDatabase.Refresh();

        BuildPipeline.BuildPlayer(new string[1] { "Assets/HookTech/SendMsg.unity" },
                Application.dataPath + "/../../VRWorkSpace/Output/SendMsg.exe",
                BuildTarget.StandaloneWindows,
                options);
    }

    public void BuildHook()
    {
        PlayerSettings.productName = "钩子窗体";
        PlayerSettings.runInBackground = true;
        PlayerSettings.displayResolutionDialog = ResolutionDialogSetting.Disabled;

        AssetDatabase.Refresh();

        BuildPipeline.BuildPlayer(new string[1] { "Assets/HookTech/HookMsg.unity" },
                Application.dataPath + "/../../VRWorkSpace/Output/HookMsg.exe",
                BuildTarget.StandaloneWindows,
                options);
    }
    #endregion BuildHook

}
