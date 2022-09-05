namespace TradesWorker.Extensions;

public static class ServiceCollectionExtensions 
{
    public static IServiceCollection ConfigureSection<TConfiguration>(this IServiceCollection services, IConfiguration configuration, string sectionName = null)
        where TConfiguration : class
    {
        if (string.IsNullOrEmpty(sectionName))
            sectionName = GetDefaultSectionName<TConfiguration>();
        services.Configure<TConfiguration>(options => configuration.GetSection(sectionName).Bind(options));
        return services;
    }

    private static string GetDefaultSectionName<TConfiguration>() => 
        typeof(TConfiguration).Name.Replace("Configuration", string.Empty, StringComparison.InvariantCultureIgnoreCase);
}