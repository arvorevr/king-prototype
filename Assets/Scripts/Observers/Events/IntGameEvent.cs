using System;
using UnityEngine;

namespace Game.ScriptableObjects.Events
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Int Event", menuName = "Game Int Event", order = 56)]
    public class IntGameEvent : GenericGameEvent<int>
    {
    }
}