using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public sealed class SubscriptionEnum
    {
        private SubscriptionEnum(string value) { Value = value; }

        public string Value { get; set; }

        public static SubscriptionEnum Activated => new SubscriptionEnum("Activated");
        public static SubscriptionEnum Disabled => new SubscriptionEnum("Disabled"); 
        public static SubscriptionEnum Unknown => new SubscriptionEnum("Unknown");

        public static SubscriptionEnum FromString(string value)
        {
            return
                value.Equals(SubscriptionEnum.Activated.Value, StringComparison.InvariantCultureIgnoreCase) ? SubscriptionEnum.Activated :
                    value.Equals(SubscriptionEnum.Disabled.Value, StringComparison.InvariantCultureIgnoreCase) ? SubscriptionEnum.Disabled :
                        SubscriptionEnum.Unknown;
        }

        public SubscriptionEnum() { }
    }
}
