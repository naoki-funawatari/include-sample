using System.Collections.Generic;

namespace include_sample.Models
{
    public class ThirdLayer : LayerBase
    {
        public List<LayerBase> Values { get; set; }
    }
}
