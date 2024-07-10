<Query Kind="Statements">
  <Connection>
    <ID>ecb6ec08-d8cb-4021-8fcb-e556fe62b88b</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <Server>CARA</Server>
    <Database>NexaWorksDB</Database>
    <DriverData>
      <EncryptSqlTraffic>True</EncryptSqlTraffic>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>

int produitId = 1; // Exemple de produitId
int versionId = 1; // Exemple de versionId
var problèmesEnCours = Problèmes
                       .Join(Problème_Version_OS,
                             p => p.ID_Problème,
                             pvo => pvo.ID_Problème,
                             (p, pvo) => new { p, pvo })
                       .Join(Versions,
                             ppvo => ppvo.pvo.ID_Version,
                             v => v.ID_Version,
                             (ppvo, v) => new { ppvo, v })
                       .Where(pv => pv.ppvo.p.Statut == "En cours" && pv.v.ID_Produit == produitId && pv.v.ID_Version == versionId)
                       .Select(pv => pv.ppvo.p)
                       .ToList();
problèmesEnCours.Dump();
