using FluentValidation.TestHelper;
using Wholesaler.DataTransferObject;
using Wholesaler.DataTransferObject.Validators;

namespace Wholesaler.IntegrationTests.Validators
{
    public class ProductQueryValidatorTests
    {
        public static IEnumerable<object[]> GetSampleValidData()
        {
            var list = new List<ProductQuery>()
            {
                new ProductQuery()
                {
                    PageNumber = 1,
                    PageSize = 3
                },
                new ProductQuery()
                {
                    PageNumber = 10,
                    PageSize = 5
                },
                new ProductQuery()
                {
                    PageNumber = 100,
                    PageSize = 10
                },
                new ProductQuery()
                {
                    PageNumber = 5,
                    PageSize = 3
                }
            };

            return list.Select(q => new object[] { q });
        }

        public static IEnumerable<object[]> GetSampleIncorrectData()
        {
            var list = new List<ProductQuery>()
            {
                new ProductQuery()
                {
                    PageNumber = 0,
                    PageSize = 1
                },
                new ProductQuery()
                {
                    PageNumber = 0,
                    PageSize = 3
                },
                new ProductQuery()
                {
                    PageNumber = 1,
                    PageSize = 4
                },
                new ProductQuery()
                {
                    PageNumber = 2,
                    PageSize = 11
                }
            };

            return list.Select(q => new object[] { q });
        }


        [Theory]
        [MemberData(nameof(GetSampleValidData))]
        public void Validate_ForCorrectModel_ReturnSucess(ProductQuery model)
        {
            // arrange 
            var validator = new ProductQueryValidator();

            // act
            var result = validator.TestValidate(model);

            // assert 
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [MemberData(nameof(GetSampleIncorrectData))]
        public void Validate_ForIncorrectModel_ReturnSucess(ProductQuery model)
        {
            // arrange 
            var validator = new ProductQueryValidator();

            // act
            var result = validator.TestValidate(model);

            // assert 
            result.ShouldHaveAnyValidationError();
        }
    }
}
