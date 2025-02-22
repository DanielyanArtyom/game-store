using GameStore.Mongo.Data.Context.Entity;

namespace GameStore.Business.Utils;

internal static class GameSearchFilterModelExtensions
{
    public static SearchContext<Game> ToGameSearchContext(this GameSearchFilterModel model, Expression<Func<Game, object>>[] include = null)
    {
        var sorting = GetSortingExpression(model);

        return new SearchContext<Game>
        {
            Filter = GetFilteringExpression(model),
            OrderBy = sorting.SortExpression,
            IsAscending = sorting.IsAscending,
            PageNumber = model.PageNumber,
            PageSize = (int)model.PageSize,
            Include = include
        };
    }
    public static SearchContext<Product> ToProductSearchContext(this GameSearchFilterModel model, Expression<Func<Product, object>>[] include = null)
    {
        var sorting = GetProductSortingExpression(model);

        return new SearchContext<Product>
        {
            Filter = GetProductFilteringExpression(model),
            OrderBy = sorting.SortExpression,
            IsAscending = sorting.IsAscending,
            PageNumber = model.PageNumber,
            PageSize = (int)model.PageSize,
            Include = include
        };
    }

    #region Private methods

    private static Expression<Func<Game, bool>> GetFilteringExpression(GameSearchFilterModel filter)
    {
        Expression<Func<Game, bool>> filterExpression = g => true;

        if (filter.GenreIds != null && filter.GenreIds.Any())
        {
            Expression<Func<Game, bool>>
                genreFilter = g => g.GameGenres.Any(gg => filter.GenreIds.Contains(gg.GenreId));
            filterExpression = CombineFilters(filterExpression, genreFilter);
        }

        if (filter.PlatformIds != null && filter.PlatformIds.Any())
        {
            Expression<Func<Game, bool>> platformFilter =
                g => g.GamePlatforms.Any(gp => filter.PlatformIds.Contains(gp.PlatformId));
            filterExpression = CombineFilters(filterExpression, platformFilter);
        }

        if (filter.CompanyNames != null && filter.CompanyNames.Any())
        {
            Expression<Func<Game, bool>> publisherFilter = g => filter.CompanyNames.Contains(g.Publisher.CompanyName);
            filterExpression = CombineFilters(filterExpression, publisherFilter);
        }

        if (filter.MinPrice.HasValue)
        {
            Expression<Func<Game, bool>> minPriceFilter = g => g.Price >= filter.MinPrice.Value;
            filterExpression = CombineFilters(filterExpression, minPriceFilter);
        }

        if (filter.MaxPrice.HasValue)
        {
            Expression<Func<Game, bool>> maxPriceFilter = g => g.Price <= filter.MaxPrice.Value;
            filterExpression = CombineFilters(filterExpression, maxPriceFilter);
        }

        if (!string.IsNullOrEmpty(filter.Name) && filter.Name.Length >= 3)
        {
            Expression<Func<Game, bool>> nameFilter = g => g.Name.Contains(filter.Name);
            filterExpression = CombineFilters(filterExpression, nameFilter);
        }
        
        if (!string.IsNullOrEmpty(filter.Key))
        {
            Expression<Func<Game, bool>> nameFilter = g => g.Key == filter.Key;
            filterExpression = CombineFilters(filterExpression, nameFilter);
        }

        if (filter.PublishDate.HasValue)
        {
            var referenceDate = filter.PublishDate.Value switch
            {
                PublishDateOption.LastWeek => DateTime.Now.AddDays(-7),
                PublishDateOption.LastMonth => DateTime.Now.AddMonths(-1),
                PublishDateOption.LastYear => DateTime.Now.AddYears(-1),
                PublishDateOption.TwoYears => DateTime.Now.AddYears(-2),
                PublishDateOption.ThreeYears => DateTime.Now.AddYears(-3),
                _ => DateTime.MinValue
            };

            Expression<Func<Game, bool>> publishDateFilter = g => g.PublishDate >= referenceDate;
            filterExpression = CombineFilters(filterExpression, publishDateFilter);
        }

        return filterExpression;
    }

    private static (Expression<Func<Game, object>> SortExpression, bool IsAscending) GetSortingExpression(
        GameSearchFilterModel filter)
    {
        switch (filter.SortBy)
        {
            case SortingOption.MostPopular:
                return (g => g.Views, false);
            case SortingOption.MostCommented:
                return (g => g.Comments.Count, false);
            case SortingOption.PriceAsc:
                return (g => g.Price, true);
            case SortingOption.PriceDesc:
                return (g => g.Price, false);
            case SortingOption.New:
            default:
                return (g => g.PublishDate, false);
        }
    }

    private static Expression<Func<Game, bool>> CombineFilters(Expression<Func<Game, bool>> expr1,
        Expression<Func<Game, bool>> expr2)
    {
        var parameter = Expression.Parameter(typeof(Game), "g");

        var body = Expression.AndAlso(
            Expression.Invoke(expr1, parameter),
            Expression.Invoke(expr2, parameter)
        );

        return Expression.Lambda<Func<Game, bool>>(body, parameter);
    }
    
    private static Expression<Func<Product, bool>> GetProductFilteringExpression(GameSearchFilterModel filter)
        {
            Expression<Func<Product, bool>> filterExpression = p => true;

            if (filter.MinPrice.HasValue)
            {
                Expression<Func<Product, bool>> minPriceFilter = p => p.UnitPrice >= filter.MinPrice.Value;
                filterExpression = CombineProductFilters(filterExpression, minPriceFilter);
            }

            if (filter.MaxPrice.HasValue)
            {
                Expression<Func<Product, bool>> maxPriceFilter = p => p.UnitPrice <= filter.MaxPrice.Value;
                filterExpression = CombineProductFilters(filterExpression, maxPriceFilter);
            }

            if (!string.IsNullOrEmpty(filter.Name) && filter.Name.Length >= 3)
            {
                Expression<Func<Product, bool>> nameFilter = p => p.ProductName.Contains(filter.Name);
                filterExpression = CombineProductFilters(filterExpression, nameFilter);
            }
            
            if (!string.IsNullOrEmpty(filter.Key))
            {
                Expression<Func<Product, bool>> nameFilter = g => g.ProductName == filter.Key;
                filterExpression = CombineProductFilters(filterExpression, nameFilter);
            }

            return filterExpression;
        }

    private static (Expression<Func<Product, object>> SortExpression, bool IsAscending) GetProductSortingExpression(GameSearchFilterModel filter)
    {
        switch (filter.SortBy)
        {
            case SortingOption.MostPopular:
                return (p => p.UnitsInStock, false);
            case SortingOption.PriceAsc:
                return (p => p.UnitPrice, true);
            case SortingOption.PriceDesc:
                return (p => p.UnitPrice, false);
            case SortingOption.New:
            default:
                return (p => p.ProductID, false);
        }
    }
    
    private static Expression<Func<Product, bool>> CombineProductFilters(Expression<Func<Product, bool>> first, Expression<Func<Product, bool>> second)
    {
        var parameter = Expression.Parameter(typeof(Product), "p");

        var combined = Expression.Lambda<Func<Product, bool>>(
            Expression.AndAlso(
                ExpressionReplacer.ReplaceParameter(first.Body, first.Parameters[0], parameter),
                ExpressionReplacer.ReplaceParameter(second.Body, second.Parameters[0], parameter)
            ), parameter);

        return combined;
    }

        #endregion
    }
