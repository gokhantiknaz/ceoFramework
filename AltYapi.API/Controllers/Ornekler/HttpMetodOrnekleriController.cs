using AltYapi.Core.Dtos;
using AltYapi.Core.Models;
using AltYapi.Core.Services;
using AltYapi.Repository.Fake;
using Bogus.DataSets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace AltYapi.API.Controllers.Ornekler
{

    public class HttpMetodOrnekleriController : CustomBaseController
    {
        private readonly List<User> _users = FakeData.GetUsers(200);

        [HttpGet]
        public List<User> All()
        {
            return _users;
        }

        [HttpGet("{id}")]
        public User GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        [HttpGet("[action]")]
        public List<User> GetByName(string name)
        {

            return _users.Where(x => x.FirstName.StartsWith(name, StringComparison.OrdinalIgnoreCase)).ToList();
            // return _users.Where(x => x.FirsName.EndsWith(name)).ToList();
            //return _users.Where(x=>x.FirsName.Contains(name)).ToList();
        }


        [HttpPost]
        public User Save(User user)
        {
            _users.Add(user);

            return user;

        }
        [HttpPut]
        public User Update(User user)
        {
            var editedUser = _users.FirstOrDefault(x => x.Id == user.Id);
            editedUser.Id = user.Id;
            editedUser.FirstName = user.FirstName;
            editedUser.LastName = user.LastName;
            editedUser.Address = user.Address;
            return editedUser;
        }
        //microsoft.aspnetcore.jsonpatch\7.0.2\
        //microsoft.aspnetcore.mvc.newtonsoftjson\7.0.2\ 
        //builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddNewtonsoftJson(); --AddNewtonsoftJson ekle
       //Repository de gerekli değişiklikler yapılacak.
        [HttpPatch]
        public User UpdatePatch(JsonPatchDocument user , int id)
        {
            //örnek request
            // [
            //  {
            //    "op": "replace","path": "firsName",
            //    
            //    "value": "ENGIN"
            //   },
            //   {
            //    "path": "lastName",
            //    "op": "replace",
            //    "value": "ERZIK"
            //   }
            //]


            //[
            //  {
            //  "op": "remove",
            //  "path":"firsName"
            //  }
            //]
            var editedUser = _users.FirstOrDefault(x => x.Id == id);
            if (editedUser!=null)
            {
                user.ApplyTo(editedUser);
            }

            return editedUser;
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var deletedUser = _users.FirstOrDefault(x => x.Id == id);
            _users.Remove(deletedUser);
        }


    }
}
