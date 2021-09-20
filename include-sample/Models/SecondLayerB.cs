using System.Collections.Generic;

namespace include_sample.Models
{
    public class SecondLayerB : LayerBase
    {
        public int FirstLayerId { get; set; }
        public FirstLayer FirstLayer { get; set; }
        public List<ThirdLayerB> ThirdLayers { get; set; }
    }
}
