using System.Collections.Generic;

namespace Sports.Business.ViewModel
{
    public class RoleSecurityEditViewModel
    {
        public RoleSecurityEditViewModel()
        {
            SecurityGroups = new List<RoleSecurityGroupViewModel>();
        }

        public int RoleId { get; set; }

        public string Name { get; set; }

        public List<RoleSecurityGroupViewModel> SecurityGroups { get; set; }
    }

    public class RoleSecurityGroupViewModel
    {
        public RoleSecurityGroupViewModel()
        {
            Securities = new List<SecurityViewModel>();
        }

        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public List<SecurityViewModel> Securities { get; set; }
    }

    public class SecurityViewModel
    {
        public int SecurityId { get; set; }

        public string Name { get; set; }

        public bool Selected { get; set; }
    }
}