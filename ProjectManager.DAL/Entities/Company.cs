using ProjectsManager.DAL.Entities.Base;


namespace ProjectsManager.DAL.Entities
{
	//[PrimaryKey(nameof(CompanyId))]
	public class Company : Entity
	{
		//public int CompanyId { get; set; }
		public string Name { get; set; }
	}
}
