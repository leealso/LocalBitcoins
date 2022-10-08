namespace LocalBitcoins.Functions.Constants;

public static class GraphQlQuery
{
    public const string MissingContactIds = @"query query($closedAt:DateTime!$contactIds:[Int!]!){missingContactIds(closedAt:$closedAtcontactIds:$contactIds)}";
}
