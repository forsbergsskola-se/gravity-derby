using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour {
    public InventoryObject inventory;
    public PlayerModel player;
    public GameObject ItemViewPrefab;

    public GameObject PrefabBlur;
    public GameObject PrefabPopup;
    public bool UIDissable = false;
    private Camera _camera;

    public Animator Anim;
    public AudioSource claimAward;
    public AudioSource magnetSound;
    public AudioSource getItemSound;

    public int[] weightsSoft = {50, 40, 30, 20, 10, 1};
    public int[] weightsHard = {30, 30, 30, 30, 10, 10};

    public int gachaCostSoft = 100;
    public int gachaRollHard = 100;
    public int rewardAmount = 3;

    void Start() {
        _camera = Camera.main;
        var moneyWon = GameObject.Find("MoneyWon");
        if (moneyWon != null) {
            moneyWon.GetComponent<MoneyWon>().AddToStash();
        }
        inventory.BlurReset();
    }
    
    public void Update() {
        if(!UIDissable) {
            if (Input.GetMouseButtonDown(0)) {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit)) {
                    var gsButton = hit.collider.CompareTag("GachaSoft");
                    if (gsButton) {
                        RollGachaSoft();
                    }                          
                    var ghButton = hit.collider.CompareTag("GachaHard");
                    if (ghButton) {
                        RollGachaHard();
                    }
                }
            }
        }
        GachaBlurController();
    }
    
    public void RollGachaSoft() {
        if (!player.HasEnoughNutsBolts(gachaCostSoft)) return;
        player.NutsBolts -= gachaCostSoft;
        Anim.SetTrigger("lowerMagnet");
        magnetSound.Play();
       // StartCoroutine(PlayAnimation());

        for (int j = 0; j < rewardAmount; j++) {
            var totalWeights = weightsSoft.Sum();
            var random = Random.Range(0, totalWeights);
            var total = weightsSoft[0];
            var i = 0;
            while (total < random) { 
                i++; 
                total += weightsSoft[i];
            }
            UIDissable = true;
            PrefabBlur.SetActive(true);
            var popUp = Instantiate(PrefabPopup, GameObject.Find("Canvas").transform);
            popUp.GetComponentInChildren<ItemView>().Display(player.gachaLootTable[i]);
            popUp.GetComponent<GachaPopup>().ColorDisplay(player.gachaLootTable[i]);
            getItemSound.Play();
        }
    }
    
    public void RollGachaHard() {
        if (!player.HasEnoughScrap(gachaRollHard)) return;
        player.Scrap -= gachaRollHard;
        Anim.SetTrigger("lowerMagnet");
        magnetSound.Play();

        for (int j = 0; j < rewardAmount; j++) {
            var totalWeights = weightsHard.Sum();
            var random = Random.Range(0, totalWeights);
            var total = weightsHard[0];
            var i = 0;
            while (total < random) { 
                i++; 
                total += weightsHard[i];
            }
            UIDissable = true;
            PrefabBlur.SetActive(true);
            var popUp = Instantiate(PrefabPopup, GameObject.Find("Canvas").transform);
            popUp.GetComponentInChildren<ItemView>().Display(player.gachaLootTable[i]);
            popUp.GetComponent<GachaPopup>().ColorDisplay(player.gachaLootTable[i]);
            getItemSound.Play();
        }
    }
    
    
    public void GachaBlurController()
    {
        if (inventory.claimAmountOfTimes == rewardAmount)
        {
            PrefabBlur.SetActive(false);
            inventory.BlurReset();
            UIDissable = false;
        }
    }
    
    public void StartRace() {
        SceneManager.LoadScene("CarRollScene");
        
    }

   /* private IEnumerator PlayAnimation()
    {
        Anim.SetTrigger("lowerMagnet");
        yield return new WaitForSeconds(2);

    }   */

    
}