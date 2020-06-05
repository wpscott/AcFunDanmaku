namespace AcFunDanmu.Enums
{
    public static class Command
    {
        public const string REGISTER = "Basic.Register";
        public const string UNREGISTER = "Basic.Unregister";
        public const string KEEP_ALIVE = "Basic.KeepAlive";
        public const string PING = "Basic.Ping";
        public const string CLIENT_CONFIG_GET = "Basic.ClientConfigGet";

        public const string MESSAGE_SESSION = "Message.Session";
        public const string MESSAGE_PULL_OLD = "Message.PullOld";

        public const string GROUP_USER_GROUP_LIST = "Group.UserGroupList";

        public const string GLOBAL_COMMAND = "Global.ZtLiveInteractive.CsCmd";

        public const string PUSH_MESSAGE = "Push.ZtLiveInteractive.Message";

    }

    public static class GlobalCommand
    {
        public const string ENTER_ROOM = "ZtLiveCsEnterRoom";
        public const string ENTER_ROOM_ACK = "ZtLiveCsEnterRoomAck";
        public const string HEARTBEAT = "ZtLiveCsHeartbeat";
        public const string HEARTBEAT_ACK = "ZtLiveCsHeartbeatAck";
    }

    public static class PushMessage
    {
        public const string ACTION_SIGNAL = "ZtLiveScActionSignal";
        public const string STATE_SIGNAL = "ZtLiveScStateSignal";
        public const string STATUS_CHANGED = "ZtLiveScStatusChanged";
        public const string TICKET_INVALID = "ZtLiveScTicketInvalid";

        public static class ActionSignal
        {
            public const string COMMENT = "CommonActionSignalComment";
            public const string LIKE = "CommonActionSignalLike";
            public const string ENTER_ROOM = "CommonActionSignalUserEnterRoom";
            public const string FOLLOW = "CommonActionSignalUserFollowAuthor";
            public const string KICKED_OUT = "CommonNotifySignalKickedOut";
            public const string VIOLATION_ALERT = "CommonNotifySignalViolationAlert";
            public const string THROW_BANANA = "AcfunActionSignalThrowBanana";
            public const string GIFT = "CommonActionSignalGift";
        }

        public static class StateSignal
        {
            public const string ACFUN_DISPLAY_INFO = "AcfunStateSignalDisplayInfo";
            public const string DISPLAY_INFO = "CommonStateSignalDisplayInfo";
            public const string TOP_USRES = "CommonStateSignalTopUsers";
            public const string RECENT_COMMENT = "CommonStateSignalRecentComment";
        }
    }
}
