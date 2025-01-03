
using GoogleSheet.Core.Type;

namespace UGS
{
    [UGS(typeof(GuestCategory))]
    public enum GuestCategory
    {
        Normal,
        Special_Wait,
        Special_Move
    }

    [UGS(typeof(WorkerPage))]
    public enum WorkerPage
    {
        Restaurant,
        Kitchen,
        kitchen,
        Terrace
    }
}

