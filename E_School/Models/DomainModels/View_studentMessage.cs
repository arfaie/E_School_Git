//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace E_School.Models.DomainModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class View_studentMessage
    {
        public int idMsgRecord { get; set; }
        public int idMessage { get; set; }
        public int idReceiverType { get; set; }
        public int idReceiver { get; set; }
        public Nullable<bool> isRead { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public int date { get; set; }
        public short idMessageType { get; set; }
        public int idSenderType { get; set; }
        public int idSender { get; set; }
        public int idYear { get; set; }
        public Nullable<bool> isActiveSend { get; set; }
        public Nullable<bool> isActiveRecive { get; set; }
    }
}
