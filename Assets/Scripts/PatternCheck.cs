using UnityEngine;
using System.Collections;

public class PatternCheck : MonoBehaviour 
{
	// player          / hpAvr / 50% . less / skill
	// one of the enemy/ hp    / 50% . over / skill
	// 

	Unit unit;

	public void patternCheck(Unit[] target, int condition1, int condition2, int overLess, int skill)
	{
		for( int pattern = 1 ; pattern <= 3 ; pattern++)
		{
			if(Percent(target, condition1, condition2, overLess))
			{
				//call skill
				break;
			}
			else if(Value(target, condition1, condition2, overLess))
			{
				//call skill
			}
			else if(buffState(target, condition1, skill))
			{

			}
			else
			{
				pattern++;
			}
        }
		//End Turn
	}

	public bool Percent(Unit[] target, int _condition1, int _integer, int _overLess)
	{//condition 1.1 = HpAvr, condition 2 = 50%, (overless = 0 - Over, 1 -Less)
	///condition 1.2 = One of the Enemy:Hp
	///condition 1.3 = TurnGaugeAvr
		int hpSum = 0;
		int turnGaugeSum = 0;
		int temp = 0;

		switch (_condition1) 
		{
		case 1:// HpAvr
		{
			temp = 0;
			foreach (Unit unit in target) 
			{
				hpSum += unit.CurrentHp;
				temp++;	
			}

			int HpAvr = hpSum / temp;
	
			if(_overLess == 0 )
			{
				if ( HpAvr >= _integer)
						return true;
				else
						return false;
			}else
			{
				if ( HpAvr >= _integer)
					return true;
				else
					return false;
			}
		}
	
		case 2:// One of The Enemy:Hp
		{
			foreach (Unit unit in target) 
			{
				if(_overLess == 0)
				{
					if ( (unit.MaximumHp / unit.CurrentHp) * 100 <= _integer)
						return true;
				}else
				{
					if ( (unit.MaximumHp / unit.CurrentHp) * 100 >= _integer)
						return true;
				}
			}
			return false;
		}

		case 3:// HpAvr
		{
			temp = 0;
			foreach (Unit unit in target) 
			{
				turnGaugeSum += unit.TurnGauge;
				temp++;	
			}
			
			int turnGaugeAvr = turnGaugeSum / temp;
			
			if(_overLess == 0 )
			{
				if ( turnGaugeAvr >= _integer)
					return true;
				else
					return false;
			}else
			{
				if ( turnGaugeAvr >= _integer)
					return true;
				else
					return false;
			}
			
		}
		}
		return false;
	}

	public bool Value(Unit[] target, int _condition1, int _integer, int _overLess)
	{//condition 1 = HpValue , condition 2 = (50), (overless = 0 - Over, 1 -Less)
		int hpSum = 0;

		switch (_condition1) 
		{ // HpAvr
		case 1:
		{
			foreach (Unit unit in target) 
			{
				hpSum += unit.CurrentHp;
			}
			
			if(_overLess == 0 )
			{
				if ( hpSum >= _integer)
					return true;
				else
					return false;
			}else
			{
				if ( hpSum >= _integer)
					return true;
				else
					return false;
			}
			
		}
			
		case 2:// One of The Enemy : Hp
		{

			foreach (Unit unit in target) 
			{
				if(_overLess == 0)
				{
					if (unit.CurrentHp <= _integer)
						return true;
				}else
				{
					if (unit.CurrentHp >= _integer)
						return true;
				}
			}
			return false;
	
		}

//		case 3:// One of The Enemy : stat
//		{
//			
//			foreach (Player unit in target) 
//			{
//				if(OverLess = 0)
//				{
//					if (unit.CurrentHp <= _integer)
//						return true;
//				}else
//				{
//					if (unit.CurrentHp >= _integer)
//						return true;
//				}
//			}
//			return false;
//			
//			break;
//		}
		}
		return false;
	}
	public bool buffState(Unit[] target, int condition1, int onOff)
	{
		switch (condition1) 
		{
		    case 1 : // BuffState : Avr
		    {
			    foreach (Unit unit in target) 
			    {
				    //unit.buffState == on
				    //call DebuffSkill
			    }
		    }
            break;
		    case 2 : // BussState : One of the Player or Enemy
            {
			    foreach (Unit unit in target) 
			    {
				    //unit.buffState == on
				    //call DebuffSkill
			    }
            }
            break;
		 }
        return false;
	}
}

