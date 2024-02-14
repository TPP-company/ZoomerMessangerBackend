using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ZM.Common.Extensions;
public static class OptionsExtensions
{
    /// <summary>
    /// Зарегистрировать опции с валидацией при запуске и возможностью получить напрямую без обертки IOptions. <br/>
    /// Название конфигурации по умолчанию - название типа TOptions.
    /// </summary>
    public static void RegisterOptions<TOptions>(this IServiceCollection services, IConfiguration configuration)
        where TOptions : class
    {
        services.RegisterOptions<TOptions>(configuration, typeof(TOptions).Name);
    }

    /// <summary>
    /// Зарегистрировать опции с валидацией при запуске и возможностью получить напрямую без обертки IOptions.
    /// </summary>
    /// <param name="sectionName">Название раздела конфигурации.</param>
    public static void RegisterOptions<TOptions>(this IServiceCollection services, IConfiguration configuration, string sectionName)
        where TOptions : class
    {
        services.AddOptions<TOptions>()
            .Bind(configuration.GetRequiredSection(sectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton(provider => provider.GetRequiredService<IOptions<TOptions>>().Value);
    }
}
