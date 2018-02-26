using System;

namespace PiedPerry.DataBase.Map
{
    [Serializable]
    public class UserMap
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public string Sex { get; set; }
        public string About { get; set; }
        public string Tags { get; set; }

        public int Rating { get; set; }

        public string BirthDate { get; set; }
    }
}
