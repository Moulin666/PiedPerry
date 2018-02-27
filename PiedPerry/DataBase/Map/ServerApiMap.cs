using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PiedPerry.DataBase.Map
{
    [Serializable]
    public class ResponseCode
    {
        public string code;
        public string answer;
    }

    [Serializable]
    public class UserMap
    {
        public string first_name;
        public string last_name;
        public string middle_name;
        public string UserGender;
        public string about_me;
        public string tags;
        public int rating;
        public string birthday_date;
        public string place_of_work;
    }

    [Serializable]
    public class Resume
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int minimal_rating { get; set; }
        public string tags { get; set; }
        public int wanted_salary { get; set; }
        public string psycho_type { get; set; }
        public int user_id { get; set; }
    }

    [Serializable]
    public class Places_of_work
    {
        public int id { get; set; }
        public string name { get; set; }
        public string position { get; set; }
        public string description { get; set; }
        public string date_begin { get; set; }
        public string date_end { get; set; }
    }

    [Serializable]
    public class Response
    {
        [JsonProperty("request_Info")]
        public ResponseCode responseCode;
        [JsonProperty("send_data")]
        public UserMap userMap;
        public Resume Resume;
        public List<Places_of_work> Places_of_work;
    }
}
