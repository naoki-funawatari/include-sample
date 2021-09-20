using System;

namespace include_sample.Views.Home
{
    public class Measure1ViewModel
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public TimeSpan DurationTime => EndDateTime - StartDateTime;
        public int FirstLayerCount { get; set; }
        public int SecondLayerACount { get; set; }
        public int ThirdLayerACount { get; set; }
        public int SecondLayerBCount { get; set; }
        public int ThirdLayerBCount { get; set; }
    }
}
