using System;

namespace include_sample.Views.Home
{
    public class PrivacyViewModel
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public TimeSpan DurationTime => EndDateTime - StartDateTime;
    }
}
