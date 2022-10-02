using LocalBitcoins.Functions.Constants;
using LocalBitcoins.Functions.Models;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Threading;
using LocalBitcoins.Functions.Utilities;
using LocalBitcoins.Functions.Extensions;

namespace LocalBitcoins.Functions.Infrastructure.HttpClients;

public class BccrHttpClient : IBccrHttpClient
{
    private readonly HttpClient _httpClient;

    private readonly string _token;

    private readonly string _email;

    private readonly ILogger<BccrHttpClient> _logger;

    public BccrHttpClient(HttpClient httpClient, ILogger<BccrHttpClient> logger)
    {
        _token = ApplicationSettingsUtility.Get(ApplicationSettings.BccrIndicatorsToken);
        _email = ApplicationSettingsUtility.Get(ApplicationSettings.BccrIndicatorsEmail);
        httpClient.BaseAddress = new Uri(ApplicationSettingsUtility.Get(ApplicationSettings.BccrIndicatorsUrl));
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IList<BccrIndicator>> GetExchangeRateAsync(DateTime startDate, DateTime endDate, int indicatorCode = Default.IndicatorCode, CancellationToken cancellationToken = default)
    {
        var startDateFormatted = startDate.ToString("dd/MM/yyyy");
        var endDateFormatted = endDate.ToString("dd/MM/yyyy");
        var route = $"/Indicadores/Suscripciones/WS/wsindicadoreseconomicos.asmx/ObtenerIndicadoresEconomicosXML?Indicador={indicatorCode}&Nombre=LBFunctions&SubNiveles=N&CorreoElectronico={_email}&Token={_token}&FechaInicio={startDateFormatted}&FechaFinal={endDateFormatted}";
        _logger.LogDebug($"Calling GET {route}");
        var response = await _httpClient.GetAsync(route, cancellationToken);
        _logger.LogDebug($"Calling GET {route} returned {response.StatusCode} {response.ReasonPhrase}");
        response.EnsureSuccessStatusCode();

        var bccrIndicatorResponse = await response.DeserializeXmlAsync<BccrIndicatorResponse>(cancellationToken);
        _logger.LogDebug($"Calling GET {route} response body {bccrIndicatorResponse}");

        return bccrIndicatorResponse.Indicators;
    }
}
