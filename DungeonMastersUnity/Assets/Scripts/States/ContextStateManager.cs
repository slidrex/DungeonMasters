using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace States
{
    public static class ContextStateManager
    {
        private static Dictionary<State, GameObject> _states = new();

        public static void RegisterState(State state, GameObject gameObject)
        {
            if(_states.ContainsKey(state)) _states.Remove(state);
            
            _states.Add(state, gameObject);
        }
        
        public static void SetState(State state)
        {
            var states = _states.ToArray();
            foreach (var s in states)
            {
                s.Value.SetActive(s.Key == state);
            }
        }

        public static void ClearState()
        {
            SetState(State.None);
        }
    }

    public enum State
    {
        None,
        Chat,
        Market,
    }
}