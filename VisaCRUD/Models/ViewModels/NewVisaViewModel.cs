using System;

namespace VisaCRUD.Models.ViewModels
{
    public class NewVisaViewModel
    {
        public int? Id { get; set; }
        public int Country { get; set; }
        public int? ServiceType { get; set; }
        public String Terms { get; set; }
        public String Validity { get; set; }
        public String Period { get; set; }
        public String Number { get; set; }
        public String WebSite { get; set; }
        public int[] Documents { get; set; }
    }
}