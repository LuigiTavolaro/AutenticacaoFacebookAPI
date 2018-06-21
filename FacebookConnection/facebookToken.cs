using FacebookConnection;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace teste.Controllers
{
    public class facebookToken
    {

        public async Task<bool> VerificaConexaoAsync(string Token)
        {
            //check token
            var httpClient = new HttpClient { BaseAddress = new Uri("https://graph.facebook.com/v2.9/") };
            var response = await httpClient.GetAsync($"me?access_token={Token}&fields=id,name,email,first_name,last_name,age_range,birthday,gender,locale,picture");

            var result = await response.Content.ReadAsStringAsync();
            // var facebookAccount = JsonConvert.DeserializeObject<FacebookAccount>(result);

            return true;
        }
        public bool ValidaUsuarioFacebook()
        {
            var facebookClient = new FacebookClient();
            var facebookService = new FacebookService(facebookClient);
            var getAccountTask = facebookService.GetAccountAsync(FacebookSettings.AccessToken);
            Task.WaitAll(getAccountTask);
            var account = getAccountTask.Result;
            if (getAccountTask.IsCompletedSuccessfully)
                return true;
            else
                return false;
            //var cont = account.Id;
            //var nome = account.Name;
            //Console.WriteLine($"{account.Id} {account.Name}");

            //var postOnWallTask = facebookService.PostOnWallAsync(FacebookSettings.AccessToken,
            //"Hello from C# .NET Core!");
            //Task.WaitAll(postOnWallTask);
        }
    }


}