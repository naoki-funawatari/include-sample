using System.Collections.Generic;

namespace include_sample.Models
{
    public class SecondLayerA : LayerBase
    {
        public int FirstLayerId { get; set; }
        public FirstLayer FirstLayer { get; set; }
        public List<ThirdLayerA> ThirdLayers { get; set; }
    }
}
