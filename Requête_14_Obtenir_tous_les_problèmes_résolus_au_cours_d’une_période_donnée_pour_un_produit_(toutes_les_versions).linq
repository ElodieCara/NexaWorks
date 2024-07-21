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
    string produitId = Util.ReadLine("Saisissez le Code Produit :");
    DateOnly dateDébut = DateOnly.Parse(Util.ReadLine("Saisissez la Date de Début (aaaa-mm-jj) :"));
    DateOnly dateFin = DateOnly.Parse(Util.ReadLine("Saisissez la Date de Fin (aaaa-mm-jj) :"));

    var problèmesRésolus = Problèmes
                           .Join(Problème_Version_OS, p => p.ID_Problème, pvo => pvo.ID_Problème, (p, pvo) => new { p, pvo })
                           .Join(Produit_Versions, ppvo => ppvo.pvo.ID_Produit_Version, pv => pv.ID_Produit_Version, (ppvo, pv) => new { ppvo.p, ppvo.pvo, pv })
                           .Join(Produits, pppv => pppv.pv.ID_Produit, pr => pr.ID_Produit, (pppv, pr) => new { pppv.p, pppv.pvo, pppv.pv, pr })
                           .Where(pppr => pppr.pr.ID_Produit.ToString() == produitId 
                                         && pppr.p.Statut == "Résolu"
                                         && pppr.p.Date_Résolution >= dateDébut 
                                         && pppr.p.Date_Résolution <= dateFin)
                           .Select(pppr => new 
                           {
                               pppr.p.ID_Problème,
                               pppr.p.Description,
                               pppr.pr.Nom,
                               pppr.p.Statut,
                               pppr.p.Date_Création,
                               pppr.p.Date_Résolution
                           })
                           .ToList();

    problèmesRésolus.Dump();
}
