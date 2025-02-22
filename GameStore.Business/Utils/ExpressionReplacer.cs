namespace GameStore.Business.Utils;

public class ExpressionReplacer
{
    public static Expression ReplaceParameter(Expression expression, ParameterExpression toReplace, ParameterExpression replaceWith)
    {
        return new ReplaceExpressionVisitor(toReplace, replaceWith).Visit(expression);
    }

    private class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _toReplace;
        private readonly ParameterExpression _replaceWith;

        public ReplaceExpressionVisitor(ParameterExpression toReplace, ParameterExpression replaceWith)
        {
            _toReplace = toReplace;
            _replaceWith = replaceWith;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node == _toReplace ? _replaceWith : base.VisitParameter(node);
        }
    }
}