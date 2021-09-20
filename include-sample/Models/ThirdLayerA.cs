namespace include_sample.Models
{
    public class ThirdLayerA : LayerBase
    {
        public int SecondLayerAId { get; set; }
        public SecondLayerA SecondLayerA { get; set; }
    }
}
