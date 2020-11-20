namespace AcFunDanmu.Models.Client
{
    public sealed record SignIn
    {
        public int result { get; init; }
        public string img { get; init; }
        public long userId { get; init; }
        public string username { get; init; }
    }
}
