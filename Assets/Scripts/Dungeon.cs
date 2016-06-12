using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using System.IO;

/*순수하게 던전 생성*/
public class Dungeon : MonoBehaviour
{
	public GameObject dungeonRoom;
	public GameObject widthRoomWall;
	public GameObject heightRoomWall;
	
	
	public static List<RoomInfo> roomList;
	public static RoomInfo currentRoom;
	private int findAlgoNum;
	private List<int> indexList;
	
	
	
	void Awake()
	{
		CreateDungeon ();
		ReviseDungeon ();
		GUIWall ();
		
		//		Application.LoadLevel("Battle");
	}
	
	IEnumerator Start ()
	{	
		Application.LoadLevelAdditive ("Battle");
		yield return StartCoroutine ("nextScene");
		yield return StartCoroutine ("nextRoomScene");
	}
	
	void Update()
	{
		if (currentRoom.roomIndex != PermanentVariables.dungeonflag) {
			Application.LoadLevelAdditive ("Battle");
			PermanentVariables.dungeonflag = currentRoom.roomIndex;
		}
	}
	
	IEnumerator nextScene()
		
	{
		yield return new WaitForSeconds(2f);
	}
	
	IEnumerator nextRoomScene()
	{
		yield return new WaitForSeconds (2f);
	}
	
	
	/*	void OnGUI()
	{
		if (currentRoom.leftWall == false && currentRoom.roomIndex !=8 && BattleManager.IsEnemyCampAlive() == false && BattleManager.IsPlayerCampAlive() == true) {
			if (GUI.Button (new Rect (5, 160, 100, 20), "left"))
			{
				currentRoom = roomList[currentRoom.roomIndex-3];
				Camera.main.transform.position = new Vector3(currentRoom.position_X, currentRoom.position_Y, -1);
				//BattleManager.DontDestroyOnLoad(GameObject.Find("player"));
				BattleManager.playerPosition[0] = new Vector2(BattleManager.playerPosition[0].x - 10f, BattleManager.playerPosition[0].y);
				BattleManager.playerPosition[1] = new Vector2(BattleManager.playerPosition[1].x - 10f, BattleManager.playerPosition[1].y);
				BattleManager.playerPosition[2] = new Vector2(BattleManager.playerPosition[2].x - 10f, BattleManager.playerPosition[2].y);
				BattleManager.playerPosition[3] = new Vector2(BattleManager.playerPosition[3].x - 10f, BattleManager.playerPosition[3].y);
				Application.LoadLevel("Battle");

			}
		}
		if (currentRoom.rightWall == false && currentRoom.roomIndex !=8 && BattleManager.IsEnemyCampAlive() == false && BattleManager.IsPlayerCampAlive() == true) {
			if (GUI.Button (new Rect (320, 160, 100, 20), 
			                "right"))
			{
				currentRoom = roomList[currentRoom.roomIndex+3];
				Camera.main.transform.position = new Vector3(currentRoom.position_X, currentRoom.position_Y, -1);
				//BattleManager.DontDestroyOnLoad(GameObject.Find("player"));
				BattleManager.playerPosition[0] = new Vector2(BattleManager.playerPosition[0].x + 10f, BattleManager.playerPosition[0].y);
				BattleManager.playerPosition[1] = new Vector2(BattleManager.playerPosition[1].x + 10f, BattleManager.playerPosition[1].y);
				BattleManager.playerPosition[2] = new Vector2(BattleManager.playerPosition[2].x + 10f, BattleManager.playerPosition[2].y);
				BattleManager.playerPosition[3] = new Vector2(BattleManager.playerPosition[3].x + 10f, BattleManager.playerPosition[3].y);
				Application.LoadLevel("Battle");
			}
		}
		if (currentRoom.topWall == false && currentRoom.roomIndex !=8 && BattleManager.IsEnemyCampAlive() == false && BattleManager.IsPlayerCampAlive() == true) {
			if (GUI.Button (new Rect (160, 5, 100, 20), "top"))
			{
				currentRoom = roomList[currentRoom.roomIndex+1];
				Camera.main.transform.position = new Vector3(currentRoom.position_X, currentRoom.position_Y, -1);
				//BattleManager.DontDestroyOnLoad(GameObject.Find("player"));
				BattleManager.playerPosition[0] = new Vector2(BattleManager.playerPosition[0].x, BattleManager.playerPosition[0].y + 10f);
				BattleManager.playerPosition[1] = new Vector2(BattleManager.playerPosition[1].x, BattleManager.playerPosition[1].y + 10f);
				BattleManager.playerPosition[2] = new Vector2(BattleManager.playerPosition[2].x, BattleManager.playerPosition[2].y + 10f);
				BattleManager.playerPosition[3] = new Vector2(BattleManager.playerPosition[3].x, BattleManager.playerPosition[3].y + 10f);
				Application.LoadLevel("Battle");
			}
		}
		if (currentRoom.bottomWall == false && currentRoom.roomIndex !=8 && BattleManager.IsEnemyCampAlive() == false && BattleManager.IsPlayerCampAlive() == true) {
			if (GUI.Button (new Rect (160, 320, 100, 20), "bottom"))
			{
				currentRoom = roomList[currentRoom.roomIndex-1];
				Camera.main.transform.position = new Vector3(currentRoom.position_X, currentRoom.position_Y, -1);
				//BattleManager.DontDestroyOnLoad(GameObject.Find("player"));
				BattleManager.playerPosition[0] = new Vector2(BattleManager.playerPosition[0].x, BattleManager.playerPosition[0].y - 10f);
				BattleManager.playerPosition[1] = new Vector2(BattleManager.playerPosition[1].x, BattleManager.playerPosition[1].y - 10f);
				BattleManager.playerPosition[2] = new Vector2(BattleManager.playerPosition[2].x, BattleManager.playerPosition[2].y - 10f);
				BattleManager.playerPosition[3] = new Vector2(BattleManager.playerPosition[3].x, BattleManager.playerPosition[3].y - 10f);
				Application.LoadLevel("Battle");
			}
		}
	}*/
	
	public Dungeon()
	{
		
	}
	
	public void ReviseDungeon()
	{
		bool a;
		do{
			MakeRandomWall ();
			a = findAlgorithm ();
			Debug.Log(a);
		}while(!a);
	}
	
	public void GUIWall()
	{
		for (int i=0; i<roomList.Count; i++)
		{
			if(roomList[i].bottomWall)
				Instantiate(widthRoomWall, new Vector3(roomList[i].position_X, roomList[i].position_Y - 5, -1), Quaternion.identity);
			if(roomList[i].leftWall)
				Instantiate(heightRoomWall, new Vector3(roomList[i].position_X - 5, roomList[i].position_Y, -1), Quaternion.identity);
			if(roomList[i].topWall)
				Instantiate(widthRoomWall, new Vector3(roomList[i].position_X, roomList[i].position_Y + 5, -1), Quaternion.identity);
			if(roomList[i].rightWall)
				Instantiate(heightRoomWall, new Vector3(roomList[i].position_X + 5, roomList[i].position_Y, -1), Quaternion.identity);
		}
	}
	public void CreateDungeon()
	{
		roomList = new List<RoomInfo> ();
		int index = 0;
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				RoomInfo room = new RoomInfo();
				room.position_X = 5+i*10;
				room.position_Y = 5+j*10;
				room.roomIndex = index++;
				MakeWall(room);
				roomList.Add(room);
			}
			currentRoom = roomList[0];
		}
		for (int i = 0; i < roomList.Count; i++)
		{
			Instantiate(dungeonRoom, new Vector2(roomList[i].position_X, roomList[i].position_Y), Quaternion.identity);
		}
	}
	
	public void MakeWall(RoomInfo room)
	{
		if (room.position_X == 5)
			room.leftWall = true;
		else if (room.position_X == 25)
			room.rightWall = true;
		if (room.position_Y == 25)
			room.topWall = true;
		else if (room.position_Y == 5)
			room.bottomWall = true;
	}
	
	public void MakeRandomWall()
	{
		randomIndex (5);
		for (int i = 0; i < indexList.Count; i++)
		{
			roomWall(i);
		} 
	}
	
	public void randomIndex(int count)
	{
		indexList = new List<int>();
		for (int i = 0; i < count; ++i)
		{
			int num = Random.Range(0, 9);
			if (indexList != null)
			{
				for (int j = 0; j < indexList.Count; j++)
				{
					if (indexList[j] == num)
						break;
				}
			}
			indexList.Add(num);
		}
	}
	public void roomWall(int countNumber) //RoomInfo roomList[indexList[i]]
	{
		int num = Random.Range(0, 4);
		switch (num)
		{
		case 0:
			if (roomList[indexList[countNumber]].bottomWall != true)
			{
				roomList[indexList[countNumber]].bottomWall = true;
				if (roomList[indexList[countNumber]].roomIndex - 1 >= 0)
				{
					roomList[roomList[indexList[countNumber]].roomIndex - 1].topWall = true;
				}
				if (roomList[indexList[countNumber]].bottomWall == true && roomList[indexList[countNumber]].topWall == true && roomList[indexList[countNumber]].rightWall == true && roomList[indexList[countNumber]].leftWall == true)
				{
					roomList[indexList[countNumber]].bottomWall = false;
					if (roomList[indexList[countNumber]].roomIndex - 1 >= 0)
					{
						roomList[roomList[indexList[countNumber]].roomIndex - 1].topWall = false;
					}
					int num2 = Random.Range(0, 9);
					indexList[countNumber] = num2;
					roomWall(countNumber);
				}
			}
			else
			{
				//					if (roomList[indexList[countNumber]].bottomWall == true && roomList[indexList[countNumber]].topWall == true && roomList[indexList[countNumber]].rightWall == true && roomList[indexList[countNumber]].leftWall == true)
				//					{
				if(!(roomList[indexList[countNumber]].roomIndex == 0 || roomList[indexList[countNumber]].roomIndex == 3 || roomList[indexList[countNumber]].roomIndex == 6))
				{
					roomList[indexList[countNumber]].bottomWall = false;
					if (roomList[indexList[countNumber]].roomIndex - 1 >= 0)
					{
						roomList[roomList[indexList[countNumber]].roomIndex - 1].topWall = false;
					}
					int num2 = Random.Range(0, 9);
					indexList[countNumber] = num2;
					roomWall(countNumber);
				}
				else
				{
					roomList[indexList[countNumber]].topWall = false;
					roomList[indexList[countNumber] + 1].bottomWall = false;
					int num2 = Random.Range(0, 9);
					indexList[countNumber] = num2;
					roomWall (countNumber);
				}
				//					}
				//					else
				//						roomWall (countNumber);
			}
			break;
		case 1:
			if (roomList[indexList[countNumber]].leftWall != true)
			{
				roomList[indexList[countNumber]].leftWall = true;
				if (roomList[indexList[countNumber]].roomIndex - 3 >= 0)
				{
					roomList[roomList[indexList[countNumber]].roomIndex - 3].rightWall = true;
				}
				if (roomList[indexList[countNumber]].bottomWall == true && roomList[indexList[countNumber]].topWall == true && roomList[indexList[countNumber]].rightWall == true && roomList[indexList[countNumber]].leftWall == true)
				{
					roomList[indexList[countNumber]].leftWall = false;
					if (roomList[indexList[countNumber]].roomIndex - 3 >= 0)
					{
						roomList[roomList[indexList[countNumber]].roomIndex - 3].rightWall = false;
					}
					int num2 = Random.Range(0, 9);
					indexList[countNumber] = num2;
					roomWall(countNumber);
				}
			}
			else
			{
				//					if (roomList[indexList[countNumber]].bottomWall == true && roomList[indexList[countNumber]].topWall == true && roomList[indexList[countNumber]].rightWall == true && roomList[indexList[countNumber]].leftWall == true)
				//					{
				if(!(roomList[indexList[countNumber]].roomIndex == 0 || roomList[indexList[countNumber]].roomIndex == 1 || roomList[indexList[countNumber]].roomIndex == 2))
				{
					roomList[indexList[countNumber]].leftWall = false;
					if (roomList[indexList[countNumber]].roomIndex - 3 >= 0)
					{
						roomList[roomList[indexList[countNumber]].roomIndex - 3].rightWall = false;
					}
					int num2 = Random.Range(0, 9);
					indexList[countNumber] = num2;
					roomWall(countNumber);
				}
				else
				{
					roomList[indexList[countNumber]].rightWall = false;
					roomList[indexList[countNumber] + 3].leftWall = false;
					int num2 = Random.Range(0, 9);
					indexList[countNumber] = num2;
					roomWall (countNumber);
				}
				//					}
				//					else
				//						roomWall (countNumber);
			}
			break;
		case 2:
			if (roomList[indexList[countNumber]].topWall != true)
			{
				roomList[indexList[countNumber]].topWall = true;
				if (roomList[indexList[countNumber]].roomIndex + 1 <= 8)
				{
					roomList[roomList[indexList[countNumber]].roomIndex + 1].bottomWall = true;
				}
				if (roomList[indexList[countNumber]].bottomWall == true && roomList[indexList[countNumber]].topWall == true && roomList[indexList[countNumber]].rightWall == true && roomList[indexList[countNumber]].leftWall == true)
				{
					roomList[indexList[countNumber]].topWall = false;
					if (roomList[indexList[countNumber]].roomIndex + 1 <= 8)
					{
						roomList[roomList[indexList[countNumber]].roomIndex + 1].bottomWall = false;
					}
					int num2 = Random.Range(0, 9);
					indexList[countNumber] = num2;
					roomWall(countNumber);
				}
			}
			else
			{
				//					if (roomList[indexList[countNumber]].bottomWall == true && roomList[indexList[countNumber]].topWall == true && roomList[indexList[countNumber]].rightWall == true && roomList[indexList[countNumber]].leftWall == true)
				//				{
				if(!(roomList[indexList[countNumber]].roomIndex == 2 || roomList[indexList[countNumber]].roomIndex == 5 || roomList[indexList[countNumber]].roomIndex == 8))
				{
					roomList[indexList[countNumber]].topWall = false;
					if (roomList[indexList[countNumber]].roomIndex + 1 <= 8)
					{
						roomList[roomList[indexList[countNumber]].roomIndex + 1].bottomWall = false;
					}
					int num2 = Random.Range(0, 9);
					indexList[countNumber] = num2;
					roomWall(countNumber);
				}
				else
				{
					roomList[indexList[countNumber]].bottomWall = false;
					roomList[indexList[countNumber] - 1].topWall = false;
					int num2 = Random.Range(0, 9);
					indexList[countNumber] = num2;
					roomWall (countNumber);
				}
				//					}
				//					else
				//						roomWall (countNumber);
			}
			break;
		case 3:
			if (roomList[indexList[countNumber]].rightWall != true)
			{
				roomList[indexList[countNumber]].rightWall = true;
				if (roomList[indexList[countNumber]].roomIndex + 3 <= 8)
				{
					roomList[roomList[indexList[countNumber]].roomIndex + 3].leftWall = true;
				}
				if (roomList[indexList[countNumber]].bottomWall == true && roomList[indexList[countNumber]].topWall == true && roomList[indexList[countNumber]].rightWall == true && roomList[indexList[countNumber]].leftWall == true)
				{
					roomList[indexList[countNumber]].rightWall = false;
					if (roomList[indexList[countNumber]].roomIndex + 3 <= 8)
					{
						roomList[roomList[indexList[countNumber]].roomIndex + 3].leftWall = false;
					}
					int num2 = Random.Range(0, 9);
					indexList[countNumber] = num2;
					roomWall(countNumber);
				}
			}
			else
			{
				//					if (roomList[indexList[countNumber]].bottomWall == true && roomList[indexList[countNumber]].topWall == true && roomList[indexList[countNumber]].rightWall == true && roomList[indexList[countNumber]].leftWall == true)
				//					{
				if(!(roomList[indexList[countNumber]].roomIndex == 6 || roomList[indexList[countNumber]].roomIndex == 7 || roomList[indexList[countNumber]].roomIndex == 8))
				{
					roomList[indexList[countNumber]].rightWall = false;
					if (roomList[indexList[countNumber]].roomIndex + 3 <= 8)
					{
						roomList[roomList[indexList[countNumber]].roomIndex + 3].leftWall = false;
					}
					int num2 = Random.Range(0, 9);
					indexList[countNumber] = num2;
					roomWall(countNumber);
				}
				else
				{
					roomList[indexList[countNumber]].leftWall = false;
					roomList[indexList[countNumber] - 3].rightWall = false;
					int num2 = Random.Range(0, 9);
					indexList[countNumber] = num2;
					roomWall (countNumber);
				}
				//					}
				//					else
				//						roomWall (countNumber);
			}
			break;
		}
	}
	
	public bool findAlgorithm()
	{
		findAlgoNum = 0;
		for (int i = 0; i < 20; i++) {
			findMove();
			if(findAlgoNum == 8)
			{
				return true;
			}
		}
		return false;
	} 
	
	public void findMove()
	{
		if (!roomList [findAlgoNum].topWall) {
			findAlgoNum = findAlgoNum + 1;
			return;
		} else if (!roomList [findAlgoNum].rightWall) {
			findAlgoNum = findAlgoNum + 3;
			return;
		} else if (!roomList [findAlgoNum].bottomWall) {
			findAlgoNum = findAlgoNum - 1;
			return;
		} else if (!roomList [findAlgoNum].leftWall) {
			findAlgoNum = findAlgoNum - 3;
			return;
		}
	}
}

public class RoomInfo
{
	public int position_X;
	public int position_Y;
	public int roomIndex;
	
	public bool rightWall = false;
	public bool leftWall = false;
	public bool topWall = false;
	public bool bottomWall = false;
}
