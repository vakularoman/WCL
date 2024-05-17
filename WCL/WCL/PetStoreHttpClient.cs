using System.Net.Http;
using System.Net.Http.Json;
using WCL.ViewModels;

namespace WCL
{
    public class PetStoreHttpClient
    {
        private readonly HttpClient _httpClient;

        public PetStoreHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(bool, UserInfoViewModel?)> TryGetUserInfoAsync(string userName)
        {
            try
            {
                var response = await _httpClient.GetAsync($"user/{userName}").ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<UserInfoViewModel>().ConfigureAwait(false);

                    return result is null ? (false, null) : (true, result);
                }
            }
            catch (Exception)
            {
                // Log exception
            }

            return (false, null);
        }

        public async Task<bool> TryLogOutAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"user/logout").ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                // Log exception
            }

            return false;
        }

        public async Task<bool> TryLogInAsync(string username, string password)
        {
            try
            {
                var response = await _httpClient.GetAsync($"user/login?username={username}&password={password}").ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                // Log exception
            }

            return false;
        }

        public async Task<bool> TryRegisterAsync(UserInfoViewModel userInfoViewModel)
        {
            try
            {
                var content = JsonContent.Create(userInfoViewModel);

                var response = await _httpClient.PostAsync("user", content).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                // Log exception
            }

            return false;
        }
    }
}
