using UnityEngine;
namespace HC.Game
{
    enum E_PAGESTATE
    {
        TERRACE = -1,
        RESTAURANT = 0,
        KITCHEN = 1,
    }
    enum E_PAGEDIRECTION
    {
        NONE = 0,
        UP = 10,
        DOWN = -10,
        LEFT = -1,
        RIGHT = 1
    }
    enum E_ANIMATION
    {
        IDLE = 0,
        WALK,
        EAT,
        SAD,
        SIT
    }
}
