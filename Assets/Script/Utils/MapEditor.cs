using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;



#if UNITY_EDITOR
using UnityEditor;
public class MapEditor
{
    [MenuItem("Tools/GenerateMap")]
    public static void GenerateMap()
    {
        string defaultPath = "prefabs/Map";
        GameObject[] gameObjects = Resources.LoadAll<GameObject>(defaultPath); // path 수정 될 수 있음

        if (gameObjects.Length == 0)
        {
            Debug.LogError($"GenerateMap Failed, path : {defaultPath}");
            return;
        }

        string targetName = "Ground";
        foreach (GameObject go in gameObjects)
        {
            Tilemap tm = Util.FindChild<Tilemap>(go, targetName, true);

            if (tm == null)
            {
                Debug.LogError($"GenerateMap Failed, cannot found child: {targetName}");
                return;
            }

            using (var txt = File.CreateText($"Common/Map/{go.name}"))
            {
                txt.WriteLine(tm.cellBounds.xMin);
                txt.WriteLine(tm.cellBounds.xMax);
                txt.WriteLine(tm.cellBounds.yMin);
                txt.WriteLine(tm.cellBounds.yMax);

                for (int y = tm.cellBounds.yMax; y >= tm.cellBounds.yMin; y--)
                {
                    for (int x = tm.cellBounds.xMin; x <= tm.cellBounds.xMax; x++)
                    {
                        TileBase tile = tm.GetTile(new Vector3Int(x, y, 0));
                        if (tile != null)
                            txt.Write("1");
                        else
                            txt.Write("0");
                    }
                    txt.WriteLine();
                }
            }
        }
    }
}
#endif