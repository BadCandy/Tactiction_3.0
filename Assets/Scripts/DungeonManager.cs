using UnityEngine;
using System.Collections;

/*던전을 생성하는데 필요한것들을 관리해주는 클래스*/
public class DungeonManager : ScriptableObject
{
	private PmlElement data;
	private Unit[] unitSet;
	private int id;
	public DungeonManager()
	{
		data = PmlLoader.Instance.Load(string.Format("Data/Players/Xml/{0:0000000}", id));
	}
}

