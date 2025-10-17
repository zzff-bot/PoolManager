using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using UnityEngine.Networking;
using System.IO;
using System.Text;

//GmaeUtils,StringUtils,TimeUtils...
public class Utils
{
    public static void SaveLevel(string path, Level level)
    {
        //处理字符串拼接的一个类
        //字符串类型的不变性     字符串内存池  不要频繁的去拼接

        //频繁处理字符串的时候，建议使用StringBuilder
        StringBuilder sb = new StringBuilder();

        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n");
        sb.Append("<Level>\n");
        sb.Append(string.Format("\t<Name>{0}</Name>\n", level.Name));
        sb.Append(string.Format("\t<CardImage>{0}</CardImage>\n", level.CardImage));
        sb.Append(string.Format("\t<Background>{0}</Background>\n", level.Background));
        sb.Append(string.Format("\t<Road>{0}</Road>\n", level.Road));
        sb.Append(string.Format("\t<InitScore>{0}</InitScore>\n", level.InitScore));

        sb.Append("\t<Holder>\n");
        foreach (Point point in level.Holder)
        {
            //<Point X="1" Y="0" />
            sb.Append(string.Format("\t\t<Point X = \"{0}\" Y = \"{1}\" />\n", point.X, point.Y));
        }
        sb.Append("\t</Holder>\n");

        sb.Append("\t<Path>\n");
        foreach (Point point in level.Path)
        {
            sb.Append(string.Format("\t\t<Point X = \"{0}\" Y = \"{1}\" />\n", point.X, point.Y));
        }
        sb.Append("\t</Path>\n");

        sb.Append("\t<Rounds>\n");
        foreach (Round round in level.Rounds)
        {
            sb.Append(string.Format("\t\t<Round Monster=\"{0}\" Count=\"{1}\" />\n", round.MonsterId, round.Count));
        }
        sb.Append("\t</Rounds>\n");

        sb.Append("</Level>\n");

        File.WriteAllText(path, sb.ToString(),Encoding.UTF8);
    }

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
        level.Road = root.SelectSingleNode("Road").InnerText;
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

    public static IEnumerator LoadImageAsync(string path, SpriteRenderer sr)
    {
        //WWW好用，性能颗粒化很多，有很多额外性能的消耗
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(path);  //新建一个加载请求
        yield return request.SendWebRequest();  //直到请求结束，往下加载

        //执行到这里，就证明加载结束
        if (request.isDone)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            Sprite sprite = Sprite.Create(texture, new Rect(new Vector2(0, 0), new Vector2(texture.width, texture.height)), new Vector2(0.5f,0.5f));
            sr.sprite = sprite;
        }
        else
        {
            Debug.LogError(request.error);
        }
    }

    public static IEnumerator LoadSpriteAsync(string path, Image image)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(path);
        yield return request.SendWebRequest();
        if(request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            Sprite sprite = Sprite.Create(texture, new Rect(new Vector2(0,0),new Vector2(texture.width,texture.height)),new Vector2(0.5f,0.5f));
            image.sprite = sprite;
        }
        else
        {
            Debug.LogError(request.error);
        }
    }

    public static List<FileInfo> GetAllLevelFiles()
    {
        //Const.LevelConfigPath
        string[] files = Directory.GetFiles(Const.LevelConfigPath, "*.xml");
        List<FileInfo> list = new List<FileInfo>();
        for (int i = 0; i < files.Length; i++)
        {
            FileInfo fileInfo = new FileInfo(files[i]);
            list.Add(fileInfo);
        }
        
        return list;
    }
}