using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;

public class loadXML : MonoBehaviour
{
    public TextAsset xmlFile;

    void Awake()
    {
        string data = xmlFile.text;
        ParseXML(data);
    }

    void ParseXML(string xmlData) {

        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(new StringReader(xmlData));

        XmlNodeList xNodes = xDoc.SelectNodes("//Song");

        // Content creator info & technical jazz
        var Info = xDoc.GetElementsByTagName("Info")[0];
        level.nick = Info.Attributes["nick"].Value;
        level.title = Info.Attributes["title"].Value;
        level.artist = Info.Attributes["artist"].Value;
        level.designer = Info.Attributes["designer"].Value;
        level.mp3Name = Info.Attributes["MP3Name"].Value;
        level.subtitle = Info.Attributes["subtitle"].Value;

        // Config
        level.enemies = float.Parse(Info.Attributes["enemies"].Value);
        level.difficulty = int.Parse(Info.Attributes["difficulty"].Value);
        level.preview = int.Parse(Info.Attributes["audioPreview"].Value);

        // Colors
        level.color1 = int.Parse(Info.Attributes["color1"].Value);
        level.color2 = int.Parse(Info.Attributes["color2"].Value);
        level.color3 = int.Parse(Info.Attributes["color3"].Value);
        level.color4 = int.Parse(Info.Attributes["color4"].Value);
        level.color5 = int.Parse(Info.Attributes["color5"].Value);
        level.color6 = int.Parse(Info.Attributes["color6"].Value);
        level.color7 = int.Parse(Info.Attributes["color7"].Value);
        level.color8 = int.Parse(Info.Attributes["color8"].Value);
        level.color9 = int.Parse(Info.Attributes["color9"].Value);
    }
}
