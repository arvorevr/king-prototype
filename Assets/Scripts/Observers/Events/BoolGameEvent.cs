using System;
using UnityEngine;

namespace Game.ScriptableObjects.Events
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Bool Event", menuName = "Game Bool Event", order = 57)]
    public class BoolGameEvent : GenericGameEvent<bool>
    {
    }
}