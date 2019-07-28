using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;

public class loadXML : MonoBehaviour
{
    // The XML file. For debugging purposes, needs to be defined in inspector
    public TextAsset xmlFile;
    public static int bullets;
    public static int warps;

    // Set all these before anything else so game doesn't shit itself complaining about missing stuff
    void Awake()
    {
        // Parse our song info before it gets used anywhere else
        parseInfo(xmlFile.text);
    }
    void Start()
    {
        // after everything else has been set now we can parse our markers
        parseMarkers(xmlFile.text);
    }

    void parseMarkers(string xmlData)
    {
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(new StringReader(xmlData));

        // This selects all bullets because all bullets and only bullets have the 'shotType' attribute
        XmlNodeList WarpNodes = xDoc.SelectNodes("//Song//Script[@warpType]");
        XmlNodeList BulletNodes = xDoc.SelectNodes("//Song//Script[@shotType]");

        level.warpStructs = new level.marker[WarpNodes.Count];
        level.bulletStructs = new level.marker[BulletNodes.Count];
        int i = 0;
        int ii = 0;

        foreach (XmlNode warpMarker in WarpNodes)
        {
            switch (warpMarker.Attributes["warpType"].Value)
            {
                case "spinRate":
                    level.warpStructs[ii].warpType = 0;
                    level.warpStructs[ii].val = float.Parse(warpMarker.Attributes["val"].Value);
                    break;
                case "timeWarp":
                    level.warpStructs[ii].warpType = 1;
                    level.warpStructs[ii].val = float.Parse(warpMarker.Attributes["val"].Value);
                    break;
            }
            level.warpStructs[ii].time = float.Parse(warpMarker.Attributes["time"].Value);
            ii++;
        }

        // Go through each bullet selected
        foreach (XmlNode bulletMarker in BulletNodes)
        {
            switch (bulletMarker.Attributes["shotType"].Value)
            {
                case "normal":
                    level.bulletStructs[i].shotType = 0;
                    break;
                case "wave":
                    level.bulletStructs[i].shotType = 1;
                    break;
                case "stream":
                    level.bulletStructs[i].shotType = 2;
                    break;
                case "burst":
                    level.bulletStructs[i].shotType = 3;
                    break;
            }
            switch (bulletMarker.Attributes["bulletType"].Value)
            {
                case "nrm":
                    level.bulletStructs[i].bulletType = 0;
                    break;
                case "nrm2":
                    level.bulletStructs[i].bulletType = 1;
                    break;
                case "bubble":
                    level.bulletStructs[i].bulletType = 2;
                    break;
                case "homing":
                    level.bulletStructs[i].bulletType = 3;
                    break;
                case "hug":
                    level.bulletStructs[i].bulletType = 4;
                    break;
                case "heart":
                    level.bulletStructs[i].bulletType = 5;
                    break;
            }
            switch (bulletMarker.Attributes["aim"].Value)
            {
                case "pl":
                    level.bulletStructs[i].playerAimed = true;
                    break;
                case "mid":
                    level.bulletStructs[i].playerAimed = false;
                    break;
            }

            level.bulletStructs[i].offset0 = float.Parse(bulletMarker.Attributes["offset0"].Value);
            level.bulletStructs[i].offset1 = 0; // Default
            string enemies = bulletMarker.Attributes["enemies"].Value;
            level.bulletStructs[i].enemies = enemies.Split(',').Select(s => int.Parse(s)).ToArray();
            level.bulletStructs[i].speed0 = float.Parse(bulletMarker.Attributes["speed0"].Value);
            level.bulletStructs[i].speed1 = 0; // Default
            level.bulletStructs[i].angle0 = float.Parse(bulletMarker.Attributes["angle0"].Value);
            level.bulletStructs[i].angle1 = 0; // Default
            level.bulletStructs[i].time = float.Parse(bulletMarker.Attributes["time"].Value);
            level.bulletStructs[i].amount0 = 0; // Default
            level.bulletStructs[i].amount1 = 0; // Default
            level.bulletStructs[i].fired = false;

            switch (level.bulletStructs[i].shotType)
            {
                case 0:
                    // Get shotType="normal" specific attributes
                    level.bulletStructs[i].amount0 = int.Parse(bulletMarker.Attributes["amount0"].Value);
                    break;
                case 1:
                    // Get shotType="wave" specific attributes
                    level.bulletStructs[i].offset1 = float.Parse(bulletMarker.Attributes["offset1"].Value);
                    level.bulletStructs[i].speed1 = float.Parse(bulletMarker.Attributes["speed1"].Value);
                    level.bulletStructs[i].amount0 = int.Parse(bulletMarker.Attributes["amount0"].Value);
                    level.bulletStructs[i].amount1 = int.Parse(bulletMarker.Attributes["amount1"].Value);
                    level.bulletStructs[i].angle1 = float.Parse(bulletMarker.Attributes["angle1"].Value);
                    level.bulletStructs[i].rows = int.Parse(bulletMarker.Attributes["rows"].Value);
                    break;
                case 2:
                    // Get shotType="stream" specific attributes

                    break;
                case 3:
                    // Get shotType="burst" specific attributes
                    level.bulletStructs[i].amount0 = int.Parse(bulletMarker.Attributes["amount0"].Value);
                    level.bulletStructs[i].speed1 = float.Parse(bulletMarker.Attributes["speed1"].Value);
                    break;
            }

            i++;
        }
    }

    void parseInfo(string xmlData)
    {
        // Internet told me to do it this way
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(new StringReader(xmlData));

        // Content creator info & technical jazz
        XmlNode Info = xDoc.GetElementsByTagName("Info")[0];
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
        level.bgBlack = bool.Parse(Info.Attributes["bgBlack"].Value);
        level.containsHeart = bool.Parse(Info.Attributes["containsHeart"].Value);

        // Colors are stored in an arrays
        level.color[0] = ConvertToColor(Info.Attributes["color1"].Value);
        level.color[1] = ConvertToColor(Info.Attributes["color2"].Value);
        level.color[2] = ConvertToColor(Info.Attributes["color3"].Value);
        level.color[3] = ConvertToColor(Info.Attributes["color4"].Value);
        level.color[4] = ConvertToColor(Info.Attributes["color5"].Value);
        level.color[5] = ConvertToColor(Info.Attributes["color6"].Value);
        level.color[6] = ConvertToColor(Info.Attributes["color7"].Value);
        level.color[7] = ConvertToColor(Info.Attributes["color8"].Value);
        level.color[8] = ConvertToColor(Info.Attributes["color9"].Value);
    }

    public static Color ConvertToColor(string dec)
    {
        string hex = int.Parse(dec).ToString("X2");
        float red = System.Convert.ToInt32(hex.Substring(0, 2), 16) / 255f;
        float green = System.Convert.ToInt32(hex.Substring(2, 2), 16) / 255f;
        float blue = System.Convert.ToInt32(hex.Substring(4, 2), 16) / 255f;
        return new Color(red, green, blue, 1f);
    }
}
