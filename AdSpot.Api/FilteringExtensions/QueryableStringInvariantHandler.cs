public class QueryableStringInvariantEqualsHandler : QueryableStringOperationHandler
{
    // REFERENCE: https://chillicream.com/docs/hotchocolate/v13/api-reference/extending-filtering
    public QueryableStringInvariantEqualsHandler(InputParser inputParser)
        : base(inputParser) { }

    // For creating a expression tree we need the `MethodInfo` of the `ToLower` method of string
    private static readonly MethodInfo _toLower = typeof(string)
        .GetMethods()
        .Single(x => x.Name == nameof(string.ToLower) && x.GetParameters().Length == 0);

    // This is used to match the handler to all `eq` fields
    protected override int Operation => DefaultFilterOperations.Equals;

    public override Expression HandleOperation(
        QueryableFilterContext context,
        IFilterOperationField field,
        IValueNode value,
        object parsedValue
    )
    {
        // We get the instance of the context. This is the expression path to the property
        // e.g. ~> y.Street
        Expression property = context.GetInstance();

        // the parsed value is what was specified in the query
        // e.g. ~> eq: "221B Baker Street"
        if (parsedValue is string str)
        {
            // Creates and returns the operation
            // e.g. ~> y.Street.ToLower() == "221b baker street"
            return Expression.Equal(Expression.Call(property, _toLower), Expression.Constant(str.ToLower()));
        }

        // Something went wrong 😱
        throw new InvalidOperationException();
    }
}

public class QueryableStringInvariantStartsWithHandler : QueryableStringOperationHandler
{
    // REFERENCE: https://chillicream.com/docs/hotchocolate/v13/api-reference/extending-filtering
    public QueryableStringInvariantStartsWithHandler(InputParser inputParser)
        : base(inputParser) { }

    // For creating a expression tree we need the `MethodInfo` of the `ToLower` method of string
    private static readonly MethodInfo _toLower = typeof(string)
        .GetMethods()
        .Single(x => x.Name == nameof(string.ToLower) && x.GetParameters().Length == 0);

    private static readonly MethodInfo _startsWith = typeof(string)
        .GetMethods()
        .Single(x =>
            x.Name == nameof(string.StartsWith)
            && x.GetParameters().Length == 1
            && x.GetParameters().First().ParameterType == typeof(string)
        );

    // This is used to match the handler to all `startsWith` fields
    protected override int Operation => DefaultFilterOperations.StartsWith;

    public override Expression HandleOperation(
        QueryableFilterContext context,
        IFilterOperationField field,
        IValueNode value,
        object parsedValue
    )
    {
        // We get the instance of the context. This is the expression path to the property
        // e.g. ~> y.Street
        Expression property = context.GetInstance();

        // the parsed value is what was specified in the query
        // e.g. ~> eq: "221B Baker Street"
        if (parsedValue is string str)
        {
            // Creates and returns the operation
            // e.g. ~> y.Street.ToLower().StartsWith("221b baker street")
            return Expression.Call(
                Expression.Call(property, _toLower),
                _startsWith,
                Expression.Constant(str.ToLower())
            );
        }

        // Something went wrong 😱
        throw new InvalidOperationException();
    }
}
