

using GoogleSheet.Core.Type;
using GoogleSheet.Type;
using System;

namespace UGS
{
    [Type(typeof(bool), new string[] { "bool" })]
    public class boolReader : IType
    {
        public object DefaultValue => null;
        public object Read(string _value)
        {
            bool result;
            if (Boolean.TryParse(_value, out result))
                return result;
            else return false;
        }

        public string Write(object value)
        {
            return null;
        }
    }

    [UGS(typeof(GuestCategory))]
    public enum GuestCategory
    {
        Normal,
        Special_Wait,
        Special_Move
    }
    [UGS(typeof(BackgroundType))]
    public enum BackgroundType
    {
        Wall,
        Floor,
        Sky,
        Wall_Side,
        Star,
        Moon
    }
    [UGS(typeof(QuestType))]
    public enum QuestType
    {
        Daily,
        Achievement,
        Guide
    }
    [UGS(typeof(Place))]
    public enum Place
    {
        Restaurant,
        Kitchen,
        Terrace
    }
}

