using System;
using userIin;
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
        public userDetail userData { get; set; }
        public string fullname { get; set; }
        public DateTime createdate { get; set; }
        public int status { get; set; }
        public int wrongpasscounter { get; set; }


    }

}
