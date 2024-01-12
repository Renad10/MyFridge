using MyFridge.Abstractions;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFridge.MVVM.Models
{
    public class Fridge: TableData
    {
        public string name { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Drink> Drinks { get; set; }
        
    }
}
