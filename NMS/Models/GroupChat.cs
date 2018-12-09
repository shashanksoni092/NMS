using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NMS.Models
{
    public class GroupChat
    {
        NMSEntities db = new NMSEntities();

        public List<NGroup> GetGroupChat(string tid)
        {
           
            return db.NGetGroupChat(tid).ToList();
        }
    }
}