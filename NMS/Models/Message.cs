using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NMS.Models
{
    public class Message
    {
        NMSEntities db = new NMSEntities();

        public List<NGroupMessageTeacher1>GetMessage(int id)
        {
            
            return db.NGetAllGroupMessage(id).ToList();
        }
    }
}