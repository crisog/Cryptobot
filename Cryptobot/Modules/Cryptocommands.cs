using Discord;
using Discord.Commands;
using Discord.Rest;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http;
using System.Text;

namespace Cryptobot.Modules
{
    public class RequireRoleAttribute : PreconditionAttribute
    {
        public override Task<PreconditionResult> CheckPermissions(ICommandContext context, CommandInfo command, IDependencyMap map)
        {
            throw new NotImplementedException();
        }

        private readonly ulong _requiredRole;

        public RequireRoleAttribute(ulong roleId)
        {
            _requiredRole = roleId;
        }
    }

    public class Cryptommands : ModuleBase<SocketCommandContext>
    {

        CommandHandler cmdh = new CommandHandler();

        [Command("c")]
        public async Task Check(string cryptocurrency)
        {
            string page = "http://coinmarketcap-nexuist.rhcloud.com/api/" + cryptocurrency + "";
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(page))
            using (HttpContent content = response.Content)
            {
                try
                {
                    string result = await content.ReadAsStringAsync();
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(page);
                    dynamic decoded = JsonConvert.DeserializeObject(result);
                    var priceusd = string.Format("{0:0.00}", (double)(decoded.price.usd));
                    var priceeur = string.Format("{0:0.00}", (double)(decoded.price.eur));
                    double pricebtc = ((double)(decoded.price.btc));
                    double change = ((double)(decoded.change));
                    await Context.Channel.SendMessageAsync(@"
```http
 " + cryptocurrency.ToUpper() + " is currently worth: $" + priceusd + " or €" + priceeur + " equivalent to " + pricebtc + " BTC (" + change + "%)" +
        "```");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

            }
        }

        [Command("g")]
        public async Task cGdax(string cryptocurrency)
        {
            string crypto = "default";
            string crypto1 = "default";
            double final = 0;
            double final1 = 0;
            string name = "default";
            if (cryptocurrency == "btc")
            {
                name = "Bitcoin";
                crypto = "BTC-USD";
                crypto1 = "BTC-EUR";
            }
            else if (cryptocurrency == "eth")
            {
                name = "Ethereum";
                crypto = "ETH-USD";
                crypto1 = "ETH-EUR";
            }
            else if (cryptocurrency == "ltc")
            {
                name = "Litecoin";
                crypto = "LTC-USD";
                crypto1 = "LTC-EUR";
            }

            try
            {

                String URL = "https://api.gdax.com/products/" + crypto + "/ticker";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.UserAgent = ".NET Framework Test Client";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var encoding = ASCIIEncoding.ASCII;
                using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
                {
                    string responseText = reader.ReadToEnd();
                    dynamic decoded = JsonConvert.DeserializeObject(responseText);
                    final = decoded.price;

                }
            }


            catch (WebException ex)
            {
                HttpWebResponse xyz = ex.Response as HttpWebResponse;
                var encoding = ASCIIEncoding.ASCII;
                using (var reader = new System.IO.StreamReader(xyz.GetResponseStream(), encoding))
                {
                    string responseText = reader.ReadToEnd();
                }
            }
            
           try
            {

                String URL = "https://api.gdax.com/products/" + crypto1 + "/ticker";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.UserAgent = ".NET Framework Test Client";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var encoding = ASCIIEncoding.ASCII;
                using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
                {
                    string responseText = reader.ReadToEnd();
                    dynamic decoded = JsonConvert.DeserializeObject(responseText);
                    final1 = decoded.price;

                }
            }


            catch (WebException ex)
            {
                HttpWebResponse xyz = ex.Response as HttpWebResponse;
                var encoding = ASCIIEncoding.ASCII;
                using (var reader = new System.IO.StreamReader(xyz.GetResponseStream(), encoding))
                {
                    string responseText = reader.ReadToEnd();
                }
            }


            await Context.Channel.SendMessageAsync(@"
```http
 GDAX || " + name + " is currently worth: $" + final.ToString("0.##") + " or €" + final1.ToString("0.##") + "" +
       "```");

        }

        /*string page = "https://api.gdax.com/products/BTC-USD/ticker";
        using (HttpClient client = new HttpClient())

        using (HttpResponseMessage response = await client.GetAsync(page))
        using (HttpContent content = response.Content)
        {

            try
            {
                client.DefaultRequestHeaders.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                Console.WriteLine(name);
                Console.WriteLine(crypto);
                string result = await content.ReadAsStringAsync();
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(page);
                dynamic decoded = JsonConvert.DeserializeObject(result);
                Console.WriteLine(result);
                //var priceusd = string.Format("{0:0.00}", (string)(decoded.price));
                await Context.Channel.SendMessageAsync(@"
```http
" + name + " is currently worth: $" + "test" + "" +
    "```");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }*/
    

        [Command("profit")]
        public async Task Profit(string cryptocurrency, double bprice, double amount)
        {
            string page = "http://coinmarketcap-nexuist.rhcloud.com/api/" + cryptocurrency + "";
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(page))
            using (HttpContent content = response.Content)
            {
                string result = await content.ReadAsStringAsync();
                dynamic decoded = JsonConvert.DeserializeObject(result);
                double priceusd = ((double)(decoded.price.usd));
                string name = ((string)(decoded.name));
                double final = (priceusd * amount) - (bprice * amount);
                var s = string.Format("{0:0.00}", final);
                await Context.Channel.SendMessageAsync(@"
```http
 " + "Your current profit on " + name + " is:  $" + s + " " +
      "```");

            }
        }

        [Command("calculate")]
        public async Task Calculate(string amt, string cryptocurrency, string currency)
        {
            double amount = Convert.ToDouble(amt);
            string page = "http://coinmarketcap-nexuist.rhcloud.com/api/" + cryptocurrency + "";
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(page))
            using (HttpContent content = response.Content)
            {
                string result = await content.ReadAsStringAsync();
                dynamic decoded = JsonConvert.DeserializeObject(result);
                double priceusd = ((double)(decoded.price.usd));
                double priceeur = ((double)(decoded.price.eur));
                double pricegbp = ((double)(decoded.price.gbp));
                string name = ((string)(decoded.name));
                var charsToRemove = new string[] { "," };
                if (currency == "usd" || currency == "dollar")
                {
                    double final = amount * priceusd;
                    var s = string.Format("{0:0.00}", final);
                    await Context.Channel.SendMessageAsync(@"
```http
 " + amount + " " + name + " is currently worth: $" + s + " " +
     "```");
                }

                else if (currency == "gbp" || currency == "pounds")
                {
                    double final = (pricegbp * amount);
                    var s = string.Format("{0:0.00}", final);
                    await Context.Channel.SendMessageAsync(@"
```http
 " + amount + " " + name + " is currently worth: £" + s + " " +
     "```");
                }


                else if (currency == "eur" || currency == "euro")
                {
                    
                    double final = (priceeur * amount);
                    var s = string.Format("{0:0.00}", final);
                    await Context.Channel.SendMessageAsync(@"
```http
 " + amount + " " + name + " is currently worth: ‎€" + s + " " +
     "```");
                }

                else
                {
                    await Context.Channel.SendMessageAsync("" + currency + " is not a valid currency.");
                }


            }
        }


        [Command("buy")]
        public async Task Buye(string amt, string currency, string cryptocurrency)
        {
            currency = currency.ToLower();
            double amount = Convert.ToDouble(amt);
            string page = "http://coinmarketcap-nexuist.rhcloud.com/api/" + cryptocurrency + "";
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(page))
            using (HttpContent content = response.Content)
            {
                string result = await content.ReadAsStringAsync();
                dynamic decoded = JsonConvert.DeserializeObject(result);
                double priceusd = ((double)(decoded.price.usd));
                double priceeur = ((double)(decoded.price.eur));
                double pricegbp = ((double)(decoded.price.gbp));
                string name = ((string)(decoded.name));
                var charsToRemove = new string[] { "," };
                if (currency == "usd" || currency == "dollar")
                {

                    double final = amount / priceusd;
                    var s = string.Format("{0:0.000000}", final);
                    await Context.Channel.SendMessageAsync(@"
```http
 " + "You can buy " + s + " " + name + " with: $" + amt + " " +
     "```");
                }

                else if (currency == "gbp" || currency == "pounds")
                {
                    
                    double final = (amount / pricegbp);
                    var s = string.Format("{0:0.00}", final);
                    await Context.Channel.SendMessageAsync(@"
```http
 " + "You can buy " + s + " " + name + " with: £" + amt + "" +
     "```");
                }


                else if (currency == "eur" || currency == "euro")
                {
                    double final = (amount / priceeur);
                    var s = string.Format("{0:0.00}", final);
                    await Context.Channel.SendMessageAsync(@"
```http
 " + "You can buy " + s + " " + name + " with: €" + amt + "" +
     "```");
                }

                else
                {
                    await Context.Channel.SendMessageAsync("" + cryptocurrency + " is not a valid currency.");
                }


            }
        }

        [Command("sell")]
        public async Task Sell(string amt, string cryptocurrency)
        {
            double amount = Convert.ToDouble(amt);
            string page = "http://coinmarketcap-nexuist.rhcloud.com/api/" + cryptocurrency + "";
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(page))
            using (HttpContent content = response.Content)
            {
                string result = await content.ReadAsStringAsync();
                dynamic decoded = JsonConvert.DeserializeObject(result);
                double priceusd = ((double)(decoded.price.usd));
                string name = ((string)(decoded.name));
                var charsToRemove = new string[] { "," };

                double final = amount * priceusd;
                var s = string.Format("{0:0.00}", final);
                await Context.Channel.SendMessageAsync(@"
```http
 " + amt + " " + name + " is equivalent to: $" + s + " " +
     "```");

            }
        }

        [Command("cryptocommands")]
        public async Task Commands()
        {

            var channel = await Context.User.CreateDMChannelAsync();
            await channel.SendMessageAsync(@"```dns
   ** LIST OF COMMANDS **
 .c [cryptocurrency] - gets the USD, EUR & BTC price of a coin.
 [DISABLED] .volume [cryptocurrency] - gets the current 24h volume of any crypto. [DISABLED]
 .marketcap [cryptocurrency] - gets the current marketcap of any crypto.
 .csupply [cryptocurrency] - gets the current circulating supply of any crypto.
 .profit [cryptocurrency] [price you bought coin in USD] [amount] - gets your current profit on any cryptocurrency.
 .calculate [amount] [cryptocurrency] [currency] - calculates value of X cryptocurrency amount to USD, EUR or GBP.
 .buy [amount] [currency] [cryptocurrency] - gets you how many cryptocoins you'll get from X amount of USD, EUR or GBP.
 .sell [amount] [cryptocurrency] - gets you how much $ you will get if you sell X amount of cryptocurrency.
 .help - use this if you're having problems with bot.
 .dev - use this to contact developer.
```
");
        }


        [Command("volume")]
        public async Task Volume(string cryptocurrency)
        {
            string page = "http://coinmarketcap-nexuist.rhcloud.com/api/" + cryptocurrency + "";
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(page))
            using (HttpContent content = response.Content)
            {
                string result = await content.ReadAsStringAsync();
                dynamic decoded = JsonConvert.DeserializeObject(result);
                double volume = ((double)(decoded.volume.usd));
                double volumebtc = ((double)(decoded.volume.btc));
                string name = ((string)(decoded.name));
                await Context.Channel.SendMessageAsync(@"
```http
 " + name + " current volume is: $" + volume + " equivalent to " + volumebtc + " BTC" +
     "```");
            }
        }

        [Command("marketcap")]
        public async Task marketcap(string cryptocurrency)
        {

            string page = "http://coinmarketcap-nexuist.rhcloud.com/api/" + cryptocurrency + "";
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(page))
            using (HttpContent content = response.Content)
            {
                string result = await content.ReadAsStringAsync();
                dynamic decoded = JsonConvert.DeserializeObject(result);
                double marketcapusd = ((double)(decoded.marketcap.usd));
                double marketcapbtc = ((double)(decoded.marketcap.btc));
                string name = ((string)(decoded.name));
                await Context.Channel.SendMessageAsync(@"
```http
 " + "" + name + " current marketcap is: $" + marketcapusd.ToString("N", new CultureInfo("en-US")) + " equivalent to " + marketcapbtc.ToString("N", new CultureInfo("en-US")) + " BTC" +
         "```");
            }
        }


        [Command("csupply")]
        public async Task Csupply(string cryptocurrency)
        {
            string page = "http://coinmarketcap-nexuist.rhcloud.com/api/" + cryptocurrency + "";
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(page))
            using (HttpContent content = response.Content)
            {
                string result = await content.ReadAsStringAsync();
                dynamic decoded = JsonConvert.DeserializeObject(result);
                double supply = ((double)(decoded.supply));
                string symbol = ((string)(decoded.symbol));
                string name = ((string)(decoded.name));

                await Context.Channel.SendMessageAsync(@"
```http
 " + "" + name + " circulating supply is: " + supply.ToString("N", new CultureInfo("en-US")) + " " + symbol + "" +
    "```");
            }
        }




        [Command("help")]
        public async Task Help()
        {
            var channel = await Context.User.CreateDMChannelAsync();
            await channel.SendMessageAsync(@"
```dns
**Instructions**

Abbreviation support for the following coins:
btc, dgb, xrp, eth, etc, ltc, strat, sc, xml, sys, rdd, bcn, golem, xmr

If the coin doesn't show in the list above, you'll have to use fullname.
Ex: iconomi instead of icn

Commands can be found using .cryptocommands.
If you are facing a bug do contact a staff member/developer.
```");
        }

        [Command("dev")]
        public async Task Dev()
        {
            var channel = await Context.User.CreateDMChannelAsync();
            await channel.SendMessageAsync(@"
```dns
**Developer Information**

Skype & Gmail: admirablyhf@gmail.com
Hackforums - https://hackforums.net/member.php?action=profile&uid=2114723
```");
        }
        /* [Command("msg")]

         public async Task Hdcw()
         {
             Service k = new Service(1, 6, () =>
             {
                 Context.Channel.SendMessageAsync(@"```Markdown
 @everyone Remember to type !cryptocommands if you need any help!```
 "); 
                 Context.Channel.SendMessageAsync(@"@everyone Remember to type !cryptocommands if you need any help!");
             });
         }*/

        public string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }

        /* [Command("alert")]
         public async Task Alarm(string cryptocurrency, double alarmprice)
         { 
             cryptocurrency = cryptocurrency.ToLower();
             if (cryptocurrency == "btc")
             {
                 cryptocurrency = "bitcoin";
             }
             else if (cryptocurrency == "dgb")
             {
                 cryptocurrency = "digibyte";
             }
             else if (cryptocurrency == "xrp")
             {
                 cryptocurrency = "ripple";
             }
             else if (cryptocurrency == "eth")
             {
                 cryptocurrency = "ethereum";
             }
             else if (cryptocurrency == "etc")
             {
                 cryptocurrency = "ethereum-classic";
             }
             else if (cryptocurrency == "ltc")
             {
                 cryptocurrency = "litecoin";
             }
             else if (cryptocurrency == "strat")
             {
                 cryptocurrency = "stratis";
             }
             else if (cryptocurrency == "strat")
             {
                 cryptocurrency = "stratis";
             }
             else if (cryptocurrency == "sc")
             {
                 cryptocurrency = "siacoin";
             }
             else if (cryptocurrency == "xml" || cryptocurrency == "stellar lumens")
             {
                 cryptocurrency = "stellar";
             }
             else if (cryptocurrency == "sys")
             {
                 cryptocurrency = "syscoin";
             }
             else if (cryptocurrency == "rdd")
             {
                 cryptocurrency = "reddcoin";
             }
             else if (cryptocurrency == "bcn")
             {
                 cryptocurrency = "bytecoin";
             }
             else if (cryptocurrency == "gnt" || cryptocurrency == "golem")
             {
                 cryptocurrency = "golem-network-tokens";
             }
             else if (cryptocurrency == "xmr")
             {
                 cryptocurrency = "monero";
             }
             cryptocurrency = FirstLetterToUpper(cryptocurrency);
             Program.runningAlerts.Add(new Alert(await Context.User.CreateDMChannelAsync(), cryptocurrency, alarmprice));
         }*/

        /*public class Alert
        {
            public RestDMChannel Channel;
            public string Currency;
            public double fprice;
            public double AlertThreshold;
            public Alert(RestDMChannel chan, string curr, double thresh)
            {
                Channel = chan;
                Currency = curr;
                AlertThreshold = thresh;
            }
           

            public double Check()
            {
                string page = "https://api.coinmarketcap.com/v1/ticker/" + Currency + "/?convert=EUR";
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.GetAsync(page))
                using (HttpContent content = response.Content)
                {
                    string result = await content.ReadAsStringAsync();
                    API convert = JsonConvert.DeserializeObject<API>(result.Substring(1, result.Length - 2));
                    fprice = Convert.ToDouble(convert.price_usd);
                }
                if (fprice >= AlertThreshold)
                    {
                        return fprice;
                    }
                    return -1d;
                }
            }*/



    }

}

