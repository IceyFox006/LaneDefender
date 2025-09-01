using UnityEngine;

[CreateAssetMenu(fileName = "EntitySO", menuName = "Scriptable Objects/EntitySO")]
public class EntitySO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Enums.EntityAlligence _alligence;
    [SerializeField] private int _maxHP;
    [SerializeField] private float _speed;

    public int MaxHP { get => _maxHP; set => _maxHP = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public Enums.EntityAlligence Alligence { get => _alligence; set => _alligence = value; }
    public string Name { get => _name; set => _name = value; }
}
