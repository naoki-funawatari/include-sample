namespace include_sample.Models
{
    public class FourthLayer : LayerBase
    {
        public int ThirdLayerId { get; set; }
        public ThirdLayer ThirdLayer { get; set; }
    }
}
