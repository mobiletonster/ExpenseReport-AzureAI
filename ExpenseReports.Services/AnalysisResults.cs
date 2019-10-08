using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseReports.Services
{
    public class AnalysisResults
    {
        public string status { get; set; }
        public Recognitionresult[] recognitionResults { get; set; }
        public Understandingresult[] understandingResults { get; set; }
    }

    public class Recognitionresult
    {
        public int page { get; set; }
        public float clockwiseOrientation { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string unit { get; set; }
        public Line[] lines { get; set; }
    }

    public class Line
    {
        public int[] boundingBox { get; set; }
        public string text { get; set; }
        public Word[] words { get; set; }
    }

    public class Word
    {
        public int[] boundingBox { get; set; }
        public string text { get; set; }
        public string confidence { get; set; }
    }

    public class Understandingresult
    {
        public int[] pages { get; set; }
        public Fields fields { get; set; }
    }

    public class Fields
    {
        public Subtotal Subtotal { get; set; }
        public Total Total { get; set; }
        public Tax Tax { get; set; }
        public Merchantaddress MerchantAddress { get; set; }
        public Merchantname MerchantName { get; set; }
        public Merchantphonenumber MerchantPhoneNumber { get; set; }
        public Transactiondate TransactionDate { get; set; }
        public Transactiontime TransactionTime { get; set; }
    }

    public class Subtotal
    {
        public string valueType { get; set; }
        public float value { get; set; }
        public string text { get; set; }
        public Element[] elements { get; set; }
    }

    public class Element
    {
        public string _ref { get; set; }
    }

    public class Total
    {
        public string valueType { get; set; }
        public float value { get; set; }
        public string text { get; set; }
        public Element1[] elements { get; set; }
    }

    public class Element1
    {
        public string _ref { get; set; }
    }

    public class Tax
    {
        public string valueType { get; set; }
        public float value { get; set; }
        public string text { get; set; }
        public Element2[] elements { get; set; }
    }

    public class Element2
    {
        public string _ref { get; set; }
    }

    public class Merchantaddress
    {
        public string valueType { get; set; }
        public string value { get; set; }
        public string text { get; set; }
        public Element3[] elements { get; set; }
    }

    public class Element3
    {
        public string _ref { get; set; }
    }

    public class Merchantname
    {
        public string valueType { get; set; }
        public string value { get; set; }
        public string text { get; set; }
        public Element4[] elements { get; set; }
    }

    public class Element4
    {
        public string _ref { get; set; }
    }

    public class Merchantphonenumber
    {
        public string valueType { get; set; }
        public string value { get; set; }
        public string text { get; set; }
        public Element5[] elements { get; set; }
    }

    public class Element5
    {
        public string _ref { get; set; }
    }

    public class Transactiondate
    {
        public string valueType { get; set; }
        public string value { get; set; }
        public string text { get; set; }
        public Element6[] elements { get; set; }
    }

    public class Element6
    {
        public string _ref { get; set; }
    }

    public class Transactiontime
    {
        public string valueType { get; set; }
        public string value { get; set; }
        public string text { get; set; }
        public Element6[] elements { get; set; }
    }
}
