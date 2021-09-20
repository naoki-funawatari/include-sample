using System.Collections.Generic;

namespace include_sample.Models
{
    public class ThirdLayer : LayerBase
    {
        public int SecondLayerId { get; set; }
        public SecondLayer SecondLayer { get; set; }
        public List<FourthLayer> FourthLayers { get; set; }
    }
}
