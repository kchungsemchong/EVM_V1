// <auto-generated />
namespace EVM.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
    public sealed partial class AddFirstNameLastNameandStatustoApplicationUser : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(AddFirstNameLastNameandStatustoApplicationUser));
        
        string IMigrationMetadata.Id
        {
            get { return "201605271644574_Add FirstName,LastName and Status to ApplicationUser"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}
