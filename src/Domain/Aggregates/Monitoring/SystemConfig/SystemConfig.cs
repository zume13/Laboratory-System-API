using SharedKernel.Primitives;
using SharedKernel.Shared;

namespace Domain.Aggregates.Monitoring.SystemConfig
{
    public class SystemConfig : AggregateRoot
    {
        private SystemConfig() { }
        private SystemConfig(
            Guid id,
            string key,
            string value)
            : base(id)
        {
            Key = key;
            Value = value;
            UpdatedAt = DateTime.UtcNow;
        }

        public string Key { get; private set; }

        public string Value { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public static ResultT<SystemConfig> Set(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key))
                return GeneralErrors.General.Empty(nameof(key));

            return new SystemConfig(Guid.NewGuid(), key, value ?? string.Empty);
        }

        public Result UpdateValue(string value)
        {
            Value = value ?? string.Empty;
            UpdatedAt = DateTime.UtcNow;

            return Result.Success();
        }
    }
}
