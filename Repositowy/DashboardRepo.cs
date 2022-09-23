using System;
using System.Collections.Generic;
using PresonelManagmentBE.Dtos;
using PresonelManagmentBE.Interface;

namespace PresonelManagmentBE.Repositowy
{
    public class DashboardRepo : IDashboardRepo
    {
        public object GetDashboard()
        {
            Random random = new Random();
            string[] category = { "Barmani", "Kelnerzy", "Kucharze" };
            var costList = new List<Cost> { };
            for(int i = 0; i < 7; i++)
            { 
                costList.Add(
                    new Cost 
                    { 
                        Month = i + 1, 
                        CostValue = random.Next(60000, 100000)
                    }
                );
            }
            
            
            var personnelCount= new List<PersonnelCount> { };
            for(int i = 0; i < 3; i++)
            {
                personnelCount.Add(
                    new PersonnelCount
                    {
                        Name = category[i],
                        Count = random.Next(5, 15)
                    }
                );
            }
            
            var eventCount = new List<Cost> { };
            for(int i = 0; i < 7; i++)
            {
                eventCount.Add(
                    new Cost
                    {
                        Month = i + 1,
                        CostValue = random.Next(8, 15)
                    }
                );
            }
            var personnelByMonth = new List<Cost> { };
            for (int i = 0; i < 7; i++)
            {
                personnelByMonth.Add(
                    new Cost
                    {
                        Month = i+1,
                        CostValue = random.Next(22, 35)
                    }
               );
            }
            var dashboard = new
            {
                CostList = costList,
                PersonnelCount = personnelCount,
                EventCount = eventCount,
                PersonnelByMonth = personnelByMonth
            };
            
            return dashboard;
        }
    }
}