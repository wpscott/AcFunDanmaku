namespace AcFunDanmu.Models.Client
{
    public sealed record Play
    {
        public int result { get; init; }
        public int error_code { get; init; }
        public string error_msg { get; init; }
        public PlayData data { get; init; }

        public sealed record PlayData
        {
            public string[] availableTickets { get; init; }
            public string caption { get; init; }
            public string enterRoomAttach { get; init; }
            public string liveId { get; init; }
            public string videoPlayRes { get; init; }
        }
    }
}
