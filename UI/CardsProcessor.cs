using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UI.Models;

namespace UI
{
    public class CardsProcessor
    {
        public static async Task<ObservableCollection<Card>> Load()
        {
            string url = "https://localhost:44324/Board";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    ObservableCollection<Card> cards = await response.Content.ReadAsAsync<ObservableCollection<Card>>();

                    return cards;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<Card> Load(int id)
        {
            string url = $"https://localhost:44324/Board/{id}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    Card card = await response.Content.ReadAsAsync<Card>();

                    return card;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<bool> Add(Card card)
        {
            string json = JsonConvert.SerializeObject(card);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            string url = "https://localhost:44324/Board";          

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, httpContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<bool> Put(Card card)
        {
            string json = JsonConvert.SerializeObject(card);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            string url = "https://localhost:44324/Board";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync(url, httpContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<bool> Delete(int id)
        {
            string url = $"https://localhost:44324/Board/{id}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
