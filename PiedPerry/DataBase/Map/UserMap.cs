using System;

namespace PiedPerry.DataBase.Map
{
    public class UserMap
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public string Sex { get; set; }
        public string About { get; set; }
        public string Tags { get; set; }

        public int Rating { get; set; }

        // place of work
        // resume
        // photo 

        public DateTime BirthDate { get; set; }
    }
}
