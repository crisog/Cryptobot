# Cryptobot
A bot for discord that grabs Cryptocurrency prices from Coinmarketcap, Gdax, and some other exchange sites.


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

If you want to change your bot token, just go to Program.cs

await _client.LoginAsync(TokenType.Bot, "MzYzMDczOTgyMDkxNDkzMzc3.DK77Mg.hFe4UqfascQ9BkkVuuUhRD3bYdM");
