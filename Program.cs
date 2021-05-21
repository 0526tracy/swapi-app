using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace swapi_app
{
    public class Films{
        public string episode{get;set;}
    }
    public class Character{
        public string id{get;set;}
        public string name{get;set;}
        public string films{get;set;}

    }
    class Program
    {
        static HttpClient client = new HttpClient();
        static  string baseUrl = "https://swapi.dev/api/";
        static void Main(string[] args)
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            Console.WriteLine("Please enter character ID: ");
            var input = Console.ReadLine();
            int inputId;
        
            if(int.TryParse(input, out inputId)){
                Console.WriteLine("You enter :"+inputId );
                GetCharacterDetails(inputId);
            }
            else{
                Console.WriteLine(input+" "+ "is not a valid ID");
            }

        }

        static async void GetCharacterDetails(int id){
           
            try{
                var response = client.GetAsync("people/"+id+"/").Result;
                var content = await response.Content.ReadAsStringAsync();
                var obj = Newtonsoft.Json.Linq.JObject.Parse(content);

                Console.WriteLine("calling swapi-api...");

                var name = obj["name"];
                var films = obj["films"];

                Console.WriteLine("Name :" + name);

                foreach(string i in films){
                    var filmResponse = client.GetAsync(i).Result;
                    var filmContent = await filmResponse.Content.ReadAsStringAsync();
                    var filmObj = Newtonsoft.Json.Linq.JObject.Parse(filmContent);
                    Console.WriteLine("Episode " + filmObj["episode_id"]+ " :"+ filmObj["title"]);
                }

            }
            catch(Exception e){
                Console.WriteLine("not a valid id : ", e);
            }
        }
        static void GetCharacterEpisode(Array data){

        }
        

    }
}
