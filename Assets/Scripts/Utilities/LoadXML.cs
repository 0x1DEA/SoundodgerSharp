using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;

public class LoadXML : MonoBehaviour {
    // The XML file. For debugging purposes, needs to be defined in inspector
    public TextAsset xmlFile;
    public static int bullets;
    public static int warps;

    // Set all these before anything else so game doesn't shit itself complaining about missing stuff
    void Awake() {
        // Parse our song info before it gets used anywhere else
        parseInfo(xmlFile.text);
    }
    void Start() {
        // after everything else has been set now we can parse our markers
        parseMarkers(xmlFile.text);
        Level.sortWarps();
    }

    void parseMarkers(string xmlData) {
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(new StringReader(xmlData));

        // This selects all warps because all warps and only warps have the 'warpType' attribute
        XmlNodeList spinRateNodes = xDoc.SelectNodes("//Song//Script[@warpType='spinRate']");
        // This selects all warps because all warps and only warps have the 'warpType' attribute
        XmlNodeList timeWarpNodes = xDoc.SelectNodes("//Song//Script[@warpType='timeWarp']");
        // This selects all bullets because all bullets and only bullets have the 'shotType' attribute
        XmlNodeList bulletNodes = xDoc.SelectNodes("//Song//Script[@shotType]");

        Level.timeWarps = new Level.marker[timeWarpNodes.Count];
        Level.spinRates = new Level.marker[spinRateNodes.Count];
        Level.bullets = new Level.marker[bulletNodes.Count];

        int k = 0;
        int i = 0;
        int j = 0;

        foreach (XmlNode timeWarpMarker in timeWarpNodes) {
            Level.timeWarps[k].warpType = 1;
            Level.timeWarps[k].val = float.Parse(timeWarpMarker.Attributes["val"].Value);
            Level.timeWarps[k].time = float.Parse(timeWarpMarker.Attributes["time"].Value);
            k++;
        }

        foreach (XmlNode spinRateMarker in spinRateNodes)
        {
            Level.spinRates[j].warpType = 0;
            Level.spinRates[j].val = float.Parse(spinRateMarker.Attributes["val"].Value);
            Level.spinRates[j].time = float.Parse(spinRateMarker.Attributes["time"].Value);
            j++;
        }

        // Go through each bullet selected
        foreach (XmlNode bulletMarker in bulletNodes) {
            switch (bulletMarker.Attributes["shotType"].Value) {
                case "normal":
                    Level.bullets[i].shotType = 0;
                    break;
                case "wave":
                    Level.bullets[i].shotType = 1;
                    break;
                case "stream":
                    Level.bullets[i].shotType = 2;
                    break;
                case "burst":
                    Level.bullets[i].shotType = 3;
                    break;
            }
            switch (bulletMarker.Attributes["bulletType"].Value) {
                case "nrm":
                    Level.bullets[i].bulletType = 0;
                    break;
                case "nrm2":
                    Level.bullets[i].bulletType = 1;
                    break;
                case "bubble":
                    Level.bullets[i].bulletType = 2;
                    break;
                case "homing":
                    Level.bullets[i].bulletType = 3;
                    break;
                case "hug":
                    Level.bullets[i].bulletType = 4;
                    break;
                case "heart":
                    Level.bullets[i].bulletType = 5;
                    break;
            }
            switch (bulletMarker.Attributes["aim"].Value) {
                case "pl":
                    Level.bullets[i].playerAimed = true;
                    break;
                case "mid":
                    Level.bullets[i].playerAimed = false;
                    break;
            }

            Level.bullets[i].offset0 = float.Parse(bulletMarker.Attributes["offset0"].Value);
            Level.bullets[i].offset1 = 0; // Default
            string enemies = bulletMarker.Attributes["enemies"].Value;
            Level.bullets[i].enemies = enemies.Split(',').Select(s => int.Parse(s)).ToArray();
            Level.bullets[i].speed0 = float.Parse(bulletMarker.Attributes["speed0"].Value);
            Level.bullets[i].speed1 = 0; // Default
            Level.bullets[i].angle0 = float.Parse(bulletMarker.Attributes["angle0"].Value);
            Level.bullets[i].angle1 = 0; // Default
            Level.bullets[i].time = float.Parse(bulletMarker.Attributes["time"].Value);
            Level.bullets[i].amount0 = 0; // Default
            Level.bullets[i].amount1 = 0; // Default
            Level.bullets[i].fired = false;

            switch (Level.bullets[i].shotType) {
                case 0:
                    // Get shotType="normal" specific attributes
                    Level.bullets[i].amount0 = int.Parse(bulletMarker.Attributes["amount0"].Value);
                    break;
                case 1:
                    // Get shotType="wave" specific attributes
                    Level.bullets[i].offset1 = float.Parse(bulletMarker.Attributes["offset1"].Value);
                    Level.bullets[i].speed1 = float.Parse(bulletMarker.Attributes["speed1"].Value);
                    Level.bullets[i].amount0 = int.Parse(bulletMarker.Attributes["amount0"].Value);
                    Level.bullets[i].amount1 = int.Parse(bulletMarker.Attributes["amount1"].Value);
                    Level.bullets[i].angle1 = float.Parse(bulletMarker.Attributes["angle1"].Value);
                    Level.bullets[i].rows = int.Parse(bulletMarker.Attributes["rows"].Value);
                    break;
                case 2:
                    // Get shotType="stream" specific attributes

                    break;
                case 3:
                    // Get shotType="burst" specific attributes
                    Level.bullets[i].amount0 = int.Parse(bulletMarker.Attributes["amount0"].Value);
                    Level.bullets[i].speed1 = float.Parse(bulletMarker.Attributes["speed1"].Value);
                    break;
            }
            i++;
        }
    }

    void parseInfo(string xmlData) {
        // Internet told me to do it this way
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(new StringReader(xmlData));

        // Content creator info & technical jazz
        XmlNode Info = xDoc.GetElementsByTagName("Info")[0];
        Level.nick = Info.Attributes["nick"].Value;
        Level.title = Info.Attributes["title"].Value;
        Level.artist = Info.Attributes["artist"].Value;
        Level.designer = Info.Attributes["designer"].Value;
        Level.mp3Name = Info.Attributes["MP3Name"].Value;
        Level.subtitle = Info.Attributes["subtitle"].Value;

        // Config
        Level.enemies = float.Parse(Info.Attributes["enemies"].Value);
        Level.difficulty = int.Parse(Info.Attributes["difficulty"].Value);
        Level.preview = int.Parse(Info.Attributes["audioPreview"].Value);
        
        // SD XML does not require the following 2 options. Set to false if unset.
        if (Info.Attributes["bgBlack"] != null) {
        	Level.bgBlack = bool.Parse(Info.Attributes["bgBlack"].Value);
        } else {
        	Level.bgBlack = false;
        }
        
        if (Info.Attributes["containsHeart"] != null) {
        	Level.containsHeart = bool.Parse(Info.Attributes["containsHeart"].Value);	
        } else {
        	Level.containsHeart = false;
        }

        // Colors are stored in an arrays
        Level.color[0] = decToColor(Info.Attributes["color1"].Value);
        Level.color[1] = decToColor(Info.Attributes["color2"].Value);
        Level.color[2] = decToColor(Info.Attributes["color3"].Value);
        Level.color[3] = decToColor(Info.Attributes["color4"].Value);
        Level.color[4] = decToColor(Info.Attributes["color5"].Value);
        Level.color[5] = decToColor(Info.Attributes["color6"].Value);
        Level.color[6] = decToColor(Info.Attributes["color7"].Value);
        Level.color[7] = decToColor(Info.Attributes["color8"].Value);
        Level.color[8] = decToColor(Info.Attributes["color9"].Value);
    }

    public static Color decToColor(string dec) {
        string hex = int.Parse(dec).ToString("X2");
        float red = System.Convert.ToInt32(hex.Substring(0, 2), 16) / 255f;
        float green = System.Convert.ToInt32(hex.Substring(2, 2), 16) / 255f;
        float blue = System.Convert.ToInt32(hex.Substring(4, 2), 16) / 255f;
        return new Color(red, green, blue, 1f);
    }
}
