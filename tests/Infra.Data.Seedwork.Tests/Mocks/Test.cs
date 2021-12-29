using Goal.Domain.Seedwork.Aggregates;

namespace Goal.Infra.Data.Seedwork.Tests.Mocks
{
    public class Test : Entity<int>
    {
        public bool Active { get; set; }

        public Test(int id)
            : this(id, true)
        {
        }

        public Test(int id, bool active)
            : this()
        {
            Id = id;
            Active = active;
        }

        public Test()
            : base()
        {
        }

        public void Deactivate() => Active = false;

        public void SetId(int id) => Id = id;
    }
}
