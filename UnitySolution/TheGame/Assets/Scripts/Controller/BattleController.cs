using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleController : MonoBehaviour
{
    private bool isFirstMove = true;

	public List<Unit> allys;
    public List<Unit> enemies;
    public Unit Choosen;
    public Unit CurrentMoveUnit;

    private static BattleController _instance;

    public static BattleController Instance
    {
        get { return _instance; }
    }

	// Use this for initialization
	void Start () {
        _instance = this;
	}

    public void Initiate(List<Unit> _allys)
    {
        int ally_pts = 0, enemy_pts = 0;
        for (int i = 0; i < _allys.Count; i++)
        {
            _allys[i].utitle = "Ally" + (i + 1).ToString();
            _allys[i].Calc();
            ally_pts += _allys[i].ATK + _allys[i].DEF + _allys[i].AGI + _allys[i].points;
        }
            
		allys = _allys;
        GenerateEnemies();
        foreach(Unit u in enemies)
            enemy_pts += u.ATK + u.DEF + u.AGI + u.points;
        Debug.Log(ally_pts.ToString() + "vs" + enemy_pts.ToString());

        Choosen = new Unit(0, 0, 0, 0, "", false);
        CurrentMoveUnit = allys[0];
        BattleView.Instance.FillHP();
        BattleView.Instance.ResetTargets();

    }

    #region fight

    private bool? SingleTeamFight(List<Unit> _team1, List<Unit> _team2)
	{
		
		byte alive = 3;
		foreach (Unit u in _team1)
		{
			if (!u.isAlive)
			{
				alive--;
				continue;
			}
			Unit enemy = ChooseEnemy(_team2);
			if (enemy == null)
				return _team2 == enemies;
			Unit ally = u;
			SingleFight(ref ally, ref enemy);
		}
		if (alive == 0)
			return allys == _team2;
		return null;
	}
	
	private void SingleFight(ref Unit u1, ref Unit u2)
	{
		u1.canMove = false;
		System.Threading.Thread.Sleep(3);
		int r = Random.Range (0, 100);
		if (u2.dodge > r)
		{
			Debug.Log("u2 dodged: "+u2.dodge+"vs"+r);
            BattleView.Instance.SetDamage(u2, " dodge");
			return;
		}
		
		System.Threading.Thread.Sleep(3);
        if (u2.block > Random.Range(0, 100))
		{
            Debug.Log("u2 blocked");
            BattleView.Instance.SetDamage(u2, " block");
			return;
		}
		
		System.Threading.Thread.Sleep(3);
		int crit = 1;
        if (u1.crit > Random.Range(0, 100))
		{
            Debug.Log("u1 did crit");
			crit = 2;
		}
        int damage = (int)((double)u1.damage * (double)crit * (double)((double)(100 - u2.dodge) / 100));
        u2.hp -= damage;
		if (u2.hp <= 0)
		{
			u2.isAlive = false;
			u2.hp = 0;
		}
        Debug.Log(string.Format("\nu1 hit [{0}]_HP", damage.ToString()));
        BattleView.Instance.SetDamage(u2, " -"+damage.ToString());

	}
	
	private Unit ChooseEnemy(List<Unit> team)
	{
		List<Unit> alive = new List<Unit>();
		foreach (Unit u in team)
		{
			if (u.isAlive && u.hp > 0)
				alive.Add(u);
		}
		if (alive.Count == 0)
			return null;
		if (alive.Count == 1)
			return alive[0];
		//double minhp = alive[0].hp;
		//foreach (Unit u in alive)
		//{
		//    if (u.hp < minhp)
		//        minhp = u.hp;
		//}
		//return alive.First<Unit>(x => x.hp == minhp);
		double maxdmg = alive[0].damage;
		foreach (Unit u in alive)
		{
			if (u.damage > maxdmg)
				maxdmg = u.damage;
		}
		return alive.FirstOrDefault<Unit>(x => x.damage == maxdmg);
	}
	
	public void DoAllyMove(string usedMove)
	{
		//makeAllSkillsDisable();
        if (isFirstMove)
        {
            BattleView.Instance.ClearDamage(false);
            isFirstMove = false;
        }
		if (usedMove == "AutoAttack")
		{
			SingleFight(ref CurrentMoveUnit, ref Choosen);
			if (enemies.FirstOrDefault<Unit>(x => x.isAlive == true) == null )
			{
                ProclaimResult(true);
				return;
			}
			Unit CurrentMoveUnitTemp = allys.FirstOrDefault<Unit>(x => x.canMove && x.isAlive);
			if (CurrentMoveUnitTemp == null)
			{
				botMove();
			}
			else
			{
				CurrentMoveUnit = CurrentMoveUnitTemp;
			}
		}

        Choosen = new Unit(0, 0, 0, 0, "", false);
        BattleView.Instance.FillHP();
        BattleView.Instance.ResetTargets();
		//refreshRectangles();
	}
	
	private void botMove()
	{
        Debug.Log("=== Enemy turn ===");
        BattleView.Instance.ClearDamage(true);
        isFirstMove = true;
		bool? result = SingleTeamFight(enemies, allys);
		if (((result != null) && (!result.Value)) || (allys.FirstOrDefault<Unit>(x => x.isAlive) == null))
		{
            ProclaimResult(false);
			return;
		}

        Debug.Log("=== Your turn ===");
		foreach (Unit u in allys)
			u.canMove = true;
        CurrentMoveUnit = allys.FirstOrDefault<Unit>(x => x.isAlive);
    }

    private void ProclaimResult(bool isVictory)
    {
        LocalStorage.SaveBattleResult(isVictory);
        BattleView.Instance.ShowVictory(isVictory);
    }

    #endregion

    private void GenerateEnemies()
    {
        int points = 0;
        double coef = 1;
        if (LocalStorage.gamesPlayed != 0)
            coef += ((double)LocalStorage.gamesWon / (double)LocalStorage.gamesPlayed - (double)0.5);

        foreach (Unit u in allys) {
            points += (int)((double)(u.ATK + u.DEF + u.AGI + u.points) * (double)(coef));
        }
        points = (int)(points / 3);

        int atk, def, agi;
        enemies = new List<Unit>();

        for (int i = 0; i < 3; i++ )
        {
            atk = Random.Range(0, points);
            def = Random.Range(0, points - atk);
            agi = points - atk - def;
            enemies.Add(new Unit(atk, def, agi, 0, "Enemy"+(i+1).ToString()));
        }
    }


}
