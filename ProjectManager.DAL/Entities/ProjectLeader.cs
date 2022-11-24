using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectOrganiser.DAL.Entities
{

	public class ProjectLeader
	{
		public int EmployeeId { get; set; }
		public Employee Employee { get; set; }
		public int ProjectId { get; set; }
		public Project Project { get; set; }
	}
}
