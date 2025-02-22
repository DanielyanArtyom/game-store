namespace GameStore.Business.Validators;

public class ValidationVisitor : IVisitor
{
    public void Visit(dynamic request)
    {
        Validate(request);
    }
    
    private void Validate(CommentModel request)
    {
        if (request.Action != null && request.ParentCommentId == null)
        {
            throw new ArgumentException("Parent Id is required");
        }
    }
    
    private void Validate(Role request)
    {
        if (request == null)
        {
            throw new ArgumentException("User could not be empty");
        }
        
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Name could not be empty.");
        }
        
        if (request.Permissions == null)
        {
            throw new ArgumentException("Permissions could not be empty.");
        }
        
    }
    
    private void Validate(UserModel request)
    {
        if (request == null)
        {
            throw new ArgumentException("User could not be empty");
        }
        
        if (string.IsNullOrWhiteSpace(request.Login))
        {
            throw new ArgumentException("Login could not be empty.");
        }
        
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Name could not be empty.");
        }
        
        if (string.IsNullOrWhiteSpace(request.Password))
        {
            throw new ArgumentException("Password could not be empty.");
        }
    }
    
    private void Validate(PaymentCardModel request)
    {
        if (request == null)
        {
            throw new ArgumentException("Card could not be empty");
        }
        
        if (string.IsNullOrWhiteSpace(request.Holder))
        {
            throw new ArgumentException("Holder name could not be empty.");
        }
        
        if (request.MonthExpire < 1 || request.MonthExpire > 12)
        {
            throw new ArgumentException("Invalid Month");
        }
    }

    private void Validate(GameModel request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Name is required and cannot be empty or whitespace.");
        }

        if (Guid.Empty == request.PublisherId)
        {
            throw new ArgumentException("Publisher is required.");
        }

        if (request.Platforms.Count == 0)
        {
            throw new ArgumentException("At least one platform is required.");
        }

        if (request.Genres.Count == 0)
        {
            throw new ArgumentException("At least one genre is required.");
        }
    }

    private void Validate(GenreModel request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Name is required and cannot be empty or whitespace.");
        }

        if (request.ParentGenreId.HasValue && request.ParentGenreId.Value == Guid.Empty)
        {
            throw new ArgumentException("ParentGenreId, if provided, cannot be an empty GUID.");
        }

        if (request.ParentGenreId != null && request.ParentGenreId == request.Id)
        {
            throw new ArgumentException("Cycled reference!");
        }
    }

    private void Validate(PlatformModel request)
    {
        if (string.IsNullOrWhiteSpace(request.Type))
        {
            throw new ArgumentException("Type is required and cannot be empty or whitespace.");
        }
    }
    
    private void Validate(PublisherModel request)
    {
        if (string.IsNullOrWhiteSpace(request.CompanyName))
        {
            throw new ArgumentException("Company Name is required and cannot be empty or whitespace.");
        }
        
        if (!string.IsNullOrWhiteSpace(request.HomePage) && request.HomePage.Length < 8)
        {
            throw new ArgumentException("HomePage must be between 8 and 100 characters.");
        }
        
        if (!string.IsNullOrWhiteSpace(request.Description) && request.Description.Length < 10)
        {
            throw new ArgumentException("Description must be between 10 and 100 characters.");
        }
    }
}