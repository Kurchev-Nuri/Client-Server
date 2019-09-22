using AutoMapper;

namespace Russian.Post.Business.Logic.Configurations.Mapper
{
    public static class AutoMapperExtensions
    {
        public static IMappingExpression<TSource, TDest> IgnoreOther<TSource, TDest>(this IMappingExpression<TSource, TDest> expression)
        {
            expression.ForAllOtherMembers(opt => opt.Ignore());
            return expression;
        }
    }
}
