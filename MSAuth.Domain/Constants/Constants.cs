using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAuth.Domain.Constants
{
    public class Constants
    {
        public readonly struct NotificationKeys
        {
            public static readonly string APP_NOT_FOUND = "APP_NOT_FOUND";
            public static readonly string USER_ALREADY_EXISTS = "USER_ALREADY_EXISTS";
            public static readonly string ENTITY_VALIDATION_ERROR = "ENTITY_VALIDATION_ERROR";
        }
    }
}
