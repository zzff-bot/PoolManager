using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class Test : MonoBehaviour
{
    //private List<GameObject> golist = new List<GameObject>();
    //public GameObject go;

    private void Awake()
    {
        SoundManager.GetInstance();
    }

    private void Start()
    {
        //TestXML();
        //TestXMLLoad();
        Level level = new Level();
        Utils.LoadLevel("level0.xml", ref level);
    }

    void TestXMLLoad()
    {
        XmlDocument doc = new XmlDocument();
        //解析文件存入doc
        doc.Load("123.xml");

        //找到根节点并且返回一个值
        XmlElement root = doc.DocumentElement;
        XmlNodeList nodeList = root.SelectNodes("book");

        for (int i = 0; i < nodeList.Count; i++)
        {
            //在book节点中寻找title的节点
            XmlNodeList tempNodeList = nodeList[i].SelectNodes("title");
            Debug.Log("Title:" + tempNodeList[0].InnerText + "  Language:" + tempNodeList[0].Attributes["lang"].Value);
        }
    }

    void TestXML()
    {
        XmlDocument doc = new XmlDocument();
        #region XML文本描述练习
        ////增加文本版本的描述 - 1.可以通过doc创建 2.也可以直接new一个新的再添加
        //XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
        //// 将创建的文本描述添加至doc中
        //doc.AppendChild(declaration);

        ////包含内容信息
        //XmlElement nodeBookStore = doc.CreateElement("BookStore");
        ////只包含层级关系
        //XmlNode root = doc.AppendChild(nodeBookStore);

        //XmlElement node1 = doc.CreateElement("Book");
        //node1.InnerText = "西游记";

        ////添加一个标签 X="1" Y="0"
        //node1.SetAttribute("X", "1");
        //node1.SetAttribute("Y", "0");

        //nodeBookStore.AppendChild(node1);
        #endregion

        XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
        doc.AppendChild(declaration);

        //根节点
        XmlElement nodeBookStore = doc.CreateElement("bookstore");
        doc.AppendChild(nodeBookStore);

        //书1
        XmlElement book1 = doc.CreateElement("book");
        book1.SetAttribute("category", "fiction");
        XmlElement title1 = doc.CreateElement("title");
        title1.SetAttribute("lang", "en");
        title1.InnerText = "The Hobbit";
        book1.AppendChild(title1);

        XmlElement author1 = doc.CreateElement("author");
        author1.InnerText = "J.R.R Tolkien";
        book1.AppendChild(author1);

        XmlElement year1 = doc.CreateElement("year");
        year1.InnerText = "1937";
        book1.AppendChild(year1);

        XmlElement price1 = doc.CreateElement("price");
        price1.InnerText = "12.99";
        book1.AppendChild(price1);

        nodeBookStore.AppendChild(book1);

        //书2
        XmlElement book2 = doc.CreateElement("book");
        book2.SetAttribute("category", "non-fiction");
        XmlElement title2 = doc.CreateElement("title");
        title2.SetAttribute("lang", "es");
        title2.InnerText = "Cien anos de soledad";
        book2.AppendChild(title2);

        XmlElement author2 = doc.CreateElement("author");
        author2.InnerText = "Gabriel Garcia Marquez";
        book2.AppendChild(author2);

        XmlElement year2 = doc.CreateElement("author");
        year2.InnerText = "1967";
        book2.AppendChild(year2);

        XmlElement price2 = doc.CreateElement("price");
        price2.InnerText = "15.50";
        book2.AppendChild(price2);

        nodeBookStore.AppendChild(book2);

        doc.Save("123.xml");

    }

    private void Update()
    {
        #region 对象池测试代码
        //    if (Input.GetKeyDown(KeyCode.A))
        //    {
        //        go = PoolManager.GetInstance().Take("Prefabs/Cube");
        //        golist.Add(go);
        //    }

        //    if (Input.GetKeyDown(KeyCode.S))
        //    {
        //        PoolManager.GetInstance().Back(golist[0]);
        //        golist.RemoveAt(0);
        //    }

        //    if (Input.GetKeyDown(KeyCode.D))
        //    {
        //        PoolManager.GetInstance().Clear();
        //        golist.Clear();
        //    }
        #endregion


    }
}
