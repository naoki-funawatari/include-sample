using System.Collections.Generic;

namespace include_sample.Models
{
    public class SecondLayer : LayerBase
    {
        public List<LayerBase> Values { get; set; }
    }
}
