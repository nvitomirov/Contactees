using Contactees.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Contactees.Web
{
    public class PeopleContext
    {
        private HttpClient client;

        public PeopleContext()
        {
            client = new HttpClient
            {
               BaseAddress = new Uri("https://localhost:44395/")
            };
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public  async Task<Uri> CreatePersonAsync(Person person)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/people",person);
            response.EnsureSuccessStatusCode();

            return response.Headers.Location;
        }


        public async Task<IEnumerable<Person>> GetPeopleAsync()
        {
            List<Person> people = new List<Person>();
            try
            {
                HttpResponseMessage response =  client.GetAsync($"api/People/").Result;
                if (response.IsSuccessStatusCode)
                {
                    people = await response.Content.ReadAsAsync<List<Person>>();
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            
            return people;
        }

        public async Task<bool> CheckNationalIdInUse(Guid id, long idNumber)
        {
            bool isInUse = false;
            try
            {
                HttpResponseMessage response = client.GetAsync($"api/People/{id}/{idNumber}").Result;
                if (response.IsSuccessStatusCode)
                {
                    isInUse = await response.Content.ReadAsAsync<bool>();
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return isInUse;
        }

        public  async Task<Person> GetPersonAsync(string personId)
        {
            Person person = new Person();
            try
            {
                HttpResponseMessage response = await client.GetAsync($"api/people/{personId}");
                if (response.IsSuccessStatusCode)
                {
                    person = await response.Content.ReadAsAsync<Person>();
                }
            }
            catch (Exception e)
            {
                
            }
           
            return person;
        }

        public async Task<Person> UpdatePersonAsync(Person person)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/people/{person.Id}", person);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated person from the response body.
            person = await response.Content.ReadAsAsync<Person>();
            return person;
        }

        public  async Task<bool> DeletePersonAsync(Guid id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(
                $"api/people/{id}");
                return response.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception)
            {
            }
            return false;
        }

       
    }
}

