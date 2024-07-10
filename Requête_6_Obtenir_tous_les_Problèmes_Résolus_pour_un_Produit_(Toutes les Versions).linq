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
var problèmesRésolus = Problèmes
                       .Join(Versions,
                             p => p.ID_Problème,
                             v => v.ID_Version,
                             (p, v) => new { p, v })
                       .Where(pv => pv.p.Statut == "Résolu" && pv.v.ID_Produit == produitId)
                       .Select(pv => pv.p)
                       .ToList();
problèmesRésolus.Dump();
