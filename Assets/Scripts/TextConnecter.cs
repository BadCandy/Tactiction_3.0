using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

/* 캐시처럼 이미 사용했던값들을 저장하여 바로꺼내쓰면 좋겠지만
 * 배열 자체를 사용할 것인가, 메소드로 배열의 요소를 리턴값으로 받아 사용할 것인가
 * 팀원들과 상의해야 하므로 일단 이렇게 작성.
*/
public class TextConnecter : Singleton<TextConnecter>, IDisposable
{
	private int[] infoArray = null;
	
	public int[] Load(string path)
	{
		infoArray = new int[15];
		string text = "";
		//		string line = "";
		string[] strArray = new string[15];
		StreamReader reader = null;
		try
		{
			reader = new StreamReader(Application.dataPath + "/Resources/" + path);
		}
		catch
		{
			reader.Close();
			reader = new StreamReader(Application.dataPath + "/Resources/" + path);
		}
		if (reader == null)
		{
			Debug.LogError("Error : " + Application.dataPath + "/Resources/" + path);
		}
		else
		{
			/*line = reader.ReadLine();
			while (line != null)
			{
				text += line;
				line = reader.ReadLine();
			}*/
			text = reader.ReadToEnd();
			strArray = text.Split('/');
			reader.Close();
		}
		for (int i = 0; i < strArray.Length; i++)
		{
			infoArray[i] = int.Parse(strArray[i]);
		}
		return infoArray;
	}
	
	public int[] Save(string path, int[] infoArr)
	{
		StreamWriter writer = new StreamWriter(Application.dataPath + "/Resources/" + path);
		infoArray = new int[15];
		if (writer == null) 
		{
			Debug.LogError("Error : " + Application.dataPath + "/Resources/" + path);
		}
		else 
		{
			for(int i = 0 ; i<infoArr.Length ; i++)
			{
				infoArray[i] = infoArr[i];
			}
			writer.Write (addDelimeter(infoArray, '/'));
		}
		writer.Close();
		return infoArray;
	}

	public int[] SavePart(string path, int index, int value)
	{
		StreamWriter writer = new StreamWriter(Application.dataPath + "/Resources/" + path);
		infoArray = new int[15];
		int[] a = Load(path);
		for (int i=0; i<infoArray.Length; i++)
		{
			infoArray[i] = a [i];
		}
		infoArray[index] = value;
		if (writer == null) 
		{
			Debug.LogError("Error : " + Application.dataPath + "/Resources/" + path);
		}
		else 
		{
			writer.Write (addDelimeter(infoArray, '/'));
		}
		writer.Close();
		return infoArray;
	}
	
	
	/*	public void Save(string path, int[] infoArr = null, int index = -1, int value = -1)
	 * 	//싱글톤을 사용하지 않았을때 가능한 세이브기능(인스턴스대신 객체를 사용하므로)
	{
		StreamWriter writer = new StreamWriter(Application.dataPath + "/Resources/" + path);
		if (writer == null) 
		{
			Debug.LogError("Error : " + Application.dataPath + "/Resources/" + path);
		}
		else 
		{
			if (infoArr == null) 
			{
				Load(path);
				if(index > -1 && value > -1) 
				{
					infoArray[index] = value;
					writer.Write (addDelimeter(infoArray, '/'));
				}
			}
			else 
			{
				for(int i = 0 ; i<infoArr.Length ; i++)
				{
					infoArray[i] = infoArr[i];
				}
			writer.Write (addDelimeter(infoArray, '/'));
			}
		writer.Close();
		}
		return infoArray;
	}
*/
	
	public string addDelimeter(int[] array, char delimeter)
	{
		string text = "";
		for (int i = 0; i < array.Length; i++) 
		{
			if(i == array.Length-1)
				delimeter = '\0';
			string str = "" + array[i] + delimeter;
			text += str;
		}
		return text;
	}
	
	public void Dispose()
	{
		infoArray = null;
	}
}

