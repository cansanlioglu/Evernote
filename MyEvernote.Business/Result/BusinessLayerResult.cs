using MyEvernote.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Business.Result
{
    public class BusinessLayerResult<T> where T:class  // Class Generic yapılda oluşturuldu
    {
        public List<ErrorMessagesObj> Errors { get; set; }
        public T Result { get; set; }

        public BusinessLayerResult()
        {
            Errors = new List<ErrorMessagesObj>();
        }

        public void AddError (ErrorMessagesCode code, string message)   
        {
            Errors.Add(new ErrorMessagesObj() { Code = code, Message = message });
        }
    }
}
