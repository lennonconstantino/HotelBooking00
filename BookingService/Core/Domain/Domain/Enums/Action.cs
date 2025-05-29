
namespace Domain.Enums
{
    public enum Action
    {
        Pay = 0,
        Finish, // after paid and used
        Cancel, // can never be paid
        Refound, // Paid then refound
        Reopen, // canceled
    }
}
