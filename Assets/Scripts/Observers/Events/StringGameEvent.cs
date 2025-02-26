using System;
using UnityEngine;

namespace Game.ScriptableObjects.Events
{
    [Serializable]
    [CreateAssetMenu(fileName = "New String Event", menuName = "Game String Event", order = 58)]
    public class StringGameEvent : GenericGameEvent<string>
    {
    }

}
