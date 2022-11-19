using E_CommerceSite.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_CommerceSite.Functions
{
    public class ConverList
    {
        public List<List<Computers>> converter(List<List<BsonDocument>> bsonData)
        {
            List<List<Computers>> lastData = new List<List<Computers>>();
            
            for (int i = 0; i < bsonData.Count; i++)
            {
                List<Computers> temp = new List<Computers>();
                lastData.Add(temp);
            }


            for (int i = 0; i < bsonData.Count; i++)
            {
                for (int j = 0; j < bsonData[i].Count; j++)
                {
                    Computers tempComp = new Computers();

                    tempComp.brand = bsonData[i][j]["brand"].ToString();
                    tempComp.model = bsonData[i][j]["model"].ToString();
                    tempComp.os = bsonData[i][j]["os"].ToString();
                    tempComp.payment = bsonData[i][j]["payment"].ToString();
                    tempComp.processor = bsonData[i][j]["processor"].ToString();
                    tempComp.ram = bsonData[i][j]["ram"].ToString();
                    tempComp.img = bsonData[i][j]["img"].ToString();
                    tempComp.category = bsonData[i][j]["category"].ToString();
                    tempComp.website = bsonData[i][j]["website"].ToString();
                    tempComp.window = bsonData[i][j]["window"].ToString();
                    tempComp.disc = bsonData[i][j]["disc"].ToString();
                    tempComp.link = bsonData[i][j]["link"].ToString();
                    lastData[i].Add(tempComp);
                }
            }

            return lastData;
        }
    }
}