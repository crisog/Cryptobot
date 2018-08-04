using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptobot.Modules
{
    public class MarketCap
    {
        public long usd { get; set; }
        public double eur { get; set; }
        public double cny { get; set; }
        public double gbp { get; set; }
        public double cad { get; set; }
        public double rub { get; set; }
        public double hkd { get; set; }
        public double jpy { get; set; }
        public double aud { get; set; }
        public double brl { get; set; }
        public double inr { get; set; }
        public long krw { get; set; }
        public double mxn { get; set; }
        public double idr { get; set; }
        public double chf { get; set; }
        public double eth { get; set; }
        public string btc { get; set; }
    }

    public class Price
    {
        public double usd { get; set; }
        public double eur { get; set; }
        public double cny { get; set; }
        public double gbp { get; set; }
        public double cad { get; set; }
        public double rub { get; set; }
        public double hkd { get; set; }
        public double jpy { get; set; }
        public double aud { get; set; }
        public double brl { get; set; }
        public double inr { get; set; }
        public double krw { get; set; }
        public double mxn { get; set; }
        public double idr { get; set; }
        public double chf { get; set; }
        public double eth { get; set; }
        public string btc { get; set; }
    }

    public class Volume
    {
        public double usd { get; set; }
        public double eur { get; set; }
        public double cny { get; set; }
        public double gbp { get; set; }
        public double cad { get; set; }
        public double rub { get; set; }
        public double hkd { get; set; }
        public double jpy { get; set; }
        public double aud { get; set; }
        public double brl { get; set; }
        public double inr { get; set; }
        public double krw { get; set; }
        public double mxn { get; set; }
        public double idr { get; set; }
        public double chf { get; set; }
        public double eth { get; set; }
        public double btc { get; set; }
    }

    public class RootObject
    {
        public string symbol { get; set; }
        public string position { get; set; }
        public string name { get; set; }
        public MarketCap market_cap { get; set; }
        public Price price { get; set; }
        public string supply { get; set; }
        public Volume volume { get; set; }
        public string change { get; set; }
        public string timestamp { get; set; }
    }
}
