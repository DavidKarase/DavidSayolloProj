using Newtonsoft.Json;

public class ProductData
{
    [JsonProperty("title")]
    public string Title;

    [JsonProperty("item_id")]
    public string ItemId;

    [JsonProperty("item_name")]
    public string ItemName;

    [JsonProperty("item_image")]
    public string ItemImage;

    [JsonProperty("price")]
    public double Price;

    [JsonProperty("currency")]
    public string Currency;

    [JsonProperty("currency_sign")]
    public string CurrencySign;

    [JsonProperty("status")]
    public string Status;

    [JsonProperty("error_code")]
    public int ErrorCode;

    public override string ToString()
    {
        return string.Format("Titel: {0}, Name: {1}", Title, ItemName);
    }
}
