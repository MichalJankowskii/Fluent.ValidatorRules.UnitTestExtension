﻿namespace FluentValidation.Validators.UnitTestExtension.Tests.Helpers.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Resources;
    using Results;

    public class FakePropertyValidator : IPropertyValidator
    {
        public IEnumerable<ValidationFailure> Validate(PropertyValidatorContext context)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ValidationFailure>> ValidateAsync(PropertyValidatorContext context, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public bool ShouldValidateAsynchronously(IValidationContext context)
        {
            throw new NotImplementedException();
        }

        public string GetDefaultMessageTemplate(string errorCode)
        {
            throw new NotImplementedException();
        }

        public bool IsAsync { get; }
        public ICollection<Func<object, object, object>> CustomMessageFormatArguments { get; }
        public Func<PropertyValidatorContext, object> CustomStateProvider { get; set; }

        public Severity Severity { get; set; }

        public string Name => throw new NotImplementedException();
    }
}
