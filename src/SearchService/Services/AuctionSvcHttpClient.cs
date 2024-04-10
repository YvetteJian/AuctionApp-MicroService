using MongoDB.Entities;

namespace SearchService;

public class AuctionSvcHttpClient
{
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;
    public AuctionSvcHttpClient(HttpClient httpClient, IConfiguration config)
    {
        _config = config;
        _httpClient = httpClient;
    }

    public async Task<List<Item>> GetItemsForSearchDb()
    {

        var lastUpdated = await DB.Find<Item, string>()
        .Sort(x => x.Descending(x => x.UpdatedAt))
        .Project(x => x.UpdatedAt.ToString())
        .ExecuteFirstAsync();

        return await _httpClient.GetFromJsonAsync<List<Item>>(_config["AuctionServiceUrl"] + "/api/auctions?date=" + lastUpdated);
    }
}
