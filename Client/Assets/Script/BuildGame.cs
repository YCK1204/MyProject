using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

// 1. 상대방 깃 최신 버전 (git pull Sub)
// 2. 자신의 브랜치 버전 최신 상태 동기화 (git pull Server)
// 3. git merge Sub

#if UNITY_EDITOR
public class ScriptBatch
{
    const string DEFAULT_TOOL_PATH = "Tools/BuildAndRun";
    const string GAMENAME = "게임이름";
    [MenuItem(DEFAULT_TOOL_PATH + "/Player1")]
    public static void BuildAndRunPlayer1()
    {
        BuildGame(1);
    }
    [MenuItem(DEFAULT_TOOL_PATH + "/Player2")]
    public static void BuildAndRunPlayer2()
    {
        BuildGame(2);
    }
    [MenuItem(DEFAULT_TOOL_PATH + "/Player3")]
    public static void BuildAndRunPlayer3()
    {
        BuildGame(3);
    }
    [MenuItem(DEFAULT_TOOL_PATH + "/Player4")]
    public static void BuildAndRunPlayer4()
    {
        BuildGame(4);
    }
    public static void BuildGame(int cnt)
    {
        string path = Application.dataPath + "/Builds";

        Debug.Log(path);

        //int height = 720;
        //int width = 480;

        //Screen.SetResolution(width, height, false);
        //string path = EditorUtility.SaveFolderPanel("제목입니다", "빌드", "폴더명"); // program title, 빌드 경로, 폴더명


        //var scenes = GetAllSceneNames();

        //for (int i = 0; i < cnt; i++)
        //{
        //    string executionPath = $"{path}/{GAMENAME}{i}.exe";

        //    BuildPipeline.BuildPlayer(scenes.ToArray(), executionPath, BuildTarget.StandaloneWindows, BuildOptions.None);

        //    // 빌드에 포함할 씬, 빌드될 경로, 
        //    Process proc = new Process();
        //    proc.StartInfo.FileName = executionPath;
        //    proc.Start();
        //}

    }
    public static List<string> GetAllSceneNames()
    {
        List<string> list = new List<string>();

        int sceneCnt = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < sceneCnt; i++)
            list.Add(SceneUtility.GetScenePathByBuildIndex(i));
        return list;
    }
}
#endif