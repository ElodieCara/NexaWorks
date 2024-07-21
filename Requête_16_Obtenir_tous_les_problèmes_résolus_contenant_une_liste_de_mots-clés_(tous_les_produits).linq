<Query Kind="Program">
  <Connection>
    <ID>54ab4c95-bb37-45d2-8ba2-24461b3ed2ca</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <Server>CARA</Server>
    <Database>NexaWorks</Database>
    <DriverData>
      <EncryptSqlTraffic>True</EncryptSqlTraffic>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>

void Main()
{
    var motsClés = Util.ReadLine("Saisissez les Mots-Clés (séparés par des virgules) :").Split(',').Select(m => m.Trim()).ToList();

    var problèmesRésolus = Problèmes
                           .Where(p => p.Statut == "Résolu" && motsClés.Any(mot => p.Description.Contains(mot)))
                           .Select(p => new 
                           {
                               p.ID_Problème,
                               p.Description,
                               p.Statut,
                               p.Date_Création,
                               p.Date_Résolution
                           })
                           .ToList();

    problèmesRésolus.Dump();
}
