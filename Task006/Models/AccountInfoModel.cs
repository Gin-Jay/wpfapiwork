using System;
using System.Collections.Generic;

namespace Task006.Models
{
    public class AccountInfoModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int World { get; set; }
        public List<string> Guilds { get; set; }
        public DateTime Created { get; set; }
        public List<string> Access { get; set; }
        public bool Commander { get; set; }
    }
}

