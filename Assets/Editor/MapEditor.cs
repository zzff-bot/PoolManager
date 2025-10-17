using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

[CustomEditor(typeof (Map))]
public class MapEditor : Editor
{
    List<FileInfo> levelFiles;

    private int curSelectIdx = -1;

    Map map;
    Level level;

    private string[] modeToolBar = new string[] { "新建关卡", "编辑关卡" };

    //Editor代码可以访问Runtime代码
    //Runtime代码无法访问Editor代码 

    int curMode = 0;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        //判断游戏是否运行
        if (!Application.isPlaying) return;

        map = target as Map;

        EditorGUILayout.BeginHorizontal();
        curMode = GUILayout.Toolbar(curMode, modeToolBar);
        EditorGUILayout.EndHorizontal();
        switch (curMode)
        {
            case 0:
                OnCreateMode();
                break;
            case 1:
                OnEditMode();
                break;
            default:
                break;
        }
    }

    //创建新的关卡面板
    string newLevelFileName = "";
    string levelName;
    string initScore;
    SerializedProperty CardImage;
    SerializedProperty Background;
    SerializedProperty TempRoad;

    private void OnEnable()
    {
        Background = serializedObject.FindProperty("Background");
        CardImage = serializedObject.FindProperty("CardImage");     //获取当前指向对象的属性
        TempRoad = serializedObject.FindProperty("TempRoad");
    }

    int roundCount = 0;
    List<int> MonsterIdList = new List<int>();
    List<int> MonsterCountList = new List<int>();
    private void OnCreateMode()
    {
        if(levelFiles == null)
        {
            LoadLevelFiles();
            newLevelFileName = "level" + levelFiles.Count + ".xml";
        }

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("关卡文件名");
        newLevelFileName = GUILayout.TextField(newLevelFileName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("关卡名字:");
        levelName = GUILayout.TextField(levelName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("初始分数:");
        initScore = GUILayout.TextField(initScore);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.PropertyField(CardImage);       //显示编辑属性框
        serializedObject.ApplyModifiedProperties();     //允许编辑属性

        //获取当前绑定的图片
        Texture2D curCardImage = CardImage.objectReferenceValue as Texture2D;
        
        EditorGUILayout.PropertyField(Background);
        serializedObject.ApplyModifiedProperties();
        
        EditorGUILayout.PropertyField(TempRoad);
        serializedObject.ApplyModifiedProperties();

        //
        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("回合信息");
        GUILayout.Label("怪物总波数");

        EditorGUILayout.EndHorizontal();

        int newRoundCount = EditorGUILayout.IntField(roundCount);

        if(newRoundCount > roundCount)
        {
            for (int i = roundCount; i < newRoundCount; i++)
            {
                MonsterIdList.Add(0);
                MonsterCountList.Add(0);
            }
        }
        roundCount = newRoundCount;
        if (roundCount > 0)
        {
            for (int i = 0; i < roundCount; i++)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("怪物ID");
                MonsterIdList[i] = EditorGUILayout.IntField(MonsterIdList[i]);

                GUILayout.Label("怪物数量");
                MonsterCountList[i] = EditorGUILayout.IntField(MonsterCountList[i]);
                EditorGUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("新建关卡"))
        {
            level = new Level();
            level.Name = levelName;            

            if (!int.TryParse(initScore, out level.InitScore))
            {
                EditorUtility.DisplayDialog("新建错误", "初始分数输入不合法新建失败", "确定");
                return;
            }

            if(CardImage.objectReferenceValue != null)
                level.CardImage = (CardImage.objectReferenceValue as Texture2D).name + ".png";

            if (Background.objectReferenceValue != null)
                level.Background = (Background.objectReferenceValue as Texture2D).name + ".png";

            if (TempRoad.objectReferenceValue != null)
                level.Road = (TempRoad.objectReferenceValue as Texture2D).name + ".png";

            if(roundCount > 0)
            {
                //构建回合信息
                for (int i = 0; i < roundCount; i++)
                {
                    Round r = new Round(MonsterIdList[i], MonsterCountList[i]);
                    level.Rounds.Add(r);
                }
            }

            string path = Const.LevelConfigPath + newLevelFileName;
            if (File.Exists(path))
            {
                EditorUtility.DisplayDialog("新建错误", "关卡文件已存在", "确定");
                return;
            }
            Utils.SaveLevel(path, level);
            EditorUtility.DisplayDialog("新建成功", "新建关卡成功", "确定");

            //AssetDatabase：编辑器环境，非运行时，加载资源，读取资源路劲，读取资源以来关系的类
            AssetDatabase.Refresh();    //刷新一下编辑器
        }
        EditorGUILayout.EndHorizontal();
    }

    //编辑关卡内容
    private void OnEditMode()
    {
        EditorGUILayout.BeginHorizontal();
        int selectIndex = EditorGUILayout.Popup(curSelectIdx, GetLevelFiles());
        if (selectIndex != curSelectIdx)
        {
            //保存选项
            curSelectIdx = selectIndex;
            LoadLevel();
        }
        if (GUILayout.Button("读取关卡"))
        {
            LoadLevelFiles();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("清除所有路径点"))
        {
            if (map != null && level != null)
                map.ClearPath();
        }

        if (GUILayout.Button("清除所有放置点"))
        {
            if (map != null && level != null)
                map.ClearHolder();
        }

        if (GUILayout.Button("恢复关卡设置"))
        {
            if (map != null && level != null)
                map.LoadLevel(level);
        }
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("保存关卡"))
        {
            SvaeLevel();
        }
    }

    private void LoadLevel()
    {
        //加载选中的关卡
        //levelFiles[selectIndex].Name
        string fileName = levelFiles[curSelectIdx].Name;

        //Level
        level = new Level();
        Utils.LoadLevel(fileName, ref level);

        //设置给Map对象的LoadLevel()函数去生成游戏信息
        //这个对象需要在游戏运行中才存在
        map.LoadLevel(level);
    }

    private void SvaeLevel()
    {
        // 保存数据
        //level

        Level saveLevel = new Level();

        saveLevel.Name = level.Name;
        saveLevel.CardImage = level.CardImage;
        saveLevel.Road = level.Road;
        saveLevel.Background = level.Background;
        saveLevel.InitScore = level.InitScore;

        foreach (Tile tile in map.Road)
        {
            Point p = new Point(tile.X, tile.Y);
            saveLevel.Path.Add(p);
        }

        foreach (Tile tile in map.Grid)
        {
            if (tile.CanHold)
            {
                Point p = new Point(tile.X, tile.Y);
                saveLevel.Holder.Add(p);
            }
        }

        saveLevel.Rounds = level.Rounds;

        Utils.SaveLevel(levelFiles[curSelectIdx].FullName, saveLevel);
    }
    private void LoadLevelFiles()
    {
        levelFiles = Utils.GetAllLevelFiles();
    }

    private string[] GetLevelFiles()
    {
        if (levelFiles == null || levelFiles.Count <= 0) return new string[0];
        string[] result = new string[levelFiles.Count];
        for (int i = 0; i < levelFiles.Count; i++)
        {
            result[i] = levelFiles[i].Name;
        }
        return result;
    }
}
