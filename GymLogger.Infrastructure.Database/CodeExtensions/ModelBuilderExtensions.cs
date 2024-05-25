using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using GymLogger.Core.CodeExtensions;

namespace GymLogger.Infrastructure.Database.CodeExtensions;
public static class ModelBuilderExtensions
{
    /// <summary>
    /// A ModelBuilder extension method that sets query filter.
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity.</typeparam>
    /// <typeparam name="TEntityInterface">Type of the entity interface.</typeparam>
    /// <param name="builder">The builder to act on.</param>
    /// <param name="filterExpression">The filter expression.</param>
    private static void SetQueryFilter<TEntity, TEntityInterface>(
        this ModelBuilder builder,
        Expression<Func<TEntityInterface, bool>> filterExpression)
        where TEntityInterface : class
        where TEntity : class, TEntityInterface =>
        builder.Entity<TEntity>()
            .HasQueryFilter(filterExpression
                .Convert<TEntityInterface, TEntity>());

    /// <summary>
    /// The set query filter method.
    /// </summary>
    private static readonly MethodInfo SetQueryFilterMethod = typeof(ModelBuilderExtensions)
        .GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
        .Single(t => t.IsGenericMethod && t.Name == nameof(SetQueryFilter));

    /// <summary>
    /// A ModelBuilder extension method that sets entity query filter.
    /// </summary>
    /// <typeparam name="TEntityInterface">Type of the entity interface.</typeparam>
    /// <param name="builder">The builder to act on.</param>
    /// <param name="entityType">Type of the entity.</param>
    /// <param name="filterExpression">The filter expression.</param>
    private static void SetEntityQueryFilter<TEntityInterface>(
        this ModelBuilder builder,
        Type entityType,
        Expression<Func<TEntityInterface, bool>> filterExpression) =>
        SetQueryFilterMethod
            .MakeGenericMethod(entityType, typeof(TEntityInterface))
            .Invoke(null, new object[] { builder, filterExpression });

    /// <summary>
    /// A ModelBuilder extension method that sets query filter on all entities.
    /// </summary>
    /// <typeparam name="TEntityInterface">Type of the entity interface.</typeparam>
    /// <param name="builder">The builder to act on.</param>
    /// <param name="filterExpression">The filter expression.</param>
    public static void SetQueryFilterOnAllEntities<TEntityInterface>(
        this ModelBuilder builder,
        Expression<Func<TEntityInterface, bool>> filterExpression)
    {
        foreach (var type in builder.Model.GetEntityTypes()
                     .Where(t => t.BaseType == null)
                     .Select(t => t.ClrType)
                     .Where(t => typeof(TEntityInterface).IsAssignableFrom(t)))
        {
            builder.SetEntityQueryFilter(
                type,
                filterExpression);
        }
    }
}