using E_CommerceSite.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_CommerceSite.Functions
{
    public class ConverList2
    {
        public List<Computers> converter(List<BsonDocument> bsonData)
        {
            List<Computers> lastData = new List<Computers>();

            for (int a = 0; a < bsonData.Count; a++)
            {
                Computers tempComp = new Computers();

                tempComp.brand = bsonData[a]["brand"].ToString();
                tempComp.model = bsonData[a]["model"].ToString();
                tempComp.os = bsonData[a]["os"].ToString();
                tempComp.payment = bsonData[a]["payment"].ToString();
                tempComp.processor = bsonData[a]["processor"].ToString();
                tempComp.ram = bsonData[a]["ram"].ToString();
                tempComp.img = bsonData[a]["img"].ToString();
                tempComp.category = bsonData[a]["category"].ToString();
                tempComp.website = bsonData[a]["website"].ToString();
                tempComp.window = bsonData[a]["window"].ToString();
                tempComp.disc = bsonData[a]["disc"].ToString();
                tempComp.link = bsonData[a]["link"].ToString();
                lastData.Add(tempComp);
            }

            return lastData;
        }
    }
}