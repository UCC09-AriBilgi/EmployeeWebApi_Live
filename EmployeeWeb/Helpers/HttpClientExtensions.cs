using System.Runtime.CompilerServices;
using System.Text.Json;

namespace EmployeeWeb.Helpers
{
    public static class HttpClientExtensions
    {
        // HttpClient işlemleri sonucu gelecek olan bilgiyi çözümleyecek.
        // Çünkü bilgi json formatında olacağı için

        public static async Task<T> ReadContentAsync<T>(this HttpResponseMessage response)
        {
            // Bana API tarafından gönderilen response(cevap) uygun durumda mı
            if (response.IsSuccessStatusCode == false)
            {
                throw new ApplicationException($"API çağrılırken problem : {response.ReasonPhrase}"); // ?? response.ReasonPhrase
            }

            // eğer durum uygunsa
            // API tarafından gönderilen/gelen veri okunacak...

            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            //Bu veri normal text formatında..Bunu Json haline çevirmece
            var result = JsonSerializer.Deserialize<T>(dataAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result;

        }
    }
}
