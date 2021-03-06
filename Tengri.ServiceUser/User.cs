using System;
using System.Globalization;

namespace Tengri.ServiceUser
{
    public class User
    {
        //public string id { get; set; }
        /// <summary>
        /// password of each user
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// unique login of each user
        /// </summary>
        public string login { get; set; }
        public string fullname { get; set; }
        public DateTime createdate { get; set; }
        public int status { get; set; }
        public int wrongpasscounter { get; set; }
        public string userIin { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public int Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string AdressOfRegistration { get; set; }
        public string DocType { get; set; }
        public string DocNumber { get; set; }
        public string DocIssuer { get; set; }
        public string DocDate { get; set; }
        public DateTime DocEndDate { get; set; }
        public int Age { get; set; }
        public string doc_status { get; set; }
        public bool city_check { get; set; }

        public User() { }
        public User(string Content)
        {
            //CultureInfo provider = new CultureInfo("de-DE");
            
            string[] DataContent = Content
                .Replace("}", "")
                .Replace("{", "")
                .Replace("\"personal_data\":", "")
                .Split(new string[] { ",\"" }, StringSplitOptions.None);
            foreach (string prop in DataContent)
            {
                string[] data = prop.Replace("\"", "").Replace(@"\", "").Split(':');
                if (data[0].Equals("Iin"))
                    this.userIin = data[1];
                else if (data[0].Equals("LastName"))
                    this.lastName = data[1];
                else if (data[0].Equals("FirstName"))
                    this.firstName = data[1];
                else if (data[0].Equals("MiddleName"))
                    this.middleName = data[1];
                else if (data[0].Equals("Gender"))
                    this.Gender = Int32.Parse(data[1]);
                else if (data[0].Equals("BirthDate"))
                    this.BirthDate = DateTime.ParseExact(data[1], "dd.MM.yyyy", CultureInfo.InvariantCulture);
                else if (data[0].Equals("AddressOfRegistation"))
                    this.AdressOfRegistration = data[1];
                else if (data[0].Equals("DocType"))
                    this.DocType = data[1];
                else if (data[0].Equals("DocNumber"))
                    this.DocNumber = data[1];
                else if (data[0].Equals("DocIssuer"))
                    this.DocIssuer = data[1];
                else if (data[0].Equals("DocDate"))
                    this.DocDate = data[1];
                else if (data[0].Equals("DocEndDate"))
                    this.DocEndDate = DateTime.ParseExact(data[1], "yyyy-MM-ddTHH", CultureInfo.InvariantCulture);
                else if (data[0].Equals("Age"))
                    this.Age = int.Parse(data[1]);
                else if (data[0].Equals("status"))
                    this.doc_status = data[1];
                else if (data[0].Equals("city_check"))
                    this.city_check = bool.Parse(data[1]);
            }

        }
    }

}
