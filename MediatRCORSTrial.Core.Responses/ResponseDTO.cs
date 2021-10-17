using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MediatRCORSTrial.Core.Responses
{
    [DataContract]
    public class ResponseDTO<T> where T : class
    {
        [DataMember]
        public T Data { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public Information Information { get; set; }
        [DataMember]
        public string RC { get; set; }
    }

    [DataContract]
    public class Information
    {
        [DataMember]
        public string TrackId { get; set; }
        [DataMember]
        public DateTime ResponseDate { get; set; }
    }

    [DataContract]

    public class ResponseDTO
    {
        [DataMember]
        public bool Data { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public Information Information { get; set; }
        [DataMember]
        public string RC { get; set; }
    }
}
