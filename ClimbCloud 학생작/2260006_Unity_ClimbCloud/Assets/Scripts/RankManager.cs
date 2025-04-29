/*
 * ��ŷ �ý��� ������ ���� ���ͳݿ��� ���۷����� �����Ͽ� ������
 * �м��Ͽ� ��ŷ�Ŵ��� ������ ����
 */

using UnityEngine;
using System.IO;
using System.Collections.Generic;

[System.Serializable]
public class RankData
{
    public string sPlayerName = null;
    public int nScore = 0;

    public RankData(string name, int score)
    {
        sPlayerName = name;
        this.nScore = score;
    }
}

[System.Serializable]
public class RankListWrapper
{
    public List<RankData> list = new List<RankData>();

    public RankListWrapper(List<RankData> rankList)
    {
        list = rankList;
    }
}

public class RankManager : MonoBehaviour
{
    public static RankManager Instance;

    private string savePath;
    public List<RankData> rankList = new List<RankData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        savePath = Application.persistentDataPath + "/rank.json";
        LoadRank();
    }

    public void SaveRank()
    {
        string json = JsonUtility.ToJson(new RankListWrapper(rankList));
        File.WriteAllText(savePath, json);
    }

    public void LoadRank()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            rankList = JsonUtility.FromJson<RankListWrapper>(json).list;
        }
    }

    public void AddRank(string playerName, int score)
    {
        rankList.Add(new RankData(playerName, score));
        rankList.Sort((a, b) => b.nScore.CompareTo(a.nScore));

        if (rankList.Count > 10)
        {
            rankList.RemoveAt(rankList.Count - 1);
        }
        SaveRank();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
