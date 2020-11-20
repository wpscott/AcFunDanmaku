namespace AcFunDanmu.Models.Client
{
    public sealed record SafetyId
    {
        public int code { get; init; }
        public string msg { get; init; }
        public string safety_id { get; init; }
    }
}
