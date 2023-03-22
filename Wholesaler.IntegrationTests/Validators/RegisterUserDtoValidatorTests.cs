using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using Wholesaler.Data;
using Wholesaler.DataTransferObject;
using Wholesaler.DataTransferObject.Validators;
using Wholesaler.Models;

namespace Wholesaler.IntegrationTests.Validators
{
    public class RegisterUserDtoValidatorTests
    {
        private readonly DataContext _dbContext;

        public static IEnumerable<object[]> GetSampleIncorrectData()
        {
            var list = new List<CreateUserDto>()
            {
                new CreateUserDto()
                {
                    Email = "test1@test.com",
                    Password = "password",
                    ConfirmPassword = "password"
                },
                new CreateUserDto()
                {
                   Email = "test@test.com",
                   Password = "short",
                   ConfirmPassword = "short"
                },
                new CreateUserDto()
                {
                    Email = "test@test.com",
                    Password = "password",
                    ConfirmPassword = "otherPassword"
                }
            };

            return list.Select(q => new object[] { q });
        }

        public RegisterUserDtoValidatorTests()
        {
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseInMemoryDatabase("TestDb");

            _dbContext = new DataContext(builder.Options);
            Seed();
        }

        public void Seed()
        {
            var testUsers = new List<User>()
            {
                new User()
                {
                    Email = "test1@test.com"
                },
                new User()
                {
                    Email = "test2@test.com"
                }
            };

            _dbContext.Users.AddRange(testUsers);
            _dbContext.SaveChanges();
        }

        [Fact]
        public void Validate_ForCorrectModel_ReturnSuccess()
        {
            // arrange
            var model = new CreateUserDto()
            {
                Email = "test@test.com",
                Password = "password",
                ConfirmPassword = "password"
            };

            var validator = new RegisterUserDtoValidator(_dbContext);

            // act
            var result = validator.TestValidate(model);

            // assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [MemberData(nameof(GetSampleIncorrectData))]
        public void Validate_ForIncorrectModel_ReturnFailure(CreateUserDto model)
        {
            // arrange
            var validator = new RegisterUserDtoValidator(_dbContext);

            // act
            var result = validator.TestValidate(model);

            // assert
            result.ShouldHaveAnyValidationError();
        }
    }
}
