namespace AdSpot.Api.Validators;

public class SubmitDeliverableInputValidator : AbstractValidator<SubmitDeliverableInput>
{
    public SubmitDeliverableInputValidator()
    {
        //https://stackoverflow.com/questions/36562243/not-sure-how-to-test-this-net-string-with-fluentvalidation
        RuleFor(x => x.Deliverable)
            .NotEmpty()
            .Must(d =>
                Uri.TryCreate(d, UriKind.Absolute, out var outUri)
                && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps)
            )
            .WithMessage("`{PropertyValue}` is not a valid URI");
    }
}
