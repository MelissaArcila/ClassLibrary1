using System.Xml;
using System.Xml.Serialization;
using UBLClasses;

namespace TestUBLClasses
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var invoice = new InvoiceType
                {
                    ID = new IDType { Value = "SPD" },
                    IssueDate = new IssueDateType { Value = DateTime.Now },
                    InvoiceTypeCode = new InvoiceTypeCodeType { Value = "60" },
                    AccountingSupplierParty = new SupplierPartyType
                    {
                        Party = new PartyType
                        {
                            PartyName = new PartyNameType[]
                            {
                                new PartyNameType { Name = new NameType1 { Value = "CADENA S.A" } }
                            },
                            PostalAddress = new AddressType
                            {
                                StreetName = new StreetNameType { Value = "ANTIOQUIA" },
                                CityName = new CityNameType { Value = "LA ESTRELLA" },
                                Country = new CountryType { IdentificationCode = new IdentificationCodeType { Value = "CO" } }
                            }
                        }
                    },
                    AccountingCustomerParty = new CustomerPartyType
                    {
                        Party = new PartyType
                        {
                            PartyName = new PartyNameType[]
                            {
                                new PartyNameType { Name = new NameType1 { Value = "MELISSA" } }
                            },
                            PostalAddress = new AddressType
                            {
                                StreetName = new StreetNameType { Value = "ANTIOQUIA" },
                                CityName = new CityNameType { Value = "ENVIGADO" },
                                Country = new CountryType { IdentificationCode = new IdentificationCodeType { Value = "CO" } }
                            }
                        }
                    },
                    InvoiceLine = new InvoiceLineType[]
                    {
                        new InvoiceLineType
                        {
                            ID = new IDType { Value = "1" },
                            InvoicedQuantity = new InvoicedQuantityType { Value = 2, unitCode = "EA" },
                            LineExtensionAmount = new LineExtensionAmountType { Value = 100, currencyID = "COP" },
                            Item = new ItemType
                            {
                                Description = new DescriptionType[]
                                {
                                    new DescriptionType { Value = "ENERGÍA ELECTRICA" }
                                },
                                Name = new NameType1 { Value = "ENERGÍA" } 
                            },
                            Price = new PriceType
                            {
                                PriceAmount = new PriceAmountType { Value = 50, currencyID = "COP" }
                            }
                        },
                        new InvoiceLineType
                        {
                            ID = new IDType { Value = "2" },
                            InvoicedQuantity = new InvoicedQuantityType { Value = 1, unitCode = "EA" },
                            LineExtensionAmount = new LineExtensionAmountType { Value = 200, currencyID = "COP" },
                            Item = new ItemType
                            {
                                Description = new DescriptionType[]
                                {
                                    new DescriptionType { Value = "ACUEDUCTO DE LA CIUDAD DE MEDELLIN" }
                                },
                                Name = new NameType1 { Value = "ACUEDUCTO" } 
                            },
                            Price = new PriceType
                            {
                                PriceAmount = new PriceAmountType { Value = 200, currencyID = "COP" }
                            }
                        }
                    }
                };

                var serializer = new XmlSerializer(typeof(InvoiceType));
                var xmlNamespaces = new XmlSerializerNamespaces();
                xmlNamespaces.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                xmlNamespaces.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");

                var xmlWriterSettings = new XmlWriterSettings
                {
                    Encoding = new System.Text.UTF8Encoding(true),
                    Indent = true,
                    OmitXmlDeclaration = true
                };


                using (var writer = new StringWriter())
                using (var xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings { Indent = true }))
                {
                    serializer.Serialize(xmlWriter, invoice, xmlNamespaces);
                    var xmlOutput = writer.ToString();
                    Console.WriteLine(" XML SERIALIZADO:");
                    Console.WriteLine(xmlOutput);


                    if (string.IsNullOrWhiteSpace(xmlOutput))
                    {
                        Console.WriteLine("EL XML ESTA VACÍO");
                    }

                    //using (var reader = new StringReader(xmlOutput))
                    //{
                    //    var deserializedInvoice = (InvoiceType)serializer.Deserialize(reader);
                    //    Console.WriteLine("XML DESERIALIZADO: " + deserializedInvoice.ToString());
                    //}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }

            Console.WriteLine("Presione enter para salir");
            Console.ReadKey();
        }

    }
}
