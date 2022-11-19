using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace E_CommerceSite.Functions
{
    public class FindComputers
    {

        public List<List<BsonDocument>> findComputers(List<BsonDocument> computerDB1, IMongoCollection<BsonDocument> computerDB2)
        {
            FilterDefinition<BsonDocument> filter;
            List<List<BsonDocument>> totalComputers = new List<List<BsonDocument>>(); 

            computerDB1.ForEach(x => {

                List<BsonDocument> tempComputerList = new List<BsonDocument>();
                List<BsonDocument> tempList = new List<BsonDocument>();

                filter = (Builders<BsonDocument>.Filter.Eq("brand", x["brand"])) & (Builders<BsonDocument>.Filter.Eq("model", x["model"]))
                & (Builders<BsonDocument>.Filter.Eq("processor", x["processor"])) & (Builders<BsonDocument>.Filter.Eq("ram", x["ram"]))
                & (Builders<BsonDocument>.Filter.Eq("disc", x["disc"]));
                tempList = computerDB2.Find(filter).ToList();
                if (tempList.Count > 0)
                {
                    tempComputerList.Add(tempList[0]);
                    tempComputerList.Add(x);
                    totalComputers.Add(tempComputerList);
                }

                tempList.Clear();
            });


            return totalComputers;
        }
    }
}