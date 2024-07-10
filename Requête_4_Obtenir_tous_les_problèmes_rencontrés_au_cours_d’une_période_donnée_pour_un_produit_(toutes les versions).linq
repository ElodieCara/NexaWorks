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
DateOnly dateDébut = new DateOnly(2023, 1, 1); // Exemple de date de début
DateOnly dateFin = new DateOnly(2023, 12, 31); // Exemple de date de fin

var problèmes = Problèmes
                .Join(Versions,
                      p => p.ID_Problème,
                      v => v.ID_Version,
                      (p, v) => new { p, v })
                .Where(pv => pv.p.Date_Création >= dateDébut && pv.p.Date_Création <= dateFin && pv.v.ID_Produit == produitId)
                .Select(pv => pv.p)
                .ToList();
problèmes.Dump();
