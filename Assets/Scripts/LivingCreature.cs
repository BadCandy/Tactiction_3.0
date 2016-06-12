using UnityEngine;
using System.Collections;

public class LivingCreature : MonoBehaviour
{
    private string _name;
    private int _currentHp;
    private int _maximumHp;
    private int _attackSpeed;
    
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }

    public int CurrentHp
    {
        get
        {
            return _currentHp;
        } 
        set
        {
            _currentHp = value;
        } 
    }
    public int MaximumHp
    {
        get 
        {
            return _maximumHp;
        }
        set
        {
            _maximumHp = value;
        }
    }
    public int AttackSpeed
    {
        get
        {
            return _attackSpeed;
        }
        set
        {
            _attackSpeed = value;
        }
    }
    public LivingCreature()
    {
    }

    public LivingCreature(string _name, int _currentHp, int _maximumHp, int _attackSpeed)
    {
        Name = _name;
        CurrentHp = _currentHp;
        MaximumHp = _maximumHp;
        AttackSpeed = _attackSpeed;
    }
}
