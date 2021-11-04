using Newtonsoft.Json;
using UnityEngine;

public class ProductData : MonoBehaviour
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
}
