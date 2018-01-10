using System;

namespace MobileBillingSample.DTOs
{
    //Contents of a CDR (Call Details Records) recieived from telecommunication switches
    public class CallDetailsRecord
    {
        public string OriginatingPhoneNumber { get; set; }
        public string RecievingPhoneNumber { get; set; }
        public DateTime StartTime { get; set; }
        public int DurationInSeconds { get; set; }
    }
}
