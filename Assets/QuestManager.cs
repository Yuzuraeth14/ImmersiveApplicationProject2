using TMPro;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string questTitle;
    public string questDetails;
    public bool questCompleted;
}

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    [SerializeField] private TMP_Text questText;
    [SerializeField] private Quest[] quests;
    private int currentQuest = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateQuestText(currentQuest);
    }

    public void NextQuest()
    {
        if (currentQuest == quests.Length) return;

        currentQuest++;
        UpdateQuestText(currentQuest);
    }

    public void UpdateQuestText(int currentQuest)
    {
        questText.text = $"Quest: {currentQuest} | QuestTitle: {quests[currentQuest].questTitle} | QuestDetails: {quests[currentQuest].questDetails}";
    }

    public int CheckQuestNumber()
    {
        return currentQuest;
    }
}
