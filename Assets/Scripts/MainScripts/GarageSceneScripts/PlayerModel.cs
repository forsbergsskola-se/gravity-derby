using UnityEngine;

public class PlayerModel : MonoBehaviour {
    public InventoryObject inventory;
    public delegate void NutsBoltsEvent(int nutsBoltsValue);
    public event NutsBoltsEvent ListenToNutsBoltsChange;
    public delegate void ScrapEvent(int scrapValue);
    public event ScrapEvent ListenToScrapChange;
    
    private int _nutsBolts = 100;
    private int _scrap = 100;
    
    public InventorySlot[] gachaLootTable;

    public int NutsBolts {
        get => _nutsBolts;
        set {
            _nutsBolts = value;
            PlayerPrefs.SetInt("NutsBolts", value);
            ListenToNutsBoltsChange?.Invoke(value);
        }
    }
    
    public int Scrap {
        get => _scrap;
        set {
            _scrap = value;
            PlayerPrefs.SetInt("Scrap", value);
            ListenToScrapChange?.Invoke(value);
        }
    }

    public bool HasEnoughGold(int cost) {
        return NutsBolts >= cost;
    }
    
    public bool HasEnoughScrap(int cost) {
        return Scrap >= cost;
    }

    private void Start() {
        NutsBolts = PlayerPrefs.GetInt("Gold");
        inventory.Load();
    }

    public void AddMoney() {
        NutsBolts += 500;
    }
    
    public void AddScrap() {
        Scrap += 500;
    }

    private void OnApplicationQuit() {
        inventory.SelectedParts.Clear();
        inventory.Save();
        // inventory.Container.Clear();
    }
}