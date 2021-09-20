namespace include_sample.Models
{
    public class ThirdLayerB : LayerBase
    {
        public int SecondLayerBId { get; set; }
        public SecondLayerB SecondLayerB { get; set; }
    }
}
