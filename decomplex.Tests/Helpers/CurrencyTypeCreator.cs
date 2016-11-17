using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using NUnit.Framework;

namespace decomplex.Tests.Helpers
{
    /// <summary>
    /// The test is used to load currency data. Not the best option.
    /// Uncomment the line with [TestFixture] to make it runnable.
    /// </summary>
    //[TestFixture]
    public class CurrencyTypeCreator
    {
        /// <summary>
        /*
        <ISO_4217 Pblshd="2016-07-01">
        <CcyTbl>
            <CcyNtry>
                <CtryNm>AFGHANISTAN</CtryNm>
                <CcyNm>Afghani</CcyNm>
                <Ccy>AFN</Ccy>
                <CcyNbr>971</CcyNbr>
                <CcyMnrUnts>2</CcyMnrUnts>
            </CcyNtry>
            ...
        */
        /// </summary>

        [Test]
        public void CreateCurrencyEnumType()
        {
            const string XmlSource = "http://www.currency-iso.org/dam/downloads/lists/list_one.xml";
            const bool GenerateCountries = true;

            var xmlDoc = XDocument.Load(XmlSource);

            var countries = xmlDoc.Descendants("CcyTbl").Descendants("CcyNtry")
                .Select(country => new
                {
                    CountryName = country.Element("CtryNm")?.Value,
                    CurrencyName = country.Element("CcyNm")?.Value,
                    CurrencyCode = country.Element("Ccy")?.Value,
                    CurrencyNumber = country.Element("CcyNbr")?.Value,
                    NumberOfDigitalPlaces = country.Element("CcyMnrUnts")?.Value,
                })
                .Where(country => country.CurrencyCode != null && country.CurrencyNumber != null)
                .ToList();

            var currencies = countries
                .GroupBy(country => country.CurrencyCode)
                .Select(currencyGroup => new
                {
                    CurrencyCode = currencyGroup.Key,
                    CurrencyName = currencyGroup.First().CurrencyName,
                    CurrencyNumber = currencyGroup.First().CurrencyNumber,
                    NumberOfDigitalPlaces = currencyGroup.First().NumberOfDigitalPlaces,
                    CountryNames = currencyGroup.Select(country => country.CountryName)
                })
                .OrderBy(currency => currency.CurrencyCode)
                .ToList();

            var sb = new StringBuilder();

            sb.AppendLine("namespace NS");
            sb.AppendLine("{");
            sb.AppendLine("public enum CurrencyType");
            sb.AppendLine("{");

            foreach (var currency in currencies)
            {
                if (GenerateCountries)
                {
                    foreach (var countryName in currency.CountryNames)
                    {
                        sb.AppendLine($"[CurrencyCountry(\"{countryName}\")]");
                    }
                }

                var numberOfDecimalPlaces = -1;
                int.TryParse(currency.NumberOfDigitalPlaces, out numberOfDecimalPlaces);

                sb.AppendLine($"[CurrencyDefinition(\"{currency.CurrencyCode}\", \"{currency.CurrencyName}\", \"{currency.CurrencyNumber}\", {numberOfDecimalPlaces})]");
                sb.AppendLine($"{currency.CurrencyCode} = {currency.CurrencyNumber},");
            }

            sb.AppendLine("}");
            sb.AppendLine("}");

            var outputFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CurrencyType.cs");
            File.WriteAllText(outputFile, sb.ToString());
        }
    }
}