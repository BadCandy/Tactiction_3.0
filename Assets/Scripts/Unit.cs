using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;

public class Unit : MonoBehaviour
{	
	//공동 사용
	private int _id;
	private bool _isAlive = true;
	private int _minimumDamage;
	private int _maximumDamage;
	private int _turnGauge;

	//플레이어 사용
	private string _name;
	private short _level;
	private int _currentHp;
	private int _maximumHp;
	private int _attackSpeed;
	private int _hitTheTarget;
	private int _critical;
	private int _experiencePoint;
	private int _strength;
	private int _constitution;
	private int _intelligence;
	private int _dexterity;
	private int _luck;
	private int _defensivePower;
	private int _dodge;
	
	private int _rewardExperiencePoint;

	public string Name
	{
		get { return _name; }
		set { _name = value; }
	}
	public short Level
	{
		get { return _level; }
		set { _level = value; }
	}
	public int CurrentHp
	{
		get { return _currentHp; }
		set { _currentHp = value; }
	}
	public int MaximumHp
	{
		get { return _maximumHp; }
		set { _maximumHp = value; }
	}
	public bool IsAlive
	{
		get { return _isAlive; }
		set { _isAlive = value; }
	}
	public int MinimumDamage
	{
		get { return _minimumDamage; }
		set { _minimumDamage = value; }
	}
	public int MaximumDamage
	{
		get { return _maximumDamage; }
		set { _maximumDamage = value; }
	}
	public int AttackSpeed
	{
		get { return _attackSpeed; }
		set { _attackSpeed = value; }
	}
	public int TurnGauge
	{
		get { return _turnGauge; }
		set { _turnGauge = value; }
	}
	public int HitTheTarget
	{
		get { return _hitTheTarget; }
		set { _hitTheTarget = value; }
	}
	public int Critical
	{
		get { return _critical; }
		set { _critical = value; }
	}
	public int ExperiencePoint
	{
		get { return _experiencePoint; }
		set { _experiencePoint = value; }
	}
	public int Strength
	{
		get { return _strength; }
		set { _strength = value; }
	}
	public int Constitution
	{
		get { return _constitution; }
		set { _constitution = value; }
	}
	public int Intelligence
	{
		get { return _intelligence; }
		set { _intelligence = value; }
	}
	public int Dexterity
	{
		get { return _dexterity; }
		set { _dexterity = value; }
	}
	public int Luck
	{
		get { return _luck; }
		set { _luck = value; }
	}
	public int DefensivePower
	{
		get { return _defensivePower; }
		set { _defensivePower = value; }
	}
	public int Dodge
	{
		get { return _dodge; }
		set { _dodge = value; }
	}
	public int Id
	{
		get
		{
			return _id;
		}
		set
		{
			_id = value;
		}
	}
	public int RewardExperiencePoint
	{
		get
		{
			return _rewardExperiencePoint;
		}
		set
		{
			_rewardExperiencePoint = value;
		}
	}

	public Unit(int id)
	{
		PmlElement data;
		//	int[] a;
		
		try
		{
			if(id >= 0000000 && id < 0100000)
			{
				data  = PmlLoader.Instance.Load(string.Format("Data/Players/Xml/{0:0000000}", id));
				_id = id;
				_name = data.GetStringValue("data/name");
				int _baseHp = data.GetIntValue("data/baseHp");
				int _baseAttackSpeed = data.GetIntValue("data/baseAttackspeed");
				string _type = data.GetStringValue("data/type");
				_strength = data.GetIntValue("data/str");
				_constitution = data.GetIntValue("data/con");
				_intelligence = data.GetIntValue("data/int");
				_dexterity = data.GetIntValue("data/dex");
				_luck = data.GetIntValue("data/luk");
				_maximumHp = _baseHp + _constitution * 5;
				CurrentHp = _baseHp + _constitution * 5;
				_attackSpeed = _baseAttackSpeed + _dexterity * 2;
				if (_type == "magic")
				{
					_minimumDamage = 300;
					_maximumDamage = 300;
				}
				else
				{
					_minimumDamage = 300;
					_maximumDamage = 300;
				}
			}
			else 
			{
				data  = PmlLoader.Instance.Load(string.Format("Data/Enemies/Xml/{0:0000000}", id));
				_id = data.GetIntValue ("data/id");
				Name = data.GetStringValue ("data/name");
				CurrentHp = data.GetIntValue ("data/currentHP");
				MaximumHp = data.GetIntValue ("data/maximumHp");
				MinimumDamage = data.GetIntValue ("data/minimumDamage");
				MaximumDamage = data.GetIntValue ("data/maximumDamage");
				_rewardExperiencePoint = data.GetIntValue ("data/rewardExperiencePoint");
				AttackSpeed = data.GetIntValue ("data/speed");
			}
			
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
		}
		
	}
	//			_currentHp = a[0];
	//			a[0] = 1;
	
	
	//			int[] b = TextConnecter.Instance.Save (string.Format ("Data/Players/Text/{0:0000000}.txt", id), a);
	//			_baseHp = b[0];
	//			_maximumHp = b[0];
	//			_currentHp = b[0];
	//			_maximumHp = a[0];
	//			int _baseHp = a[0];
	
}