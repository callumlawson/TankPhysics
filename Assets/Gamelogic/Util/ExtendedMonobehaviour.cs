using System;
using UnityEngine;

namespace Assets.Gamelogic.Util
{
    public class ExtendedMonobehaviour : MonoBehaviour
    {
        public void Invoke(Action task, float time)
        {
            Invoke(task.Method.Name, time);
        }
    }
}
