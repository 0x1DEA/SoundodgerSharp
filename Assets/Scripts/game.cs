using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class game : MonoBehaviour
{

    // Level XML Variables
    public static float timeWarp = 1f;
    public static float spinRate = 0f;
    public static float enemies;

    public float radius;
    public GameObject enemy;
    public GameObject ring;
    public Quaternion rot;
    public static GameObject player;
    private int iframe;
    AudioSource source;

    public float arenaRadius;
    public float enemyRadius;
    public Vector3 center;

    void Start() {
        enemies = level.enemies;
        SpawnEnemies();
        player = GameObject.Find("Player");

        string path = "C:/AudioFiles/audio1.wav";
        string url = "file:///" + path;
        UnityWebRequest song = UnityWebRequest.Get(url);
        AudioSource = www.downloadHandler.data;
    }

    void Update()
    {
        iframe++;
        if (iframe > 20) {
            MakeRing();
            iframe = 0;
        }
    }

    IEnumerator StartSong(string path) {

        string songPath = Environment.SpecialFolder.MyDocuments + level.mp3Name;
        string url = "file:///" + path;
        UnityWebRequest song = UnityWebRequest.Get(url);
        source.clip = song.audioClip;
        if (www.error != null) {
            Debug.Log(www.error);
        } else {
            source.clip = www.audioClip;
            while (source.clip.loadState != AudioDataLoadState.Loaded)
                yield return new WaitForSeconds(0.1f);
            source.Play();
        }
    }


    void SpawnEnemies() {

        for (int i = 0; i < enemies; i++) {
            float ang = i * Mathf.PI * 2f / enemies;
            Vector3 newPos = new Vector3(Mathf.Cos(ang) * radius, Mathf.Sin(ang) * radius, 0);
            GameObject enemyInstance = Instantiate(enemy, newPos, Quaternion.identity);
            enemyInstance.gameObject.name = (i + 1).ToString();
            enemy enemyObject = enemyInstance.GetComponent<enemy>();
            enemyObject.i = i + 1;
        }
    }

    void MakeRing() {

        Instantiate(ring, new Vector3(0, 0, 0), rot);
    }
}
