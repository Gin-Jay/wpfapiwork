using System.Collections.Generic;

namespace Task006.Models
{
   
    public class Background
    {
        public int Id { get; set; }
        public List<int> Colors { get; set; }
    }

    public class Foreground
    {
        public int Id { get; set; }
        public List<int> Colors { get; set; }
    }

    public class Emblem
    {
        public Background Background { get; set; }
        public Foreground Foreground { get; set; }
        public List<string> Flags { get; set; }
    }

    public class GuildInfoModel
    {
        public int Level { get; set; }
        public string Motd { get; set; }
        public int Influence { get; set; }
        public int Aetherium { get; set; }
        public int Resonance { get; set; }
        public int Favor { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public Emblem Emblem { get; set; }

        public override string ToString()
        {
            return $"{Name} [{Tag}]";
        }
    }
}
