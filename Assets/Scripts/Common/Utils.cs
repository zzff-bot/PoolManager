using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.Networking;

//GmaeUtils,StringUtils,TimeUtils...
public class Utils
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

    public static IEnumerator LoadImageAsync(string path,SpriteRenderer sr)
    {
        //WWW好用，性能颗粒化很多，有很多额外性能的消耗
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(path);  //新建一个加载请求
        yield return request.SendWebRequest();  //直到请求结束，往下加载

        //执行到这里，就证明加载结束
        if (request.isDone)
        {
            //isDone==true，资源加载成功
            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            Sprite sprite = Sprite.Create(texture, new Rect(new Vector2(0, 0), new Vector2(texture.width, texture.height)), Vector2.zero);
            sr.sprite = sprite;
        }
    }
}