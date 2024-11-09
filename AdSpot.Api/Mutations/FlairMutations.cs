namespace AdSpot.Api.Mutations;

[MutationType]
public class FlairMutations
{
    [Error<FlairAlreadyExistsError>]
    public MutationResult<IQueryable<Flair>> AddFlair(
        int userId,
        string flairTitle,
        FlairRepository flairRepo
    )
    {
        var flairExists = flairRepo.GetFlair(userId, flairTitle).Any();
        if (flairExists)
        {
            return new(new FlairAlreadyExistsError(flairTitle));
        }

        var flair = flairRepo.AddFlair(
            new Flair
            {
                UserId = userId,
                FlairTitle = flairTitle
            }
        );

        return new(flair);
    }
    
    public MutationResult<IQueryable<Flair>> DeleteFlair(
        int userId,
        string flairTitle,
        FlairRepository flairRepo
    )
    {
        var flairs = flairRepo.GetFlair(userId, flairTitle);
        if (flairs.Any())
        {
            return new(flairRepo.DeleteFlair(
                new Flair
                {
                    UserId = userId,
                    FlairTitle = flairTitle
                }
            ));
        }

        return new(flairRepo.GetFlairs(userId));
    }
}