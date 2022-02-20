using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour, ISave
{
    [System.Serializable]
    public struct MenuValue
    {
        [SerializeField]
        private string prefix;
        [SerializeField]
        private TextMeshProUGUI textMesh;

        public void SetText(string newValue)
        {
            this.textMesh.text = prefix + newValue;
        }
    }

    [Header("Main menu")]
    [SerializeField]
    private TextMeshProUGUI diamondsMainMenuText;

    [SerializeField]
    private MenuValue highscoreText;

    [Header("Shop")]
    [SerializeField]
    private TextMeshProUGUI diamondsShopText;

    [SerializeField]
    private List<ShopElement> shopElements;
    public List<ShopElement> ShopElements { get => shopElements; }

    /// <summary>
    /// Starts from Main Menu button
    /// </summary>
    public void StartGame()
    {
        SceneManager.Instance.StartGame();
    }

    public void RefreshValues()
    {
        var data = DataManager.Instance.PlayerData;

        diamondsMainMenuText.text = data.Diamonds.ToString();
        diamondsShopText.text = data.Diamonds.ToString();
        highscoreText.SetText(data.Highscore.ToString());
    }

    private void Start()
    {
        RefreshValues();
    }

    [ContextMenu("Save")]
    public void Save()
    {
        foreach (var shopElement in shopElements)
        {
            shopElement.Save();
        }
    }

    [ContextMenu("Load")]
    public void Load()
    {
        foreach (var shopElement in shopElements)
        {
            shopElement.Load();
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
