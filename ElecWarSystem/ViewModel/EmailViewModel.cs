using ElecWarSystem.Models;
using ElecWarSystem.Serivces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services.Description;

namespace ElecWarSystem.ViewModel
{
    public class Mostalem
    {
        public int RecId { get; set; }
        public String We7daName { get; set; }
        public bool Checked { get; set; } = false;
    }
    public class EmailViewModel
    {
        public Email Email { get; set; }
        public List<String> RecIds { get; set; }
        public IEnumerable<Mostalem> Mostalem { get; set; }
        public String Message { get; set; } = "";
        public EmailViewModel()
        {
            UserService userService = new UserService();
            Mostalem = userService.getAllUsers().Select(m => new Mostalem()
            {
                We7daName = m.We7daName,
                RecId = m.ID
            });
        }
    }
}