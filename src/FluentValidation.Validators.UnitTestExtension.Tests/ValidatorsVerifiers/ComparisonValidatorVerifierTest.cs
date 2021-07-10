﻿namespace FluentValidation.Validators.UnitTestExtension.Tests.ValidatorsVerifiers
{
    using System;
    using System.Collections.Generic;
    using Exceptions;
    using Helpers;
    using Helpers.Fakes;
    using ValidatorVerifiers;
    using Xunit;
    using Xunit.Sdk;

    public class ComparisonValidatorVerifierTest
    {
        [Fact]
        public void Given_CustomValidatorType_When_ConstructingVerifier_Then_Exception()
        {
            // Act & Assert
            Assert.Throws<ComparisonNotProvidedException>(() => new ComparisonValidatorVerifier<FakeComparisonValidator<object, int>, object, int>(10));
        }

        [Fact]
        public void Given_DifferentValidatorType_When_Verifying_Then_ValidationMustFail()
        {
            // Arrange
            var otherValidator = new LessThanValidator<object, int>(15);
            var verifier = new ComparisonValidatorVerifier<LessThanOrEqualValidator<object, int>, object, int>(10);

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(otherValidator), "(wrong type)");
        }

        [Fact]
        public void Given_CorrectValidatorWithDifferentValueToCompare_When_Verifying_Then_ValidationFail()
        {
            // Arrange
            var comparisonValidator = new LessThanValidator<object, int>(15);
            var verifier = new ComparisonValidatorVerifier<LessThanValidator<object, int>, object, int>(10);

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(comparisonValidator), "(ValueToCompare property)");
        }

        [Fact]
        public void Given_CorrectBuildInValidatorWithDifferentValueToCompare_When_Verifying_Then_ValidationFail()
        {
            // Arrange
            var comparisonValidator = new LessThanValidator<object, int>(10);
            var verifier = new ComparisonValidatorVerifier<LessThanValidator<object, int>, object, int>(15);

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(comparisonValidator), "(ValueToCompare property)");
        }

        [Fact]
        public void Given_CorrectValidatorWithDifferentMemberToCompare_When_Verifying_Then_ValidationFail()
        {
            // Arrange
            var memberToCompare1 = typeof(FakeMemberInfoProvider).GetMember("Member1")[0];
            var memberToCompare2 = typeof(FakeMemberInfoProvider).GetMember("Member2")[0];
            var comparisonValidator = new LessThanValidator<object, int>(o => 0, memberToCompare1, memberToCompare1.Name);
            var verifier = new ComparisonValidatorVerifier<LessThanValidator<object, int>, object, int>(null, Comparison.LessThan, memberToCompare2);

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(comparisonValidator), "(MemberToCompare property)");
        }

        [Fact]
        public void Given_CorrectValidatorWithDifferentComparison_When_Verifying_Then_ValidationFail()
        {
            // Arrange
            var comparisonValidator = new LessThanValidator<object, int>(10);
            var verifier = new ComparisonValidatorVerifier<LessThanValidator<object, int>, object, int>(10, Comparison.NotEqual);

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(comparisonValidator), "(Comparison property)");
        }

        [Fact]
        public void Given_CorrectValidatorWithSameValueAndBuildInValidator_When_Verifying_Then_ValidationPass()
        {
            // Arrange
            var comparisonValidator = new LessThanValidator<object, int>(15);
            var verifier = new ComparisonValidatorVerifier<LessThanValidator<object, int>, object, int>(15);

            // Act & Assert
            AssertExtension.NotThrows(() => verifier.Verify(comparisonValidator));
        }

        [Fact]
        public void Given_CorrectValidatorWithSameValueToCompareAndComparison_When_Verifying_Then_ValidationPass()
        {
            // Arrange
            var comparisonValidator = new LessThanValidator<object, int>(15);
            var verifier = new ComparisonValidatorVerifier<LessThanValidator<object, int>, object, int>(15);

            // Act & Assert
            AssertExtension.NotThrows(() => verifier.Verify(comparisonValidator));
        }

        [Fact]
        public void Given_CorrectValidatorWithSameValueToCompareAndComparisonAndMemberToCompare_When_Verifying_Then_ValidationPass()
        {
            // Arrange
            var memberToCompare1A = typeof(FakeMemberInfoProvider).GetMember("Member1")[0];
            var memberToCompare1B = typeof(FakeMemberInfoProvider).GetMember("Member1")[0];
            var comparisonValidator = new LessThanValidator<object, int>(o => 0, memberToCompare1A, memberToCompare1A.Name);
            var verifier = new ComparisonValidatorVerifier<LessThanValidator<object, int>, object, int>(null, Comparison.LessThan, memberToCompare1B);

            // Act & Assert
            AssertExtension.NotThrows(() => verifier.Verify(comparisonValidator));
        }

        [Theory]
        [MemberData(nameof(ProvideBuildInComparisonValidatorMapping))]
        public void Given_BuildInValidator_When_Verifying_Then_ValidationPass<TComparisonValidator, T, TProperty>(ComparisonValidatorVerifier<TComparisonValidator, T, TProperty> verifier, TComparisonValidator validator) where TComparisonValidator : AbstractComparisonValidator<T, TProperty> where TProperty : IComparable<TProperty>, IComparable
        {
            // Act & assert
            AssertExtension.NotThrows(() => verifier.Verify(validator));
        }

        public static IEnumerable<object[]> ProvideBuildInComparisonValidatorMapping()
        {
            // Arrange
            const int sameValue = 1;

            yield return new object[] {new ComparisonValidatorVerifier<LessThanValidator<object, int>, object, int>(sameValue), new LessThanValidator<object, int>(sameValue)};
            yield return new object[] {new ComparisonValidatorVerifier<LessThanOrEqualValidator<object, int>, object, int>(sameValue), new LessThanOrEqualValidator<object, int>(sameValue)};
            yield return new object[] {new ComparisonValidatorVerifier<GreaterThanValidator<object, int>, object, int>(sameValue), new GreaterThanValidator<object, int>(sameValue)};
            yield return new object[] {new ComparisonValidatorVerifier<GreaterThanOrEqualValidator<object, int>, object, int>(sameValue), new GreaterThanOrEqualValidator<object, int>(sameValue)};
        }
    }
}
