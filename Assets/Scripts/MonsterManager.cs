using UnityEngine;
using System.Collections;

public class MonsterManager : ScriptableObject
{
	public Unit[] monster;
	
	public MonsterManager()
	{
		monster = new Unit[4];
	}
	
	public Unit[] makeOneMonster(int id0)
	{
		monster[0] = new Unit(id0);
		monster[1] = null;
		monster[2] = null;
		monster[3] = null;;
		return monster;
	}
	public Unit[] makeTwoMonster(int id0, int id1)
	{
		monster[0] = new Unit(id0);
		monster[1] = new Unit(id1);
		monster[2] = null;
		monster[3] = null;
		return monster;
	}
	public Unit[] makeThreeMonster(int id0, int id1, int id2)
	{
		monster[0] = new Unit(id0);
		monster[1] = new Unit(id1);
		monster[2] = new Unit(id2);
		monster[3] = null;
		return monster;
	}
	public Unit[] makeFourMonster(int id0, int id1, int id2, int id3)
	{
		monster[0] = new Unit(id0);
		monster[1] = new Unit(id1);
		monster[2] = new Unit(id2);
		monster[3] = new Unit(id3);
		return monster;
	}
	
	public int getAverageLevel()
	{
		int count = 0;
		int sum = 0;
		for (int i = 0; i < monster.Length ; i++) 
		{
			if(monster[i] != null)
			{
				count++;	sum += monster[i].Level;
			}
		}
		return sum;
	}

}

