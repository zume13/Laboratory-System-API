
namespace SharedKernel.Shared
{
    public static class GeneralErrors
    {
        public static class General
        {
            public static Error Empty(string emptyProperty) => Error.Failure($"{emptyProperty}.Empty", $"The {emptyProperty} cannot be empty");
            public static Error Invalid(string invalidProperty) => Error.Failure($"{invalidProperty}.Invalid", $"The {invalidProperty} is invalid");

        }

    }
}
