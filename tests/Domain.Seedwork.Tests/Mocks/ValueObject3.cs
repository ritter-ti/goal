namespace Ritter.Domain.Tests.ValueObjects.Mocks
{
    internal class ValueObject3 : ValueObject
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public ValueObject3 ValueObject { get; set; }
    }
}
