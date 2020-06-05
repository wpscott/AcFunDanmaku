namespace AcFunDanmu.Models.Client
{
    public class Play
    {
        public int result { get; set; }
        public int error_code { get; set; }
        public string error_msg { get; set; }
        public PlayData data { get; set; }

        public class PlayData
        {
            public string[] availableTickets { get; set; }
            public string caption { get; set; }
            public string enterRoomAttach { get; set; }
            public string liveId { get; set; }
            public string videoPlayRes { get; set; }
        }
    }
}
