using System.Collections.Generic;

namespace include_sample.Models
{
    public class FirstLayer : LayerBase
    {
        public List<SecondLayer> SecondLayers { get; set; }
    }
}
