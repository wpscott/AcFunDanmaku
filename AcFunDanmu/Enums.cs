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
        public const string USER_EXIT = "ZtLiveCsUserExit";
        public const string USER_EXIT_ACK = "ZtLiveCsUserExitAck";
    }

    public static class PushMessage
    {
        public const string ACTION_SIGNAL = "ZtLiveScActionSignal";
        public const string STATE_SIGNAL = "ZtLiveScStateSignal";
        public const string NOTIFY_SIGNAL = "ZtLiveScNotifySignal";
        public const string STATUS_CHANGED = "ZtLiveScStatusChanged";
        public const string TICKET_INVALID = "ZtLiveScTicketInvalid";

        public static class ActionSignal
        {
            public const string COMMENT = "CommonActionSignalComment";
            public const string LIKE = "CommonActionSignalLike";
            public const string ENTER_ROOM = "CommonActionSignalUserEnterRoom";
            public const string FOLLOW = "CommonActionSignalUserFollowAuthor";
            public const string THROW_BANANA = "AcfunActionSignalThrowBanana";
            public const string GIFT = "CommonActionSignalGift";
            public const string RICH_TEXT = "CommonActionSignalRichText";
        }

        public static class StateSignal
        {
            public const string ACFUN_DISPLAY_INFO = "AcfunStateSignalDisplayInfo";
            public const string DISPLAY_INFO = "CommonStateSignalDisplayInfo";
            public const string TOP_USRES = "CommonStateSignalTopUsers";
            public const string RECENT_COMMENT = "CommonStateSignalRecentComment";
            public const string CHAT_CALL = "CommonStateSignalChatCall";
            public const string CHAT_ACCEPT = "CommonStateSignalChatAccept";
            public const string CHAT_READY = "CommonStateSignalChatReady";
            public const string CHAT_END = "CommonStateSignalChatEnd";
            public const string CURRENT_RED_PACK_LIST = "CommonStateSignalCurrentRedpackList";
            public const string AUTHOR_CHAT_CALL = "CommonStateSignalAuthorChatCall";
            public const string AUTHOR_CHAT_ACCEPT = "CommonStateSignalAuthorChatAccept";
            public const string AUTHOR_CHAT_READY = "CommonStateSignalAuthorChatReady";
            public const string AUTHOR_CHAT_END = "CommonStateSignalAuthorChatEnd";
            public const string AUTHOR_CHAT_CHANGE_SOUND_CONFIG = "CommonStateSignalAuthorChatChangeSoundConfig";
        }

        public static class NotifySignal
        {
            public const string KICKED_OUT = "CommonNotifySignalKickedOut";
            public const string VIOLATION_ALERT = "CommonNotifySignalViolationAlert";
            public const string LIVE_MANAGER_STATE = "CommonNotifySignalLiveManagerState";
        }
    }
}
