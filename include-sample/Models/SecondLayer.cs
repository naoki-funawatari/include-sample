using System.Collections.Generic;

namespace include_sample.Models
{
    public class SecondLayer : LayerBase
    {
        public int FirstLayerId { get; set; }
        public FirstLayer FirstLayer { get; set; }
        public List<ThirdLayer> ThirdLayers { get; set; }
    }
}
