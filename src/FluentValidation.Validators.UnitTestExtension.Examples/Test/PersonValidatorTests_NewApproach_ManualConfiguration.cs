﻿#region License
// MIT License
// 
// Copyright(c) 2016 Michał Jankowski (http://www.jankowskimichal.pl)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// 
// The latest version of this file can be found at https://github.com/MichalJankowskii/FluentValidation.Validators.UnitTestExtension
#endregion

namespace FluentValidation.Validators.UnitTestExtension.Examples.Test
{
    using Core;
    using Production;
    using ValidatorVerifiers;
    using Xunit;

    public class PersonValidatorTests_NewApproach_ManualConfiguration
    {
        // Act
        readonly PersonValidator personValidator = new PersonValidator();

        [Fact]
        public void Given_When_PersonValidatorConstructing_Then_3PropertiesShouldHaveRules()
        {
            // Assert
            personValidator.ShouldHaveRulesCount(3);
        }

        [Fact]
        public void Given_When_PersonValidatorConstructing_Then_RulesForFirstNameAreConfiguredCorrectly()
        {
            // Assert
            personValidator.ShouldHaveRules(x => x.FirstName,
                new IValidatorVerifier[]
                {
                    new TypeValidatorVerifier<NotNullValidator>(),
                    new TypeValidatorVerifier<NotEmptyValidator>(),
                    new LengthValidatorVerifier<LengthValidator>(0, 20)
                });
        }

        [Fact]
        public void Given_When_PersonValidatorConstructing_Then_RulesForLastNameAreConfiguredCorrectly()
        {
            // Assert
            personValidator.ShouldHaveRules(x => x.LastName,
                new IValidatorVerifier[]
                {
                    new TypeValidatorVerifier<NotNullValidator>(),
                    new TypeValidatorVerifier<NotEmptyValidator>(),
                    new LengthValidatorVerifier<LengthValidator>(0, 20)
                });
        }

        [Fact]
        public void Given_When_PersonValidatorConstructing_Then_RulesForHeightAreConfiguredCorrectly()
        {
            // Assert
            personValidator.ShouldHaveRules(x => x.HeightInCentimeters,
                new IValidatorVerifier[]
                {
                    new ComparisonValidatorVerifier<GreaterThanValidator>(0),
                    new ComparisonValidatorVerifier<LessThanOrEqualValidator>(250),
                });
        }
    }
}
