namespace AdSpot.Api.Validators;

public class AddUserInputValidator : AbstractValidator<AddUserInput>
{
    public AddUserInputValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("`{PropertyValue}` is not a valid email address");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
    }
}
