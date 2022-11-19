using E_CommerceSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_CommerceSite.Functions
{
    
    public class CheckDuplicates
    {
        public List<Computers> removeDuplicates(List<Computers> computerList)
        {
            List<int> duplicateList = new List<int>();

            for (int i = 0; i < computerList.Count; i++)
            {
                for (int j = 0; j < computerList.Count; j++)
                {
                    if (computerList[i].brand == computerList[j].brand &&
                        computerList[i].model == computerList[j].model &&
                        computerList[i].ram == computerList[j].ram &&
                        computerList[i].processor == computerList[j].processor &&
                        computerList[i].disc == computerList[j].disc)
                    {
                        if (i != j && i < j) duplicateList.Add(j);
                    }
                }
            }

            duplicateList.Sort();
            duplicateList = duplicateList.Distinct().ToList();

            for (int i = duplicateList.Count - 1; i >= 0; i--) computerList.RemoveAt(duplicateList[i]);

          return computerList;  
        }

        
    }
}