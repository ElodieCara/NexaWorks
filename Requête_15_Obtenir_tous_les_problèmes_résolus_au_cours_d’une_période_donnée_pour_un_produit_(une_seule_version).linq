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
    string versionId = Util.ReadLine("Saisissez le Code Version :");
    DateOnly dateDébut = DateOnly.Parse(Util.ReadLine("Saisissez la Date de Début (aaaa-mm-jj) :"));
    DateOnly dateFin = DateOnly.Parse(Util.ReadLine("Saisissez la Date de Fin (aaaa-mm-jj) :"));

    var problèmesRésolus = Problèmes
                           .Join(Problème_Version_OS, p => p.ID_Problème, pvo => pvo.ID_Problème, (p, pvo) => new { p, pvo })
                           .Join(Produit_Versions, ppvo => ppvo.pvo.ID_Produit_Version, pv => pv.ID_Produit_Version, (ppvo, pv) => new { ppvo.p, ppvo.pvo, pv })
                           .Join(Produits, pppv => pppv.pv.ID_Produit, pr => pr.ID_Produit, (pppv, pr) => new { pppv.p, pppv.pvo, pppv.pv, pr })
                           .Join(Versions, pppvpr => pppvpr.pv.ID_Version, v => v.ID_Version, (pppvpr, v) => new { pppvpr.p, pppvpr.pvo, pppvpr.pv, pppvpr.pr, v })
                           .Where(ppvpr => ppvpr.pr.ID_Produit.ToString() == produitId 
                                         && ppvpr.v.ID_Version.ToString() == versionId 
                                         && ppvpr.p.Statut == "Résolu"
                                         && ppvpr.p.Date_Résolution >= dateDébut 
                                         && ppvpr.p.Date_Résolution <= dateFin)
                           .Select(ppvpr => new 
                           {
                               ppvpr.p.ID_Problème,
                               ppvpr.p.Description,
                               ppvpr.pr.Nom,
                               ppvpr.v.Numéro_Version,
                               ppvpr.p.Statut,
                               ppvpr.p.Date_Création,
                               ppvpr.p.Date_Résolution
                           })
                           .ToList();

    problèmesRésolus.Dump();
}
