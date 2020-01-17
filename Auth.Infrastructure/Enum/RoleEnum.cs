using System.ComponentModel;

namespace Auth.Infrastructure.Enum
{
    public enum RoleEnum
    {

        [Description("Admin access")]
        ADMIN = 100,
        [Description("Access to reports")]
        PUBLIC = 200,
        [Description("Access to edit data")]
        CONTRIBUTOR = 300,
        [Description("Reading access")]
        READER = 400,

    }
}
