﻿namespace FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Exceptions;
    using FluentAssertions;


    // TODO: Czy sa testy jednostkowe
    public class ComparisonValidatorVerifier<TComparisonValidator, T, TProperty> : TypeValidatorVerifier<TComparisonValidator> where TComparisonValidator : PropertyValidator<T, TProperty>
    {
        private static readonly Dictionary<Type, Comparison> ComparisonValidatorSetUp = new Dictionary<Type, Comparison>()
        {
            {typeof(EqualValidator<T, TProperty>), Comparison.Equal},
            {typeof(NotEqualValidator<T, TProperty>), Comparison.NotEqual},
        };

        private readonly object valueToCompare;

        private readonly Comparison? comparison;

        private readonly MemberInfo memberToCompare;

        public ComparisonValidatorVerifier(object valueToCompare, Comparison? comparison = null, MemberInfo memberToCompare = null)
        {
            this.valueToCompare = valueToCompare;
            this.comparison = comparison;
            this.memberToCompare = memberToCompare;

            if (this.comparison == null)
            {
                if (ComparisonValidatorSetUp.ContainsKey(typeof(TComparisonValidator)))
                {
                    this.comparison = ComparisonValidatorSetUp[typeof(TComparisonValidator)];
                }
                else
                {
                    throw new ComparisonNotProvidedException();
                }
            }
        }

        public override void Verify<TValidator>(TValidator validator)
        {
            base.Verify(validator);
            ((IComparisonValidator)validator).ValueToCompare.Should().BeEquivalentTo(this.valueToCompare, "(ValueToCompare property)");
            ((IComparisonValidator)validator).Comparison.Should().BeEquivalentTo(this.comparison, "(Comparison property)");
            if (this.memberToCompare != null)
            {
                ((IComparisonValidator) validator).MemberToCompare.Should().BeEquivalentTo(this.memberToCompare, "(MemberToCompare property)");
            }
        }
    }
}
