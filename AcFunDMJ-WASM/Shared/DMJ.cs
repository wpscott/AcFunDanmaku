using System;

namespace AcFunDMJ_WASM.Shared
{
    public struct Comment
    {
        public string Name { get; set; }
        public string Content { get; set; }
    }
    public struct Like { public string Name { get; set; } }
    public struct Enter { public string Name { get; set; } }
    public struct Follow { public string Name { get; set; } }
    public struct Banana
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
    public struct Gift
    {
        public string Name { get; set; }
        public string ComboId { get; set; }
        public int Count { get; set; }
        public GiftInfo Detail { get; set; }

        public struct GiftInfo
        {
            public string Name { get; set; }
            public Uri Pic { get; set; }
        }
    }
}
