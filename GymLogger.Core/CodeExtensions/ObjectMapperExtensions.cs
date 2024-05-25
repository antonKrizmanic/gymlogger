using AutoMapper;
using System.Linq.Expressions;

namespace GymLogger.Core.CodeExtensions;

public static class ObjectMapperExtensions
{
    /// <summary>
    /// An IMappingExpression&lt;TSource,TDest&gt; extension method that maps property to source
    /// member.
    /// </summary>
    /// <typeparam name="TSource">Type of the source.</typeparam>
    /// <typeparam name="TDest">Type of the destination.</typeparam>
    /// <typeparam name="TMemberDest">Type of the member destination.</typeparam>
    /// <typeparam name="TMemberSource">Type of the member source.</typeparam>
    /// <param name="map">The map to act on.</param>
    /// <param name="dest">Destination for the.</param>
    /// <param name="source">Source for the.</param>
    /// <returns>
    /// An IMappingExpression&lt;TSource,TDest&gt;
    /// </returns>
    public static IMappingExpression<TSource, TDest> MapProperty<TSource, TDest, TMemberDest, TMemberSource>(
        this IMappingExpression<TSource, TDest> map,
        Expression<Func<TDest, TMemberDest>> dest,
        Expression<Func<TSource, TMemberSource>> source) =>
        map.ForMember(dest, opt => opt.MapFrom(source));

    /// <summary>
    /// An IMappingExpression&lt;TSource,TDest&gt; extension method that maps property to source
    /// member.
    /// </summary>
    /// <typeparam name="TSource">Type of the source.</typeparam>
    /// <typeparam name="TDest">Type of the destination.</typeparam>
    /// <typeparam name="TMemberDest">Type of the member destination.</typeparam>
    /// <typeparam name="TResult">Type of the result.</typeparam>
    /// <param name="map">The map to act on.</param>
    /// <param name="dest">Destination for the.</param>
    /// <param name="sourceFunc">Source for the.</param>
    /// <returns>
    /// An IMappingExpression&lt;TSource,TDest&gt;
    /// </returns>
    public static IMappingExpression<TSource, TDest> MapPropertyFunc<TSource, TDest, TMemberDest, TResult>(
        this IMappingExpression<TSource, TDest> map,
        Expression<Func<TDest, TMemberDest>> dest,
        Func<TSource, TResult> sourceFunc) =>
        map.ForMember(dest, opt => opt.MapFrom((source, _) => sourceFunc(source)));

    public static IMappingExpression<TSource, TDest> MapPropertyFunc<TSource, TDest, TMemberDest, TResult>(
        this IMappingExpression<TSource, TDest> map,
        Expression<Func<TDest, TMemberDest>> composition,
        Func<TSource, TDest, ResolutionContext, TResult> sourceFunc) =>
        map.ForMember(composition, opt => opt.MapFrom((source, dest, _, ctx) => sourceFunc(source, dest, ctx)));

    public static IMappingExpression<TSource, TDest> MapPropertyFunc<TSource, TDest, TMemberDest, TResult>(
        this IMappingExpression<TSource, TDest> map,
        Expression<Func<TDest, TMemberDest>> dest,
        Func<TSource, ResolutionContext, TResult> sourceFunc) =>
        map.ForMember(dest, opt => opt.MapFrom((source, _, __, ctx) => sourceFunc(source, ctx)));

    /// <summary>
    /// An object extension method that maps one object to specified object using provided mapper.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="obj">The obj to act on.</param>
    /// <param name="mapper">The mapper.</param>
    /// <returns>
    /// A the mapped object.
    /// </returns>
    public static T MapTo<T>(this object obj, IMapper mapper) => mapper.Map<T>(obj);
}