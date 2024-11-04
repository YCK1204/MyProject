using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

#if UNITY_EDITOR
public class ScriptBatch
{
    const string DEFAULT_TOOL_PATH = "Tools/BuildAndRun";
    const string GAMENAME = "BuiltGame";
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
        string path = Application.dataPath.Replace("Assets", "Builds");
        var scenes = GetAllSceneNames();

        for (int i = 0; i < cnt; i++)
        {
            string fileName = $"{ GAMENAME }{ i}.exe";
            string executionPath = $"{path}/{fileName}";

            BuildPipeline.BuildPlayer(scenes.ToArray(), executionPath, BuildTarget.StandaloneWindows, BuildOptions.None);

            // ºôµå¿¡ Æ÷ÇÔÇÒ ¾À, ºôµåµÉ °æ·Î, 
            Process proc = new Process();
            proc.StartInfo.FileName = executionPath;
            proc.Start();
        }

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