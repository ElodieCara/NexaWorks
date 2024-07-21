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

    var problèmesEnCoursPourProduitVersion = from p in Problèmes
                                             join pvo in Problème_Version_OS on p.ID_Problème equals pvo.ID_Problème
                                             join pv in Produit_Versions on pvo.ID_Produit_Version equals pv.ID_Produit_Version
                                             join pr in Produits on pv.ID_Produit equals pr.ID_Produit
                                             join v in Versions on pv.ID_Version equals v.ID_Version
                                             where pr.ID_Produit.ToString() == produitId && v.ID_Version.ToString() == versionId && p.Statut == "En cours"
                                             select new 
                                             {
                                                 p.ID_Problème,
                                                 p.Description,
                                                 pr.Nom,
                                                 v.Numéro_Version,
                                                 p.Statut,
                                                 p.Date_Création,
                                                 p.Date_Résolution
                                             };

    problèmesEnCoursPourProduitVersion.ToList().Dump();
}
