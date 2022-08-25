using UnityEngine;
using System.IO;
public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    [System.Serializable]
    private class GameData
    {
        public int BestScore = 0;

        public string BestScoreName = "";
        public string Name = "";
    }

    private GameData data = new GameData();
    public string Name
    {
        get
        {
            return data.Name;
        }
    }

    public int BestScore
    {
        get
        {
            return data.BestScore;
        }
    }

    public string BestScoreName
    {
        get
        {
            return data.BestScoreName;
        }
    }
    private string savePath = "game_data.json";

    /// Awake is called when the script instance is being loaded.
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + savePath;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<GameData>(json);
        }
    }

    private void _saveData()
    {
        string path = Application.persistentDataPath + savePath;
        File.WriteAllText(path, JsonUtility.ToJson(data));
    }

    public void SaveData()
    {
        _saveData();
    }

    public void ChangeName(string newName)
    {
        data.Name = newName;
        _saveData();
    }

    public void ChangeBestScore(int newBestScore, string newBestScoreName)
    {
        if (data.BestScore < newBestScore)
        {
            data.BestScore = newBestScore;
            data.BestScoreName = newBestScoreName;
            _saveData();
        }

    }
}
