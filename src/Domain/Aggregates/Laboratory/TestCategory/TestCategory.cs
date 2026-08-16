using Domain.ValueObjects;
using PDDLPortal.Domain.ValueObjects;
using SharedKernel.Primitives;
using SharedKernel.Shared;

namespace Domain.Aggregates.Laboratory.TestCategory
{
    public class TestCategory : AggregateRoot
    {
        private TestCategory(
            Guid id,
            Name name,
            Money price)
            : base(id)
        {
            Name = name;
            Price = price;
            IsActive = true;
        }

        public Name Name { get; private set; }

        public Money Price { get; private set; }

        public bool IsActive { get; private set; }

        public static ResultT<TestCategory> Create(Name name, Money price)
        {
            if (string.IsNullOrWhiteSpace(name.value))
                return GeneralErrors.General.Empty(nameof(name));

            return new TestCategory(Guid.NewGuid(), name, price);
        }

        public Result UpdatePrice(decimal price)
        {
            var newPrice = Money.Create(price);

            if (newPrice.IsFailure)
                return newPrice.Error;

            Price = newPrice.value;

            return Result.Success();
        }

        public Result Deactivate()
        {
            if (!IsActive)
                return TestCategoryErrors.InactiveCategory(Name.value);

            IsActive = false;

            return Result.Success();
        }

        public Result Reactivate()
        {
            if (IsActive)
                return TestCategoryErrors.AlreadyActiveCategory(Name.value);

            IsActive = true;

            return Result.Success();
        }
    }

}
