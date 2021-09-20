using System.Collections.Generic;

namespace include_sample.Models
{
    public class FirstLayer : LayerBase
    {
        public List<SecondLayerA> SecondLayersA { get; set; }
        public List<SecondLayerB> SecondLayersB { get; set; }
    }
}
