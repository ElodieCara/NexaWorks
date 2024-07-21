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
    var motsClés = Util.ReadLine("Saisissez les Mots-Clés (séparés par des virgules) :").Split(',').Select(m => m.Trim()).ToList();

    var problèmesEnCoursProduitMotsClés = from p in Problèmes
                                          join pvo in Problème_Version_OS on p.ID_Problème equals pvo.ID_Problème
                                          join pv in Produit_Versions on pvo.ID_Produit_Version equals pv.ID_Produit_Version
                                          join pr in Produits on pv.ID_Produit equals pr.ID_Produit
                                          where pr.ID_Produit.ToString() == produitId 
                                                && p.Statut == "En cours" 
                                                && motsClés.Any(mot => p.Description.Contains(mot))
                                          select new 
                                          {
                                              p.ID_Problème,
                                              p.Description,
                                              pr.Nom,
                                              p.Statut,
                                              p.Date_Création,
                                              p.Date_Résolution
                                          };

    problèmesEnCoursProduitMotsClés.ToList().Dump();
}
