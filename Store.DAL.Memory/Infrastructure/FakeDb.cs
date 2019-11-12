namespace Store.DAL.Memory.Infrastructure
{
    internal static class FakeDb
    {
        public static ProductTable Products { get; } = new ProductTable();
    }
}