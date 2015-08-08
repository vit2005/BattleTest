using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class BattleView : MonoBehaviour {

    private static BattleView _instance;
    public static BattleView Instance
    {
        get { return _instance; }
    }


    //List<Text> allys, enemies;
    List<Transform> allys, enemies, all;



	// Use this for initialization
	void Start () {
        _instance = this;
        allys = new List<Transform>();
        allys.Add(transform.FindChild("Ally1"));
        allys.Add(transform.FindChild("Ally2"));
        allys.Add(transform.FindChild("Ally3"));
        enemies = new List<Transform>();
        enemies.Add(transform.FindChild("Enemy1"));
        enemies.Add(transform.FindChild("Enemy2"));
        enemies.Add(transform.FindChild("Enemy3"));
        all = new List<Transform>();
        all.AddRange(allys);
        all.AddRange(enemies);

        allys[0].GetComponent<Button>().onClick.AddListener(ally1target);
        allys[1].GetComponent<Button>().onClick.AddListener(ally2target);
        allys[2].GetComponent<Button>().onClick.AddListener(ally3target);
        enemies[0].GetComponent<Button>().onClick.AddListener(enemy1target);
        enemies[1].GetComponent<Button>().onClick.AddListener(enemy2target);
        enemies[2].GetComponent<Button>().onClick.AddListener(enemy3target);

        transform.FindChild("AutoAttack").GetComponent<Button>().onClick.AddListener(autoAttackClick);
        transform.FindChild("Victory").GetComponent<Button>().onClick.AddListener(closeBattle);
        transform.FindChild("Defeat").GetComponent<Button>().onClick.AddListener(closeBattle);

        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowVictory(bool? show)
    {
        transform.FindChild("Victory").gameObject.SetActive(show.HasValue && show.Value);
        transform.FindChild("Defeat").gameObject.SetActive(show.HasValue && !show.Value);
		if (show.HasValue)
			RewardWindowScript.isVictory = show.Value;
    }

    public void FillHP()
    {
        for(int i = 0; i < 3; i++)
        {
            allys[i].FindChild("Text").GetComponent<Text>().text = BattleController.Instance.allys[i].hp.ToString();
            enemies[i].FindChild("Text").GetComponent<Text>().text = BattleController.Instance.enemies[i].hp.ToString();
        }
    }

    public void ClearDamage(bool? clearAllyDamage)
    {
        for (int i = 0; i < 3; i++)
        {
            if (!clearAllyDamage.HasValue || clearAllyDamage.Value)
                allys[i].FindChild("Damage").GetComponent<Text>().text = "";
            if (!clearAllyDamage.HasValue || !clearAllyDamage.Value)
                enemies[i].FindChild("Damage").GetComponent<Text>().text = "";
        }
    }
    public void SetDamage(Unit u, string text)
    {
        for (int i = 0; i < 3; i++)
        {
            if (BattleController.Instance.allys[i] == u)
                allys[i].FindChild("Damage").GetComponent<Text>().text += text;
            if (BattleController.Instance.enemies[i] == u)
                enemies[i].FindChild("Damage").GetComponent<Text>().text += text;
        }
    }

    public void ResetTargets()
    {
        foreach (Transform t in all)
        {
            t.FindChild("Target").gameObject.SetActive(t.gameObject.name == BattleController.Instance.Choosen.utitle);
            t.FindChild("Move").gameObject.SetActive(t.gameObject.name == BattleController.Instance.CurrentMoveUnit.utitle);
        }

        transform.FindChild("AutoAttackBlocker").gameObject.SetActive(BattleController.Instance.allys.Contains(BattleController.Instance.Choosen) || !BattleController.Instance.Choosen.isAlive);
    }

    void ally1target() { BattleController.Instance.Choosen = BattleController.Instance.allys[0]; ResetTargets(); }
    void ally2target() { BattleController.Instance.Choosen = BattleController.Instance.allys[1]; ResetTargets(); }
    void ally3target() { BattleController.Instance.Choosen = BattleController.Instance.allys[2]; ResetTargets(); }
    void enemy1target() { BattleController.Instance.Choosen = BattleController.Instance.enemies[0]; ResetTargets(); }
    void enemy2target() { BattleController.Instance.Choosen = BattleController.Instance.enemies[1]; ResetTargets(); }
    void enemy3target() { BattleController.Instance.Choosen = BattleController.Instance.enemies[2]; ResetTargets(); }

    void autoAttackClick() {
        transform.FindChild("AutoAttackBlocker").gameObject.SetActive(true);
        BattleController.Instance.DoAllyMove("AutoAttack");
    }

    void closeBattle()
    {
        ShowVictory(null);
        Main.Instance.OpenRewardWindow();
    }
}
