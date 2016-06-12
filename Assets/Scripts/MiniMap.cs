using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour
{
	
	public Transform Target;
	
	void LateUpdate()
	{
		Target.position = new Vector3 (Dungeon.currentRoom.position_X, Dungeon.currentRoom.position_Y, Target.position.z);
	}
}
