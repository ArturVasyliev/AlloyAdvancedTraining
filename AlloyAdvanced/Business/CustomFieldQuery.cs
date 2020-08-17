using EPiServer.Search.Queries;
using EPiServer.Search.Queries.Lucene;

namespace AlloyAdvanced.Business
{
    public class CustomFieldQuery : IQueryExpression
    {
        public CustomFieldQuery(string queryExpression, string fieldName)
        {
            Expression = queryExpression;
            Field = fieldName;
        }

        public string GetQueryExpression()
        {
            return string.Format("{0}:({1})", Field,
                LuceneHelpers.EscapeParenthesis(Expression));
        }

        public string Field { get; set; }

        public string Expression { get; set; }
    }
}