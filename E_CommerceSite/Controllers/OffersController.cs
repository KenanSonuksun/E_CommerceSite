using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using HtmlAgilityPack;
using E_CommerceSite.Models;
using E_CommerceSite.Functions;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceSite.Controllers
{
    
    public class OffersController : Controller
    {
        
        public ActionResult Index()
        {
            #region Descriptions
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<Computers> n11Computers = new List<Computers>();
            List<Computers> vatanComputers = new List<Computers>();
            List<Computers> trendyolComputers = new List<Computers>();
            List<Computers> webComputers = new List<Computers>();
            List<List<Computers>> totalList = new List<List<Computers>>();
            List<List<Computers>> viewList = new List<List<Computers>>();
            WebSites webSites = new WebSites();
            CheckDuplicates checkDuplicates = new CheckDuplicates();
            IMongoDatabase db;
            IMongoCollection<BsonDocument> n11Db, vatanDb, trendyolDb,webDb;
            List<BsonDocument> n11Data, vatanData, trendyolData;
            FindComputers findComputers = new FindComputers();
            List<List<BsonDocument>> totalComputers1, totalComputers2, totalComputers3, totalComputers4, totalComputers5, totalComputers6;
            ConverList converList = new ConverList();
            List<List<Computers>> lastData1 = new List<List<Computers>>(); 
            List<List<Computers>> lastData2 = new List<List<Computers>>(); 
            List<List<Computers>> lastData3 = new List<List<Computers>>();
            List<List<Computers>> lastData4 = new List<List<Computers>>();
            List<List<Computers>> lastData5 = new List<List<Computers>>();
            List<List<Computers>> lastData6 = new List<List<Computers>>();
            MongoClient mongoClient = new MongoClient(connectionString: "mongodb://localhost:27017");
            List<BsonDocument> webData = new List<BsonDocument>();
            ConverList2 converList2 = new ConverList2();
            #endregion

            #region Web Scraping
            n11Computers = webSites.N11Computers();
            vatanComputers = webSites.VatanComputers();
            trendyolComputers = webSites.TrendyolComputers();
            #region Web Computers Settings
            db = mongoClient.GetDatabase("Computers");

            webDb = db.GetCollection<BsonDocument>("Web Computers");
            webData = webDb.Find(FilterDefinition<BsonDocument>.Empty).ToList();

            if (webData.Count > 0) webComputers = converList2.converter(webData);
            #endregion
            #endregion

            #region Check Duplicates
            n11Computers = checkDuplicates.removeDuplicates(n11Computers);
            vatanComputers =  checkDuplicates.removeDuplicates(vatanComputers);
            trendyolComputers = checkDuplicates.removeDuplicates(trendyolComputers);
            webComputers = checkDuplicates.removeDuplicates(webComputers);
            
            #endregion

            #region MongoDB Connection

            db = mongoClient.GetDatabase("Computers");

            n11Db = db.GetCollection<BsonDocument>("N11 Computers");
            vatanDb = db.GetCollection<BsonDocument>("Vatan Computers");
            trendyolDb = db.GetCollection<BsonDocument>("Trendyol Computers");

            n11Db.Database.DropCollection("N11 Computers");
            vatanDb.Database.DropCollection("Vatan Computers");
            trendyolDb.Database.DropCollection("Trendyol Computers");
            
            n11Computers.ForEach(x => n11Db.InsertOne(x.ToBsonDocument()));
            vatanComputers.ForEach(x => vatanDb.InsertOne(x.ToBsonDocument()));
            trendyolComputers.ForEach(x => trendyolDb.InsertOne(x.ToBsonDocument()));
            

            n11Data = n11Db.Find(FilterDefinition<BsonDocument>.Empty).ToList();
            vatanData = vatanDb.Find(FilterDefinition<BsonDocument>.Empty).ToList();
            trendyolData = trendyolDb.Find(FilterDefinition<BsonDocument>.Empty).ToList();
            #endregion

            #region Find Common Computers
            totalComputers1 = findComputers.findComputers(vatanData, n11Db);
            totalComputers2 = findComputers.findComputers(trendyolData, n11Db);
            totalComputers3 = findComputers.findComputers(trendyolData, vatanDb);
            totalComputers4 = findComputers.findComputers(vatanData, webDb);
            totalComputers5 = findComputers.findComputers(trendyolData, webDb);
            totalComputers6 = findComputers.findComputers(n11Data, webDb);
            #endregion

            #region Converter
            if (totalComputers1.Count > 0) lastData1 = converList.converter(totalComputers1);
            if (totalComputers2.Count > 0) lastData2 = converList.converter(totalComputers2);
            if (totalComputers3.Count > 0) lastData3 = converList.converter(totalComputers3);
            if (totalComputers4.Count > 0) lastData4 = converList.converter(totalComputers4);
            if (totalComputers5.Count > 0) lastData5 = converList.converter(totalComputers5);
            if (totalComputers6.Count > 0) lastData6 = converList.converter(totalComputers6);
            #endregion

            #region Collect All Data
            lastData1.ForEach(x => viewList.Add(x));
            lastData2.ForEach(x => viewList.Add(x));
            lastData3.ForEach(x => viewList.Add(x));
            lastData4.ForEach(x => viewList.Add(x));
            lastData5.ForEach(x => viewList.Add(x));
            lastData6.ForEach(x => viewList.Add(x));
            #endregion

            #region Remove Elements from Memory
            n11Computers = null;
            vatanComputers = null;
            trendyolComputers = null;
            webComputers = null;
            totalList = null;
            webSites = null;
            checkDuplicates = null;
            db = null; 
            n11Db = null;
            vatanDb = null;
            trendyolDb = null;
            webDb = null;
            n11Data = null;
            vatanData = null; 
            trendyolData = null;
            webData = null;
            findComputers = null;
            totalComputers1 = null; 
            totalComputers2 = null; 
            totalComputers3 = null;
            totalComputers4 = null;
            totalComputers5 = null;
            totalComputers6 = null;
            converList = null;
            lastData1 = null;
            lastData2 = null;
            lastData3 = null;
            lastData4 = null;
            lastData5 = null;
            lastData6 = null;
            #endregion

            return View(viewList);
        }
    }
}