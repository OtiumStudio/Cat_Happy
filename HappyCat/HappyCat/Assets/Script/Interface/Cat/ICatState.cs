using UnityEngine;

namespace HC.Game
{
    public interface ICatState
    {
        void Enter();
        void Update();
        void Exit();
    }
}

