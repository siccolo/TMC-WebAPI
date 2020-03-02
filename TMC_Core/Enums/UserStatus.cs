using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public sealed class UserStatusEnum
    {
        private UserStatusEnum(string value) { Value = value; }

        public string Value { get; set; }

        public static UserStatusEnum Activated => new UserStatusEnum("Activated");
        public static UserStatusEnum Disabled => new UserStatusEnum("Disabled");
        public static UserStatusEnum Suspended => new UserStatusEnum("Suspended");

        public static UserStatusEnum Unknown => new UserStatusEnum("Unknown");

        public static UserStatusEnum FromString(string value)
        {
            return
                value.Equals(UserStatusEnum.Activated.Value, StringComparison.InvariantCultureIgnoreCase) ? UserStatusEnum.Activated :
                    value.Equals(UserStatusEnum.Disabled.Value, StringComparison.InvariantCultureIgnoreCase) ? UserStatusEnum.Disabled :
                    value.Equals(UserStatusEnum.Suspended.Value, StringComparison.InvariantCultureIgnoreCase) ? UserStatusEnum.Suspended :
                        UserStatusEnum.Unknown;
        }

        public UserStatusEnum() { }
    }
}
