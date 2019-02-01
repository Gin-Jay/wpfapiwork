using System.Collections.Generic;

namespace Task006.Models
{
    public class TitleInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Achievement { get; set; }
        public List<int> Achievements { get; set; }
    }
}
