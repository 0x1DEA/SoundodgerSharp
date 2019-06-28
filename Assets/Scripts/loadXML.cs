using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;

// Unsure if MonoBehavior is actually neccessary but i'm using Awake() to gett all this info before anything else happens
public class loadXML : MonoBehaviour
{
    // The XML file. For debugging purposes, needs to be defined in inspector
    public TextAsset xmlFile;

    // Set all these before anything else so game doesn't shit itself complaining about missing stuff
    void Awake()
    {

        // The good part
        ParseXML(xmlFile.text);
    }

    void Start() {
        doStuff(xmlFile.text);
    }
    // Enum for all our bullet types
    public enum IbulletType
    {
        nrm,
        nrm2,
        hug,
        bubble,
        heart,
        homing
    }
    IbulletType bulletType;
    public enum IshotType
    {
        normal,
        wave,
        stream,
        burst
    }
    IshotType shotType;

    void doStuff(string xmlData) {
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(new StringReader(xmlData));

        // For testing focusing only on grabbing bullets. Selecting all nodes which have 'bulletType' attribute.
        // Keeping it in a variable since this 'magic value' will change over the course of our script
        string query = "//Song//Script";

        // Do our query
        XmlNodeList bullets = xDoc.SelectNodes(query + "[@bulletType='nrm']");
        XmlNodeList timeWarps = xDoc.SelectNodes(query + "[@warpType='timeWarp']");
        XmlNodeList spinRates = xDoc.SelectNodes(query + "[@warpType='spinRate']");
        // Go through each bullet selected
        foreach (XmlNode bullet in bullets)
        {

            // Get attributes all bullets have
            string _shotType = bullet.Attributes["shotType"].Value;
            string _bulletType = bullet.Attributes["bulletType"].Value;
            string aim = bullet.Attributes["aim"].Value;
            float offset = float.Parse(bullet.Attributes["offset0"].Value);
            string enemies = bullet.Attributes["enemies"].Value;
            float speed = float.Parse(bullet.Attributes["speed0"].Value);
            float coneSize = float.Parse(bullet.Attributes["angle0"].Value);
            int[] actual_enemies = enemies.Split(',').Select(s => int.Parse(s)).ToArray();
            float time = float.Parse(bullet.Attributes["time"].Value);

            switch (_shotType)
            {
                case "normal":
                    //shotType = IshotType.normal;
                    int amount = int.Parse(bullet.Attributes["amount0"].Value);
                    // Get shotType="normal" specific attributes

                    // Run our functions from script.cs
                    script.pattern.normal(aim, _bulletType, offset, amount, speed, coneSize, actual_enemies);
                    break;
                case "wave":
                    //shotType = IshotType.wave;

                    // Get shotType="normal" specific attributes

                    // Run our functions from script.cs
                    //script.pattern.wave(aim, bulletType, offset, amount, speed, coneSize, actual_enemies);
                    break;
                case "stream":
                    //shotType = IshotType.stream;

                    // Get shotType="normal" specific attributes

                    // Run our functions from script.cs
                    //script.pattern.stream(aim, bulletType, offset, amount, speed, coneSize, actual_enemies);
                    break;
                default:
                    // Bursts
                    //shotType = IshotType.burst;

                    // Get shotType="burst" specific attributes

                    // Run our functions from script.cs
                    //script.pattern.burst(aim, bulletType, offset, amount, speed, coneSize, actual_enemies);
                    break;
            }
        }
    }

    void ParseXML(string xmlData) {
        // Internet told me to do it this way
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(new StringReader(xmlData));

        // For testing focusing only on grabbing bullets. Selecting all nodes which have 'bulletType' attribute.
        // Keeping it in a variable since this 'magic value' will change over the course of our script
        string query = "//Song//Script";

        // Do our query
        XmlNodeList bullets = xDoc.SelectNodes(query + "[@bulletType='nrm']");
        XmlNodeList timeWarps = xDoc.SelectNodes(query + "[@warpType='timeWarp']");
        XmlNodeList spinRates = xDoc.SelectNodes(query + "[@warpType='spinRate']");

        // All this only needs to be grabbed once, almost made the mistake of putting it in a foreach loop
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

        // Colors
        level.color[0] = int.Parse(Info.Attributes["color1"].Value);
        level.color[1] = int.Parse(Info.Attributes["color2"].Value);
        level.color[2] = int.Parse(Info.Attributes["color3"].Value);
        level.color[3] = int.Parse(Info.Attributes["color4"].Value);
        level.color[4] = int.Parse(Info.Attributes["color5"].Value);
        level.color[5] = int.Parse(Info.Attributes["color6"].Value);
        level.color[6] = int.Parse(Info.Attributes["color7"].Value);
        level.color[7] = int.Parse(Info.Attributes["color8"].Value);
        level.color[8] = int.Parse(Info.Attributes["color9"].Value);
    }
}
