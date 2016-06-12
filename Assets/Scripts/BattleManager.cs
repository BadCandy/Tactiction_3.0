using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleManager : MonoBehaviour
{
	public Unit[] player;
	public Unit[] enemy = new Unit[4];
	public Vector2[] playerPosition = new Vector2[4];
	private Vector2[] enemyPosition = new Vector2[4];
	private GameObject[] playerObject = new GameObject[4];
	private GameObject[] enemyObject = new GameObject[4];
	private int buttonFlag = 0;
	private PlayerManager pManager;
	
	//UI things
	public int playerTurnGauge = 0;
	public int enemyTurnGauge = 0;
	public int maxGauge = 100;
	public Slider playerHPSlider;
	public Slider enemyHPSlider;
	public Slider playerAttackSlider;
	public Slider enemyAttackSlider;
	public Text playerText;
	public Text enemyText;
	public int[] player_0_Info;
	public int[] player_1_Info;
	public int[] player_2_Info;
	public int[] player_3_Info;
	public GameObject canvas;
	public GameObject battleManager;
	
	
	void Awake()
	{
		pManager = new PlayerManager();
		canvas = GameObject.Find("Canvus");
		battleManager = GameObject.Find ("BattleManger");
		playerObject[0] = GameObject.Find("player0");
		playerObject[1] = GameObject.Find("player1");
		playerObject[2] = GameObject.Find("player2");
		playerObject[3] = GameObject.Find("player3");
		enemyObject[0] = GameObject.Find("enemy0");
		enemyObject[1] = GameObject.Find("enemy1");
		enemyObject[2] = GameObject.Find("enemy2");
		enemyObject[3] = GameObject.Find("enemy3");
		playerPosition[0] = new Vector2(playerObject[0].transform.position.x, playerObject[0].transform.position.y);
		playerPosition[1] = new Vector2(playerObject[1].transform.position.x, playerObject[1].transform.position.y);
		playerPosition[2] = new Vector2(playerObject[2].transform.position.x, playerObject[2].transform.position.y);
		playerPosition[3] = new Vector2(playerObject[3].transform.position.x, playerObject[3].transform.position.y);
		enemyPosition[0] = new Vector2(enemyObject[0].transform.position.x, enemyObject[0].transform.position.y);
		enemyPosition[1] = new Vector2(enemyObject[1].transform.position.x, enemyObject[1].transform.position.y);
		enemyPosition[2] = new Vector2(enemyObject[2].transform.position.x, enemyObject[2].transform.position.y);
		enemyPosition[3] = new Vector2(enemyObject[3].transform.position.x, enemyObject[3].transform.position.y);
		
		//player0~3, enemy0~3 생성
		player = pManager.makeFourPlayer (0000000, 0000001, 0000002, 0000003);
		enemy[0] = new Unit(0100000);
		enemy[1] = new Unit(0100001);
		enemy[2] = new Unit(0100002);
		enemy[3] = new Unit(0100003);
	}
	
	IEnumerator Start()
	{
		pManager.LoadFormTextToPlayerInfo();
		//UI things
		playerHPSlider.maxValue = player[0].MaximumHp;
		enemyHPSlider.maxValue = enemy[0].MaximumHp;
		playerHPSlider.value = player[0].CurrentHp;
		enemyHPSlider.value = enemy[0].CurrentHp;
		playerAttackSlider.maxValue = maxGauge;
		enemyAttackSlider.maxValue = maxGauge;
		playerAttackSlider.value = playerTurnGauge;
		enemyAttackSlider.value = enemyTurnGauge;
		
		//전투 시작 (플레이어 진영과 적군 진영에 최소 각각 하나이상 살아있어야 전투시작)
		while ((IsPlayerCampAlive() == true) && (IsEnemyCampAlive() == true))
		{
			CheckDeath();
			//Battle info
			Debug.Log("[      HP      ]  " + player[0].Name + " : " + player[0].CurrentHp + " | " + player[1].Name + " : " + player[1].CurrentHp + " | " + player[2].Name + " : " + player[2].CurrentHp + " | " + player[3].Name + " : " + player[3].CurrentHp + " | " + enemy[0].Name + " : " + enemy[0].CurrentHp + " | " + enemy[1].Name + " : " + enemy[1].CurrentHp + " | " + enemy[2].Name + " : " + enemy[2].CurrentHp + " | " + enemy[3].Name + " : " + enemy[3].CurrentHp);
			Debug.Log("[Turn Gauge] " + player[0].Name + " : " + player[0].TurnGauge + " | " + player[1].Name + " : " + player[1].TurnGauge + " | " + player[2].Name + " : " + player[2].TurnGauge + " | " + player[3].Name + " : " + player[3].TurnGauge + " | " + enemy[0].Name + " : " + enemy[0].TurnGauge + " | " + enemy[1].Name + " : " + enemy[1].TurnGauge + " | " + enemy[2].Name + " : " + enemy[2].TurnGauge + " | " + enemy[3].Name + " : " + enemy[3].TurnGauge);
			yield return new WaitForSeconds(0.5f);
			
			
			switch (IsGaugeOverHundered()) //턴게이지 100넘는 creature가 있는지 확인
			{
			case true: //true이면 게이지가 가장높은 creature 부터 공격시작
				yield return StartCoroutine(FindAttackOrder());
				CheckDeath();
				break;
				
			case false: //false 이면(턴게이지 100넘는 creature가 없으면) 각각 게이지 채우기
				for (int i = 0; i < 4; i++)
				{
					if (player[i].CurrentHp > 0)
						player[i].TurnGauge += player[i].AttackSpeed;
					if (enemy[i].CurrentHp > 0)
						enemy[i].TurnGauge += enemy[i].AttackSpeed;
				}
				break;
			}
		}
		
		if (IsPlayerCampAlive() == false)
		{
			Defeat();
		}
		else if (IsEnemyCampAlive() == false)
		{	
			pManager.SavePlayerInfo();
			Victory();
		}
	}

	
	public void DestroyThisObject()
	{
		for(int i =0 ; i< playerObject.Length; i++) 
		{ 
			Destroy(playerObject[i]); 
		}
		Destroy (GameObject.Find("Players"));	Destroy (GameObject.Find("Enemies"));
		Destroy (GameObject.Find("Canvas"));	Destroy (GameObject.Find("BattleManager"));
		Destroy (GameObject.Find("EventSystem"));
		Destroy (GameObject.Find("Main Camera"));
		Destroy (GameObject.Find("the_grid"));
		Destroy (this);
	}
	
	void OnGUI()
	{
		if (Dungeon.currentRoom.leftWall == false && Dungeon.currentRoom.roomIndex !=8 && IsEnemyCampAlive() == false && IsPlayerCampAlive() == true) {
			if (GUI.Button (new Rect (5, 160, 100, 20), "left"))
			{
				Dungeon.currentRoom = Dungeon.roomList[Dungeon.currentRoom.roomIndex-3];
				DestroyThisObject();
			}
		}
		if (Dungeon.currentRoom.rightWall == false && Dungeon.currentRoom.roomIndex !=8 && IsEnemyCampAlive() == false && IsPlayerCampAlive() == true) {
			if (GUI.Button (new Rect (320, 160, 100, 20), 
			                "right"))
			{
				Dungeon.currentRoom = Dungeon.roomList[Dungeon.currentRoom.roomIndex+3];
				DestroyThisObject();
			}
		}
		if (Dungeon.currentRoom.topWall == false && Dungeon.currentRoom.roomIndex !=8 && IsEnemyCampAlive() == false && IsPlayerCampAlive() == true) {
			if (GUI.Button (new Rect (160, 5, 100, 20), "top"))
			{
				Dungeon.currentRoom = Dungeon.roomList[Dungeon.currentRoom.roomIndex+1];
				DestroyThisObject();
			}
		}
		if (Dungeon.currentRoom.bottomWall == false && Dungeon.currentRoom.roomIndex !=8 && IsEnemyCampAlive() == false && IsPlayerCampAlive() == true) {
			if (GUI.Button (new Rect (160, 320, 100, 20), "bottom"))
			{
				Dungeon.currentRoom = Dungeon.roomList[Dungeon.currentRoom.roomIndex-1];
				DestroyThisObject();
			}
		}
	}
	
	//Move pointA to pointB
	IEnumerator MoveObject(GameObject target, Vector2 startPos, Vector2 endPos, float time)
	{
		float i = 0.0f;
		float rate = 5.0f / time;
		while (i < 1.0f)
		{
			i += Time.deltaTime * rate;
			target.transform.position = Vector2.Lerp(startPos, endPos, i);
			yield return null;
		}
	}
	
	//Find Who has the Max TurnGauge and Do Pattern
	//턴게이지가 가장 높은 순서대로 공격을 하고, 턴게이지가 같은경우 플레이어0123 에너미0123 순서대로 공격
	IEnumerator FindAttackOrder()
	{
		int maxTurnGauge = Mathf.Max(player[0].TurnGauge, player[1].TurnGauge, player[2].TurnGauge, player[3].TurnGauge, enemy[0].TurnGauge, enemy[1].TurnGauge, enemy[2].TurnGauge, enemy[3].TurnGauge);
		
		//for Pattern work
		int maxAttackSpeedEnemy = Mathf.Max(enemy[0].AttackSpeed, enemy[1].AttackSpeed, enemy[2].AttackSpeed, enemy[3].AttackSpeed); //공격속도가 가장 빠른적 찾기
		int minAttackSpeedEnemy = Mathf.Min(enemy[0].AttackSpeed, enemy[1].AttackSpeed, enemy[2].AttackSpeed, enemy[3].AttackSpeed); //공격속도가 가장 느린적 찾기
		int maxHpEnemy = Mathf.Max(enemy[0].CurrentHp, enemy[1].CurrentHp, enemy[2].CurrentHp, enemy[3].CurrentHp); //가장 Hp가 많이 남은 enemy 찾기
		
		//Players' Pattern
		for (int i = 0; i < 4; i++)
		{
			if (maxTurnGauge == player[i].TurnGauge && player[i].CurrentHp > 0)                            //player[i]의 턴게이지가 가장 높으면
			{
				Debug.Log("→ " + player[i].Name + "의 턴");
				if (enemy[i].CurrentHp > 0)
					yield return StartCoroutine(AttackEnemy(i, i));             //패턴1 : 기본적으로 공격자가 바라보는 적(Attack(i,i))을 공격하고
				else
					for (int j = 0; j < 4; j++)
				{
					if (maxHpEnemy == enemy[j].CurrentHp)                   //패턴2 : 패턴1의 대상이 죽었을 경우 HP가 가장 많은 적을 공격
					{
						yield return StartCoroutine(AttackEnemy(i, j));
						break;
					}
				}
			}
			
			else if (maxTurnGauge == player[i].TurnGauge && player[i].CurrentHp <= 0)
			{
				player[i].TurnGauge = 0;
			}
			
			//Enemies' pattern
			else if (maxTurnGauge == enemy[i].TurnGauge && enemy[i].CurrentHp > 0)                         //enemy[i]의 턴게이지가 가장 높으면
			{
				Debug.Log("→ " + enemy[i].Name + "의 턴");
				if (enemy[i].CurrentHp > 0)
					yield return StartCoroutine(AttackPlayer(i, i));             //패턴1
				else
					for (int j = 0; j < 4; j++)
				{
					if (player[j].CurrentHp > 0)
					{
						yield return StartCoroutine(AttackPlayer(i, j));    //패턴5
						break;
					}
				}
			}
			else if (maxTurnGauge == enemy[i].TurnGauge && enemy[i].CurrentHp <= 0)
			{
				enemy[i].TurnGauge = 0;
			}
		}
		
		/*------------------------------------------------------------
         * 패턴 1 : 같은 방향에 있는 적을 공격한다. Attack(i,i)
         * 패턴 2 : 살아있는 적 중 Hp가 가장 많은 적을 공격한다.
         * 패턴 3 : 살아있는 적 중 공격속도가 가장 빠른 적을 공격한다.
         * 패턴 4 : 살아있는 적 중 공격속도가 가장 느린 적을 공격한다.
         * 패턴 5 : 살아있는 적 중 아무나 공격한다.
        ---------------------------------------------------------------*/
		
		/* for문 안쓴 ~
            if (max == player[0].TurnGauge)
            {
                Debug.Log("→ " + player[0].Name + "의 턴");
                yield return new WaitForSeconds(0.5f);

                if (enemy[0].CurrentHp > 0)
                    yield return StartCoroutine(AttackEnemy(0, 0));
                else if (enemy[1].CurrentHp > 0)
                    yield return StartCoroutine(AttackEnemy(0, 1));
                else if (enemy[2].CurrentHp > 0)
                    yield return StartCoroutine(AttackEnemy(0, 2));
                else if (enemy[3].CurrentHp > 0)
                    yield return StartCoroutine(AttackEnemy(0, 3));
            }
            else if (max == player[1].TurnGauge)
            {
                Debug.Log("→ " + player[1].Name + "의 턴");
                yield return new WaitForSeconds(0.5f);
             
                if (enemy[1].CurrentHp > 0)
                    yield return StartCoroutine(AttackEnemy(1, 1));
                else if (enemy[0].CurrentHp > 0)
                    yield return StartCoroutine(AttackEnemy(1, 0));
                else if (enemy[2].CurrentHp > 0)
                    yield return StartCoroutine(AttackEnemy(1, 2));
                else if (enemy[3].CurrentHp > 0)
                    yield return StartCoroutine(AttackEnemy(1, 3));
            }
            else if (max == player[2].TurnGauge)
            {
                Debug.Log("→ " + player[2].Name + "의 턴");
                yield return new WaitForSeconds(0.5f);

                if (enemy[2].CurrentHp > 0)
                    yield return StartCoroutine(AttackEnemy(2, 2));
                else if (enemy[0].CurrentHp > 0)
                    yield return StartCoroutine(AttackEnemy(2, 0));
                else if (enemy[1].CurrentHp > 0)
                    yield return StartCoroutine(AttackEnemy(2, 1));
                else if (enemy[3].CurrentHp > 0)
                    yield return StartCoroutine(AttackEnemy(2, 3));
            }
            else if (max == player[3].TurnGauge)
            {
                Debug.Log("→ " + player[3].Name + "의 턴");
                yield return new WaitForSeconds(0.5f);
                yield return StartCoroutine(AttackEnemy(3, 3));
            }
            else if (max == enemy[0].TurnGauge)
            {
                Debug.Log("→ " + enemy[0].Name + "의 턴");
                yield return new WaitForSeconds(0.5f);
                yield return StartCoroutine(AttackPlayer(0, 0));
            }
            else if (max == enemy[1].TurnGauge)
            {
                Debug.Log("→ " + enemy[1].Name + "의 턴");
                yield return new WaitForSeconds(0.5f);
                yield return StartCoroutine(AttackPlayer(1, 1));
            }
            else if (max == enemy[2].TurnGauge)
            {
                Debug.Log("→ " + enemy[0].Name + "의 턴");
                yield return new WaitForSeconds(0.5f);
                yield return StartCoroutine(AttackPlayer(2, 2));
            }
            else if (max == enemy[3].TurnGauge)
            {
                Debug.Log("→ " + enemy[3].Name + "의 턴");
                yield return new WaitForSeconds(0.5f);
                yield return StartCoroutine(AttackPlayer(3, 3));
            }
         */
	}
	
	public void CheckDeath()
	{
		for (int i = 0; i < 4; i++)
		{
			if (player[i].CurrentHp < 0)
			{
				//Debug.Log(player[i].Name + "이(가) 쓰러졌습니다.");    //todo: 한번만 뜨게 고쳐야함
				player[i].CurrentHp = 0;
				player[i].TurnGauge = 0;
				Destroy(playerObject[i]);
			}
			if (enemy[i].CurrentHp <= 0)
			{
				//Debug.Log(enemy[i].Name + "을(를) 물리쳤습니다.");    //todo: 한번만 뜨게 고쳐야함
				enemy[i].CurrentHp = 0;
				enemy[i].TurnGauge = 0;
				Destroy(enemyObject[i]);
			}
		}
	}
	
	public bool IsPlayerCampAlive()
	{
		if (player[0].CurrentHp > 0 || player[1].CurrentHp > 0 || player[2].CurrentHp > 0 || player[3].CurrentHp > 0)
			return true;
		else
			return false;
	}
	public bool IsEnemyCampAlive()
	{
		if (enemy [0].CurrentHp > 0 || enemy [1].CurrentHp > 0 || enemy [2].CurrentHp > 0 || enemy [3].CurrentHp > 0)
			return true;
		else 
		{
			return false;
		}
	}
	public bool IsGaugeOverHundered()
	{
		if (player[0].TurnGauge >= 100 || player[1].TurnGauge >= 100 || player[2].TurnGauge >= 100 || player[3].TurnGauge >= 100 ||
		    enemy[0].TurnGauge >= 100 || enemy[1].TurnGauge >= 100 || enemy[2].TurnGauge >= 100 || enemy[3].TurnGauge >= 100)
			return true;
		else
			return false;
	}
	
	
	//Player's Attack
	//todo : DRY! 
	IEnumerator AttackEnemy(int attacker, int target)
	{
		int damageToEnemy = Random.Range(player[attacker].MinimumDamage, player[attacker].MaximumDamage);
		
		if (damageToEnemy > 0)
		{
			switch (target)
			{
			case 0:
				enemy[target].CurrentHp -= damageToEnemy;
				yield return StartCoroutine(MoveObject(playerObject[attacker], playerPosition[attacker], enemyPosition[target], 1.5f));
				yield return enemyObject[target].GetComponent<Renderer>().material.color = Color.red;
				GameObject.Find("Main Camera").GetComponent<CameraShake>().DoShake();
				yield return StartCoroutine(MoveObject(playerObject[attacker], enemyPosition[target], playerPosition[attacker], 3.0f));
				break;
			case 1:
				enemy[target].CurrentHp -= damageToEnemy;
				yield return StartCoroutine(MoveObject(playerObject[attacker], playerPosition[attacker], enemyPosition[target], 1.5f));
				yield return enemyObject[target].GetComponent<Renderer>().material.color = Color.red;
				GameObject.Find("Main Camera").GetComponent<CameraShake>().DoShake();
				yield return StartCoroutine(MoveObject(playerObject[attacker], enemyPosition[target], playerPosition[attacker], 3.0f));
				break;
			case 2:
				enemy[target].CurrentHp -= damageToEnemy;
				yield return StartCoroutine(MoveObject(playerObject[attacker], playerPosition[attacker], enemyPosition[target], 1.5f));
				yield return enemyObject[target].GetComponent<Renderer>().material.color = Color.red;
				GameObject.Find("Main Camera").GetComponent<CameraShake>().DoShake();
				yield return StartCoroutine(MoveObject(playerObject[attacker], enemyPosition[target], playerPosition[attacker], 3.0f));
				break;
			case 3:
				enemy[target].CurrentHp -= damageToEnemy;
				yield return StartCoroutine(MoveObject(playerObject[attacker], playerPosition[attacker], enemyPosition[target], 1.5f));
				yield return enemyObject[target].GetComponent<Renderer>().material.color = Color.red;
				GameObject.Find("Main Camera").GetComponent<CameraShake>().DoShake();
				yield return StartCoroutine(MoveObject(playerObject[attacker], enemyPosition[target], playerPosition[attacker], 3.0f));
				break;
			}
			//GameObject.Find("Main Camera").GetComponent<CameraShake>().DoShake();
			enemyHPSlider.value = enemy[0].CurrentHp;
			yield return enemyObject[target].GetComponent<Renderer>().material.color = Color.white;      //todo : OnCollisionEnter2D 사용해서 충돌했을때 애니메이션, 빨간색 으로 바꾸기
			enemyText.text = System.Convert.ToInt32(damageToEnemy) + " damage!";
			Debug.Log(player[attacker].Name + "이(가) " + enemy[target].Name + "에게 " + damageToEnemy + "의 데미지를 입혔습니다. " + enemy[target].Name + " HP : " + enemy[target].CurrentHp + "/" + enemy[target].MaximumHp);
		}
		else //빗나감
		{
			yield return StartCoroutine(MoveObject(playerObject[attacker], playerPosition[attacker], enemyPosition[target], 1.5f));
			Debug.Log("Miss!");
			enemyText.text = "Miss!";
			yield return StartCoroutine(MoveObject(playerObject[attacker], enemyPosition[target], playerPosition[attacker], 3.0f));
		}
		player[attacker].TurnGauge -= 100;
	}
	
	//Enemies Attack
	IEnumerator AttackPlayer(int attacker, int target)
	{
		int damageToPlayer = Random.Range(enemy[attacker].MinimumDamage, enemy[attacker].MaximumDamage);
		
		if (damageToPlayer > 0)
		{
			switch (target)
			{
			case 0:
				player[target].CurrentHp -= damageToPlayer;
				yield return StartCoroutine(MoveObject(enemyObject[attacker], enemyPosition[attacker], playerPosition[target], 1.5f));
				yield return playerObject[target].GetComponent<Renderer>().material.color = Color.red;
				GameObject.Find("Main Camera").GetComponent<CameraShake>().DoShake();
				yield return StartCoroutine(MoveObject(enemyObject[attacker], playerPosition[target], enemyPosition[attacker], 3.0f));
				break;
			case 1:
				player[target].CurrentHp -= damageToPlayer;
				yield return StartCoroutine(MoveObject(enemyObject[attacker], enemyPosition[attacker], playerPosition[target], 1.5f));
				yield return playerObject[target].GetComponent<Renderer>().material.color = Color.red;
				GameObject.Find("Main Camera").GetComponent<CameraShake>().DoShake();
				yield return StartCoroutine(MoveObject(enemyObject[attacker], playerPosition[target], enemyPosition[attacker], 3.0f));
				break;
			case 2:
				player[target].CurrentHp -= damageToPlayer;
				yield return StartCoroutine(MoveObject(enemyObject[attacker], enemyPosition[attacker], playerPosition[target], 1.5f));
				yield return playerObject[target].GetComponent<Renderer>().material.color = Color.red;
				GameObject.Find("Main Camera").GetComponent<CameraShake>().DoShake();
				yield return StartCoroutine(MoveObject(enemyObject[attacker], playerPosition[target], enemyPosition[attacker], 3.0f));
				break;
			case 3:
				player[target].CurrentHp -= damageToPlayer;
				yield return StartCoroutine(MoveObject(enemyObject[attacker], enemyPosition[attacker], playerPosition[target], 1.5f));
				yield return playerObject[target].GetComponent<Renderer>().material.color = Color.red;
				GameObject.Find("Main Camera").GetComponent<CameraShake>().DoShake();
				yield return StartCoroutine(MoveObject(enemyObject[attacker], playerPosition[target], enemyPosition[attacker], 3.0f));
				break;
			}
			playerHPSlider.value = player[0].CurrentHp;
			yield return playerObject[target].GetComponent<Renderer>().material.color = Color.white;
			playerText.text = System.Convert.ToInt32(damageToPlayer) + " damage!";
			Debug.Log(enemy[attacker].Name + "이(가) " + player[target].Name + "에게 " + damageToPlayer + "의 데미지를 입혔습니다. " + player[target].Name + " HP : " + player[target].CurrentHp + "/" + player[target].MaximumHp);
		}
		else //빗나감
		{
			yield return StartCoroutine(MoveObject(enemyObject[attacker], enemyPosition[attacker], playerPosition[target], 1.5f));
			Debug.Log("Miss!");
			playerText.text = "Miss!";
			yield return StartCoroutine(MoveObject(enemyObject[attacker], playerPosition[target], enemyPosition[attacker], 3.0f));
		}
		enemy[attacker].TurnGauge -= 100;
	}
	
	
	
	//Player's Victory
	public void Victory()
	{
		Debug.Log("Victory!");
		
		Destroy(GameObject.Find("Enemy"));
		Destroy(GameObject.Find("EnemyHPSlider"));
		Destroy(GameObject.Find("EnemyAttackSlider"));
		
		int rewardExp = (enemy[0].RewardExperiencePoint + enemy[1].RewardExperiencePoint + enemy[2].RewardExperiencePoint + enemy[3].RewardExperiencePoint) / 4;
		for (int i = 0; i < 4; i++)
		{
			player[i].ExperiencePoint += rewardExp;
		}
		//player[0].ExperiencePoint += rewardExp;
		
		Debug.Log("경험치 " + rewardExp + "을 각각 획득하였습니다. [Current EXP] " + player[0].Name + ":" + player[0].ExperiencePoint + " | " + player[1].Name + ":" + player[1].ExperiencePoint + " | " + player[2].Name + ":" + player[2].ExperiencePoint + " | " + player[3].Name + ":" + player[3].ExperiencePoint);
		
		//todo : LevelUpCheck() method
		//todo : 가장 많은 데미지를 입힌 MVP 캐릭터 표시 (그 캐릭터가 죽었을 경우는?)
		//todo : 경험치를 어떻게 분배할지 결정. 나눠서 골고루 나눠주기, 한명 몰아주기 고를수있게 할까?
		//todo : more rewards - gold, item, etc.
	}
	
	//Player's Defeat
	public void Defeat()
	{
		Debug.Log("Defeat");
		Destroy(GameObject.Find("Player"));
		Destroy(GameObject.Find("PlayerHPSlider"));
		Destroy(GameObject.Find("PlayerAttackSlider"));
		
		//todo : Add death penalty (ex. enemy stole 10% of your gold / Condition down / exp losing )
		//todo : 마을 Scene으로 귀환
	}
}