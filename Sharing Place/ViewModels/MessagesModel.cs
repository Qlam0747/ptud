using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharing_Place.ViewModels
{
    internal class MessagesModel
    {
        public class MessageModel()
        {
            public string Name { get; set; }
            public string Message { get; set; }
            public string ImgAvt { get; set; }
            public string SentAt { get; set; }
        }
    }
}
