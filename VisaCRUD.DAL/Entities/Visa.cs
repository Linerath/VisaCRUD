using System;
using System.Collections.Generic;

namespace VisaCRUD.DAL.Entities
{
    public class Visa
    {
        public int Id { get; set; }
        public Country Country { get; set; }
        public ServiceType ServiceType { get; set; }
        public String Terms { get; set; }
        public String Validity { get; set; }
        public String Period { get; set; }
        public String Number { get; set; }
        public String WebSite { get; set; }
        public String AdditionalDocs { get; set; }
        public List<Document> Documents { get; set; }

        public Visa()
        {
            Documents = new List<Document>();
        }
    }
}
