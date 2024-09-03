using ElecWarSystem.Models;
using ElecWarSystem.Models.OutDoor;
using ElecWarSystem.Models.OutDoorDetails;
using System.Data.Entity;
using ElecWarSystem.Models.OutDoor.OutDoorNew;
using System.Collections.Generic;






namespace ElecWarSystem.Data
{
    public class AppDBContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelbuilder)
        { 
                
        }
        public AppDBContext() : base("name = ElectronicWarDB_Local") { /*Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDBContext, Migrations.Configuration>()); */}

        public DbSet<We7daRa2eeseya> We7daRa2eeseya { get; set; }
        public DbSet<Taba3eya> Taba3eya { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Mostalem> Mostalem { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<We7daFar3eya> We7daFar3eya { get; set; }
        public DbSet<FardDetails> FardDetails { get; set; }
        public DbSet<Rotba> Rotba { get; set; }
        public DbSet<Tmam> Tmams { get; set; }
        public DbSet<TmamDetails> TmamDetails { get; set; }
        public DbSet<MaradyDetails> MaradyDetails { get; set; }
        public DbSet<Marady> Marady { get; set; }
        public DbSet<Ma2moreyaDetails> Ma2moreyaDetails { get; set; }
        public DbSet<Ma2moreya> Ma2moreya { get; set; }
        public DbSet<CommandItems> CommandItems { get; set; }
        public DbSet<Segn> Segn { get; set; }
        public DbSet<SegnDetails> SegnDetails { get; set; }
        public DbSet<Agaza> Agaza { get; set; }
        public DbSet<AgazaDetails> AgazaDetails { get; set; }
        public DbSet<Gheyab> Gheyab { get; set; }
        public DbSet<GheyabDetails> GheyabDetails { get; set; }
        public DbSet<Mostashfa> Mostashfa { get; set; }
        public DbSet<MostashfaDetails> MostashfaDetails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<KharegBelad> KharegBelad { get; set; }
        public DbSet<KharegBeladDetails> KharegBeladDetails { get; set; }
        public DbSet<Mo3askr> Mo3askr { get; set; }
        public DbSet<Mo3askrDetails> Mo3askrDetails { get; set; }
        public DbSet<Fer2a> Fer2a { get; set; }
        public DbSet<Fer2aDetails> Fer2aDetails { get; set; }
        public DbSet<Fard> Fard { get; set; }
        public DbSet<KharegTmarkoz> KharegTmarkoz { get; set; }
        public DbSet<A8radTa7arok> A8radTa7arok { get; set; }
        public DbSet<A8radTgarob> A8radTgarob { get; set; }
        public DbSet<Al7ala> Al7alas { get; set; }
        public DbSet<Asl7aName> Asl7asNames { get; set; }
        public DbSet<Asl7a> Asl7as { get; set; }
        public DbSet<MarkbatName> MarkbatNames { get; set; }
        public DbSet<Markbat> Markbats { get; set; }
        public DbSet<Mo3edat> Mo3edats { get; set; }
        public DbSet<Mo3edatName> Mo3edatNames { get; set; }
        public DbSet<AnaserMonawba> AnaserMonawba { get; set; }
        public DbSet<Ma2torat> Ma2torat { get; set; }
        public DbSet<MakanEsla7> MakanEsla7s { get; set; }
        public DbSet<MostawaEIsla7> MostawaEIsla7s { get; set; }
        public DbSet<TypeMo2eda> TypeMo2edas { get; set; }
        public DbSet<Elsala7eyaEl8aneya> Elsala7eyaEl8aneyas { get; set; }
        public DbSet<Sla7yaFanya> Sla7yaFanya { get; set; }
        public DbSet<Za5ira> Za5iras { get; set; }
        public DbSet<Mot8ayeratEst3dadQetali> Mot8ayeratEst3dadQetalis { get; set; }
        public DbSet<Za5iraName> Za5iraNames { get; set; }
        public DbSet<Ge7aTasdek> Ge7aTasdeks { get; set; }
        public DbSet<TgarobMydanyas> TgarobMydanyas { get; set; }

        public DbSet<Mowasalat> Mowasalats { get; set; }
        public DbSet<KhetaErada> KhetaEradas { get; set; }
        public DbSet<Manzomat> Manzomats { get; set; }


    }

}

/*
 <add name="ElectronicWarDBLocal" connectionString="Data Source=DESKTOP-L9CA1CG; Initial Catalog=ElecWarDB;Integrated Security=True"
			providerName="System.Data.SqlClient"/>
 */