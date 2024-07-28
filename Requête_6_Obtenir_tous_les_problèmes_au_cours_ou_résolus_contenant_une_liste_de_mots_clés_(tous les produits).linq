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
    string statut = Util.ReadLine("Saisissez le Statut (En cours / Résolu) :");
    string motsClés = Util.ReadLine("Saisissez les mots-clés (séparés par des virgules) :");
    var motsClésArray = motsClés.Split(',');

    var problèmesAvecMotsClés = from p in Problèmes
                                where p.Statut == statut && motsClésArray.Any(mot => p.Description.Contains(mot))
                                select new 
                                {
                                    p.ID_Problème,
                                    p.Description,
                                    p.Statut,
                                    p.Date_Création,
                                    p.Date_Résolution
                                };

    problèmesAvecMotsClés.ToList().Dump();
}
