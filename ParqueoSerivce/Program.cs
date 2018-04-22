using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using ENTITIES_POJO;
using TerminalApp.Models;
using Exceptions;

namespace ParqueoSerivce
{
    class Program
    {
        static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            client.BaseAddress = new Uri("http://localhost:61693/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            StartService();   
        }
        static void StartService() {
            try
            {
                RunService().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Algo salio mal");
            }
            finally
            {

                Console.WriteLine("Continuar? Y/N");
                var moreActions = Console.ReadLine();

                if (moreActions.Equals("Y", StringComparison.CurrentCultureIgnoreCase))
                    StartService();
            }


        }
        static async Task RunService()
        {
            int op = 0;

           
            var terminal = await GetTerminal();
            var lstCard = await GetCards();

            do
            {

                    Console.WriteLine("Que desea hacer?");
                    Console.WriteLine("1. Simular ingreso de clientes");
                    Console.WriteLine("2. Simular salida de cliente");
                    Console.WriteLine("3. Salir");
                    op = int.Parse(Console.ReadLine());
                    ExecuteOption(op, terminal, lstCard);
                
            
            } while (op != 3);


        }

        static async void ExecuteOption(int op, Terminal term, List<Card> lstCard) {

            switch (op) {
                case 1:
                    await SimulateEntrace(term, lstCard);
                    break;
                case 2:
                    await SimulateExist(term, lstCard);
                    break;
                default:
                    break;
            }
        }

        static async Task<Terminal> GetTerminal()
        {
            int count = 0;
            HttpResponseMessage response = await client.GetAsync(
             "api/Terminal");
            ApiResponse lst = await response.Content.ReadAsAsync<ApiResponse>();
            var lstTerminals = JsonConvert.DeserializeObject<List<Terminal>>(lst.Data.ToString());

            Console.WriteLine("Digite la Teminal en la que desea iniciarlizar el servicio");
            foreach (Terminal term in lstTerminals)
            {
                Console.WriteLine(++count + ". " + term.Name);

            }

            var op = int.Parse(Console.ReadLine());
            Terminal selectedTerminal = lstTerminals[op - 1];
            return selectedTerminal;

        }

        static async Task<List<Card>> GetCards( ) {
            
            HttpResponseMessage response = await client.GetAsync(
            "api/Card");
            ApiResponse lst = await response.Content.ReadAsAsync<ApiResponse>();
            var lstCard = JsonConvert.DeserializeObject<List<Card>>(lst.Data.ToString());
           

            return lstCard;
        }

        static async Task SimulateEntrace(Terminal terminal, List<Card> lstCard) {
            
            foreach (Card card in lstCard)
            {
                if (terminal.IdTerminal == card.Terminal.IdTerminal)
                {
                    ParkingBill ParkingBill = new ParkingBill();
                    ParkingBill.BeginDate = DateTime.Now;
                    ParkingBill.ParkingCard = card;
                    HttpResponseMessage response = await client.PostAsJsonAsync<ParkingBill>("api/ParkingBill", ParkingBill);
                    if (response.IsSuccessStatusCode) {
                        Console.WriteLine("el usuario " + ParkingBill.ParkingCard.User.Name + " " + ParkingBill.ParkingCard.User.LastName + " ha entrado");

                    }
                }
                else
                {
                    Console.WriteLine("Lo sentimos  su tarjeta no es valida para esta terminal");
                }
            }

        }

        static async Task SimulateExist(Terminal terminal, List<Card> lstCard) {
            int hours = 0;
            foreach (Card card in lstCard)
            {
                if (terminal.IdTerminal == card.Terminal.IdTerminal)
                {
                    ParkingBill ParkingBill = new ParkingBill();
                    ParkingBill.EndDate = DateTime.Now.AddHours(hours);
                    
                    ParkingBill.ParkingCard = card;
                    HttpResponseMessage response = await client.PutAsJsonAsync<ParkingBill>("api/ParkingBill", ParkingBill);
                    if (response.IsSuccessStatusCode)
                    {
                        hours++;
                        Console.WriteLine("el usuario " + ParkingBill.ParkingCard.User.Name + " " + ParkingBill.ParkingCard.User.LastName + " ha salido");

                    }
                }
                else
                {
                    Console.WriteLine("Lo sentimos  su tarjeta no es valida para esta terminal");
                }
            }

        }
    }
}
