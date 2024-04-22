namespace Saobracaj.eDokumenta
{
    class Const
    {
        #region Customization
        public const string pom = "";
        public const string xmlStart = "xml";
        public const string Invoice = "Invoice";
        public const string CustomizationID = "CustomizationID";
        public static string EN_MFIN_CUSTOMIZATION_ID = @"urn:cen.eu:en16931:2017#compliant#urn:mfin.gov.rs:srbdt:2021";
        public const string ProfileID = "ProfileID";
        public const string MeR = "MojEracunInvoice";
        public const string BrojFakture = "ID";
        public const string DatumIzdavanja = "IssueDate";
        public const string ValutaPlacanja = "DueDate";
        public const string OznakaDok = "InvoiceTypeCode";
        public const string Racun = "380";
        public const string VremeIzdavanja = "Note";
        public const string OdgovornaOsoba = "Note";
        public const string ImeOdgOsobe = "Note";
        public const string Napomena = "Note";
        public const string Valuta = "DocumentCurrencyCode";
        public const string Ino_Tax = "TaxCurrencyCode";
        public const string PeriodObaveze = "InvoicePeriod";
        public const string Pocetak = "StartDate";
        public const string Kraj = "EndDate";
        public const string DescriptionCode = "DescriptionCode";
        public const string Narudzbenica = "OrderReference";
        #endregion

        #region PDF
        public const string PDF = "AdditionalDocumentReference";
        public const string Dok = "ID";
        public const string kod = "DocumentTypeCode";
        public const string Attachment = "Attachment";
        public const string base64 = "EmbeddedDocumentBinaryObject";
        #endregion

        #region Podaci o posiljaocu i primaocu
        public const string Posiljalac = "AccountingSupplierParty";
        public const string Party = "Party";
        public const string PIB = "EndpointID";
        public const string Identification = "PartyIdentification";
        public const string ID = "ID";
        public const string PartyName = "PartyName";
        public const string Name = "Name";
        public const string PodaciOAdresi = "PostalAddress";
        public const string Ulica = "StreetName";
        public const string Grad = "CityName";
        public const string PostanskiBroj = "PostalZone";
        public const string AddressLine = "AddressLine";
        public const string Line = "Line";
        public const string Drzava = "Country";
        public const string Oznaka = "IdentificationCode";
        public const string Oznaka_Value = "RS";
        public const string PartyTax = "PartyTaxScheme";
        public const string CompanyID = "CompanyID";
        public const string TaxScheme = "TaxScheme";
        public const string TaxID_Value = "VAT";
        public const string PartyLegal = "PartyLegalEntity";
        public const string RegName = "RegistrationName";
        public const string MB = "CompanyID";
        public const string DodatnoOFirmi = "CompanyLegalForm";
        public const string Kontakt = "Contact";
        public const string Ime = "Name";
        public const string Mail = "ElectronicMail";
        public const string Primalac = "AccountingCustomerParty";
        #endregion

        #region Podaci o dostavi
        public const string Dostava = "Delivery";
        public const string DatumDostave = "ActualDeliveryDate";
        public const string LokacijaDostave = "DeliveryLocation";
        public const string Adresa = "Address";
        #endregion

        #region Podaci o placanju
        public const string Placanje = "PaymentMeans";
        public const string SifraPlacanja = "PaymentMeansCode";
        public const string SifraPlacanja_Value = "30";
        public const string NapomenaPlacanja = "InstructionNote";
        public const string Model = "PaymentID";
        public const string Tekuci = "PayeeFinancialAccount";
        public const string Tekuci_Value = "RS220-0000000054233-62";
        public const string UsloviPlacanja = "PaymentTerms";
        public const string UsloviPlacanja_Value = "Uslovi placanja";
        #endregion

        #region Obracun poreza
        public const string Tax = "TaxTotal";
        public const string UkupnoPorez = "TaxAmount";
        public const string TaxSub = "TaxSubtotal";
        public const string Osnovica = "TaxableAmount";
        public const string Porez = "TaxAmount";
        public const string Kategorija = "TaxCategory";
        public const string Procenat = "Percent";
        public const string Oslobodjeno = "TaxExemptionReason";
        public const string OslobodjenoKod = "TaxExemptionReasonCode";
        #endregion

        #region Rekapitulacija ukupnih iznosa
        public const string Rekap = "LegalMonetaryTotal";
        public const string Neto = "LineExtensionAmount";
        public const string NetoPorez = "TaxExclusiveAmount";
        public const string Bruto = "TaxInclusiveAmount";
        public const string Popust = "AllowanceTotalAmount";
        public const string Trosak = "ChargeTotalAmount";
        public const string Avans = "PrepaidAmount";
        public const string Ukupno = "PayableAmount";
        #endregion

        #region Podaci o stavkama racuna
        public const string Stavke = "InvoiceLine";
        public const string Kolicina = "InvoicedQuantity";
        public const string Stavka = "Item";
        public const string SifraArtikla = "SellersItemIdentification";
        public const string Barkod = "StandardItemIdentification";
        public const string PorezStavka = "ClassifiedTaxCategory";
        public const string Cena = "Price";
        public const string JedinicnaCena = "PriceAmount";
        public const string JedinicnaKolicina = "BaseQuantity";
        #endregion

        #region Odobrenje i zaduzenje
        /* Odobrenje namespace
         * <CreditNote 
         * xmlns:cac="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2"
         * xmlns:cbc="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2" 
         * xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
         * xmlns:xsd="http://www.w3.org/2001/XMLSchema"
         * xmlns="urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2">
        */
        public const string Zaduzenje = "383";
        public const string CreditTypeCode = "CreditNoteTypeCode";
        public const string Odobrenje = "381";
        public const string CreditNote = "CreditNote";
        public const string OdobrenjeValutaPlacanja = "PaymentDueDate";
        public const string OdobrenjeStavke = "CreditNoteLine";
        public const string OdobrenjeKolicina = "CreditedQuantity";
        public const string BilingRef = "BillingReference";
        public const string BilingRefDoc = "InvoiceDocumentReference";
        #endregion

        #region testiranje
        public const string PibPrimaoca = "";
        #endregion
    }
}
