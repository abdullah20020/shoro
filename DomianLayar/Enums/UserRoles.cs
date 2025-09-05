namespace DomianLayar.Enums
{
    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string Lawyer = "Lawyer";
        public const string LawFirm = "LawFirm";
        public const string User = "User";

        public static readonly IReadOnlyList<string> AllRoles = new List<string>
        {
            Admin, Lawyer, LawFirm, User
        };
    }
}
