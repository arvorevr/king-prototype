using System;
using UnityEngine;

namespace Game.ScriptableObjects.Events
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Int Event", menuName = "Game Int Event", order = 59)]
    public class ObjectGameEvent : GenericGameEvent<GameObject>
    {
    }
}

