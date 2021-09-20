using System.Collections.Generic;

namespace include_sample.Models
{
    public class FirstLayer : LayerBase
    {
        public List<LayerBase> Values { get; set; }
    }
}
