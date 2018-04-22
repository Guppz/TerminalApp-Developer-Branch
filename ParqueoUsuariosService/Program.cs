using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using ENTITIES_POJO;

namespace ParqueoUsuariosService
{
    class Program
    {
        static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            Console.ReadLine();
        //    RunService().GetAwaiter().GetResult();
        }

     /*   static async Task RunService() {

            client.BaseAddress = new Uri("http://localhost:61693/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            Console.WriteLine("Digite la Teminal en la que desea iniciarlizar el servicio");
            await GetTerminal();
            Console.ReadLine();

        }
        static async Task GetTerminal (){
             HttpResponseMessage response = await client.GetAsync(
              "api/Terminal");
            List<Terminal> lst = await response.Content.ReadAsAsync<List<Terminal>>();
            foreach (Terminal terminal in lst)
            {
                Console.WriteLine(terminal.IdTerminal);
            }

        }*/

    }
}
