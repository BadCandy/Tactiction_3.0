using UnityEngine;
using System.Collections;

public class Skill : MonoBehaviour
{

    //미구현
    
    private int _id;
    private string _name;
    private int _minimumDamage;
    private int _maximumDamage;

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
    public int MinimumDamage 
    {
        get 
        {
            return _minimumDamage;
        }
        set 
        {
            _minimumDamage = value;
        }
    }
    public int MaximumDamage
    {
        get
        {
            return _maximumDamage;
        }
        set
        {
            _maximumDamage = value;
        }
    }

    public Skill(int _id, string _name, int _minimumDamage, int _maximumDamage)
    {
        Id = _id;
        Name = _name;
        MinimumDamage = _minimumDamage;
        MaximumDamage = _maximumDamage;
    }
}
