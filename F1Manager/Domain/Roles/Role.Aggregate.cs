using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Roles
{
	public partial class Role
	{
		public Role(string roleName) : base()
		{
			this.RoleName = roleName;
		}

		public bool ValidOnAdd()
		{
			return !string.IsNullOrEmpty(RoleName);
		}
	}
}