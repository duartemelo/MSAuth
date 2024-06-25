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
            public static readonly string USER_NOT_FOUND = "USER_NOT_FOUND";
            public static readonly string INVALID_USER_CREDENTIALS = "INVALID_USER_CREDENTIALS";
            public static readonly string ENTITY_VALIDATION_ERROR = "ENTITY_VALIDATION_ERROR";
            public static readonly string DATABASE_COMMIT_ERROR = "DATABASE_COMMIT_ERROR";
            public static readonly string USER_CONFIRMATION_NOT_FOUND = "USER_CONFIRMATION_NOT_FOUND";
            public static readonly string USER_CONFIRMATION_EXPIRED = "USER_CONFIRMATION_EXPIRED";
            public static readonly string USER_IS_NOT_CONFIRMED = "USER_IS_NOT_CONFIRMED";
            public static readonly string USER_CONFIRMATION_ALREADY_EXISTS = "USER_CONFIRMATION_ALREADY_EXISTS";
            public static readonly string USER_IS_ALREADY_CONFIRMED = "USER_IS_ALREADY_CONFIRMED";
            public static readonly string INVALID_REFRESH_TOKEN = "INVALID_REFRESH_TOKEN";
        }
    }
}
