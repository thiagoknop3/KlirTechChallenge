using Klir.TechChallenge.Domain.Entities;
using Klir.TechChallenge.Domain.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Klir.TechChallenge.Infra.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly string _jsonFile;
        protected readonly List<TEntity> _entityObj;

        public BaseRepository(string jsonFile)
        {

            if (!File.Exists(jsonFile))
                using (FileStream fs = File.Create(jsonFile))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("");
                    fs.Write(info, 0, info.Length);
                }
            var json = File.ReadAllText(jsonFile);
            _entityObj = JsonConvert.DeserializeObject<List<TEntity>>(json);
            _jsonFile = jsonFile;
        }

        public void Insert(TEntity obj)
        {
            _entityObj.Add(obj);
            SaveJsonFIle();
        }

        public void Update(TEntity obj)
        {
            var objToReplace = _entityObj
                .FirstOrDefault(e => e.Id.Equals(obj.Id));
            objToReplace = obj;
            SaveJsonFIle();

        }

        public void Upsert(TEntity obj)
        {
            var objToReplace = _entityObj
                .FirstOrDefault(e => e.Id.Equals(obj.Id));
            if (objToReplace == null)
                Insert(obj);
            else
                Update(obj);

        }

        public void Delete(int id)
        {
            var obj = _entityObj.FirstOrDefault(x => x.Id == id);
            _entityObj.Remove(obj);
            SaveJsonFIle();
        }

        public IList<TEntity> Select() =>
            _entityObj.ToList();

        public TEntity Select(int id) =>
            _entityObj.Find(e => e.Id == id);

        private void SaveJsonFIle()
        {
            var json = JsonConvert.SerializeObject(_entityObj);
            File.WriteAllText(_jsonFile, json);
        }


    }
}
