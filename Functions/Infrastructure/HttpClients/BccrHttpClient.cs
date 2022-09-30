using Newtonsoft.Json;
using LocalBitcoins.Functions.Constants;
using LocalBitcoins.Functions.Models;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Threading;
using LocalBitcoins.Functions.Utilities;
using System.Linq;
using System.Xml.Serialization;
using System.IO;

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

    public async Task<IList<Indicator>> GetExchangeRateAsync(DateTime startDate, DateTime endDate, int indicatorCode = Default.IndicatorCode, CancellationToken cancellationToken = default)
    {
        var startDateFormatted = startDate.ToString("dd/MM/yyyy");
        var endDateFormatted = endDate.ToString("dd/MM/yyyy");
        var route = $"Suscripciones/WS/wsindicadoreseconomicos.asmx/ObtenerIndicadoresEconomicosXML?Indicador={indicatorCode}&Nombre=LBFunctions&SubNiveles=N&CorreoElectronico={_email}&Token={_token}&FechaInicio={startDateFormatted}&FechaFinal={endDateFormatted}";
        _logger.LogDebug($"Calling GET {route}");
        var response = await _httpClient.GetAsync(route, cancellationToken);
        _logger.LogDebug($"Calling GET {route} returned {response.StatusCode} {response.ReasonPhrase}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        var xmlSerializer = new XmlSerializer(typeof(BccrIndicatorResponse));
        using var xmlReader = new StringReader(responseContent);
        var bccrIndicatorResponse = (BccrIndicatorResponse)xmlSerializer.Deserialize(xmlReader );
        _logger.LogDebug($"Calling GET {route} response body {responseContent}");

        return bccrIndicatorResponse.Data;
    }
}
