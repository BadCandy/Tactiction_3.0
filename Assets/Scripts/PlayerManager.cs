using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : ScriptableObject
{
	public Unit[] player;

	public int[] player_0_Info;
	public int[] player_1_Info;
	public int[] player_2_Info;
	public int[] player_3_Info;

	public PlayerManager()
	{
		player = new Unit[4];
	}

	public Unit[] makeOnePlayer(int id0)
	{
		player[0] = new Unit(id0);
		player[1] = null;
		player[2] = null;
		player[3] = null;;
		return player;
	}
	public Unit[] makeTwoPlayer(int id0, int id1)
	{
		player[0] = new Unit(id0);
		player[1] = new Unit(id1);
		player[2] = null;
		player[3] = null;
		return player;
	}
	public Unit[] makeThreePlayer(int id0, int id1, int id2)
	{
		player[0] = new Unit(id0);
		player[1] = new Unit(id1);
		player[2] = new Unit(id2);
		player[3] = null;
		return player;
	}
	public Unit[] makeFourPlayer(int id0, int id1, int id2, int id3)
	{
		player[0] = new Unit(id0);
		player[1] = new Unit(id1);
		player[2] = new Unit(id2);
		player[3] = new Unit(id3);
		return player;
	}

	public int getAverageLevel()
	{
		int count = 0;
		int sum = 0;
		for (int i = 0; i < player.Length ; i++) 
		{
			if(player[i] != null)
			{
				count++;	sum += player[i].Level;
			}
		}
		return sum;
	}

	public void LoadFormTextToPlayerInfo()
	{
		player_0_Info = TextConnecter.Instance.Load (string.Format ("Data/Players/Text/{0:0000000}.txt", 0000000));
		player_1_Info = TextConnecter.Instance.Load (string.Format ("Data/Players/Text/{0:0000000}.txt", 0000001));
		player_2_Info = TextConnecter.Instance.Load (string.Format ("Data/Players/Text/{0:0000000}.txt", 0000002));
		player_3_Info = TextConnecter.Instance.Load (string.Format ("Data/Players/Text/{0:0000000}.txt", 0000003));
		player[0].CurrentHp = player_0_Info[0];
		player[1].CurrentHp = player_1_Info[0];
		player[2].CurrentHp = player_2_Info[0];
		player[3].CurrentHp = player_3_Info[0];
	}

	public void SavePlayerInfo()
	{
		if (Dungeon.currentRoom.roomIndex != 8)
		{
			player_0_Info [0] = player [0].CurrentHp;
			player_1_Info [0] = player [1].CurrentHp;
			player_2_Info [0] = player [2].CurrentHp;
			player_3_Info [0] = player [3].CurrentHp;
			TextConnecter.Instance.Save (string.Format ("Data/Players/Text/{0:0000000}.txt", 0000000), player_0_Info);
			TextConnecter.Instance.Save (string.Format ("Data/Players/Text/{0:0000000}.txt", 0000001), player_1_Info);
			TextConnecter.Instance.Save (string.Format ("Data/Players/Text/{0:0000000}.txt", 0000002), player_2_Info);
			TextConnecter.Instance.Save (string.Format ("Data/Players/Text/{0:0000000}.txt", 0000003), player_3_Info);
		}
		else
		{
			player_0_Info [0] = player[0].MaximumHp;
			player_1_Info [0] = player[1].MaximumHp;
			player_2_Info [0] = player[2].MaximumHp;
			player_3_Info [0] = player[3].MaximumHp;
			TextConnecter.Instance.Save (string.Format ("Data/Players/Text/{0:0000000}.txt", 0000000), player_0_Info);
			TextConnecter.Instance.Save (string.Format ("Data/Players/Text/{0:0000000}.txt", 0000001), player_1_Info);
			TextConnecter.Instance.Save (string.Format ("Data/Players/Text/{0:0000000}.txt", 0000002), player_2_Info);
			TextConnecter.Instance.Save (string.Format ("Data/Players/Text/{0:0000000}.txt", 0000003), player_3_Info);
		}
	}


}

