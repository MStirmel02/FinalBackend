using FinalBackend.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBackend.Services.Services
{
    public interface IObjectService
    {

        public List<ObjectModel> GetObjectList();

        public int PostObject(FullObjectModel obj);

        public FullObjectModel GetObjectByID(string id);

        public int PutObject(FullObjectModel obj, string editUser, string action);
    }
}
