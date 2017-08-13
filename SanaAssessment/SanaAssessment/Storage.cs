using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SanaAssessment
{
    public class Storage
    {
        public String Name { get; set; }

        public static List<SelectListItem> GetAvailableStorages()
        {
            var list = new List<SelectListItem>();

            list.Add(new SelectListItem()
            {
                Text = "Memory (default)",
                Value = "mem"
            });

            list.Add(new SelectListItem()
            {
                Text = "XML File",
                Value = "xml"
            });

            return list;
        }       
    }
}