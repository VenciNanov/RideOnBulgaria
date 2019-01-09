using System;

namespace RideOnBulgaria.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string RedirectCode { get; set; }

        public string AdditionalInformation  { get; set; }

    }
}