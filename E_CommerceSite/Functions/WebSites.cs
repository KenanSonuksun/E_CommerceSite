using E_CommerceSite.Models;
using HtmlAgilityPack;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace E_CommerceSite.Functions
{
    public class WebSites
    {
        #region Descriptions
        int i = 0, j = 0;
        string brand = "", model, os = "", processor = "", ram = "", disc = "", window = "", payment = "", website = "", img = "",
        firstSection = "", secondSection = "", detail = "", url = "", category = "Notebook",link="";
        bool didFind = false;
        #endregion

        public List<Computers> N11Computers()
        {
            #region Clear 
            List<Computers> n11Computers = new List<Computers>();
            i = 0; j = 0;brand = ""; model=""; os = ""; processor = ""; ram = ""; disc = ""; window = ""; link=""; 
            payment = ""; website = ""; img = "";firstSection = ""; secondSection = ""; detail = ""; url = "";
            didFind = false;
            string[] text = new string[] { " i" };
            var web = new HtmlWeb();
            #endregion

            j = 1;
            do
            {
                
                url = "https://www.n11.com/bilgisayar/dizustu-bilgisayar?pg=" + j.ToString();
                var docN11 = web.Load(url);

                if (docN11.DocumentNode.SelectNodes("//div[@class='pro']") != null)
                {
                    foreach (var item in docN11.DocumentNode.SelectNodes("//div[@class='pro']"))
                    {

                        #region Seperate the Information
                        link = item.SelectSingleNode(".//a").Attributes["href"].Value;
                        detail = item.SelectSingleNode(".//h3").InnerText.Trim();
                        payment = item.SelectSingleNode(".//ins").InnerText.Trim().Split(' ', (char)2)[0] + " ₺";
                        website = "N-11 Computers";
                        img = item.SelectSingleNode(".//img").GetAttributeValue("data-src", null).Trim();
                    
                        string[] fulldetails = detail.Split(text, StringSplitOptions.None);

                        if (fulldetails.Length > 1)
                        {

                            firstSection = fulldetails[0];
                            secondSection = "i" + fulldetails[1];

                            brand = firstSection.Split(' ', (char)2)[0];
                            if(firstSection.Split(' ', (char)2).Length>1) model = firstSection.Split(' ', (char)2)[1];
                            processor = secondSection.Split(' ', (char)2)[0];

                            string[] secondSectionArray = secondSection.Split(' ');

                            i = 0;
                            do
                            {
                                if (secondSectionArray[i] == "Free") { os = "Free Dos"; didFind = true; }
                                if (secondSectionArray[i] == "Dos" && !didFind) os = "Dos";
                                if (secondSectionArray[i] == "8" || secondSectionArray[i] == "16" || secondSectionArray[i] == "32" || secondSectionArray[i] == "64") ram = secondSectionArray[i];
                                if (secondSectionArray[i] == "256" || secondSectionArray[i] == "512" || secondSectionArray[i] == "1") disc = secondSectionArray[i];
                                if (secondSectionArray[i] == "14&quot;" || secondSectionArray[i] == "15.6&quot;" || secondSectionArray[i] == "16.1&quot;" || secondSectionArray[i] == "17.3&quot;") window = secondSectionArray[i].Substring(0, 4);
                                if (secondSectionArray[i] == "14&quot;") window = secondSectionArray[i].Substring(0, 2);
                                i++;
                            } while (i < secondSectionArray.Length);

                            if(website != "" && payment != "" && os != "" && disc != "" && window != "" && processor != "" && brand != "" && model != "" && ram != "" && img != "")
                            {
                                n11Computers.Add(new Computers()
                                {
                                    website = this.website,
                                    payment = this.payment,
                                    os = this.os.ToUpper(),
                                    disc = this.disc == "1" ? this.disc+" TB":this.disc+" GB",
                                    window = this.window + " Inch",
                                    processor = this.processor,
                                    brand = this.brand.ToUpper(),
                                    model = this.model.ToUpper(),
                                    ram = this.ram + " GB",
                                    img = this.img,
                                    category = this.category,
                                    link = this.link
                                });
                            }
                        }
                        #endregion

                        brand = "";
                        model = "";
                        ram = "";
                        disc = "";
                        os = "";
                        window = "";
                        didFind = false;
                        img = "";
                        detail = "";
                        payment = "";
                        processor = "";
                    }
                }
                

                j++;
            } while (j <= 24);

            return n11Computers;
        }
        public List<Computers> VatanComputers()
        {
            #region Clear
            List<Computers> vatanComputers = new List<Computers>();
            i = 0; j = 0;
            brand = ""; model="";  os = ""; processor = ""; ram = ""; disc = ""; window = ""; payment = ""; website = "";
            img = ""; firstSection = ""; secondSection = ""; detail = ""; url = "";link = "";
            string[] text = new string[] { " i" };
            var web = new HtmlWeb();
            #endregion

            j = 0;
            do
            {
                url = "https://www.vatanbilgisayar.com/notebook/?page=" + j.ToString();
                var docVatan = web.Load(url);

                if(docVatan.DocumentNode.SelectNodes("//div[@class='product-list product-list--list-page']") != null)
                {
                    foreach (var item in docVatan.DocumentNode.SelectNodes("//div[@class='product-list product-list--list-page']"))
                    {

                        #region Seperate the Information
                        link = item.SelectSingleNode(".//a[@class='product-list__image-safe-link sld']").Attributes["href"].Value;
                        link = "https://www.vatanbilgisayar.com/" + link;
                        detail = item.SelectSingleNode(".//h3").InnerText.Trim();
                        payment = item.SelectSingleNode(".//span[@class='product-list__price']").InnerText.Trim() + " ₺";
                        website = "Vatan Computers";
                        img = item.SelectSingleNode(".//img").GetAttributeValue("data-src", null).Trim();

                        string[] fulldetails = detail.Split(text, StringSplitOptions.None);
                    

                        if (fulldetails.Length > 1)
                        {
                        

                            firstSection = fulldetails[0];
                            secondSection = "i" + fulldetails[1];
                            brand = firstSection.Split(' ', (char)2)[0];
                            model = firstSection.Split(' ', (char)2)[1];
                            processor = secondSection.Split('-', (char)2)[0];
                            processor = processor.Replace(" ", "-");
                            ram = secondSection.Split('-', (char)2)[1].Split('-', (char)2)[0];
                            if (ram.Length >= 3) ram = ram.Substring(0, ram.Length - 2);
                            disc = secondSection.Split(' ')[1].Split('-').Length > 2 ? secondSection.Split(' ')[1].Split('-')[2] : "";;
                            if (disc.Length >= 3) disc = disc.Substring(0,disc.Length-2);
                            if (disc.Length >= 3) disc = disc.Substring(0, 3);
                            if (disc == "1Tb") disc = "1";
                            window = secondSection.Split(' ').Length > 2 ? secondSection.Split(' ')[2].Split('-').Length > 1 ? secondSection.Split(' ')[2].Split('-')[1] : "" : "";
                            if (window.Length >= 5) window = window.Substring(0, window.Length - 3);
                            if (window == "14" || window == "15.6" || window == "16" || window == "13.5" || window == "17.3" || window == "16.1") window = window ;
                            else window = "";
                            
                            if (website != "" && payment != "" && disc != "" && window != "" && processor != "" && brand != "" && model != "" && ram != "" && img != "")
                            {
                                vatanComputers.Add(new Computers()
                                {
                                website = this.website,
                                payment = this.payment,
                                os = this.os.ToUpper(),
                                disc = this.disc == "1" ? this.disc + " TB" : this.disc + " GB",
                                window = this.window + " Inch",
                                processor = this.processor,
                                brand = this.brand.ToUpper(),
                                model = this.model.ToUpper(),
                                ram = this.ram + " GB",
                                img = this.img,
                                category = this.category,
                                link = this.link
                                });
                            }
                        }
                        #endregion

                        brand = "";
                        model = "";
                        ram = "";
                        disc = "";
                        os = "";
                        window = "";
                        img = "";
                        detail = "";
                        payment = "";
                        processor = "";
                        link = "";
                    }
                }
                

                j++;
            } while (j <= 19);

            return vatanComputers;
        }   
        public List<Computers> TrendyolComputers()
        {
            #region Clear
            List<Computers> trendyolComputers = new List<Computers>();
            int counter = 0;
            i = 0; j = 0;
            brand = ""; model = ""; os = ""; processor = ""; ram = ""; disc = ""; window = ""; payment = ""; website = "";
            img = "";detail = ""; url = "";link = "";
            didFind = false;
            string[] text = new string[] { " In" };
            string[] text2 = new string[] { " I" };
            var web = new HtmlWeb();
            #endregion

            j = 1;
            do
            {
                url = "https://www.trendyol.com/laptop-x-c103108?pi=" + j.ToString();
                var docTrendyol = web.Load(url);


                if (docTrendyol.DocumentNode.SelectNodes("//div[@class='product-down']") != null)
                {
                    var linkData = docTrendyol.DocumentNode.SelectNodes("//div[@class='p-card-chldrn-cntnr card-border']");
                    counter = 0;
                    foreach (var item in docTrendyol.DocumentNode.SelectNodes("//div[@class='product-down']"))
                    {

                        #region Seperate the Information
                        link = "https://www.trendyol.com/" + linkData[counter].SelectSingleNode(".//a").Attributes["href"].Value;
                        System.Diagnostics.Debug.WriteLine(link);
                        detail = item.SelectSingleNode(".//div[@class='prdct-desc-cntnr-ttl-w two-line-text']").InnerText.Trim();
                        brand  = item.SelectSingleNode(".//span[@class='prdct-desc-cntnr-ttl']").InnerText.Trim();
                        detail = detail.Substring(brand.Length);
                        model = detail.Split(' ', (char)2)[0];
                        payment = item.SelectSingleNode(".//div[@class='prc-box-dscntd']").InnerText.Trim().Split(' ', (char)2)[0] + " ₺";
                        //img = item.SelectSingleNode(".//img").GetAttributeValue("src", null).Trim();
                        website = "Trendyol Computers";

                        string[] fulldetails = detail.Split(text, StringSplitOptions.None);
                        if (fulldetails.Length > 1) fulldetails = fulldetails[1].Split(text2, StringSplitOptions.None);

                        if (fulldetails.Length > 1 && fulldetails[1].Length > 20)
                        {
                        
                            detail="i"+fulldetails[1];
                            processor = detail.Split(' ', (char)2)[0];
                            processor = processor.ToUpper();
                            processor = "i" + processor.Substring(1); 

                            string[] secondSectionArray = detail.Split(' ');

                            i = 0;
                            do
                            {
                                if (secondSectionArray[i] == "Free") { os = "Free Dos"; didFind = true; }
                                if (secondSectionArray[i] == "Dos" && !didFind) os = "Dos";
                                if (secondSectionArray[i] == "8gb" || secondSectionArray[i] == "16gb" || secondSectionArray[i] == "32gb" || secondSectionArray[i] == "64gb") ram = secondSectionArray[i];
                                if (secondSectionArray[i] == "256gb" || secondSectionArray[i] == "512gb" || secondSectionArray[i] == "1tb") disc = secondSectionArray[i];
                                if (secondSectionArray[i] == "14&quot;" || secondSectionArray[i] == "15.6&quot;" || secondSectionArray[i] == "16.1&quot;" || secondSectionArray[i] == "17.3&quot;") window = secondSectionArray[i].Substring(0, secondSectionArray[i].Length-6);
                            
                                i++;
                            } while (i < secondSectionArray.Length);

                            if (website != "" && payment != "" && disc != "" && processor != "" && brand != "" && model != "" && ram != "" )
                            {
                                ram = ram.Substring(0, ram.Length - 2);
                                disc = disc.Substring(0, disc.Length - 2);
                                trendyolComputers.Add(new Computers()
                                {
                                    website = this.website,
                                    payment = this.payment,
                                    os = this.os.ToUpper(),
                                    disc = this.disc == "1" ? this.disc + " TB" : this.disc + " GB",
                                    window = this.window + " Inch",
                                    processor = this.processor,
                                    brand = this.brand.ToUpper(),
                                    model = this.model.ToUpper(),
                                    ram = this.ram + " GB",
                                    img = this.img,
                                    category = this.category,
                                    link = this.link
                                });
                            }
                        }
                        #endregion

                        brand = "";
                        model = "";
                        ram = "";
                        disc = "";
                        os = "";
                        window = "";
                        img = "";
                        detail = "";
                        payment = "";
                        processor = "";
                        counter++;
                    }
                }
                

                j++;
            } while (j <= 50);

            return trendyolComputers;
        }
    }
}
