using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    private static System.Random rng = new System.Random();

    private static Room[] Shuffle(Room[] list)
    {
        int n = list.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            var value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
    }

    public int pointsPerMission = 1000;
    public int extraPointsPerSecond = 100;
    public Room finalRoom;
    public string nextScene;

    private Room[] rooms;
    private AudioSource source;
    private Room currentMission;

    void Start()
    {
        rooms = Shuffle(FindObjectsOfType<Room>()).Where((r) => {
            return r != finalRoom;
        }).Take(5).ToArray();
        Debug.Log("Found rooms: " + rooms.Select((r) => {
            return r.gameObject.name;
        }));

        source = GetComponent<AudioSource>();
        SelectNextMission();
    }

    void Update()
    {
        ScoreSingleton.Instance.AddTime(-Time.deltaTime);
        if (ScoreSingleton.Instance.TimeRemaining < 0)
        {
            Debug.Log("Game over");
            SceneManager.LoadScene("GameOver");
        }
    }

    public void OnRoomTriggered(Room room)
    {
        if (room == currentMission) {
            if (room == finalRoom) {
                Debug.Log("Level complete");
                SceneManager.LoadScene(nextScene);
                return;
            }
            ScoreSingleton.Instance.Add(pointsPerMission);
            rooms = rooms.Where((r) => {
                return r != currentMission;
            }).ToArray();
            source.PlayOneShot(currentMission.done);
            SelectNextMission();
        }
    }


    private void SelectNextMission()
    {
        currentMission = rooms.Length > 0 ? rooms[UnityEngine.Random.Range(0, rooms.Length)] : finalRoom;
        StartCoroutine(AnnounceNext(currentMission));
        Debug.Log("Current mission: " + currentMission.gameObject.name);
    }

    public string Mission()
    {
        if (currentMission == null)
        {
            return "";
        }
        return currentMission.gameObject.name;
    }

    private IEnumerator AnnounceNext(Room room)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        if (room.announcement != null) {
            source.PlayOneShot(room.announcement);
        }
    }

}
