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
    int versionId = 4; // Exemple de versionId

    var problèmesFiltrés = Problèmes
                           .Join(Problème_Version_OS,
                                 p => p.ID_Problème,
                                 pvo => pvo.ID_Problème,
                                 (p, pvo) => new { p, pvo })
                           .Join(Versions,
                                 pv => pv.pvo.ID_Version,
                                 v => v.ID_Version,
                                 (pv, v) => new { pv.p, pv.pvo, v })
                           .Where(pvv => pvv.p.Statut == "En cours" && pvv.v.ID_Produit == produitId && pvv.v.ID_Version == versionId)
                           .Select(pvv => pvv.p)
                           .ToList();
    problèmesFiltrés.Dump("Problèmes Filtrés pour le produitId et versionId spécifiés");

