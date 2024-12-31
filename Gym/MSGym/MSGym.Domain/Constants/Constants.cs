namespace MSGym.Domain.Constants
{
    public class Constants
    {
        public readonly struct NotificationKeys
        {
            public static readonly string GYM_ALREADY_EXISTS = "GYM_ALREADY_EXISTS";
            public static readonly string USER_NOT_FOUND = "USER_NOT_FOUND";
            public static readonly string DATABASE_COMMIT_ERROR = "DATABASE_COMMIT_ERROR";
            public static readonly string USER_ALREADY_EXISTS = "USER_ALREADY_EXISTS";
            public static readonly string NO_EMAIL_FOUND_ON_CLAIM = "NO_EMAIL_FOUND_ON_CLAIM";
            public static readonly string GYM_NOT_FOUND = "GYM_NOT_FOUND";
            public static readonly string USER_HAS_NO_PERMISSION_TO_DELETE_GYM = "USER_HAS_NO_PERMISSION_TO_DELETE_GYM";
            public static readonly string GYM_IS_NOT_DELETABLE = "GYM_IS_NOT_DELETABLE";
        }
    }
}
