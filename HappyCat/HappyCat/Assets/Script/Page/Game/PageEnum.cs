using UnityEngine;
namespace HC.Game
{
    public enum E_PAGESTATE
    {
        TERRACE = -1,
        RESTAURANT = 0,
        KITCHEN = 1,
    }
    public enum E_PAGEDIRECTION
    {
        NONE = 0,
        UP = 10,
        DOWN = -10,
        LEFT = -1,
        RIGHT = 1
    }
    public enum E_ANIMATION
    {
        IDLE = 0,
        WALK,
        EAT,
        SAD,
        SIT
    }
    public enum E_FOOD
    {
        RESTAURANT,
        KITCHEN
    }
    public enum E_DESTINATION
    {
        NONE,
        TABLE
    }
}
