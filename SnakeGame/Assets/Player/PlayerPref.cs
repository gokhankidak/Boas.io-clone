using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerData", menuName = "ObjectPref/PlayerPref")]
public class PlayerPref : ScriptableObject
{
    [SerializeField]private float speed = 4;
    public float Scale { get => speed/8;}
    public float Speed { get => speed; set => speed= value; }
}
