/*
 * Copyright 2020 Sage Intacct, Inc.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"). You may not
 * use this file except in compliance with the License. You may obtain a copy 
 * of the License at
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * or in the "LICENSE" file accompanying this file. This file is distributed on 
 * an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either 
 * express or implied. See the License for the specific language governing 
 * permissions and limitations under the License.
 */

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Intacct.SDK.Rest
{
    public class RestRequestHandler
    {
        private String token;
        
        public RestRequestHandler(String token)
        {
            this.token = token;
        }
        public class Customer
        {
            public string Key { get; set; }
            public string Id { get; set; }
            public string Name { get; set; }
            public decimal TotalDue { get; set; }
            public bool OnHold { get; set; }
        }

        public async Task<Customer> GetCustomerAsync(string path)
        {
            Customer customer = null;
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri("https://dev01.intacct.com/users/cho/src.apiintg/api/");
            
            client.DefaultRequestHeaders.Add("sender_password", "babbage");
            client.DefaultRequestHeaders.Add("sender_id", "intacct_dev");
            client.DefaultRequestHeaders.Add("user-agent", ".NET-SDK-Hackathon");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.token);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                
                Console.WriteLine(str);
                
                customer = await response.Content.ReadAsAsync<Customer>();
            }
            
            return customer;
        }
    }
}