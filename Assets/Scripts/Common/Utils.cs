using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

//GmaeUtils,StringUtils,TimeUtils...
public static class Utils
{
    //加载配置表文件，解析内容，输出对应的Level对象
    public static void LoadLevel(string path,ref Level level)
    {
        path = Const.LevelConfigPath + path;
        //加载配置表
        XmlDocument doc = new XmlDocument();
        doc.Load(path);

        //读取基础数据
         XmlElement root = doc.DocumentElement;
        level.Name = root.SelectSingleNode("Name").InnerText;
        level.CardImage = root.SelectSingleNode("CardImage").InnerText;
        level.Background = root.SelectSingleNode("Background").InnerText;
        level.Road = root.SelectSingleNode("Rounds").InnerText;
        level.InitScore = int.Parse(root.SelectSingleNode("InitScore").InnerText);

        //读取可放置区域
        XmlNodeList holderNodeList = root.SelectNodes("Holder/Point");
        foreach (XmlNode node in holderNodeList)
        {
            Point p = new Point(int.Parse(node.Attributes["X"].Value), int.Parse(node.Attributes["Y"].Value));
            level.Holder.Add(p);
        }

        //读取路径点
        XmlNodeList pathNodeList = root.SelectNodes("Path/Point");
        for (int i = 0; i < pathNodeList.Count; i++)
        {
            Point p = new Point(
                int.Parse(pathNodeList[i].Attributes["X"].Value),
                int.Parse(pathNodeList[i].Attributes["Y"].Value)
                );
            level.Path.Add(p);
        }

        //读取刷怪信息
        XmlNodeList roundsNodeList = root.SelectNodes("Rounds/Round");
        foreach (XmlNode node in roundsNodeList)
        {
            Round round = new Round(
                int.Parse(node.Attributes["Monster"].Value),
                int.Parse(node.Attributes["Count"].Value)
                );
            level.Rounds.Add(round);
        }
    }
}