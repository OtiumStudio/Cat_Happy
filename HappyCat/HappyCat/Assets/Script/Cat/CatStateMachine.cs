using UnityEngine;

namespace HC.Game
{
    public class CatStateMachine
    {
        private ICatState currentState;

        public void ChangeState(ICatState state)
        {
            currentState?.Exit();
            currentState = state;
            currentState.Enter();
        }

        public void Update()
        {
            currentState?.Update();
        }
    }
}

